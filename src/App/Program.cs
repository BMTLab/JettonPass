#define TEST

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using JettonPass.App.Models.Options;
using JettonPass.App.Overlays;
using JettonPass.App.Services.Configuration.Extensions;
using JettonPass.App.Services.Managers;
using JettonPass.App.Services.Receivers;
using JettonPass.App.Services.Receivers.Abstractions;
using JettonPass.App.Utils.AppUtils;
using JettonPass.App.Utils.WindowUtils;
using JettonPass.SerialPortListener;
using JettonPass.SerialPortListener.Models.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Process.NET;
using Process.NET.Memory;

using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;


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
        internal static readonly IConfiguration Configuration;
        #endregion


        #region Methods
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static async Task Main()
        {
            var appManager = ServiceProvider.GetRequiredService<AppManager>();
            appManager.RunApp();

            var overlayManager = ServiceProvider.GetRequiredService<OverlayManager>();
            await overlayManager.InitializeAsync();
            
            CursorUtils.ToggleMouseCursorVisibility();

            await WaitForExitAsync();
            overlayManager.Dispose();
            appManager.Dispose();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSectionOptions<TimeManagerOptions>(Configuration);
            services.AddSectionOptions<AppManagerOptions>(Configuration);
            services.AddSectionOptions<SerialPortReceiverOptions>(Configuration);
            services.AddSectionOptions<SerialPortOptions>(Configuration);
            
            services.AddSingleton(sp => new SerialPortSwitch(sp.GetRequiredService<IOptions<SerialPortOptions>>().Value));

            #if DEBUG || TEST
            if (!SerialPortSwitch.IsAnyPortsOnSystem())
                services.AddSingleton<IJettonReceiver, TestReceiver>();
            else
                services.AddSingleton<IJettonReceiver, SerialPortReceiver>();
            #else
             services.AddSingleton<IJettonReceiver, SerialPortReceiver>();
            #endif
            services.AddSingleton<TimeManager>();
            services.AddSingleton<AppManager>();
            services.AddSingleton<OverlayManager>();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async ValueTask WaitForExitAsync()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            await currentProcess.WaitForExitAsync();
        }
        #endregion _Methods
    }
}