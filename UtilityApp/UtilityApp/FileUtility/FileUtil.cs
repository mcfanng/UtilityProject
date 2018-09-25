
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UtilityApp.Interfaces;

namespace UtilityApp.FileUtility
{
    public class FileUtil : IFileUtil {

        private ILogger<FileUtil> _logger;
        public FileUtil(ILogger<FileUtil> logger) {
            _logger = logger;
        }

        public void AlterFilenameOfFiles(string changeFromPattern, string changeToPattern, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public void AppendFilenameOfFiles(string filenameSuffix, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public string [] FindFile(string filenameToSearchFor, params string[] searchPaths)
        {
            string path = "";
            List<string> paths = new List<string>();
            foreach (var item in searchPaths)
            {

                DirectoryInfo info = new DirectoryInfo(item);
                if (!info.Exists) {
                    _logger.LogInformation($"{item}: Directory doesn't exist.");
                    continue;
                }
                var childDirectories =  info.EnumerateDirectories();
                if (childDirectories.Count() > 1)
                {


                }
                else
                {
                  paths.AddRange(info.EnumerateFiles(filenameToSearchFor).Select( m => m.FullName));
                }
            }

            return paths.ToArray();

        }

        public string[] FindFiles(string FilenamePattern, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public string FindFolder(string folderName, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public void PrependFilenameOfFiles(string filenamePrefix, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }
    }

}