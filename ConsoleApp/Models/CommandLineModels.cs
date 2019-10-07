using System;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace ConsoleApp.CommandLineModels
{
    [Verb("ConsoleApp", HelpText = "Testing console app to join name and age.")]
    class AppOptions
    {
        // Custom parametrs

        [Option('n', "name", Required = true, HelpText = "User name.")]
        public string strName { get; set; }

        [Option('a', "age", Required = false, HelpText = "Age of the user.", Default = 35)]
        public int intAge { get; set; }

        // Serilog paramaters

        [Option('w', "log-to-file", Required = false, HelpText = "Enable logging to file with defined max log level. (Disabled: -1, Verbose: 0, Debug: 1, Informational: 2, Debug: 3, Warning: 4, Error: 5, Fatal: 6)", Default = -1)]
        public int intLogToFile { get; set; }

        [Option('y', "log-to-dir", Required = false, HelpText = "Path to store log files.")]
        public string strLogDir { get; set; }

        [Option('x', "separate-logs", Required = false, HelpText = "Saparate log files by verbosity.", Default = false)]
        public bool boolSeparateFiles { get; set; }

        [Option('z', "separate-debug-log", Required = false, HelpText = "Store debug log to separate file.", Default = false)]
        public bool boolSeparateDebugFile { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set console output to verbose.")]
        public bool boolVerbose { get; set; }

        // CommandLineParser helper parameter
        public bool LastParserState { get; set; }

        // CommandLineParser example
        [Usage(ApplicationAlias = "")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                        new Example("Test application to demonstrate parameter function", new AppOptions {
                            strName = "John Connor",
                            intAge = 35
                        })
                    };
            }
        }

    }

    
}
