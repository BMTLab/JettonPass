using System;
using System.ComponentModel;
using System.Windows.Forms;

using JettonPass.App.Forms;


namespace JettonPass.App
{
    internal static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                Application.Exit(new CancelEventArgs(true));
                MessageBox.Show($@"FATAL: {(Exception) e.ExceptionObject}");
            };
        }


        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            using var mainForm = new MainForm();
            Application.Run(mainForm);

            Console.ReadKey();
        }
    }
}