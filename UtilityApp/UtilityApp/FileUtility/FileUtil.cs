
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        public void RunFileUtil()
        {
            Console.WriteLine(_title);
            while (true)
            {
                Console.WriteLine(_menu);
                Console.Write(BeginingOfLineIndicator);
                var val = Console.ReadLine().ToUpper();
                FileFindAndReplaceModel searchModel = new FileFindAndReplaceModel();
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

        public void AlterFilenameOfFiles(string filenameOrPatternToSearchFor, string changeFromPattern, string changeToPattern, bool changeFromPatternIsRegex = false, bool overWriteExistingFiles = true, bool searchRecursively = false, params string[] searchPaths)
        {
            Regex regex = new Regex(changeFromPattern);
            var paths = FindFile(new FileFindAndReplaceModel() { PathsToSearchThrough = searchPaths, PatternToSearchFor = filenameOrPatternToSearchFor, SearchForFolders = false, SearchRecursively = searchRecursively });
            foreach (var path in paths)
            {
                var file = new FileInfo(path);
                var directoryOfFile = file.Directory;
                var oldFilename = $"{file.Name}.{file.Extension}";
                var match = regex.Match(file.Name);

                if (!match.Success) {
                    _logger.LogError($"Couldn't match the regex of: {changeFromPattern} on filename{file.Name}");
                    continue;
                }

                var newFilename = $"{regex.Replace(oldFilename,changeToPattern)}.{file.Extension}"; 
                ChangeFileName(overWriteExistingFiles, directoryOfFile, oldFilename, newFilename);
            }
        }

        public void AppendFilenameOfFiles(string filenameOrPatternToSearchFor, string filenameSuffix, bool overWriteExistingFiles = true, bool searchRecursively = false, params string[] searchPaths)
        {
            var paths = FindFile(new FileFindAndReplaceModel() { PathsToSearchThrough = searchPaths, PatternToSearchFor = filenameOrPatternToSearchFor, SearchForFolders = false, SearchRecursively = searchRecursively });

            foreach (var path in paths)
            {
                var file = new FileInfo(path);
                var directoryOfFile = file.Directory;
                var oldFilename = $"{file.Name}.{file.Extension}";
                var newFilename = $"{oldFilename}{filenameSuffix}";
                ChangeFileName(overWriteExistingFiles, directoryOfFile, oldFilename, newFilename);

            }
        }

        public void PrependFilenameOfFiles(string filenameOrPatternToSearchFor, string filenamePrefix, bool overWriteExistingFiles = true, bool searchRecursively = false, params string[] searchPaths)
        {
            var paths = FindFile(new FileFindAndReplaceModel() { PathsToSearchThrough = searchPaths, PatternToSearchFor = filenameOrPatternToSearchFor, SearchForFolders = false, SearchRecursively = searchRecursively });

            foreach (var path in paths)
            {
                var file = new FileInfo(path);
                var directoryOfFile = file.Directory;
                var oldFilename = $"{file.Name}.{file.Extension}";
                var newFilename =$"{filenamePrefix}{oldFilename}";

                ChangeFileName(overWriteExistingFiles, directoryOfFile, oldFilename, newFilename);

            }
        }

        private void ChangeFileName(bool overWriteExistingFiles, DirectoryInfo directoryOfFile, string oldFilename, string newFilename)
        {
            if (File.Exists(Path.Combine(directoryOfFile.FullName, oldFilename)))
            {
                if (File.Exists(Path.Combine(directoryOfFile.FullName, newFilename)))
                {
                    _logger.LogWarning($"{Path.Combine(directoryOfFile.FullName, newFilename)} : File already exists.");
                    if (overWriteExistingFiles)
                    {
                        _logger.LogInformation($"Overwriting existing file :{Path.Combine(directoryOfFile.FullName, oldFilename)} with {Path.Combine(directoryOfFile.FullName, newFilename)}.");
                        File.Move(Path.Combine(directoryOfFile.FullName, oldFilename), Path.Combine(directoryOfFile.FullName, newFilename));
                    }
                    else
                    {
                        _logger.LogInformation($"Skipping rename of :{Path.Combine(directoryOfFile.FullName, oldFilename)} to {Path.Combine(directoryOfFile.FullName, newFilename)}.");
                    }
                }
                else
                {
                    _logger.LogInformation($"Renaming {Path.Combine(directoryOfFile.FullName, oldFilename)} to {Path.Combine(directoryOfFile.FullName, newFilename)}");
                    File.Move(Path.Combine(directoryOfFile.FullName, oldFilename), Path.Combine(directoryOfFile.FullName, newFilename));
                }
            }
            else
            {
                _logger.LogError($"{Path.Combine(directoryOfFile.FullName, oldFilename)} : File doesn't exist or is inaccessible.");
                _logger.LogError($"Can't rename {Path.Combine(directoryOfFile.FullName, oldFilename)} to {Path.Combine(directoryOfFile.FullName, newFilename)}");
            }
        }

        public string[] FindFile(string filenameOrPatternToSearchFor, bool searchRecursively = false, params string[] searchPaths)
        {
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
                if (childDirectories.Count() > 0 && searchRecursively)
                {

                    paths.AddRange(FindFile(filenameOrPatternToSearchFor, searchRecursively, childDirectories.Select(m => m.FullName).ToArray()));
                }
                else
                {
                    paths.AddRange(info.EnumerateFiles(filenameOrPatternToSearchFor).Select(m => m.FullName));
                }
            }

            return paths.ToArray();

        }

        public string[] FindFile(FileFindAndReplaceModel searchModel)
        {
            return FindFile(searchModel.PatternToSearchFor, searchModel.SearchRecursively, searchModel.PathsToSearchThrough);
        }

        public string[] FindFolder(string folderName, bool searchRecursively = false, params string[] searchPaths)
        {
            List<string> paths = new List<string>();
            foreach (var item in searchPaths)
            {

                DirectoryInfo info = new DirectoryInfo(item);
                if (!info.Exists)
                {
                    _logger.LogInformation($"{item}: Directory doesn't exist or it is inaccesible.");
                    continue;
                }
                var childDirectories = info.EnumerateDirectories();
                if (childDirectories.Count() > 0 && searchRecursively)
                {

                    paths.AddRange(FindFile(folderName, searchRecursively, childDirectories.Select(m => m.FullName).ToArray()));
                }
                else
                {
                    //If the directory is the root then there isn't a parent directory.
                    if (info.Root.Name == info.Name)
                    {
                        paths.AddRange(info.EnumerateDirectories(folderName).Select(m => m.FullName));
                    }
                    else
                    {
                        paths.AddRange(info.Parent.EnumerateDirectories(folderName).Select(m => m.FullName));
                    }
                }
            }
            return paths.Distinct().ToArray();
        }

        public string[] FindFolder(FileFindAndReplaceModel searchModel)
        {
            return FindFolder(searchModel.PatternToSearchFor, searchModel.SearchRecursively, searchModel.PathsToSearchThrough);
        }

        private void SetupPathsToSearchThrough(FileFindAndReplaceModel searchModel)
        {
            Console.Write($"Enter " + (searchModel.SearchForFolders ? "Folder name" : "File name or pattern" + " to search for: ") + BeginingOfLineIndicator);
            searchModel.PatternToSearchFor = Console.ReadLine();
            Console.Write("Enter Absolute File path(s) to search through, delimited by | .");
            searchModel.PathsToSearchThrough = Console.ReadLine().Split('|');
        }

        public void AppendFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel)
        {
            AppendFilenameOfFiles(fileFindAndReplaceModel.PatternToSearchFor, fileFindAndReplaceModel.SuffixToAppend, fileFindAndReplaceModel.OverWriteExistingFiles, fileFindAndReplaceModel.SearchRecursively, fileFindAndReplaceModel.PathsToSearchThrough);
        }

        public void PrependFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel)
        {
            PrependFilenameOfFiles(fileFindAndReplaceModel.PatternToSearchFor, fileFindAndReplaceModel.SuffixToAppend, fileFindAndReplaceModel.OverWriteExistingFiles, fileFindAndReplaceModel.SearchRecursively, fileFindAndReplaceModel.PathsToSearchThrough);

        }

        public void AlterFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel)
        {
            throw new NotImplementedException();
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