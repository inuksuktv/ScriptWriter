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

        private void EnemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bNoUpdate) return;

            int index = EnemyBox.SelectedIndex;
            List<byte> script = EnemyScripts[index];
            GetAttacksAndReactions(script, out List<byte> attacks, out List<byte> reactions);

            attackTree.BeginUpdate();
            reactionTree.BeginUpdate();

            UpdateNodes(attackTree, attacks);
            UpdateNodes(reactionTree, reactions);

            attackTree.EndUpdate();
            reactionTree.EndUpdate();
        }

        private void GetAttacksAndReactions(List<byte> fullScript, out List<byte> attacks, out List<byte> reactions)
        {
            attacks = new List<byte>();
            reactions = new List<byte>();
            int i = 0;
            byte cell;

            // The attack and reaction sections of the script end in an FF byte.
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

        private void UpdateNodes(TreeView tree, List<byte> scriptSection)
        {
            tree.Nodes.Clear();
            StringBuilder sb;
            int index = 0;
            // Each condition and action pair is referred to as a block.
            int blockCount = 1;

            do
            {
                // Return the index so we can pick up where we left off reading the last block.
                index = ReadCurrentBlock(scriptSection, index, out List<byte> conditions, out List<byte> actions);
                var blockNode = new TreeNode($"Block {blockCount++}");
                tree.Nodes.Add(blockNode);

                sb = new StringBuilder();
                foreach (var cell in conditions) sb.Append(G.HexStr(cell)).Append(" ");
                blockNode.Nodes.Add($"Conditions {sb.ToString()}");

                sb = new StringBuilder();
                foreach (var cell in actions) sb.Append(G.HexStr(cell)).Append(" ");
                blockNode.Nodes.Add($"Actions {sb.ToString()}");
            } // The last byte in the current script is FF to signal the end of the section.
            while (index < scriptSection.Count - 1);

            tree.Nodes.Add("End");
        }

        private int ReadCurrentBlock(List<byte> scriptSection, int index, out List<byte> conditions, out List<byte> actions)
        {
            conditions = new List<byte>();
            actions = new List<byte>();
            byte cell;

            // The condition and action sections of each block end in an FE byte.
            do
            {
                cell = scriptSection[index++];
                conditions.Add(cell);
            }
            while (cell != 0xFE);

            do
            {
                cell = scriptSection[index++];
                actions.Add(cell);
            }
            while (cell != 0xFE);

            return index;
        }
    }
}