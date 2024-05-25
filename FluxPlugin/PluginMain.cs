using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;
// ReSharper disable UnusedMember.Global

namespace ScriptWriter {
	public class PluginMain : IFluxPlugin{
        private const int Bank = 0x0C0000;
        private const int PointerAddress = 0x8B08;
        private const int MaxRecordSize = 0x8000;
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

            // The dictionary's key is the problem script's index and the value is the offset of the block where the first problem was found.
            Dictionary<int, int> problemScripts = G.ScriptParser.Parse(enemyScripts);
            bool[] modifiedScripts = AskUserToModifyScripts(problemScripts, scriptPointers, enemyScripts);

            CreateRecords(modifiedScripts, scriptPointers, enemyScripts);

            return true;
        }

        private void WelcomeUser()
        {
            G.PostStatus("Script Writer: Welcoming user...");
            string welcome = @"Thank you for using Script Writer!
Please save immediately after loading (Ctrl+Shift+S) so that Script Writer can reserve space to protect against other edits.";
            MessageBox.Show(welcome, "Script Writer", MessageBoxButtons.OK);
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
                int pointerAddress = startAddress + (i * 2);
                pointers[i][0] = _bankData[pointerAddress];
                pointers[i][1] = _bankData[pointerAddress + 1];
            }
            return pointers;
        }

        private List<List<byte>> GetEnemyScripts(List<byte[]> pointers)
        {
            var scripts = new List<List<byte>>();
            for (var i = 0; i < 256; i++)
            {
                ushort pointer = (ushort)((pointers[i][1] << 8) + pointers[i][0]);
                List<byte> script = GetScriptStartingAt(pointer);
                scripts.Add(script);
            }
            // These script addresses are hardcoded in the ROM e.g. at $C1B4DA.
            const ushort berserkOffset = 0x8D08;
            const ushort confuseOffset = 0x8D1E;
            scripts.Add(GetScriptStartingAt(berserkOffset));
            scripts.Add(GetScriptStartingAt(confuseOffset));
            return scripts;
        }

        private List<byte> GetScriptStartingAt(ushort pointer)
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
            // Override for an older version of default scripts that were just "FF FF". The new default script won't be saved.
            if (script.Count < 3) return new List<byte>(_defaultScript);
            return script;
        }

        private bool[] AskUserToModifyScripts(Dictionary<int, int> problemScripts, List<byte[]> scriptPointers, List<List<byte>> enemyScripts)
        {
            var modified = new bool[enemyScripts.Count];
            if (problemScripts.Count > 0)
            {
                var message = $"{problemScripts.Count} script(s) with problem(s) found.";
                MessageBox.Show(message, "Script Writer", MessageBoxButtons.OK);
            }
            foreach (KeyValuePair<int, int> problem in problemScripts)
            {
                int scriptIndex = problem.Key;
                var askFix = $@"Found a problem in script 0x{G.HexStr((byte)scriptIndex)} near byte {problem.Value}.
Replace script with a placeholder? (Script data will then be over-written when you save.)";
                var dialogResult = MessageBox.Show(askFix, "Script Writer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    enemyScripts[scriptIndex] = new List<byte>(_defaultScript);
                    modified[scriptIndex] = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    scriptPointers[scriptIndex] = null;
                    enemyScripts[scriptIndex] = null;
                }
            }
            return modified;
        }

        private static void CreateRecords(bool[] modifiedScripts, List<byte[]> scriptPointers, List<List<byte>> enemyScripts)
        {
            G.PostStatus("Script Writer: Creating records...");
            CreateScriptRecords(modifiedScripts, scriptPointers, enemyScripts);
            CreateReserveRecords();

            // Setting script 1 modified, so I can reserve space when the user saves (and don't risk user fat-fingering script 0 and altering it unintentionally.)
            G.SaveRec[(byte)RecType.EnemyScripts][1].bModified = true;
        }

        private static void CreateScriptRecords(bool[] modifiedScripts, List<byte[]> scriptPointers, List<List<byte>> enemyScripts)
        {
            G.SaveRec[(byte)RecType.EnemyScripts] = new SaveRecord[256];
            // Todo: implement pointer handling for the Berserk and Confuse scripts and create records for them.
            for (var i = 0; i < 256; i++)
            {
                G.SaveRec[(byte)RecType.EnemyScripts][i] = new PlugRecord();
                var record = G.SaveRec[(byte)RecType.EnemyScripts][i];

                // The script is null if the user answered "No" to using a default script.
                if (enemyScripts[i] == null) continue;

                int length = enemyScripts[i].Count;
                record.nData = new byte[length];
                record.nDataSize = (uint)length;
                record.nMaxSize = MaxRecordSize;
                record.nOrigSize = (uint)length;
                record.nID = (ushort)i;
                record.bModified = modifiedScripts[i];
                byte[] script = enemyScripts[i].ToArray();
                Array.Copy(script, record.nData, length);

                // Pointers get created during the save process if the user answered "Yes" to using a default script.
                if (modifiedScripts[i]) continue;

                uint pointerAddress = (uint)(G.GetRomAddr(PlugRomAddr.AttackScriptPointers) + (i * 2));
                int scriptAddress = Bank + (scriptPointers[i][1] << 8) + scriptPointers[i][0];
                record.nOrigAddr = (uint)scriptAddress;
                record.Pointer = new PointerRecord[1];
                record.Pointer[0] = new PointerRecord(pointerAddress, 0, false, true);
            }
        }

        private static void CreateReserveRecords()
        {
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
