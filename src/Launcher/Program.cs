using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using JettonPass.App.Services.Configuration.Extensions;
using JettonPass.App.Utils.AppUtils;
using JettonPass.Launcher.Models.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JettonPass.Launcher
{
    public static class Program
    {
        #region Fields
        internal static readonly IConfiguration Configuration;
        internal static readonly ServiceProvider ServiceProvider;
        #endregion _Fields
        
        
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                MessageBox.Show($@"LAUNCHER FATAL: {(Exception) e.ExceptionObject}");
                ProcessManager.Start("explorer.exe");
                Application.Exit(new CancelEventArgs(true));
            };
            
            Configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"Properties"))
               .AddJsonFile(@"jettonPass.json", false, false)
                #if DEBUG
               .AddJsonFile(@"jettonPass.Debug.json", false, false)
                #endif
               .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            GC.Collect();
        }


        #region Methods
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSectionOptions<LauncherOptions>(Configuration);
        }
        
        public static async Task Main()
        {
            var launcherOptions = new LauncherOptions();
            Configuration.GetSection(nameof(LauncherOptions)).Bind(launcherOptions);
            
            if (launcherOptions.ShutdownExplorer)
                ShutdownExplorer();

            var process = ProcessManager.Start(launcherOptions.AppPath);

            while (true)
            {
                await process!.WaitForExitAsync();
                process.Start();
            }
        }
        

        private static void ShutdownExplorer() =>
            ProcessManager.Shutdown("explorer.exe");
        #endregion _Methods
    }
}