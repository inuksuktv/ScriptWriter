using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FluxShared;



namespace BattleScriptWriter
{
	public partial class PluginForm : DockContent {
		public bool bNoUpdate = false;

        public BindingList<string> EnemyNames { get; set; }

        private List<List<byte>> EnemyScripts { get; set; }

        public PluginForm() {
			InitializeComponent();
		}



		public void InitForm() {
			bNoUpdate = true;

			bool bReleaseRom = (G.nRomType != 2);
            //if(bReleaseRom) {
            //	BlackOmenStorySpin.Value = G.SaveRec[(byte) RecType.BlackOmenStory][0].nData[0];
            //}
            //BlackOmenStorySpin.Enabled = bReleaseRom;

            GetEnemyNames();
            EnemyBox.DataSource = EnemyNames;

            GetEnemyScripts();

            bNoUpdate = false;
		}

        private void GetEnemyNames()
        {
            EnemyNames = new BindingList<string>();
            for (ushort i = 0; i < 256; i++)
            {
                EnemyNames.Add(G.GetStrFromGroup(StrRecType.Enemies, i));
            }
        }

        private void GetEnemyScripts()
        {
            for (var i = 0; i < 256; i++)
            {
                var record = G.SaveRec[(byte)RecType.AttackScriptPointers][i];
            }
        }



        //private void OnBlackOmen(object sender, EventArgs e) {
        //	if(bNoUpdate) {
        //		return;
        //	}

        //	SaveRecord Rec = G.SaveRec[(byte) RecType.BlackOmenStory][0];
        //	Rec.nData[0] = (byte) BlackOmenStorySpin.Value;
        //	Rec.bModified = true;
        //}

        private void EnemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bNoUpdate) return;

            int index = EnemyBox.SelectedIndex;
            var record = G.SaveRec[(byte)RecType.AttackScriptPointers][index];
            var text = new StringBuilder();
            foreach (byte cell in record.nData)
            {
                text.Append(G.HexStr(cell) + " ");
            }

            attackTree.BeginUpdate();
            attackTree.Nodes.Clear();
            attackTree.Nodes.Add(text.ToString());
            attackTree.EndUpdate();
        }
    }
}