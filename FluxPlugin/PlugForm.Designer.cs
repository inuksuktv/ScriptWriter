namespace BattleScriptWriter
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
            this.conditionButton = new System.Windows.Forms.Button();
            this.actionButton = new System.Windows.Forms.Button();
            this.conditionSelectBox = new System.Windows.Forms.ComboBox();
            this.actionSelectBox = new System.Windows.Forms.ComboBox();
            this.attackTree = new System.Windows.Forms.TreeView();
            this.reactionTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // EnemyBox
            // 
            this.EnemyBox.FormattingEnabled = true;
            this.EnemyBox.Location = new System.Drawing.Point(6, 29);
            this.EnemyBox.Name = "EnemyBox";
            this.EnemyBox.Size = new System.Drawing.Size(121, 25);
            this.EnemyBox.TabIndex = 55;
            this.EnemyBox.SelectedIndexChanged += new System.EventHandler(this.EnemyBox_SelectedIndexChanged);
            // 
            // EnemyLabel
            // 
            this.EnemyLabel.AutoSize = true;
            this.EnemyLabel.Location = new System.Drawing.Point(3, 9);
            this.EnemyLabel.Name = "EnemyLabel";
            this.EnemyLabel.Size = new System.Drawing.Size(51, 17);
            this.EnemyLabel.TabIndex = 56;
            this.EnemyLabel.Text = "Enemy";
            // 
            // AttackLabel
            // 
            this.AttackLabel.AutoSize = true;
            this.AttackLabel.Location = new System.Drawing.Point(3, 132);
            this.AttackLabel.Name = "AttackLabel";
            this.AttackLabel.Size = new System.Drawing.Size(86, 17);
            this.AttackLabel.TabIndex = 57;
            this.AttackLabel.Text = "Attack Script";
            // 
            // ReactionLabel
            // 
            this.ReactionLabel.AutoSize = true;
            this.ReactionLabel.Location = new System.Drawing.Point(653, 133);
            this.ReactionLabel.Name = "ReactionLabel";
            this.ReactionLabel.Size = new System.Drawing.Size(100, 17);
            this.ReactionLabel.TabIndex = 59;
            this.ReactionLabel.Text = "Reaction Script";
            // 
            // conditionButton
            // 
            this.conditionButton.Location = new System.Drawing.Point(167, 29);
            this.conditionButton.Name = "conditionButton";
            this.conditionButton.Size = new System.Drawing.Size(109, 23);
            this.conditionButton.TabIndex = 62;
            this.conditionButton.Text = "Add Condition";
            this.conditionButton.UseVisualStyleBackColor = true;
            // 
            // actionButton
            // 
            this.actionButton.Location = new System.Drawing.Point(167, 59);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(109, 23);
            this.actionButton.TabIndex = 63;
            this.actionButton.Text = "Add Action";
            this.actionButton.UseVisualStyleBackColor = true;
            // 
            // conditionSelectBox
            // 
            this.conditionSelectBox.FormattingEnabled = true;
            this.conditionSelectBox.Items.AddRange(new object[] {
            "00 True",
            "01 HP < half",
            "02 Target has status",
            "03 Target moved",
            "04 Target is alive/dead",
            "05 At most $number of living enemies",
            "06 Battle frame counter > $value",
            "07 Custom compare $value",
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
            "21 Unknown",
            "22 Unknown",
            "23 Unknown",
            "24 Unknown",
            "25 Unknown",
            "26 Unknown",
            "27 Unknown",
            "28 Unknown"});
            this.conditionSelectBox.Location = new System.Drawing.Point(283, 29);
            this.conditionSelectBox.Name = "conditionSelectBox";
            this.conditionSelectBox.Size = new System.Drawing.Size(121, 25);
            this.conditionSelectBox.TabIndex = 64;
            // 
            // actionSelectBox
            // 
            this.actionSelectBox.FormattingEnabled = true;
            this.actionSelectBox.Items.AddRange(new object[] {
            "00 Wander",
            "01 Attack",
            "02 Tech",
            "03 Unused",
            "04 Random",
            "05 Unknown",
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
            this.actionSelectBox.Location = new System.Drawing.Point(283, 60);
            this.actionSelectBox.Name = "actionSelectBox";
            this.actionSelectBox.Size = new System.Drawing.Size(121, 25);
            this.actionSelectBox.TabIndex = 65;
            // 
            // attackTree
            // 
            this.attackTree.Location = new System.Drawing.Point(6, 153);
            this.attackTree.Name = "attackTree";
            this.attackTree.Size = new System.Drawing.Size(621, 299);
            this.attackTree.TabIndex = 66;
            // 
            // reactionTree
            // 
            this.reactionTree.Location = new System.Drawing.Point(656, 153);
            this.reactionTree.Name = "reactionTree";
            this.reactionTree.Size = new System.Drawing.Size(638, 299);
            this.reactionTree.TabIndex = 67;
            // 
            // PluginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1325, 554);
            this.Controls.Add(this.reactionTree);
            this.Controls.Add(this.attackTree);
            this.Controls.Add(this.actionSelectBox);
            this.Controls.Add(this.conditionSelectBox);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.conditionButton);
            this.Controls.Add(this.ReactionLabel);
            this.Controls.Add(this.AttackLabel);
            this.Controls.Add(this.EnemyLabel);
            this.Controls.Add(this.EnemyBox);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "PluginForm";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.ShowInTaskbar = false;
            this.TabText = "ScriptWriter";
            this.Text = "BattleScripts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox EnemyBox;
        private System.Windows.Forms.Label EnemyLabel;
        private System.Windows.Forms.Label AttackLabel;
        private System.Windows.Forms.Label ReactionLabel;
        private System.Windows.Forms.Button conditionButton;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.ComboBox conditionSelectBox;
        private System.Windows.Forms.ComboBox actionSelectBox;
        private System.Windows.Forms.TreeView attackTree;
        private System.Windows.Forms.TreeView reactionTree;
    }
}