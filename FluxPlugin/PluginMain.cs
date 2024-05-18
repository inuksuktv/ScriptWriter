using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace BattleScriptWriter {
	public class PluginMain : IFluxPlugin{
        MenuItem mnuPlug;
        private readonly int _bank = 0x0C0000;
        private byte[] _bankData;

        #region Default properties
        public List<SaveRecord[]> RecList {
			get {
				return G.SaveRec;
			}
			set {
				G.SaveRec = value;
			}
		}

		public MenuItem PlugMenu {
			get {
				return mnuPlug;
			}
			set {
				mnuPlug = value;
			}
		}

		public string sPlugName {
			get {
				return "Battle Script Writer";
			}
		}

		public ushort nFluxSchema {
			get {
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
				return G.RecTypeDict;
			}
			set {
				G.RecTypeDict = value;
			}
		}
        #endregion

        public bool Init() {
			G.SaveRec = new List<SaveRecord[]>(new SaveRecord[(byte) RecType.Count][]);

			G.MainForm = (Form) G.DockMan.Parent;
			G.PlugForm = new PluginForm();

			G.Init();
            mnuPlug = new MenuItem("Battle Script Writer", new EventHandler(OnPlugForm));

            return true;
		}

        public bool GetRecords() {
            #region Get Records
            G.PostStatus("Battle Script Writer: Reading scripts...");

            // Scripts must be read and their length measured before the records can be created.
            _bankData = new byte[0x010000];
            Array.Copy(G.WorkingData, _bank, _bankData, 0, _bankData.Length);

            List<byte[]> scriptPointers = GetScriptPointers();
            List<List<byte>> enemyScripts = GetEnemyScripts(scriptPointers);

            // Create the records.
            G.SaveRec[(byte)RecType.EnemyScripts] = new PlugRecord[256];
            for (var i = 0; i < G.SaveRec[(byte)RecType.EnemyScripts].Length; i++)
            {
                G.SaveRec[(byte)RecType.EnemyScripts][i] = new PlugRecord();
                var record = G.SaveRec[(byte)RecType.EnemyScripts][i];
                
                record.nMaxSize = 0x0800;
                var pointerAddress = (uint)(G.GetRomAddr(PlugRomAddr.AttackScriptPointers) + (i * 2));
                record.Pointer = new PointerRecord[1];
                record.Pointer[0] = new PointerRecord(pointerAddress, 0, false, true);

                uint localScriptAddress = (uint)(scriptPointers[i][1] << 8) + scriptPointers[i][0];
                // Case: the modder changed a "stale" pointer to 00 00.
                if (localScriptAddress == 0)
                {
                    record.nData = new byte[2] { 0xFF, 0xFF };
                    record.nDataSize = 2;
                    record.nOrigSize = 2;
                    record.bModified = true;
                }
                // Case: all other pointer values.
                else
                {
                    record.nData = new byte[enemyScripts[i].Count];
                    record.nDataSize = (uint)enemyScripts[i].Count;
                    record.nOrigSize = (uint)enemyScripts[i].Count;
                    record.nOrigAddr = (uint)_bank + localScriptAddress;
                    record.Get();
                }
            }

            // Set a record modified so that I can run code to reserve space when the user Saves.
            G.SaveRec[(byte)RecType.EnemyScripts][0].bModified = true;

            // Create the records for the reserved space now, but we can't give them an address yet.
            decimal shortestScript = 18;
            decimal quarterBank = 0x4000;
            int maxPartitions = (int)Math.Floor(quarterBank / shortestScript);
            G.SaveRec[(byte)RecType.ReservedSpace] = new PlugRecord[maxPartitions];
            for (var i = 0; i < G.SaveRec[(byte)RecType.ReservedSpace].Length; i++)
            {
                G.SaveRec[(byte)RecType.ReservedSpace][i] = new PlugRecord();
                var record = G.SaveRec[(byte)RecType.ReservedSpace][i];
                record.nDataSize = (uint)shortestScript;
                record.nMaxSize = (uint)shortestScript;
                record.nOrigSize = (uint)shortestScript;
            }
            #endregion

            #region Data-dependant form setup
            #endregion

            return true;
		}

        private List<byte[]> GetScriptPointers()
        {
            var pointers = new List<byte[]>();
            var startingAddress = 0x8B08;
            for (var i = 0; i < 256; i++)
            {
                pointers.Add(new byte[2]);
                int pointerAddress = startingAddress + (i * 2);
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
                var pointer = (pointers[i][1] << 8) + pointers[i][0];
                if (pointer == 0)
                {
                    scripts.Add(new List<byte> { 0xFF, 0xFF });
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
            byte cell;

            // A second cell value of 0xFF signals the end of the script.
            while (ffCount < 2)
            {
                cell = _bankData[pointer + index++];
                script.Add(cell);

                if (cell == 0xFF) ffCount++;
            }

            return script;
        }

		public bool Close() {
			return true;
		}

		public void OnPlugForm(object sender, System.EventArgs e) {
			G.PlugForm.InitForm();
			G.PlugForm.Show(G.DockMan);
		}
	}
}
