using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using JettonPass.App.Models.Apps;
using JettonPass.App.Models.Options;
using JettonPass.App.Utils.AppUtils;

using Microsoft.Extensions.Options;


namespace JettonPass.App.Services.Managers
{
    public sealed class AppManager
    {
        #region Fields
        private readonly AppManagerOptions _options;
        #endregion _Fields


        #region Ctors
        public AppManager(IOptions<AppManagerOptions> options)
        {
            _options = options.Value;
        }
        #endregion


        #region Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IEnumerable<AppEntity> LoadApps()
        {
            var collection = new List<AppEntity>(8);
            
            if (_options.UseBasePath && !string.IsNullOrWhiteSpace(_options.BasePath))
            {
                collection.AddRange(AppInfo.FindApps(new DirectoryInfo(_options.BasePath), _options.SearchInSubfolders));
            }

            if (_options.CustomPaths is not null && _options.CustomPaths.Count > 0)
            {
                collection.AddRange(_options.CustomPaths.Select(path => AppInfo.FindApp(new FileInfo(path))));
            }

            return collection;
        }
        #endregion _Methods
    }
}