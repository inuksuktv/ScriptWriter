using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace BattleScriptWriter {
	public class PluginMain : IFluxPlugin{
		MenuItem mnuPlug;
        private byte[] _localBank;

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
			G.PostStatus("Battle Script Writer: Getting scripts...");
            #region Get Records
            SaveRecord record;

            // Scripts need to be read and their length measured before a Record class can be created.
            _localBank = new byte[0x010000];
            Array.Copy(G.WorkingData, 0x0C0000, _localBank, 0, _localBank.Length);

            // Get the script pointers.
            var scriptPointers = new List<byte[]>();
            var startingAddress = 0x8B08;
            for (var i = 0; i < 256; i++)
            {
                scriptPointers.Add(new byte[2]);
                int pointer = startingAddress + (i * 2);
                scriptPointers[i][0] = _localBank[pointer];
                scriptPointers[i][1] = _localBank[pointer + 1];
            }

            // Read the scripts.
            var enemyScripts = new List<List<byte>>(256);
            for (var i = 0; i < 256; i++)
            {
                var pointer = (scriptPointers[i][1] << 8) + scriptPointers[i][0];
                List<byte> script = GetScriptStartingAt(pointer);
                enemyScripts.Add(script);
            }

            // Create the records.
            G.SaveRec[(byte)RecType.EnemyScripts] = new PlugRecord[256];
            for (var i = 0; i < G.SaveRec[(byte)RecType.EnemyScripts].Length; i++)
            {
                G.SaveRec[(byte)RecType.EnemyScripts][i] = new PlugRecord();
                record = (PlugRecord)G.SaveRec[(byte)RecType.EnemyScripts][i];
                record.nDataSize = (uint)enemyScripts[i].Count;
                record.nMaxSize = 0x0400;
                record.nOrigSize = (uint)enemyScripts[i].Count;
                uint scriptAddress = (uint)((0x0C << 16) + (scriptPointers[i][1] << 8) + scriptPointers[i][0]);
                record.nOrigAddr = scriptAddress;
                record.bCompressed = false;
                record.bCreateEmpty = false;
                record.bOverride = false;

                record.Pointer = new PointerRecord[1];
                uint pointerAddress = (uint)(G.GetRomAddr(PlugRomAddr.AttackScriptPointers) + (i * 2));
                record.Pointer[0] = new PointerRecord(pointerAddress, 0, false, true);

                record.Get();
            }

            G.PostStatus("BattleScriptWriter: Reserving space...");
            int shortestScript = 22;
            int quarterBank = 0x8000;
            int partitionAmount = (int)Math.Floor((decimal)(quarterBank / shortestScript));
            G.SaveRec[(byte)RecType.ReservedSpace] = new SaveRecord[partitionAmount];
            // Initialize some dummy records.
            uint dummyLocation = 0x027FE9;
            for (var i = 0; i < G.SaveRec[(byte)RecType.ReservedSpace].Length; i++)
            {
                G.SaveRec[(byte)RecType.ReservedSpace][i] = new PlugRecord();
                record = G.SaveRec[(byte)RecType.ReservedSpace][i];
                record.nDataSize = (uint)shortestScript;
                record.nMaxSize = (uint)shortestScript;
                record.nOrigSize = (uint)shortestScript;
                record.nOrigAddr = dummyLocation;
                record.bCompressed = false;
                record.bCreateEmpty = false;
                record.bOverride = false;
                record.Get();
            }
            #endregion

            #region Data-dependant form setup
            #endregion

            return true;
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
                cell = _localBank[pointer + index++];
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
