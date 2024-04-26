using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FluxShared;
using InstructionType = BattleScriptWriter.Instruction.InstructionType;



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
        private InstructionFactory _factory;

		public void InitForm() {
			bNoUpdate = true;

            GetEnemyNames();
            EnemyBox.DataSource = EnemyNames;

            _localBank = new byte[0x010000];
            Array.Copy(G.WorkingData, 0x0C0000, _localBank, 0, _localBank.Length);

            _factory = new InstructionFactory();
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
                List<byte> script = GetScriptStartingAt(pointer);
                EnemyScripts.Add(script);
            }
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

            // The Attack and Reaction sections of the script each end in 0xFF.
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

        // Works with either the Attack or Reaction sections of the script passed in.
        private void UpdateNodes(TreeView tree, List<byte> scriptSection)
        {
            tree.Nodes.Clear();
            int index = 0;
            TreeNode finalCondition = new TreeNode();

            // Process one block (condition and action pair) per loop.
            do
            {
                // Return the index so we can pick up where we left off.
                index = GetCurrentBlock(scriptSection, index, out List<byte> conditions, out List<byte> actions);

                List<TreeNode> conditionNodes = ParseConditions(conditions);

                foreach (TreeNode node in conditionNodes)
                {
                    var instruction = (Instruction)node.Tag;
                    node.Text = "If " + instruction.Description;
                    tree.Nodes.Add(node);
                    finalCondition = node;
                }

                List<TreeNode> actionNodes = ParseActions(actions);

                foreach (TreeNode node in actionNodes)
                {
                    var instruction = (Instruction)node.Tag;
                    node.Text = instruction.Description;
                    finalCondition.Nodes.Add(node);
                }
            } // The last byte in the current section is FF.
            while (index < scriptSection.Count - 1);

            tree.Nodes.Add("End");
            tree.ExpandAll();
        }

        private int GetCurrentBlock(List<byte> scriptBlock, int index, out List<byte> conditions, out List<byte> actions)
        {
            conditions = new List<byte>();
            actions = new List<byte>();
            byte cell;

            // The condition and action sections of each block end in an 0xFE byte.
            do
            {
                cell = scriptBlock[index++];
                conditions.Add(cell);
            }
            while (cell != 0xFE);

            do
            {
                cell = scriptBlock[index++];
                // Must check for 0xFF here since the vanilla Red Beast script is missing an 0xFE.
                if (cell == 0xFF) break;
                actions.Add(cell);
            }
            while (cell != 0xFE);

            return index;
        }

        // Returns the list of all conditions in a single block terminating with an 0xFE.
        private List<TreeNode> ParseConditions(List<byte> script)
        {
            var conditionList = new List<TreeNode>();
            var type = InstructionType.Condition;

            // Every condition instruction is 4 bytes.
            while (script.Count > 1)
            {
                var instruction = new Instruction(script.GetRange(0, 4), type);
                script.RemoveRange(0, 4);

                var node = new TreeNode { Tag = instruction };
                conditionList.Add(node);
            }

            return conditionList;
        }

        // Returns the list of all actions in a single block terminating with an 0xFE.
        private List<TreeNode> ParseActions(List<byte> script)
        {
            var actionList = new List<TreeNode>();
            var type = InstructionType.Action;

            while (script.Count > 1)
            {
                byte opcode = script[0];
                int length = G.GetInstructionLength(opcode);

                var instruction = new Instruction(script.GetRange(0, length), type);
                script.RemoveRange(0, length);

                var node = new TreeNode { Tag = instruction };
                actionList.Add(node);
            }

            return actionList;
        }

        private void attackTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (tree.SelectedNode != null) {
                var selection = (Instruction)tree.SelectedNode.Tag;
                instructionProperties.SelectedObject = selection;
            }
        }

        private void reactionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (tree.SelectedNode != null)
            {
                var selection = (Instruction)tree.SelectedNode.Tag;
                instructionProperties.SelectedObject = selection;
            }
        }
    }
}