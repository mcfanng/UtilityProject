
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UtilityApp.Interfaces;
using UtilityApp.Models;

namespace UtilityApp.FileUtility
{
    public class FileUtil : AppBase, IFileUtil
    {

        private ILogger<FileUtil> _logger;

        public FileUtil(ILogger<FileUtil> logger)
        {
            _logger = logger;
            Menu = _menu;
            Title = _title;
        }

        public void AlterFilenameOfFiles(string changeFromPattern, string changeToPattern, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public void AppendFilenameOfFiles(string filenameSuffix, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public string[] FindFile(string filenameOrPatternToSearchFor, params string[] searchPaths)
        {
            string path = "";
            List<string> paths = new List<string>();
            foreach (var item in searchPaths)
            {

                DirectoryInfo info = new DirectoryInfo(item);
                if (!info.Exists)
                {
                    _logger.LogInformation($"{item}: Directory doesn't exist.");
                    continue;
                }
                var childDirectories = info.EnumerateDirectories();
                if (childDirectories.Count() > 0)
                {

                    paths.AddRange(FindFile(filenameOrPatternToSearchFor, childDirectories.Select(m => m.FullName).ToArray()));
                }
                else
                {
                    paths.AddRange(info.EnumerateFiles(filenameOrPatternToSearchFor).Select(m => m.FullName));
                }
            }

            return paths.ToArray();

        }
        public string[] FindFile(SearchModel searchModel)
        {
            return FindFile(searchModel.NameToSearchFor, searchModel.PathsToSearchThrough);
        }
        public string[] FindFolder(string folderName, params string[] searchPaths)
        {
            string path = "";
            List<string> paths = new List<string>();
            foreach (var item in searchPaths)
            {

                DirectoryInfo info = new DirectoryInfo(item);
                if (!info.Exists)
                {
                    _logger.LogInformation($"{item}: Directory doesn't exist.");
                    continue;
                }
                var childDirectories = info.EnumerateDirectories();
                if (childDirectories.Count() > 0)
                {

                    paths.AddRange(FindFile(folderName, childDirectories.Select(m => m.FullName).ToArray()));
                }
                else
                {
                    paths.AddRange(info.Parent.EnumerateDirectories(folderName).Select(m => m.FullName));
                }
            }
            return paths.ToArray();
        }
        public string[] FindFolder(SearchModel searchModel)
        {
            return FindFolder(searchModel.NameToSearchFor, searchModel.PathsToSearchThrough);
        }
        public void PrependFilenameOfFiles(string filenamePrefix, bool searchRecursively = false, params string[] searchPaths)
        {
            throw new System.NotImplementedException();
        }

        public void RunFileUtil()
        {
            Console.WriteLine(_title);
            while (true)
            {
                Console.WriteLine(_menu);
                Console.Write(BeginingOfLineIndicator);
                var val = Console.ReadLine().ToUpper();
                SearchModel searchModel = new SearchModel();
                switch (val)
                {
                    case "1":
                        SetupPathsToSearchThrough(searchModel);
                        var filePaths = FindFile(searchModel);
                        _logger.LogInformation("Files Found : ", filePaths.ToArray());
                        break;
                    case "2":
                        searchModel.SearchForFolders = true;
                        SetupPathsToSearchThrough(searchModel);
                        var folderPaths = FindFolder(searchModel);
                        _logger.LogInformation("Folders Found : ", folderPaths.ToArray());
                        break;
                    case "3":
                        SetupPathsToSearchThrough(searchModel);
                        
                        break;
                    case "4":
                        SetupPathsToSearchThrough(searchModel);
                        break;
                    case "5":
                        SetupPathsToSearchThrough(searchModel);
                        break;
                    case "E":
                        GoBack = true;
                        break;
                    default:
                        break;
                }
                if (GoBack) { GoBack = false; break; }
            }
        }



        private void SetupPathsToSearchThrough(SearchModel searchModel)
        {
            Console.Write($"Enter " + (searchModel.SearchForFolders ? "Folder name" : "File name or pattern" + " to search for: ") + BeginingOfLineIndicator);
            searchModel.NameToSearchFor = Console.ReadLine();
            Console.Write("Enter Absolute File path(s) to search through, delimited by | .");
            searchModel.PathsToSearchThrough = Console.ReadLine().Split('|');
        }

        private const string _title = @"
  ______  _  _          ____          _    _                    
 |  ____|(_)| |        / __ \        | |  (_)                   
 | |__    _ | |  ___  | |  | | _ __  | |_  _   ___   _ __   ___ 
 |  __|  | || | / _ \ | |  | || '_ \ | __|| | / _ \ | '_ \ / __|
 | |     | || ||  __/ | |__| || |_) || |_ | || (_) || | | |\__ \
 |_|     |_||_| \___|  \____/ | .__/  \__||_| \___/ |_| |_||___/
                              | |                               
                              |_|
(File Options)";
        private const string _menu = @"
||***** MENU *****\|/->
|| 1: Find File(s)
|| 2: Find Folder(s)
|| 3: Append Filename(s)
|| 4: Prepend Filename(s)
|| 5: Alter FileName(s)
|| E: Go Back to Main Menu.
";
    }


}