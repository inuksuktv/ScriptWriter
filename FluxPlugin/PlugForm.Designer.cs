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
            // 
            // PluginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(750, 367);
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
            this.Load += new System.EventHandler(this.PluginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BlackOmenStorySpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpochStorySpin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label sLocationCat;
        public System.Windows.Forms.NumericUpDown BlackOmenStorySpin;
        public System.Windows.Forms.Label BlackOmenLabel;
        public System.Windows.Forms.Label EpochLabel;
        public System.Windows.Forms.NumericUpDown EpochStorySpin;
    }
}