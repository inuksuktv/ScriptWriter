namespace FluxPlugin {
	partial class PluginForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label3 = new System.Windows.Forms.Label();
			this.BlackOmenStorySpin = new System.Windows.Forms.NumericUpDown();
			this.sLocationCat = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) (this.BlackOmenStorySpin)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.Window;
			this.label3.Location = new System.Drawing.Point(3, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(143, 17);
			this.label3.TabIndex = 51;
			this.label3.Text = "Black Omen";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BlackOmenStorySpin
			// 
			this.BlackOmenStorySpin.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.BlackOmenStorySpin.Enabled = false;
			this.BlackOmenStorySpin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.BlackOmenStorySpin.Hexadecimal = true;
			this.BlackOmenStorySpin.Location = new System.Drawing.Point(148, 23);
			this.BlackOmenStorySpin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.BlackOmenStorySpin.Name = "BlackOmenStorySpin";
			this.BlackOmenStorySpin.Size = new System.Drawing.Size(81, 17);
			this.BlackOmenStorySpin.TabIndex = 50;
			this.BlackOmenStorySpin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BlackOmenStorySpin.ValueChanged += new System.EventHandler(this.OnBlackOmen);
			// 
			// sLocationCat
			// 
			this.sLocationCat.BackColor = System.Drawing.SystemColors.Control;
			this.sLocationCat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.sLocationCat.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.sLocationCat.Location = new System.Drawing.Point(3, 3);
			this.sLocationCat.Name = "sLocationCat";
			this.sLocationCat.Size = new System.Drawing.Size(155, 17);
			this.sLocationCat.TabIndex = 49;
			this.sLocationCat.Text = "Storyline Points";
			this.sLocationCat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PluginForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(409, 82);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.BlackOmenStorySpin);
			this.Controls.Add(this.sLocationCat);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.HideOnClose = true;
			this.Name = "PluginForm";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.ShowInTaskbar = false;
			this.TabText = "PlugForm";
			this.Text = "PlugForm";
			((System.ComponentModel.ISupportInitialize) (this.BlackOmenStorySpin)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Label label3;
		public System.Windows.Forms.NumericUpDown BlackOmenStorySpin;
		private System.Windows.Forms.Label sLocationCat;
	}
}