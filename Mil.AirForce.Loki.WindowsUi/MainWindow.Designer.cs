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
			this.ApplicationStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.ApplicationVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.ApplicationConnectivity = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanSingle = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanAuto = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainStatus.SuspendLayout();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainStatus
			// 
			this.MainStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
			this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationStatus,
            this.ApplicationVersion,
            this.ApplicationConnectivity});
			this.MainStatus.Location = new System.Drawing.Point(0, 495);
			this.MainStatus.Name = "MainStatus";
			this.MainStatus.Size = new System.Drawing.Size(806, 22);
			this.MainStatus.TabIndex = 0;
			this.MainStatus.Text = "Status";
			// 
			// ApplicationStatus
			// 
			this.ApplicationStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.ApplicationStatus.AutoSize = false;
			this.ApplicationStatus.Name = "ApplicationStatus";
			this.ApplicationStatus.Size = new System.Drawing.Size(200, 17);
			this.ApplicationStatus.Text = "Ready";
			this.ApplicationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ApplicationVersion
			// 
			this.ApplicationVersion.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.ApplicationVersion.AutoSize = false;
			this.ApplicationVersion.Name = "ApplicationVersion";
			this.ApplicationVersion.Size = new System.Drawing.Size(574, 17);
			this.ApplicationVersion.Spring = true;
			this.ApplicationVersion.Text = "Version 1.0.0.0";
			// 
			// ApplicationConnectivity
			// 
			this.ApplicationConnectivity.AccessibleRole = System.Windows.Forms.AccessibleRole.Indicator;
			this.ApplicationConnectivity.AutoSize = false;
			this.ApplicationConnectivity.BackColor = System.Drawing.Color.Red;
			this.ApplicationConnectivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.ApplicationConnectivity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ApplicationConnectivity.IsLink = true;
			this.ApplicationConnectivity.Name = "ApplicationConnectivity";
			this.ApplicationConnectivity.Size = new System.Drawing.Size(17, 17);
			this.ApplicationConnectivity.ToolTipText = "Disconnected";
			// 
			// MainMenu
			// 
			this.MainMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ScanMenu,
            this.HelpMenu});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(806, 24);
			this.MainMenu.TabIndex = 1;
			this.MainMenu.Text = "Main";
			// 
			// FileMenu
			// 
			this.FileMenu.AccessibleName = "File Menu";
			this.FileMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "&File";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.AccessibleName = "Exit";
			this.ExitMenuItem.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.ExitMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ExitMenuItem.Text = "E&xit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// ScanMenu
			// 
			this.ScanMenu.AccessibleName = "Scan Menu";
			this.ScanMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
			this.ScanMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScanSingle,
            this.ScanAuto});
			this.ScanMenu.Name = "ScanMenu";
			this.ScanMenu.Size = new System.Drawing.Size(44, 20);
			this.ScanMenu.Text = "&Scan";
			// 
			// ScanSingle
			// 
			this.ScanSingle.AccessibleName = "Single Entry";
			this.ScanSingle.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
			this.ScanSingle.Name = "ScanSingle";
			this.ScanSingle.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.ScanSingle.Size = new System.Drawing.Size(155, 22);
			this.ScanSingle.Text = "Single &Entry";
			// 
			// ScanAuto
			// 
			this.ScanAuto.AccessibleName = "Autoscan";
			this.ScanAuto.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
			this.ScanAuto.Name = "ScanAuto";
			this.ScanAuto.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.ScanAuto.Size = new System.Drawing.Size(155, 22);
			this.ScanAuto.Text = "&Autoscan";
			// 
			// HelpMenu
			// 
			this.HelpMenu.AccessibleName = "Help Menu";
			this.HelpMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
			this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
			this.HelpMenu.Name = "HelpMenu";
			this.HelpMenu.Size = new System.Drawing.Size(44, 20);
			this.HelpMenu.Text = "&Help";
			// 
			// AboutMenuItem
			// 
			this.AboutMenuItem.AccessibleName = "About";
			this.AboutMenuItem.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
			this.AboutMenuItem.Name = "AboutMenuItem";
			this.AboutMenuItem.Size = new System.Drawing.Size(152, 22);
			this.AboutMenuItem.Text = "&About";
			this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(806, 517);
			this.Controls.Add(this.MainStatus);
			this.Controls.Add(this.MainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "USAF - LOKI";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.MainStatus.ResumeLayout(false);
			this.MainStatus.PerformLayout();
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.StatusStrip MainStatus;
		protected System.Windows.Forms.ToolStripStatusLabel ApplicationStatus;
		protected System.Windows.Forms.ToolStripStatusLabel ApplicationVersion;
		protected System.Windows.Forms.ToolStripStatusLabel ApplicationConnectivity;
		protected System.Windows.Forms.MenuStrip MainMenu;
		protected System.Windows.Forms.ToolStripMenuItem FileMenu;
		protected System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		protected System.Windows.Forms.ToolStripMenuItem ScanMenu;
		protected System.Windows.Forms.ToolStripMenuItem ScanSingle;
		protected System.Windows.Forms.ToolStripMenuItem ScanAuto;
		protected System.Windows.Forms.ToolStripMenuItem HelpMenu;
		protected System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
	}
}