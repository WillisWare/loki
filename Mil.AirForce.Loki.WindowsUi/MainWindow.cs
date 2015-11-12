using System;
using System.Windows.Forms;

namespace Mil.AirForce.Loki.WindowsUi
{
	/// <summary>
	/// Main form for the application user interface.
	/// </summary>
	public partial class MainWindow : Form
	{
		#region Constructors

		/// <summary>
		/// Default constructor. Initializes an instance of this form.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handlers

		protected void AboutMenuItem_Click(object sender, EventArgs e)
		{
			var aboutWindow = new About();

			aboutWindow.ShowDialog(this);
		}

		protected void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		protected void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Make sure nothing is left open.
		}

		#endregion
	}
}
