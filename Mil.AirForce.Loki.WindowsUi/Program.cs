using System;
using System.Windows.Forms;

namespace Mil.AirForce.Loki.WindowsUi
{
	/// <summary>
	/// Main entry point for the application.
	/// </summary>
   static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
			      Application.EnableVisualStyles();
      			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
