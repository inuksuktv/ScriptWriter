using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FluxShared;

namespace BattleScriptWriter {
	public class PluginMain : IFluxPlugin{
		MenuItem mnuPlug;

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


		public bool Init() {
			G.SaveRec = new List<SaveRecord[]>(new SaveRecord[(byte) RecType.Count][]);

			mnuPlug = new MenuItem("Battle Script Writer", new EventHandler(OnPlugForm));

			G.MainForm = (Form) G.DockMan.Parent;
			G.PlugForm = new PluginForm();

			G.Init();

			return true;
		}



		public bool GetRecords() {
			G.PostStatus("Battle Script Writer: Getting scripts...");
			#region Get Records
			SaveRecord Rec;
			G.SaveRec[(byte) RecType.BlackOmenStory] = new SaveRecord[0x01];
			for(int i = 0; i < G.SaveRec[(byte) RecType.BlackOmenStory].Length; i++) {
				G.SaveRec[(byte) RecType.BlackOmenStory][i] = new SaveRecord();
				Rec = G.SaveRec[(byte) RecType.BlackOmenStory][i];
				Rec.nMaxSize = 0x01;
				Rec.nOrigSize = 0x01;
				Rec.nOrigAddr = (uint) (G.GetRomAddr(PlugRomAddr.BlackOmenStory) + (i * Rec.nMaxSize));
				Rec.bCompressed = false;
				Rec.bCreateEmpty = false;
				Rec.bOverride = true;
				Rec.Get();
			}

            G.SaveRec[(byte)RecType.EpochToLastVillage] = new SaveRecord[0x01];
            for (int i = 0; i < G.SaveRec[(byte) RecType.EpochToLastVillage].Length; i++)
            {
                G.SaveRec[(byte)RecType.EpochToLastVillage][i] = new SaveRecord();
                Rec = G.SaveRec[(byte)RecType.EpochToLastVillage][i];
                Rec.nMaxSize = 0x01;
                Rec.nOrigSize = 0x01;
                Rec.nOrigAddr = (uint)(G.GetRomAddr(PlugRomAddr.EpochToLastVillage) + (i * Rec.nMaxSize));
                Rec.bCompressed = false;
                Rec.bCreateEmpty = false;
                Rec.bOverride = true;
                Rec.Get();
            }
			#endregion

			#region Data-dependant form setup
			#endregion

			return true;
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
