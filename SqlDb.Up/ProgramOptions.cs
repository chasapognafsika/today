using CommandLine;

namespace SqlDb.Up
{
    public class ProgramOptions
    {
        [Option('c', "connectionString", Required = true, HelpText = "Set the connection string.")]
        public string ConnectionString { get; set; } = string.Empty;

        [Option('d', "development", Required = false, HelpText = "Use the development environment.")]
        public bool Development { get; set; }

        [Option('r', "recreate", Required = false, HelpText = "Recreate the development environment. -d flag must be used.")]
        public bool Recreate { get; set; }
    }
}
