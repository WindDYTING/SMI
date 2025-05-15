using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace SMI.Example.Winform {
    internal class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>(true)
                .AddJsonFile("appsettings.json")
                .Build();

            Application.Run(new MainForm(config));
        }
    }
}
