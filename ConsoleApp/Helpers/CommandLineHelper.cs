using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using ConsoleApp.CommandLineModels;
using Serilog;

namespace ConsoleApp.Helpers
{
    class CommandLineHelper
    {

        /// <summary>
        /// 
        /// Logger helper
        /// 
        /// Install packages in Package Manager Console:
        ///     Install-Package CommandLineParser 
        /// 
        /// In Program.cs include:
        ///     using ConsoleApp.Helpers;
        /// 
        /// Than init commandline parameters:
        ///     var options = CommandLineHelper.InitCommands(args);
        ///     
        /// </summary>

        public static AppOptions InitCommands(string[] args)
        {
            if (args.Length == 0)
            {
                Parser.Default.ParseArguments<AppOptions>(new[] { "--help" });
                return null;
            }
            var options = new AppOptions{ LastParserState = false };
            Parser.Default.ParseArguments<AppOptions>(args)
                .WithParsed<AppOptions>(opts => RunOptionsAndReturnExitCode(opts))
                .WithParsed<AppOptions>(opts => options = opts)
                .WithNotParsed<AppOptions>((errs) => HandleParseError(errs));
            return options;
        }

        static int RunOptionsAndReturnExitCode(AppOptions options)
        {
            options.LastParserState = true;
            return 0;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //foreach (var err in errs)
            //    Console.WriteLine("{0}", err);

            //if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
            //    result = -1;
        }

        public static void DumpOptionsForDebug(Object o)
        {
            Log.Debug("--- Commandline options dump starting... -----------------");
            System.Type type = o.GetType();
            System.Reflection.PropertyInfo[] pi = type.GetProperties();
            if (pi.Length > 0)
            {
                foreach (System.Reflection.PropertyInfo p in pi)
                {
                    if (p.GetCustomAttributes(typeof(OptionAttribute), false).Length > 0)
                    {
                        var option = p.GetCustomAttributes(typeof(OptionAttribute), false).Cast<OptionAttribute>().FirstOrDefault();
                        Log.Debug("{0}, {1}: {2}", option.ShortName.ToString(), option.LongName.ToString(), p.GetValue(o, null));
                    }
                }
            }
            Log.Debug("--- Commandline options dump done! -----------------------");
        }

    }
}
