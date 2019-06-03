using CommandLine;
using DbUp.Builder;
using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbUpMigrations.Features
{
    public static class Script
    {
        [Verb("script", HelpText = "Output the script that would be run by the migration tool.")]
        public class Options
        {
            [Value(0, MetaName = "Connection String", MetaValue = "[String]", Required = true, HelpText = "Connection string for the database to report on")]
            public string ConnectionString { get; }

            [Option('q', "quiet", HelpText = "Stops output to the console. Default is false.")]
            public bool Quiet { get; }

            [Option('p', "path", Default = ".\\MigrationScript.sql", MetaValue = "[String]", HelpText = "Path to where the script will be published.")]
            public string ScriptPath { get; }

            public Options(string connectionString, bool quiet, string scriptPath)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
                ScriptPath = scriptPath;
            }
        }

        public static void Execute(Options opts, Func<Build.Options, UpgradeEngineBuilder> builder)
        {
            var scripts = builder(new Build.Options(opts.ConnectionString, opts.Quiet, false))
                .Build()
                .GetScriptsToExecute();

            //Do a thing?
        }
    }
}
