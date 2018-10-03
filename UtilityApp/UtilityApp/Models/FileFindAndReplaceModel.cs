using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityApp.Models
{
    /// <summary>
    /// Contains the properties that contain the search criteria.
    /// </summary>
    public class FileFindAndReplaceModel
    {
        public string[] PathsToSearchThrough { get; set; }
        public string PatternToSearchFor { get; set; }
        /// <summary>
        /// If set to true we want folders. If not we want files (default).
        /// </summary>
        public bool SearchForFolders { get; set; }
        public bool SearchRecursively { get; set; }
        public bool OverWriteExistingFiles { get; set; }
        public string PrefixToPrepend { get; set; }
        public string SuffixToAppend { get; set; }
        public string PatternToBeUsedToAlter { get; set; }
        public bool PatternToBeUseToAlterIsRegex { get; set; }
    }
}
