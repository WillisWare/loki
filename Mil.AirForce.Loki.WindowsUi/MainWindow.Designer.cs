namespace Mil.AirForce.Loki.WindowsUi
{
	partial class MainWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.MainStatus = new System.Windows.Forms.StatusStrip();
			this.SuspendLayout();
			// 
			// MainStatus
			// 
			this.MainStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
			this.MainStatus.Location = new System.Drawing.Point(0, 351);
			this.MainStatus.Name = "MainStatus";
			this.MainStatus.Size = new System.Drawing.Size(565, 22);
			this.MainStatus.TabIndex = 0;
			this.MainStatus.Text = "Status";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 373);
			this.Controls.Add(this.MainStatus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "USAF - LOKI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.StatusStrip MainStatus;
	}
}