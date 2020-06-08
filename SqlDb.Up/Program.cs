using CommandLine;
using DbUp;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace SqlDb.Up
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = default(ProgramOptions);

            var input = Parser.Default.ParseArguments<ProgramOptions>(args);
            input.WithParsed<ProgramOptions>(p => options = p);

            if (options == null)
            {
                EndProgram(0, false);
                return;
            }

            WriteHeader(options);

            input.WithNotParsed<ProgramOptions>(errors =>
            {
                if (errors.Any(e => e.Tag == ErrorType.MissingRequiredOptionError))
                    EndProgram(1, options.Development);
            });

            if (options.Development && options.Recreate)
                RecreateDatabase(options);

            RunScripts(options);

            if (options.Development)
                RunDevScripts(options);

            EndProgram(0, options.Development);
        }

        private static void RecreateDatabase(ProgramOptions options)
        {
            WriteWithColor(() => Console.WriteLine("Recreate database... "), ConsoleColor.Yellow);
            DropDatabase.For.SqlDatabase(options.ConnectionString);
            EnsureDatabase.For.SqlDatabase(options.ConnectionString);
            WriteWithColor(() => Console.WriteLine("Success!"), ConsoleColor.Green);
        }

        private static void RunScripts(ProgramOptions options)
        {
            WriteWithColor(() => Console.WriteLine("Upgrade database schema... "), ConsoleColor.Yellow);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(options.ConnectionString)
                    .WithTransaction()
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => !s.Contains("Scripts.Dev"))
                    .LogToConsole()
                    .Build();

            var upgradeResult = upgrader.PerformUpgrade();
            if (!upgradeResult.Successful)
            {
                WriteWithColor(() => Console.WriteLine(upgradeResult.Error), ConsoleColor.Red);
                EndProgram(1, options.Development);
            }
            WriteWithColor(() => Console.WriteLine("Success!"), ConsoleColor.Green);
        }

        private static void RunDevScripts(ProgramOptions options)
        {
            WriteWithColor(() => Console.WriteLine("Adding Development scripts... "), ConsoleColor.Yellow);
            var devUpgrader =
                DeployChanges.To
                    .SqlDatabase(options.ConnectionString)
                    .WithTransaction()
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => s.Contains("Scripts.Dev"))
                    .LogToConsole()
                    .Build();

            var devUpgradeResult = devUpgrader.PerformUpgrade();
            if (!devUpgradeResult.Successful)
            {
                WriteWithColor(() => Console.WriteLine(devUpgradeResult.Error), ConsoleColor.Red);
                EndProgram(1, options.Development);
            }
            WriteWithColor(() => Console.WriteLine("Success!"), ConsoleColor.Green);
        }

        private static void WriteHeader(ProgramOptions options)
        {
            WriteWithColor(() => Console.WriteLine("Use --help flag to display the help screen."), ConsoleColor.Gray);
            WriteWithColor(() => Console.WriteLine("Command line args:"), ConsoleColor.Yellow);
            WriteWithColor(() => Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented)), ConsoleColor.Gray);
        }

        private static void WriteWithColor(Action action, ConsoleColor color)
        {
            var defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            action.Invoke();
            Console.ForegroundColor = defaultColor;
        }

        private static void EndProgram(int code, bool waitActionToClose = false)
        {
            if (waitActionToClose)
            {
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
            }
            Environment.Exit(code);
        }
    }
}