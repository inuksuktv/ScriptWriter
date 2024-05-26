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

            if (_selectedTree.SelectedNode == null) return;

            var selection = (Instruction)_selectedTree.SelectedNode.Tag;
            InstructionPropertyGrid.SelectedObject = selection;
            selection.PropertyChanged += CurrentInstruction_PropertyChanged;
        }

        private void AttackTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (NoUpdate) return;
            if (e.KeyCode == Keys.Delete) DeleteButton_Click(sender, e);
        }

        private void ReactionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (NoUpdate) return;
            _selectedTree = (TreeView)sender;
            _selectedNode = e.Node;

            if (_selectedTree.SelectedNode == null) return;

            var instruction = (Instruction)_selectedTree.SelectedNode.Tag;
            InstructionPropertyGrid.SelectedObject = instruction;
            instruction.PropertyChanged += CurrentInstruction_PropertyChanged;
        }

        private void ReactionTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (NoUpdate) return;
            if (e.KeyCode == Keys.Delete) DeleteButton_Click(sender, e);
        }
        #endregion

        #region TreeView Update
        // Update both TreeViews to show the current script.
        private void UpdateTreeViews(int index)
        {
            SaveRecord record = G.SaveRec[(byte)RecType.EnemyScripts][index];
            if (record.nOrigSize == 0) { InvalidData(); return; }

            var script = new List<byte>(record.nData);
            ScriptParser.GetSections(script, out List<byte> activeSection, out List<byte> reactiveSection, out int reactiveOffset);
            
            var scriptAddress = (int)record.nOrigAddr;
            UpdateNodes(AttackTree, activeSection, scriptAddress, 0);
            UpdateNodes(ReactionTree, reactiveSection, scriptAddress, reactiveOffset);
        }

        private void InvalidData()
        {
            Instruction invalid = G.Factory.CreateInstruction(-1);
            AttackTree.Nodes.Clear();
            AttackTree.Nodes.Add(new TreeNode { Tag = invalid, Text = "Invalid data" });
            ReactionTree.Nodes.Clear();
            ReactionTree.Nodes.Add(new TreeNode { Tag = invalid, Text = "Invalid data" });
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
            // Process the script section one conditional block at a time.
            while (index < scriptSection.Count - 1)
            {
                instructionOffset = 0;
                blockOffset = reactionOffset + index;

                // Return the index so that we can pick up where we left off next loop.
                index = ScriptParser.GetCurrentBlock(scriptSection, index, out List<byte> conditions, out List<byte> actions);

                List<Instruction> parsedConditions = ScriptParser.ParseConditions(conditions);
                List<Instruction> parsedActions = ScriptParser.ParseActions(actions);

                foreach (Instruction condition in parsedConditions)
                {
                    condition.Address = G.HexStr(scriptAddress + blockOffset + instructionOffset, 6);
                    instructionOffset += condition.Length;
                    var node = new TreeNode { Text = $"If {condition.Description}", Tag = condition };
                    tree.Nodes.Add(node);
                    lastCondition = node;
                }
                instructionOffset++;
                
                foreach (Instruction action in parsedActions)
                {
                    action.Address = G.HexStr(scriptAddress + blockOffset + instructionOffset, 6);
                    instructionOffset += action.Length;
                    lastCondition.Nodes.Add(new TreeNode { Text = action.Description, Tag = action });
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
        #endregion

        #region Update Button
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (NoUpdate) return;

            List<byte> script = GetScriptFromTreeDisplays();
            if (script.Count == 0)
            {
                const string invalid = @"Invalid data.
To enable editing this script, use a placeholder at load time.";
                MessageBox.Show(invalid, "Script Writer", MessageBoxButtons.OK);
                return;
            }

            var record = G.SaveRec[(byte)RecType.EnemyScripts][EnemyBox.SelectedIndex];
            for (var i = 0; i < script.Count; i++)
            {
                if (record.nData[i] == script[i]) continue;

                // If the script doesn't match the record, update the record.
                record.bModified = true;
                record.nDataSize = (uint)script.Count;
                record.nData = new byte[script.Count];
                byte[] scriptArray = script.ToArray();
                Array.Copy(scriptArray, record.nData, scriptArray.Length);
                const string update = @"Script record updated.
Records must still be saved to the ROM.";
                MessageBox.Show(update, "Script Writer", MessageBoxButtons.OK);
                break;
            }

            if (record.bModified) return;
            const string noUpdate = "Edit, add, or remove instructions and then click Update Script.";
            MessageBox.Show(noUpdate, "Script Writer", MessageBoxButtons.OK);
        }

        private List<byte> GetScriptFromTreeDisplays()
        {
            var bytes = new List<byte>();

            List<Instruction> active = GetScriptFrom(AttackTree);
            List<Instruction> reactive = GetScriptFrom(ReactionTree);
            if (active.Count == 0 || reactive.Count == 0) return bytes;

            bytes.AddRange(InstructionsToByteCode(active));
            bytes.AddRange(InstructionsToByteCode(reactive));
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
                foreach (TreeNode child in node.Nodes)
                {
                    var action = (Instruction)child.Tag;
                    instructions.Add(action);
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
            const InstructionType type = InstructionType.Action;
            byte actionIndex = (byte)ActionSelectBox.SelectedIndex;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                const string selectMessage = @"Please select a Condition or Action from the script before inserting a new Action.";
                MessageBox.Show(selectMessage, "Script Writer", MessageBoxButtons.OK);
                return;
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);

            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                int insertIndex = (GetChildNodeIndex(_selectedNode, _selectedTree) + 1);
                var action = G.Factory.CreateInstruction(actionIndex, type);
                var node = new TreeNode { Tag = action, Text = action.Description };
                _selectedNode.Parent.Nodes.Insert(insertIndex, node);
            }
            // Case: the "End" node is selected.
            else if (nodeIndex == (_selectedTree.Nodes.Count - 1))
            {
                const string endMessage = @"Cannot add an Action to the End marker.";
                MessageBox.Show(endMessage, "Script Writer", MessageBoxButtons.OK);
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
            const InstructionType type = InstructionType.Condition;
            byte conditionIndex = (byte)ConditionSelectBox.SelectedIndex;

            // Case: no tree or node is selected.
            if (_selectedTree == null || _selectedNode == null)
            {
                const string selectMessage = @"Please select a Condition from the script before inserting a new one.";
                MessageBox.Show(selectMessage, "Script Writer", MessageBoxButtons.OK);
                return;
            }

            int nodeIndex = _selectedTree.Nodes.IndexOf(_selectedNode);
            // Case: a nested node is selected.
            if (_selectedNode.Parent != null)
            {
                const string actionMessage = @"Cannot insert a Condition into the Action list.
Please select a Condition to insert a new Condition after it.";
                MessageBox.Show(actionMessage, "Script Writer", MessageBoxButtons.OK);
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
                const string selectMessage = @"Please select an instruction from the script to delete.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var instruction = (Instruction)_selectedNode.Tag;
            if (instruction.Type == InstructionType.Other)
            {
                const string selectMessage = @"The End instruction cannot be deleted.";
                MessageBox.Show(selectMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_selectedNode.Nodes.Count != 0)
            {
                const string childMessage = @"Are you sure you want to delete this instruction and ALL its children?";
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