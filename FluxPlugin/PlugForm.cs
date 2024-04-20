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
        public PluginForm()
        {
            InitializeComponent();
        }

        public BindingList<string> EnemyNames { get; set; }

        private List<List<byte>> EnemyScripts { get; set; }

        public bool bNoUpdate = false;
        private byte[] _localBank;

		public void InitForm() {
			bNoUpdate = true;

            GetEnemyNames();
            EnemyBox.DataSource = EnemyNames;

            _localBank = new byte[0x010000];
            Array.Copy(G.WorkingData, 0x0C0000, _localBank, 0, _localBank.Length);

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
            EnemyScripts = new List<List<byte>>(256);

            for (var i = 0; i < 256; i++)
            {
                var record = G.SaveRec[(byte)RecType.AttackScriptPointers][i];
                int pointer = (record.nData[1] << 8) + record.nData[0];
                List<byte> script = ReadScript(pointer);
                EnemyScripts.Add(script);
            }
        }

        private List<byte> ReadScript(int pointer)
        {
            var script = new List<byte>();
            int i = 0;
            int count = 0;
            byte cell;

            // A second cell value of 0xFF signals the end of the script.
            while (count < 2)
            {
                cell = _localBank[pointer + i];
                script.Add(cell);

                i++;
                if (cell == 0xFF) count++;
            }

            return script;
        }

        private void GetAttacksAndReactions(List<byte> fullScript, out List<byte> attacks, out List<byte> reactions)
        {
            attacks = new List<byte>();
            reactions = new List<byte>();
            int i = 0;
            byte cell = 0;

            do
            {
                cell = fullScript[i++];
                attacks.Add(cell);
            }
            while (cell != 0xFF);

            do
            {
                cell = fullScript[i++];
                reactions.Add(cell);
            }
            while (cell != 0xFF);
        }

        private void EnemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bNoUpdate) return;

            int index = EnemyBox.SelectedIndex;
            var script = EnemyScripts[index];
            GetAttacksAndReactions(script, out List<byte> attacks, out List<byte> reactions);

            var sb1 = new StringBuilder();
            foreach (byte cell in attacks) sb1.Append(G.HexStr(cell));

            var sb2 = new StringBuilder();
            foreach (byte cell in reactions) sb2.Append(G.HexStr(cell));

            attackTree.BeginUpdate();
            attackTree.Nodes.Clear();
            attackTree.Nodes.Add(sb1.ToString());
            reactionTree.Nodes.Clear();
            reactionTree.Nodes.Add(sb2.ToString());
            attackTree.EndUpdate();
        }
    }
}