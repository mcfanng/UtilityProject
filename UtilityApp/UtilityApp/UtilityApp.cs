using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UtilityApp.Interfaces;

namespace UtilityApp
{
    
   public class UtilityApp : AppBase
    {
        private IFileUtil _fileUtil;
        private ILogger<UtilityApp> _logger;
        private bool wantToExit;

        public UtilityApp(IFileUtil fileUtil, ILogger<UtilityApp> logger) {

            _fileUtil = fileUtil;
            _logger = logger;
            Menu = _menu;
            Title = _title;

        }

        internal void Run()
        {
            Console.WriteLine(_title);
            while (true)
            {
                Console.WriteLine(_menu);
                Console.Write(_beginingOfLineIndicator);
                var val = Console.ReadLine().ToUpper();

                switch (val)
                {
                    case "FILE":
                        _fileUtil.RunFileUtil();
                        break;
                    case "EXIT":
                        wantToExit = true;
                        break;
                    default:
                        break;
                }
                if (wantToExit) {
                    break;
                }
            }
            Console.Write("Press any key to exit...");
            Console.Read();
            Environment.Exit(0);
        }

        #region Graphic Elements
        private const string _title = @"
  _    _ _   _ _ _ _                                 __    __    __    __    __    __   
 | |  | | | (_) (_) |             /\                 \ \   \ \   \ \   \ \   \ \   \ \  
 | |  | | |_ _| |_| |_ _   _     /  \   _ __  _ __    \ \   \ \   \ \   \ \   \ \   \ \ 
 | |  | | __| | | | __| | | |   / /\ \ | '_ \| '_ \    > >   > >   > >   > >   > >   > >
 | |__| | |_| | | | |_| |_| |  / ____ \| |_) | |_) |  / /   / /   / /   / /   / /   / / 
  \____/ \__|_|_|_|\__|\__, | /_/    \_\ .__/| .__/  /_/   /_/   /_/   /_/   /_/   /_/  
                        __/ |          | |   | |                                        
                       |___/           |_|   |_|                                        
|";


        private const string _menu = @"
  __  __                  
 |  \/  | ___ _ __  _   _ ==============================================================================================================
 | |\/| |/ _ \ '_ \| | | |    Type one of the following:
 | |  | |  __/ | | | |_| |    'File' for File Options.
 |_|  |_|\___|_| |_|\__,_|    'Exit' to exit.
                          ==============================================================================================================";
        private const string _beginingOfLineIndicator = "> ";

        #endregion

    }
  
}
