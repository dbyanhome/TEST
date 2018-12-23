using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AbpProject.Core
{
    public static class AppConfigurer
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static AppConfigurer()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot GetConfigurationRoot(string path)
        {
            var cahceKey = $"{path}#";
            return _configurationCache.GetOrAdd(cahceKey, BuildConfiguratonRoot(path));
        }

        public static string FindWebContentFolder()
        {
            bool DirectoryIsExistSolutionFile(string dirPath, string fileName)
            {
                return Directory.GetFiles(dirPath).Any(filePath => Path.GetFileName(filePath) == fileName);
            }

            var coreAssemblyDirPath = Path.GetDirectoryName(typeof(AbpProjectCoreModule).Assembly.Location);
            if (coreAssemblyDirPath == null) throw new Exception("无法找到 Core 项目的目录!");

            var directoryInfo = new DirectoryInfo(coreAssemblyDirPath);
            while (!DirectoryIsExistSolutionFile(directoryInfo.FullName, "AbpProjectTest02.sln"))
            {
                if (directoryInfo.Parent == null) throw new Exception("无法找到解决方案目录!");
                directoryInfo = directoryInfo.Parent;
            }

            var webHostDirPath = Path.Combine(directoryInfo.FullName, "AbpWebProject.Host");
            if (!Directory.Exists(webHostDirPath)) throw new Exception("Web 项目的目录不存在!");
            return webHostDirPath;
        }

        private static IConfigurationRoot BuildConfiguratonRoot(string path) => new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json").Build();
    }
}
