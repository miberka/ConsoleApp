using System;
using Serilog;
using Serilog.Events;
using SeriLogTest.Helpers;
using ConsoleApp.Helpers;

namespace ConsoleApp
{
    class Program
    {

        private static string strAppName = "ConsoleApp";

        static void Main(string[] args)
        {
            Console.Title = strAppName;
            // Init commandline parameters
            var options = CommandLineHelper.InitCommands(args);
            if (options == null) return;
            if (!options.LastParserState) return;
            // Init logger
            Logger.InitLogger(logdir: options.strLogDir,
                logtofile: options.intLogToFile >= 0 ? true: false,
                separatefiles: options.boolSeparateFiles,
                separatedebugfile: options.boolSeparateDebugFile,
                filemaxlevel: options.intLogToFile >= 0 ? (LogEventLevel)options.intLogToFile : (LogEventLevel)6,
                consolemaxlevel: options.boolVerbose ? LogEventLevel.Verbose : LogEventLevel.Information);
            // Dump commandline parameters
            CommandLineHelper.DumpOptionsForDebug(options);
            // Init complete
            Log.Debug("Init complete!");
            // Lets do some work
            Log.Information("{0} at age {1}", options.strName, options.intAge);    
            // Finished
            Log.Debug("Done!");
        }

    }
}
