using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FluxShared;



namespace FluxPlugin {
	public partial class PluginForm : DockContent {
		public bool bNoUpdate = false;
		public PluginForm() {
			InitializeComponent();
		}



		public void InitForm() {
			bNoUpdate = true;

			bool bReleaseRom = (G.nRomType != 2);
			if(bReleaseRom) {
				BlackOmenStorySpin.Value = G.SaveRec[(byte) RecType.BlackOmenStory][0].nData[0];
			}
			BlackOmenStorySpin.Enabled = bReleaseRom;

			bNoUpdate = false;
		}



		private void OnBlackOmen(object sender, EventArgs e) {
			if(bNoUpdate) {
				return;
			}

			SaveRecord Rec = G.SaveRec[(byte) RecType.BlackOmenStory][0];
			Rec.nData[0] = (byte) BlackOmenStorySpin.Value;
			Rec.bModified = true;
		}
	}
}