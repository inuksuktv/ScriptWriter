using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using FluxShared;
using InstructionType = ScriptWriter.Instruction.InstructionType;

namespace ScriptWriter {
    public partial class PluginForm : DockContent {
        public PluginForm()
        {
            InitializeComponent();
            AttackTree.KeyDown += AttackTree_KeyDown;
            ReactionTree.KeyDown += ReactionTree_KeyDown;
        }

        public bool NoUpdate;
        private TreeView _selectedTree;
        private TreeNode _selectedNode;

        #region Init
        public void InitForm() {
			NoUpdate = true;

            EnemyBox.DataSource = GetEnemyNames();

            EnemyBox.SelectedIndex = 0;
            ConditionSelectBox.SelectedIndex = 0;
            ActionSelectBox.SelectedIndex = 0;

            UpdateTreeViews(EnemyBox.SelectedIndex);

            NoUpdate = false;
		}

        private static BindingList<string> GetEnemyNames()
        {
            var enemyNames = new BindingList<string>();
            for (ushort i = 0; i < 256; i++)
            {
                string name = $"{G.HexStr((byte)i)} {G.GetStrFromGroup(StrRecType.Enemies, i)}";
                enemyNames.Add(name);
            }
            return enemyNames;
        }
        #endregion Init

        private void EnemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoUpdate) return;

            int index = EnemyBox.SelectedIndex;
            UpdateTreeViews(index);

            InstructionPropertyGrid.SelectedObject = null;
        }

        #region TreeViews
        private void AttackTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (NoUpdate) return;
            _selectedTree = (TreeView)sender;
            _selectedNode = e.Node;

            if (_selectedTree.SelectedNode != null)
            {
                var selection = (Instruction)_selectedTree.SelectedNode.Tag;
                InstructionPropertyGrid.SelectedObject = selection;
                selection.PropertyChanged += CurrentInstruction_PropertyChanged;
            }
        }

        private void AttackTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DeleteButton_Click(sender, e);
        }

        private void ReactionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (NoUpdate) return;
            _selectedTree = (TreeView)sender;
            _selectedNode = e.Node;

            if (_selectedTree.SelectedNode != null)
            {
                var instruction = (Instruction)_selectedTree.SelectedNode.Tag;
                InstructionPropertyGrid.SelectedObject = instruction;
                instruction.PropertyChanged += CurrentInstruction_PropertyChanged;
            }
        }

        private void ReactionTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DeleteButton_Click(sender, e);
        }
        #endregion

        #region TreeView update
        // Update both TreeViews to show the current script.
        private void UpdateTreeViews(int index)
        {
            SaveRecord record = G.SaveRec[(byte)RecType.EnemyScripts][index];
            if (record.nOrigSize == 0)
            {
                AttackTree.Nodes.Clear();
                var invalid = G.Factory.CreateInstruction(-1);
                AttackTree.Nodes.Add(new TreeNode { Tag = invalid, Text = "Invalid data" });
                ReactionTree.Nodes.Clear();
                ReactionTree.Nodes.Add(new TreeNode { Tag = invalid, Text = "Invalid data" });
                return;
            }

            GetAttacksAndReactions(record.nData, out List<byte> activeSection, out List<byte> reactiveSection, out int reactiveOffset);

            var scriptAddress = (int)record.nOrigAddr;
            UpdateNodes(AttackTree, activeSection, scriptAddress, 0);
            UpdateNodes(ReactionTree, reactiveSection, scriptAddress, reactiveOffset);
        }

        private static void GetAttacksAndReactions(byte[] fullScript, out List<byte> active, out List<byte> reactive, out int reactiveOffset)
        {
            active = new List<byte>();
            reactive = new List<byte>();
            byte cell = 0;
            int i = 0;

            // The Attack and Reaction sections of the script each end in 0xFF.
            while (cell != 0xFF)
            {
                cell = fullScript[i++];
                active.Add(cell);
            }
            cell = fullScript[i];
            reactiveOffset = i;
            while (cell != 0xFF)
            {
                cell = fullScript[i++];
                reactive.Add(cell);
            }
        }

        // Works with either the Attack or Reaction sections of the script passed in.
        private static void UpdateNodes(TreeView tree, List<byte> scriptSection, int scriptAddress, int reactionOffset)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();

            if (scriptSection.Count == 0) throw new ArgumentException("Tried to update a TreeView with an empty script.");
            bool isDummyScript = (scriptSection.Count == 1);

            // Actions get nested within the final condition in a block.
            TreeNode lastCondition = new TreeNode();

            int index = 0;
            int instructionOffset = 0;
            int blockOffset = reactionOffset;
            while (index < scriptSection.Count - 1)
            {
                instructionOffset = 0;
                blockOffset = reactionOffset + index;

                // Return the index so that we can pick up where we left off next loop.
                index = GetCurrentBlock(scriptSection, index, out List<byte> conditions, out List<byte> actions);

                List<TreeNode> conditionNodes = ParseConditions(conditions);
                List<TreeNode> actionNodes = ParseActions(actions);

                foreach (TreeNode node in conditionNodes)
                {
                    var instruction = (Instruction)node.Tag;
                    instruction.Address = G.HexStr(scriptAddress + blockOffset + instructionOffset, 6);
                    instructionOffset += instruction.Length;
                    node.Text = "If " + instruction.Description;
                    tree.Nodes.Add(node);
                    lastCondition = node;
                }
                instructionOffset++;
                
                foreach (TreeNode node in actionNodes)
                {
                    var instruction = (Instruction)node.Tag;
                    instruction.Address = G.HexStr(scriptAddress + blockOffset + instructionOffset, 6);
                    instructionOffset += instruction.Length;
                    node.Text = instruction.Description;
                    lastCondition.Nodes.Add(node);
                }
                instructionOffset++;
            }
            if (isDummyScript) instructionOffset = 0;

            // The last byte in the current section is FF.
            Instruction end = G.Factory.CreateInstruction(0xFF, InstructionType.Other);
            end.Address = G.HexStr(scriptAddress + blockOffset + instructionOffset, 6);
            var endNode = new TreeNode { Tag = end, Text = "End" };
            tree.Nodes.Add(endNode);

            tree.ExpandAll();
            tree.EndUpdate();
        }

        private static int GetCurrentBlock(List<byte> scriptBlock, int index, out List<byte> conditions, out List<byte> actions)
        {
            conditions = new List<byte>();
            actions = new List<byte>();
            byte cell = 0;

            // The condition and action sections of each block end in an 0xFE byte.
            while (cell != 0xFE)
            {
                cell = scriptBlock[index++];
                conditions.Add(cell);
            }

            cell = scriptBlock[index];

            while (cell != 0xFE)
            {
                cell = scriptBlock[index++];
                // Must check for 0xFF here since the vanilla Red Beast script is missing an 0xFE.
                if (cell == 0xFF) break;
                actions.Add(cell);
            }
            return index;
        }

        // Returns the list of all conditions in a single block.
        private static List<TreeNode> ParseConditions(List<byte> script)
        {
            var conditions = new List<TreeNode>();
            var type = InstructionType.Condition;

            if ((script.Count % 4) != 1) { throw new ArgumentException("The conditions part of the script had an unexpected number of bytes."); }

            while (script.Count > 1)
            {
                var instruction = G.Factory.CreateInstruction(script.GetRange(0, 4), type);
                script.RemoveRange(0, 4);

                var node = new TreeNode { Tag = instruction };
                conditions.Add(node);
            }
            return conditions;
        }

        // Returns the list of all actions in a single block.
        private static List<TreeNode> ParseActions(List<byte> script)
        {
            var actionList = new List<TreeNode>();
            var type = InstructionType.Action;

            while (script.Count > 1)
            {
                byte opcode = script[0];
                int length = G.GetInstructionLength(opcode);

                var instruction = G.Factory.CreateInstruction(script.GetRange(0, length), type);
                script.RemoveRange(0, length);

                var node = new TreeNode { Tag = instruction };
                actionList.Add(node);
            }
            return actionList;
        }
        #endregion

        #region Update Button
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            List<byte> script = GetScriptFromTreeDisplays();
            if (script.Count == 0)
            {
                string invalid = @"Invalid data.
To enable editing this script, use a placeholder at load time.";
                MessageBox.Show(invalid, "Script Writer", MessageBoxButtons.OK);
                return;
            }

            var record = G.SaveRec[(byte)RecType.EnemyScripts][EnemyBox.SelectedIndex];
            for (var i = 0; i < script.Count; i++)
            {
                // If the user's script doesn't match the record, update the record.
                if (record.nData[i] != script[i])
                {
                    record.bModified = true;
                    record.nDataSize = (uint)script.Count;
                    record.nData = new byte[script.Count];
                    byte[] scriptArray = script.ToArray();
                    Array.Copy(scriptArray, record.nData, scriptArray.Length);
                    string update = @"Script record updated.
Records must still be saved to the ROM.";
                    MessageBox.Show(update, "Script Writer", MessageBoxButtons.OK);
                    break;
                }
            }
            if (!record.bModified)
            {
                string noUpdate = "Edit, add, or remove instructions and then click Update Script.";
                MessageBox.Show(noUpdate, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<byte> GetScriptFromTreeDisplays()
        {
            var bytes = new List<byte>();

            List<Instruction> attacks = GetScriptFrom(AttackTree);
            List<Instruction> reactions = GetScriptFrom(ReactionTree);
            if (attacks.Count == 0 || reactions.Count == 0) return bytes;

            bytes.AddRange(InstructionsToByteCode(attacks));
            bytes.AddRange(InstructionsToByteCode(reactions));
            return bytes;
        }

        private static List<Instruction> GetScriptFrom(TreeView tree)
        {
            var instructions = new List<Instruction>();

            foreach (TreeNode node in tree.Nodes)
            {
                var condition = (Instruction)node.Tag;
                if (condition.IsInvalid()) return new List<Instruction>();

                instructions.Add(condition);
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        var action = (Instruction)child.Tag;
                        instructions.Add(action);
                    }
                }
            }
            return instructions;
        }

        private static List<byte> InstructionsToByteCode(List<Instruction> instructions)
        {
            var bytes = new List<byte>();
            for (var i = 0; i < instructions.Count; i++)
            {
                bytes.AddRange(instructions[i].Bytes);
                // Check for the terminal instruction and break so that we don't get an out-of-bounds exception.
                if (i == (instructions.Count - 1)) break;
                // Add separators after the Conditions and Actions within a block.
                if (instructions[i].Type != instructions[i + 1].Type) { bytes.Add(0xFE); }
            }
            return bytes;
        }
        #endregion

        #region Other Buttons
        private void ActionButton_Click(object sender, EventArgs e)
        {
            if (NoUpdate) return;
            byte actionIndex = (byte)ActionSelectBox.SelectedIndex;
            var type = InstructionType.Action;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                string selectMessage = @"Please select a Condition or Action from the script before inserting a new Action.";
                MessageBox.Show(selectMessage, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);

            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                int insertIndex = GetChildNodeIndex(_selectedNode, _selectedTree) + 1;
                var action = G.Factory.CreateInstruction(actionIndex, type);
                var node = new TreeNode { Tag = action, Text = action.Description };
                _selectedNode.Parent.Nodes.Insert(insertIndex, node);
            }
            // Case: the "End" node is selected.
            else if (nodeIndex == (_selectedTree.Nodes.Count - 1))
            {
                string endMessage = @"Cannot add an Action to the End marker.";
                MessageBox.Show(endMessage, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            // Case: any other top-level node is selected.
            else
            {
                var action = G.Factory.CreateInstruction(actionIndex, type);
                var node = new TreeNode { Tag = action, Text = action.Description };
                _selectedNode.Nodes.Add(node);
            }
        }

        private void ConditionButton_Click(object sender, EventArgs e)
        {
            if (NoUpdate) return;
            byte conditionIndex = (byte)ConditionSelectBox.SelectedIndex;
            var type = InstructionType.Condition;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                string selectMessage = @"Please select a Condition from the script before inserting a new one.";
                MessageBox.Show(selectMessage, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);
            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                string actionMessage = @"Cannot insert a Condition into the Action list.
Please select a Condition to insert a new Condition after it.";
                MessageBox.Show(actionMessage, "Script Writer", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            // Case: the "End" node is selected.
            else if (nodeIndex == (_selectedTree.Nodes.Count - 1))
            {
                var condition = G.Factory.CreateInstruction(conditionIndex, type);
                var node = new TreeNode { Tag = condition, Text = condition.Description };
                _selectedTree.Nodes.Insert(nodeIndex, node);
            }
            // Case: any other top-level node is selected.
            else
            {
                int insertIndex = nodeIndex + 1;
                var condition = G.Factory.CreateInstruction(conditionIndex, type);
                var node = new TreeNode { Tag = condition, Text = condition.Description };
                _selectedTree.Nodes.Insert(insertIndex, node);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedNode == null)
            {
                string selectMessage = @"Please select an instruction from the script to delete.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var instruction = (Instruction)_selectedNode.Tag;
            if (instruction.Type == InstructionType.Other)
            {
                string selectMessage = @"The End instruction cannot be deleted.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_selectedNode.Nodes.Count != 0)
            {
                string childMessage = @"Are you sure you want to delete this instruction and ALL its children?";
                DialogResult dialog = MessageBox.Show(childMessage, "Dialog", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    _selectedNode.Remove();
                }
            }
            else _selectedNode.Remove();
        }

        private void ExpandButton_Click(object sender, EventArgs e)
        {
            AttackTree.ExpandAll();
            ReactionTree.ExpandAll();
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            AttackTree.CollapseAll();
            ReactionTree.CollapseAll();
        }
        #endregion Other Buttons


        // Generate a new instruction and update the TreeNode when the user changes the opcode of an instruction.
        private void CurrentInstruction_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Opcode")
            {
                var oldInstruction = (Instruction)_selectedNode.Tag;
                oldInstruction.PropertyChanged -= CurrentInstruction_PropertyChanged;

                // Read the user's desired opcode from the old instruction and generate a new one.
                var newInstruction = G.Factory.CreateInstruction(oldInstruction.Opcode, oldInstruction.Type);
                _selectedNode.Tag = newInstruction;
                _selectedNode.Text = newInstruction.Description;
                newInstruction.PropertyChanged += CurrentInstruction_PropertyChanged;

                InstructionPropertyGrid.SelectedObject = newInstruction;
                InstructionPropertyGrid.Refresh();
            }
        }

        private static int GetChildNodeIndex(TreeNode node, TreeView tree)
        {
            if (node == null || tree == null || node.Parent == null) return -1;

            for (var i = 0; i < node.Parent.Nodes.Count; i++)
            {
                if (node.Parent.Nodes[i] == node) return i;
            }
            return -1;
        }
    }
}