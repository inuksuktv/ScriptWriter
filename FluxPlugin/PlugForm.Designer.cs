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
            this.sLocationCat = new System.Windows.Forms.Label();
            this.BlackOmenStorySpin = new System.Windows.Forms.NumericUpDown();
            this.BlackOmenLabel = new System.Windows.Forms.Label();
            this.EpochLabel = new System.Windows.Forms.Label();
            this.EpochStorySpin = new System.Windows.Forms.NumericUpDown();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BlackOmenStorySpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpochStorySpin)).BeginInit();
            this.SuspendLayout();
            // 
            // sLocationCat
            // 
            this.sLocationCat.BackColor = System.Drawing.SystemColors.Control;
            this.sLocationCat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sLocationCat.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.sLocationCat.Location = new System.Drawing.Point(3, 3);
            this.sLocationCat.Name = "sLocationCat";
            this.sLocationCat.Size = new System.Drawing.Size(155, 17);
            this.sLocationCat.TabIndex = 49;
            this.sLocationCat.Text = "Storyline Points";
            this.sLocationCat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BlackOmenStorySpin
            // 
            this.BlackOmenStorySpin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BlackOmenStorySpin.Enabled = false;
            this.BlackOmenStorySpin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackOmenStorySpin.Hexadecimal = true;
            this.BlackOmenStorySpin.Location = new System.Drawing.Point(148, 23);
            this.BlackOmenStorySpin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlackOmenStorySpin.Name = "BlackOmenStorySpin";
            this.BlackOmenStorySpin.Size = new System.Drawing.Size(81, 20);
            this.BlackOmenStorySpin.TabIndex = 50;
            this.BlackOmenStorySpin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BlackOmenStorySpin.ValueChanged += new System.EventHandler(this.OnBlackOmen);
            // 
            // BlackOmenLabel
            // 
            this.BlackOmenLabel.BackColor = System.Drawing.SystemColors.Window;
            this.BlackOmenLabel.Location = new System.Drawing.Point(3, 23);
            this.BlackOmenLabel.Name = "BlackOmenLabel";
            this.BlackOmenLabel.Size = new System.Drawing.Size(143, 17);
            this.BlackOmenLabel.TabIndex = 51;
            this.BlackOmenLabel.Text = "Black Omen";
            this.BlackOmenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EpochLabel
            // 
            this.EpochLabel.BackColor = System.Drawing.SystemColors.Window;
            this.EpochLabel.Location = new System.Drawing.Point(3, 44);
            this.EpochLabel.Name = "EpochLabel";
            this.EpochLabel.Size = new System.Drawing.Size(143, 17);
            this.EpochLabel.TabIndex = 52;
            this.EpochLabel.Text = "Epoch to Last Village";
            this.EpochLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EpochStorySpin
            // 
            this.EpochStorySpin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EpochStorySpin.Hexadecimal = true;
            this.EpochStorySpin.Location = new System.Drawing.Point(148, 44);
            this.EpochStorySpin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.EpochStorySpin.Name = "EpochStorySpin";
            this.EpochStorySpin.Size = new System.Drawing.Size(81, 20);
            this.EpochStorySpin.TabIndex = 53;
            this.EpochStorySpin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EpochStorySpin.ValueChanged += new System.EventHandler(this.EpochStorySpin_ValueChanged);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UpdateButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UpdateButton.Location = new System.Drawing.Point(261, 23);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(126, 23);
            this.UpdateButton.TabIndex = 54;
            this.UpdateButton.Text = "Update Script";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 155);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 25);
            this.comboBox1.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 56;
            this.label1.Text = "Enemy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Attack Script";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(148, 155);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 24);
            this.textBox1.TabIndex = 58;
            // 
            // PluginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(750, 367);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.EpochStorySpin);
            this.Controls.Add(this.EpochLabel);
            this.Controls.Add(this.BlackOmenLabel);
            this.Controls.Add(this.BlackOmenStorySpin);
            this.Controls.Add(this.sLocationCat);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "PluginForm";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.ShowInTaskbar = false;
            this.TabText = "PlugForm";
            this.Text = "BattleScripts";
            ((System.ComponentModel.ISupportInitialize)(this.BlackOmenStorySpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpochStorySpin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sLocationCat;
        public System.Windows.Forms.NumericUpDown BlackOmenStorySpin;
        public System.Windows.Forms.Label BlackOmenLabel;
        public System.Windows.Forms.Label EpochLabel;
        public System.Windows.Forms.NumericUpDown EpochStorySpin;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}