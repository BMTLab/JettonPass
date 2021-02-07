using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

using JettonPass.App.Services.Configuration.Extensions;
using JettonPass.App.Utils.AppUtils;
using JettonPass.Launcher.Models.Options;

using Microsoft.Extensions.Configuration;


namespace JettonPass.Launcher
{
    public static class Program
    {
        #region Fields
        internal static readonly IConfiguration Configuration;
        //internal static readonly ServiceProvider ServiceProvider;
        #endregion _Fields
        
        
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                MessageBox.Show($@"LAUNCHER FATAL: {(Exception) e.ExceptionObject}");
                ProcessUtils.Start("explorer.exe");
            };
            
            Configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"Properties"))
               .AddJsonFile(@"jettonPass.json", false, false)
                #if DEBUG
               .AddJsonFile(@"jettonPass.Debug.json", false, false)
                #endif
               .Build();
            
            GC.Collect();
        }


        #region Methods
        public static async Task Main()
        {
            var launcherOptions = Configuration.GetFrom<LauncherOptions>();
            
            if (launcherOptions.ShutdownExplorer)
                ShutdownExplorer();

            var process = ProcessUtils.Start(launcherOptions.AppPath);

            while (true)
            {
                await process!.WaitForExitAsync();
                process.Start();
            }
        }
        

        private static void ShutdownExplorer() =>
            ProcessUtils.Shutdown("explorer.exe");
        #endregion _Methods
    }
}