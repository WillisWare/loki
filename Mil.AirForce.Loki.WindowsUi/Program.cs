using System;
using System.Windows.Forms;

namespace Mil.AirForce.Loki.WindowsUi
{
	/// <summary>
	/// Main entry point for the application.
	/// </summary>
	static class Program
	{
		#region Methods

		/// <summary>
		/// Main entry point for the application.
		/// </summary>
		/// <param name="args">An array of String values containing the command-line arguments for the application.</param>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		#endregion
	}
}
