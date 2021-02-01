using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using JettonPass.App.Models.Apps;
using JettonPass.App.Utils.IconUtils;


namespace JettonPass.App.Utils.AppUtils
{
    public static class AppInfo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AppEntity FindApp(FileInfo file) =>
            new(
                file.Name.Split('.')[0],
                file.FullName,
                IconInfo.GetIcon(file.FullName)
            );
        

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<AppEntity> FindApps(DirectoryInfo baseDirectory, bool searchInSubfolders = true)
        {
            try
            {
                if (searchInSubfolders)
                {
                    return baseDirectory.GetDirectories()
                       .Select(directory => directory.GetFiles().FirstOrDefault(t => t.Extension.Equals(@".exe", StringComparison.CurrentCultureIgnoreCase)))
                       .Where(executableFile => executableFile is not null)
                       .Select(FindApp!);
                }

                return baseDirectory.GetFiles()
                   .Where(t => t.Extension.Equals(@".exe", StringComparison.CurrentCultureIgnoreCase))
                   .Select(FindApp!);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppEntity>();
            }
        }
    }
}