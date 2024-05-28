namespace ScriptWriter
{
    partial class PluginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnemyBox = new System.Windows.Forms.ComboBox();
            this.EnemyLabel = new System.Windows.Forms.Label();
            this.AttackLabel = new System.Windows.Forms.Label();
            this.ReactionLabel = new System.Windows.Forms.Label();
            this.AddConditionButton = new System.Windows.Forms.Button();
            this.AddActionButton = new System.Windows.Forms.Button();
            this.ConditionSelectBox = new System.Windows.Forms.ComboBox();
            this.ActionSelectBox = new System.Windows.Forms.ComboBox();
            this.AttackTree = new System.Windows.Forms.TreeView();
            this.ReactionTree = new System.Windows.Forms.TreeView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CollapseButton = new System.Windows.Forms.Button();
            this.ExpandButton = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.InstructionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnemyBox
            // 
            this.EnemyBox.FormattingEnabled = true;
            this.EnemyBox.Location = new System.Drawing.Point(140, 36);
            this.EnemyBox.Name = "EnemyBox";
            this.EnemyBox.Size = new System.Drawing.Size(121, 25);
            this.EnemyBox.TabIndex = 55;
            this.EnemyBox.SelectedIndexChanged += new System.EventHandler(this.EnemyBox_SelectedIndexChanged);
            // 
            // EnemyLabel
            // 
            this.EnemyLabel.AutoSize = true;
            this.EnemyLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnemyLabel.Location = new System.Drawing.Point(72, 38);
            this.EnemyLabel.Name = "EnemyLabel";
            this.EnemyLabel.Size = new System.Drawing.Size(53, 18);
            this.EnemyLabel.TabIndex = 56;
            this.EnemyLabel.Text = "Enemy";
            // 
            // AttackLabel
            // 
            this.AttackLabel.AutoSize = true;
            this.AttackLabel.Location = new System.Drawing.Point(21, 16);
            this.AttackLabel.Name = "AttackLabel";
            this.AttackLabel.Size = new System.Drawing.Size(92, 17);
            this.AttackLabel.TabIndex = 57;
            this.AttackLabel.Text = "Active section";
            // 
            // ReactionLabel
            // 
            this.ReactionLabel.AutoSize = true;
            this.ReactionLabel.Location = new System.Drawing.Point(352, 16);
            this.ReactionLabel.Name = "ReactionLabel";
            this.ReactionLabel.Size = new System.Drawing.Size(107, 17);
            this.ReactionLabel.TabIndex = 59;
            this.ReactionLabel.Text = "Reactive section";
            // 
            // AddConditionButton
            // 
            this.AddConditionButton.Location = new System.Drawing.Point(16, 83);
            this.AddConditionButton.Name = "AddConditionButton";
            this.AddConditionButton.Size = new System.Drawing.Size(109, 27);
            this.AddConditionButton.TabIndex = 62;
            this.AddConditionButton.Text = "Add Condition";
            this.AddConditionButton.UseVisualStyleBackColor = true;
            this.AddConditionButton.Click += new System.EventHandler(this.ConditionButton_Click);
            // 
            // AddActionButton
            // 
            this.AddActionButton.Location = new System.Drawing.Point(16, 112);
            this.AddActionButton.Name = "AddActionButton";
            this.AddActionButton.Size = new System.Drawing.Size(109, 27);
            this.AddActionButton.TabIndex = 63;
            this.AddActionButton.Text = "Add Action";
            this.AddActionButton.UseVisualStyleBackColor = true;
            this.AddActionButton.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // ConditionSelectBox
            // 
            this.ConditionSelectBox.FormattingEnabled = true;
            this.ConditionSelectBox.Items.AddRange(new object[] {
            "00 True",
            "01 HP < half",
            "02 Target has status",
            "03 Target moved",
            "04 Target is alive/dead",
            "05 At most $number of living enemies",
            "06 Battle frame counter > $value",
            "07 Check state",
            "08 HP < $value",
            "09 Stat < $value",
            "0A Stat has bitflags set",
            "0B Stat <= $value",
            "0C Target is close/far",
            "0D Target is close/far inverted?",
            "0E Distance related, unknown",
            "0F Distance related, unknown",
            "10 NCSV Screen position",
            "11 Hit by player/enemy Tech",
            "12 Hit by specific Tech",
            "13 Attacker is player/enemy",
            "14 Attacker is $value",
            "15 Attack is $element type",
            "16 True",
            "17 Percent chance",
            "18 Stat = $value",
            "19 True",
            "1A Enemy is alone",
            "1B At most $number of living PCs",
            "1C PC is present",
            "1D Target is alive/dead",
            "1E False",
            "1F Target is close/far",
            "20 Final attack",
            "21 Unused",
            "22 Unused",
            "23 Unused",
            "24 Unused",
            "25 Unused",
            "26 Unused",
            "27 Unused",
            "28 Unused"});
            this.ConditionSelectBox.Location = new System.Drawing.Point(140, 84);
            this.ConditionSelectBox.Name = "ConditionSelectBox";
            this.ConditionSelectBox.Size = new System.Drawing.Size(121, 25);
            this.ConditionSelectBox.TabIndex = 64;
            // 
            // ActionSelectBox
            // 
            this.ActionSelectBox.FormattingEnabled = true;
            this.ActionSelectBox.Items.AddRange(new object[] {
            "00 Wander",
            "01 Attack",
            "02 Tech",
            "03 Unused",
            "04 Random",
            "05 Unused",
            "06 Unused",
            "07 Transform",
            "08 Unused",
            "09 Unused",
            "0A Run away",
            "0B Set stat",
            "0C Stat math",
            "0D Change state",
            "0E Unused",
            "0F Display message",
            "10 Revive support enemies",
            "11 Multi stat set",
            "12 Tech & multi stat set",
            "13 Unused",
            "14 Multi stat math",
            "15 Tech & Multi stat math",
            "16 Multi revive and set stat"});
            this.ActionSelectBox.Location = new System.Drawing.Point(140, 113);
            this.ActionSelectBox.Name = "ActionSelectBox";
            this.ActionSelectBox.Size = new System.Drawing.Size(121, 25);
            this.ActionSelectBox.TabIndex = 65;
            // 
            // AttackTree
            // 
            this.AttackTree.Location = new System.Drawing.Point(23, 36);
            this.AttackTree.Name = "AttackTree";
            this.AttackTree.Size = new System.Drawing.Size(300, 500);
            this.AttackTree.TabIndex = 66;
            this.AttackTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AttackTree_AfterSelect);
            // 
            // ReactionTree
            // 
            this.ReactionTree.Location = new System.Drawing.Point(355, 36);
            this.ReactionTree.Name = "ReactionTree";
            this.ReactionTree.Size = new System.Drawing.Size(300, 500);
            this.ReactionTree.TabIndex = 67;
            this.ReactionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ReactionTree_AfterSelect);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.DeleteButton);
            this.Panel1.Controls.Add(this.CollapseButton);
            this.Panel1.Controls.Add(this.ExpandButton);
            this.Panel1.Controls.Add(this.AttackTree);
            this.Panel1.Controls.Add(this.ReactionTree);
            this.Panel1.Controls.Add(this.AttackLabel);
            this.Panel1.Controls.Add(this.ReactionLabel);
            this.Panel1.Location = new System.Drawing.Point(334, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(863, 630);
            this.Panel1.TabIndex = 68;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(23, 542);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(83, 32);
            this.DeleteButton.TabIndex = 71;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CollapseButton
            // 
            this.CollapseButton.Location = new System.Drawing.Point(444, 542);
            this.CollapseButton.Name = "CollapseButton";
            this.CollapseButton.Size = new System.Drawing.Size(83, 32);
            this.CollapseButton.TabIndex = 70;
            this.CollapseButton.Text = "Collapse All";
            this.CollapseButton.UseVisualStyleBackColor = true;
            this.CollapseButton.Click += new System.EventHandler(this.CollapseButton_Click);
            // 
            // ExpandButton
            // 
            this.ExpandButton.Location = new System.Drawing.Point(355, 542);
            this.ExpandButton.Name = "ExpandButton";
            this.ExpandButton.Size = new System.Drawing.Size(83, 32);
            this.ExpandButton.TabIndex = 69;
            this.ExpandButton.Text = "Expand All";
            this.ExpandButton.UseVisualStyleBackColor = true;
            this.ExpandButton.Click += new System.EventHandler(this.ExpandButton_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.InstructionPropertyGrid);
            this.Panel2.Controls.Add(this.UpdateButton);
            this.Panel2.Controls.Add(this.EnemyBox);
            this.Panel2.Controls.Add(this.EnemyLabel);
            this.Panel2.Controls.Add(this.ActionSelectBox);
            this.Panel2.Controls.Add(this.AddConditionButton);
            this.Panel2.Controls.Add(this.ConditionSelectBox);
            this.Panel2.Controls.Add(this.AddActionButton);
            this.Panel2.Location = new System.Drawing.Point(29, 12);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(299, 630);
            this.Panel2.TabIndex = 69;
            // 
            // InstructionPropertyGrid
            // 
            this.InstructionPropertyGrid.Location = new System.Drawing.Point(6, 169);
            this.InstructionPropertyGrid.Name = "InstructionPropertyGrid";
            this.InstructionPropertyGrid.Size = new System.Drawing.Size(290, 319);
            this.InstructionPropertyGrid.TabIndex = 66;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(86, 528);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(121, 46);
            this.UpdateButton.TabIndex = 68;
            this.UpdateButton.Text = "Update Record";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // PluginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1325, 746);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "PluginForm";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.ShowInTaskbar = false;
            this.TabText = "ScriptWriter";
            this.Text = "ScriptWriter";
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox EnemyBox;
        private System.Windows.Forms.Label EnemyLabel;
        private System.Windows.Forms.Label AttackLabel;
        private System.Windows.Forms.Label ReactionLabel;
        private System.Windows.Forms.Button AddConditionButton;
        private System.Windows.Forms.Button AddActionButton;
        private System.Windows.Forms.ComboBox ConditionSelectBox;
        private System.Windows.Forms.ComboBox ActionSelectBox;
        private System.Windows.Forms.TreeView AttackTree;
        private System.Windows.Forms.TreeView ReactionTree;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.PropertyGrid InstructionPropertyGrid;
        private System.Windows.Forms.Button CollapseButton;
        private System.Windows.Forms.Button ExpandButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button DeleteButton;
    }
}