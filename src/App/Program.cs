//#define TEST

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using JettonPass.App.Forms;
using JettonPass.App.Models.Options;
using JettonPass.App.Services.Configuration.Extensions;
using JettonPass.App.Services.Managers;
using JettonPass.App.Services.Receivers;
using JettonPass.App.Services.Receivers.Abstractions;
using JettonPass.SerialPortListener;
using JettonPass.SerialPortListener.Models.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProcessManager = JettonPass.App.Utils.AppUtils.ProcessManager;


namespace JettonPass.App
{
    internal static class Program
    {
        #region Ctor
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                MessageBox.Show($@"FATAL: {(Exception) e.ExceptionObject}");
                Application.Exit(new CancelEventArgs(true));
            };

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);


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
        #endregion _Ctor


        #region Fileds
        internal static readonly ServiceProvider ServiceProvider;
        internal static readonly IConfigurationRoot Configuration;
        #endregion


        #region Methods
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ShutdownExplorer();
            
            new Thread(() => Application.Run(ServiceProvider.GetService<AppsForm>()))
            {
                Name = nameof(AppsForm),
                IsBackground = false
            }.Start();
            
            new Thread(() => Application.Run(ServiceProvider.GetService<TimeForm>()))
            {
                Name = nameof(TimeForm),
                IsBackground = true
            }.Start();
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSectionOptions<TimeManagerOptions>(Configuration);
            services.AddSectionOptions<AppManagerOptions>(Configuration);
            services.AddSectionOptions<SerialPortReceiverOptions>(Configuration);
            services.AddSectionOptions<SerialPortOptions>(Configuration);
            
            
            services.AddSingleton(sp => new SerialPortSwitch(sp.GetRequiredService<IOptions<SerialPortOptions>>().Value));

            #if TEST
            services.AddSingleton<IJettonReceiver, TestReceiver>();
            #else
             services.AddSingleton<IJettonReceiver, SerialPortReceiver>();
            #endif
            services.AddSingleton<TimeManager>();
            services.AddSingleton<AppManager>();
            services.AddSingleton<AppsForm>();
            services.AddSingleton<TimeForm>();
        }


        [Conditional("RELEASE")]
        private static void ShutdownExplorer() =>
            ProcessManager.Shutdown("explorer.exe");
        #endregion _Methods
    }
}