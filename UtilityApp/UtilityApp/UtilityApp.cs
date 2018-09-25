using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UtilityApp.Interfaces;

namespace UtilityApp
{
    
   public class UtilityApp
    {
        private IFileUtil _fileUtil;
        private ILogger<UtilityApp> _logger;

        public UtilityApp(IFileUtil fileUtil, ILogger<UtilityApp> logger) {

            _fileUtil = fileUtil;
            _logger = logger;

        }

        internal void Run()
        {
            throw new NotImplementedException();
        }
    }
}
