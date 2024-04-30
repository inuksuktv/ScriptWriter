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

        public BindingList<string> EnemyNames { get; private set; }
        
        public bool bNoUpdate = false;
        private List<List<byte>> _enemyScripts;
        private byte[] _localBank;
        private InstructionFactory _factory;
        private TreeView _selectedTree;
        private TreeNode _selectedNode;

		public void InitForm() {
			bNoUpdate = true;

            GetEnemyNames();
            EnemyBox.DataSource = EnemyNames;
            EnemyBox.SelectedIndex = 0;
            conditionSelectBox.SelectedIndex = 0;
            actionSelectBox.SelectedIndex = 0;

            _localBank = new byte[0x010000];
            Array.Copy(G.WorkingData, 0x0C0000, _localBank, 0, _localBank.Length);

            _factory = new InstructionFactory();
            GetEnemyScripts();
            UpdateScript(EnemyBox.SelectedIndex);

            bNoUpdate = false;
		}

        private void GetEnemyNames()
        {
            EnemyNames = new BindingList<string>();
            for (ushort i = 0; i < 256; i++)
            {
                var byteSize = (byte)i;
                EnemyNames.Add($"{G.HexStr(byteSize)} {G.GetStrFromGroup(StrRecType.Enemies, i)}");
            }
        }

        // Populate the initial list of scripts directly from ROM data.
        private void GetEnemyScripts()
        {
            _enemyScripts = new List<List<byte>>(256);

            for (var i = 0; i < 256; i++)
            {
                var record = G.SaveRec[(byte)RecType.AttackScriptPointers][i];
                int pointer = (record.nData[1] << 8) + record.nData[0];
                List<byte> script = GetScriptStartingAt(pointer);
                _enemyScripts.Add(script);
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
            UpdateScript(index);

            instructionPropertyGrid.SelectedObject = null;
        }

        private void attackTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (bNoUpdate) return;
            _selectedTree = (TreeView)sender;
            _selectedNode = e.Node;

            if (_selectedTree.SelectedNode != null)
            {
                var selection = (Instruction)_selectedTree.SelectedNode.Tag;
                instructionPropertyGrid.SelectedObject = selection;
                selection.PropertyChanged += currentInstruction_PropertyChanged;
            }
        }

        private void reactionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (bNoUpdate) return;
            _selectedTree = (TreeView)sender;
            _selectedNode = e.Node;

            if (_selectedTree.SelectedNode != null)
            {
                var instruction = (Instruction)_selectedTree.SelectedNode.Tag;
                instructionPropertyGrid.SelectedObject = instruction;
                instruction.PropertyChanged += currentInstruction_PropertyChanged;
            }
        }

        private void currentInstruction_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Opcode")
            {
                var oldInstruction = (Instruction)_selectedNode.Tag;
                oldInstruction.PropertyChanged -= currentInstruction_PropertyChanged;

                // The user's Opcode selection has already been updated on the Instruction.
                var newInstruction = _factory.CreateInstruction(oldInstruction.Opcode, oldInstruction.Type);
                _selectedNode.Tag = newInstruction;
                _selectedNode.Text = newInstruction.Description;
                newInstruction.PropertyChanged += currentInstruction_PropertyChanged;

                instructionPropertyGrid.SelectedObject = newInstruction;
                instructionPropertyGrid.Refresh();
            }
        }

        private void conditionButton_Click(object sender, EventArgs e)
        {
            if (bNoUpdate) return;
            byte conditionIndex = (byte)conditionSelectBox.SelectedIndex;
            var type = InstructionType.Condition;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                string selectMessage = @"Please select a Condition from the script before inserting a new one.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);
            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                string actionMessage = @"Cannot insert a Condition into the Action list.
Please select a Condition to insert a new Condition after it.";
                MessageBox.Show(actionMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Case: the "End" node is selected.
            else if (nodeIndex == (_selectedTree.Nodes.Count - 1))
            {
                var condition = _factory.CreateInstruction(conditionIndex, type);
                var node = new TreeNode { Tag = condition, Text = condition.Description };
                _selectedTree.Nodes.Insert(nodeIndex, node);
            }
            // Case: any other top-level node is selected.
            else
            {
                int insertIndex = nodeIndex + 1;
                var condition = _factory.CreateInstruction(conditionIndex, type);
                var node = new TreeNode { Tag = condition, Text = condition.Description };
                _selectedTree.Nodes.Insert(insertIndex, node);
            }
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            if (bNoUpdate) return;
            byte actionIndex = (byte)actionSelectBox.SelectedIndex;
            var type = InstructionType.Action;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                string selectMessage = @"Please select a Condition or Action from the script before inserting a new Action.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);

            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                int insertIndex = GetChildNodeIndex(_selectedNode, _selectedTree) + 1;
                var action = _factory.CreateInstruction(actionIndex, type);
                var node = new TreeNode { Tag = action, Text = action.Description };
                _selectedNode.Parent.Nodes.Insert(insertIndex, node);
            }
            // Case: the "End" node is selected.
            else if (nodeIndex == (_selectedTree.Nodes.Count - 1))
            {
                string endMessage = @"Cannot add an Action to the End marker.";
                MessageBox.Show(endMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Case: any other top-level node is selected.
            else
            {
                var action = _factory.CreateInstruction(actionIndex, type);
                var node = new TreeNode { Tag = action, Text = action.Description };
                _selectedNode.Nodes.Add(node);
            }

        }

        private void UpdateScript(int index)
        {
            GetAttacksAndReactions(_enemyScripts[index], out List<byte> attacks, out List<byte> reactions);

            UpdateNodes(attackTree, attacks);
            UpdateNodes(reactionTree, reactions);

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
            tree.BeginUpdate();
            tree.Nodes.Clear();

            int index = 0;
            // Save a reference to the final condition in a block. Actions get nested within the final condition.
            TreeNode finalCondition = new TreeNode();

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
            
            tree.EndUpdate();
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

        // Returns the list of all conditions in a single block.
        private List<TreeNode> ParseConditions(List<byte> script)
        {
            var conditions = new List<TreeNode>();
            var type = InstructionType.Condition;

            // Every condition instruction is 4 bytes.
            while (script.Count > 1)
            {
                var instruction = _factory.CreateInstruction(script.GetRange(0, 4), type);
                script.RemoveRange(0, 4);

                var node = new TreeNode { Tag = instruction };
                conditions.Add(node);
            }

            return conditions;
        }

        // Returns the list of all actions in a single block.
        private List<TreeNode> ParseActions(List<byte> script)
        {
            var actionList = new List<TreeNode>();
            var type = InstructionType.Action;

            while (script.Count > 1)
            {
                byte opcode = script[0];
                int length = G.GetInstructionLength(opcode);

                var instruction = _factory.CreateInstruction(script.GetRange(0, length), type);
                script.RemoveRange(0, length);

                var node = new TreeNode { Tag = instruction };
                actionList.Add(node);
            }

            return actionList;
        }
        
        private int GetChildNodeIndex(TreeNode node, TreeView tree)
        {
            if (node == null || tree == null || node.Parent == null) return -1;
            for (var i = 0; i < node.Parent.Nodes.Count; i++)
            {
                if (node.Parent.Nodes[i] == node)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}