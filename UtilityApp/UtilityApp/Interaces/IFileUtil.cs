
namespace UtilityApp.Interfaces
{
    public interface IFileUtil {
        /// <summary>
        /// Finds a file matching the exact Filename passed in.
        /// </summary>
        /// <param name="filenameToSearchFor">The Filename of the file to find.</param>
        /// <param name="searchPaths">The searchPaths to recursively look through.</param>
        /// <returns>Returns the paths of the file to search for.</returns>
        string [] FindFile(string filenameToSearchFor, params string[] searchPaths);

        /// <summary>
        /// Finds a folder matching the exact folderName passed in.
        /// </summary>
        /// <param name="folderName">The folderName of the folder to find.</param>
        /// <param name="searchPaths">The searchPaths to recursively look through.</param>
        /// <returns>Returns the path of the folder to search for.</returns>
        string FindFolder(string folderName, params string[] searchPaths);

        /// <summary>
        /// Finds files matching the FilenamePattern passed in.
        /// </summary>
        /// <param name="FilenamePattern">The pattern of the Filename to look for.</param>
        /// <param name="searchPaths">The searchPaths to recursively look through.</param>
        /// <returns>Returns the paths of files that match the pattern.</returns>
        string[] FindFiles(string FilenamePattern, params string[] searchPaths);

        /// <summary>
        /// Appends any files found in the given search paths with the suffix given.
        /// </summary>
        /// <param name="filenameSuffix">The string to add to the beginning of the filename.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void AppendFilenameOfFiles(string filenameSuffix, bool searchRecursively = false, params string [] searchPaths);

        /// <summary>
        /// Prepends any files found in the given search paths with the prefix given.
        /// </summary>
        /// <param name="filenamePrefix">The string to add to the beginning of the filename.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void PrependFilenameOfFiles(string filenamePrefix, bool searchRecursively = false, params string[] searchPaths);

        /// <summary>
        /// Changes Files found in the given search paths from the pattern to the new pattern.
        /// </summary>
        /// <param name="changeFromPattern">The pattern in the filename to change.</param>
        /// <param name="changeToPattern">The pattern to change the filename to.</param>
        /// <param name="searchRecursively">Whether or not to append files in child folders or just the root given.</param>
        /// <param name="searchPaths">The searchPaths to look through.</param>
        void AlterFilenameOfFiles(string changeFromPattern, string changeToPattern, bool searchRecursively = false, params string[] searchPaths);

    }
}