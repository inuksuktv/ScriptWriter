using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;
// ReSharper disable UnusedMember.Global

namespace ScriptWriter {
	public class PluginMain : IFluxPlugin{
        private const int Bank = 0x0C0000;
        private const int PointerAddress = 0x8B08;
        private byte[] _bankData;
        private MenuItem _menuPlug;

        // Shortest possible well-formed script.
        private readonly byte[] _defaultScript = {
            0x00, 0x00, 0x00, 0x00, 0xFE,
            0x0F, 0x00, 0xFE,
            0xFF,
            0x00, 0x00, 0x00, 0x00, 0xFE,
            0x0F, 0x00, 0xFE,
            0xFF
        };

        #region Default properties
        public List<SaveRecord[]> RecList {
			get
            {
				return G.SaveRec;
			}
			set
            {
				G.SaveRec = value;
			}
		}

		public MenuItem PlugMenu {
			get
            {
				return _menuPlug;
			}
			set
            {
				_menuPlug = value;
			}
		}

		public string sPlugName
        {
			get
            {
				return "Script Writer";
			}
		}

		public ushort nFluxSchema {
			get
            {
				return 0x0001;
			}
		}

		public ushort nFluxVersion {
			get {
				return 0x0100;
			}
		}

		public ushort nFluxMinSchema {
			get {
				return 0x0001;
			}
		}

		public ushort nFluxMinVersion {
			get {
				return 0x0100;
			}
		}

		public Dictionary<string, int> RecDict {
			get {
				return G.RecordTypes;
			}
			set {
				G.RecordTypes = value;
			}
		}
        #endregion

        public bool Init() {
			G.SaveRec = new List<SaveRecord[]>(new SaveRecord[(byte) RecType.Count][]);

			G.MainForm = (Form) G.DockMan.Parent;
			G.PlugForm = new PluginForm();

			G.Init();
            _menuPlug = new MenuItem("Script Writer", OnPlugForm);

            return true;
		}

        public bool GetRecords()
        {
            WelcomeUser();

            // Scripts must be read and their length measured before the records can be created.
            ReadData(PointerAddress, out List<byte[]> scriptPointers, out List<List<byte>> enemyScripts);

            Dictionary<int, int> problemScripts = G.Parser.Parse(enemyScripts);

            bool[] modified = new bool[enemyScripts.Count];
            if (problemScripts.Count > 0)
            {
                var message = $"{problemScripts.Count} script(s) with problem(s) found.";
                MessageBox.Show(message, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            foreach (var problem in problemScripts)
            {
                var askFix = $@"Found a problem in script 0x{G.HexStr((byte)problem.Key)} near byte {problem.Value}.
Replace script with a placeholder? (Script data will then be over-written when you save.)";
                var dialogResult = MessageBox.Show(askFix, "Script Writer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    enemyScripts[problem.Key] = new List<byte>(_defaultScript);
                    modified[problem.Key] = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    scriptPointers[problem.Key] = null;
                    enemyScripts[problem.Key] = null;
                }
            }

            #region Get Records
            G.PostStatus("Script Writer: Creating records...");
            G.SaveRec[(byte)RecType.EnemyScripts] = new SaveRecord[enemyScripts.Count];
            for (var i = 0; i < G.SaveRec[(byte)RecType.EnemyScripts].Length; i++)
            {
                G.SaveRec[(byte)RecType.EnemyScripts][i] = new PlugRecord();
                var record = G.SaveRec[(byte)RecType.EnemyScripts][i];
                
                record.nMaxSize = 0x10000;

                if (enemyScripts[i] == null) continue;

                if (!modified[i])
                {
                    record.Pointer = new PointerRecord[1];
                    uint pointerAddress = (uint)(G.GetRomAddr(PlugRomAddr.AttackScriptPointers) + (i * 2));
                    record.Pointer[0] = new PointerRecord(pointerAddress, 0, false, true);
                    int address = Bank + (scriptPointers[i][1] << 8) + scriptPointers[i][0];
                    record.nOrigAddr = (uint)address;
                }

                int length = enemyScripts[i].Count;
                record.nData = new byte[length];
                record.nDataSize = (uint)length;
                record.nOrigSize = (uint)length;
                record.nID = (ushort)i;
                record.bModified = modified[i];
                byte[] script = enemyScripts[i].ToArray();
                Array.Copy(script, record.nData, length);
            }

            // Set a record modified so that I can run code to reserve space when the user Saves. Didn't choose script 0 because the user might make edits unintentionally.
            G.SaveRec[(byte)RecType.EnemyScripts][1].bModified = true;

            // Create the records for the reserved space now, but we can't give them an address until the ROM is saved.
            const decimal shortestScript = 18;
            const decimal quarterBank = 0x4000;
            int maxPartitions = (int)Math.Floor(quarterBank / shortestScript);
            G.SaveRec[(byte)RecType.ReservedSpace] = new SaveRecord[maxPartitions];

            for (var i = 0; i < G.SaveRec[(byte)RecType.ReservedSpace].Length; i++)
            {
                G.SaveRec[(byte)RecType.ReservedSpace][i] = new PlugRecord();
                var record = G.SaveRec[(byte)RecType.ReservedSpace][i];
                record.nDataSize = (uint)shortestScript;
                record.nMaxSize = (uint)shortestScript;
                record.nOrigSize = (uint)shortestScript;
            }
            #endregion

            return true;
        }

        private void WelcomeUser()
        {
            G.PostStatus("Script Writer: Welcoming user...");
            string welcome = @"Thank you for using Script Writer!
Please save immediately after loading (Ctrl+Shift+S) so that Script Writer can reserve space to protect against other edits.";
            MessageBox.Show(welcome, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void ReadData(int pointerAddress, out List<byte[]> scriptPointers, out List<List<byte>> enemyScripts)
        {
            G.PostStatus("Script Writer: Reading scripts...");
            _bankData = new byte[0x010000];
            Array.Copy(G.WorkingData, Bank, _bankData, 0, _bankData.Length);

            scriptPointers = GetScriptPointers(pointerAddress);
            enemyScripts = GetEnemyScripts(scriptPointers);
        }

        private List<byte[]> GetScriptPointers(int startAddress)
        {
            var pointers = new List<byte[]>();
            for (var i = 0; i < 256; i++)
            {
                pointers.Add(new byte[2]);
                int currentPointerAddress = startAddress + (i * 2);
                pointers[i][0] = _bankData[currentPointerAddress];
                pointers[i][1] = _bankData[currentPointerAddress + 1];
            }
            return pointers;
        }

        private List<List<byte>> GetEnemyScripts(List<byte[]> pointers)
        {
            var scripts = new List<List<byte>>();
            for (var i = 0; i < 256; i++)
            {
                var pointer = (pointers[i][1] << 8) + pointers[i][0];
                if (pointer == 0)
                {
                    scripts.Add(new List<byte>(_defaultScript));
                }
                else
                {
                    List<byte> script = GetScriptStartingAt(pointer);
                    scripts.Add(script);
                }
            }
            return scripts;
        }

        private List<byte> GetScriptStartingAt(int pointer)
        {
            var script = new List<byte>();
            int index = 0;
            int ffCount = 0;

            // A second cell value of 0xFF signals the end of the script.
            while (ffCount < 2)
            {
                byte cell = _bankData[pointer + index++];
                script.Add(cell);

                if (cell == 0xFF) ffCount++;
            }

            return script;
        }

		public bool Close() {
			return true;
		}

		public void OnPlugForm(object sender, EventArgs e) {
			G.PlugForm.InitForm();
			G.PlugForm.Show(G.DockMan);
		}
	}
}
