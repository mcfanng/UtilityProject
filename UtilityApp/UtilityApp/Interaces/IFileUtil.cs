
using UtilityApp.Models;

namespace UtilityApp.Interfaces
{
    public interface IFileUtil {
          void RunFileUtil();
        
        /// <summary>
        /// Finds a file(s) matching the pattern passed in.
        /// </summary>
        /// <param name="filenameOrPatternToSearchFor">The Filename or Pattern of the file to find.</param>
        /// <param name="searchPaths">The searchPaths to recursively look through.</param>
        /// <param name="searchRecursively">Wether or not to look through child directories of each directory.</param>
        /// <returns>Returns the paths of the file(s) to search for.</returns>
        string[] FindFile(string filenameOrPatternToSearchFor, bool searchRecursively = false, params string[] searchPaths);
        
        /// <summary>
        /// Finds a file(s) matching the pattern passed in.
        /// </summary>
        /// <param name="fileFindAndReplaceModel">The Model containing the search criteria.</param>
        /// <returns>Returns the path(s) of the file to search for.</returns>
        string[] FindFile(FileFindAndReplaceModel fileFindAndReplaceModel);

        /// <summary>
        /// Finds a folder matching the exact folderName passed in.
        /// </summary>
        /// <param name="folderName">The folderName of the folder to find.</param>
        /// <param name="searchPaths">The searchPaths to recursively look through.</param>
        /// <param name="searchRecursively">Wether or not to look through child directories of each directory.</param>
        /// <returns>Returns the path(s) of the folder to search for.</returns>
        string[] FindFolder(string folderName, bool searchRecursively = false, params string[] searchPaths);

        /// <summary>
        /// Finds a folder(s) matching the exact folderName passed in.
        /// </summary>
        /// <param name="fileFindAndReplaceModel">The Model containing the find and replace criteria.</param>
        /// <returns>Returns the path(s) of the folder to search for.</returns>
        string[] FindFolder(FileFindAndReplaceModel fileFindAndReplaceModel);

        /// <summary>
        /// Appends any files found in the given search paths with the suffix given.
        /// </summary>
        /// <param name="filenameOrPatternToSearchFor">The Filename or Pattern of the file to find.</param> 
        /// <param name="filenameSuffix">The string to add to the beginning of the filename.</param>
        /// <param name="overWriteExistingFiles">Whether or not to overwrite files with the newfilename.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void AppendFilenameOfFiles(string filenameOrPatternToSearchFor, string filenameSuffix, bool overWriteExistingFiles = true, bool searchRecursively = false, params string [] searchPaths);
        
        /// <summary>
        /// Appends a file(s) matching the pattern passed in.
        /// </summary>
        /// <param name="fileFindAndReplaceModel">The Model containing the find and replace criteria.</param>
        void AppendFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel);

        /// <summary>
        /// Prepends any files found in the given search paths with the prefix given.
        /// </summary>
        /// <param name="filenameOrPatternToSearchFor">The Filename or Pattern of the file to find.</param>
        /// <param name="filenamePrefix">The string to add to the beginning of the filename.</param>
        /// <param name="overWriteExistingFiles">Whether or not to overwrite files with the newfilename.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void PrependFilenameOfFiles(string filenameOrPatternToSearchFor, string filenamePrefix, bool overWriteExistingFiles = true, bool searchRecursively = false, params string[] searchPaths);
        
        /// <summary>
        /// Prepends a file(s) matching the pattern passed in.
        /// </summary>
        /// <param name="fileFindAndReplaceModel">The Model containing the find and replace criteria.</param>
        void PrependFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel);

        /// <summary>
        /// Changes Files found in the given search paths from the pattern to the new pattern.
        /// </summary>
        /// <param name="filenameOrPatternToSearchFor">The Filename or Pattern of the file to find.</param>
        /// <param name="changeFromPattern">The pattern in the filename to change.</param>
        /// <param name="changeToPattern">The pattern to change the filename to.</param>
        /// <param name="changeFromPatternIsRegex">Whether or not the changeFromPattern is a Regular Expression.</param>
        /// <param name="overWriteExistingFiles">Whether or not to overwrite files with the newfilename.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void AlterFilenameOfFiles(string filenameOrPatternToSearchFor, string changeFromPattern, string changeToPattern, bool changeFromPatternIsRegex = false, bool overWriteExistingFiles = true, bool searchRecursively = false, params string[] searchPaths);
        
        /// <summary>
        /// Alters a file(s) matching the pattern passed in.
        /// </summary>
        /// <param name="fileFindAndReplaceModel">The Model containing the find and replace criteria.</param>
        void AlterFilenameOfFiles(FileFindAndReplaceModel fileFindAndReplaceModel);

    }
}