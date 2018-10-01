using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityApp.Models
{
    /// <summary>
    /// Contains the properties that contain the search criteria.
    /// </summary>
    public class SearchModel
    {
        public string[] PathsToSearchThrough { get; set; }
        public string NameToSearchFor { get; set; }
        /// <summary>
        /// If set to true we want folders. If not we want files (default).
        /// </summary>
        public bool SearchForFolders { get; set; }
    }
}
