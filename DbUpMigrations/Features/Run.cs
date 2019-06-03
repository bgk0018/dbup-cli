using CommandLine;
using DbUp.Builder;
using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbUpMigrations.Features
{
    public static class Run
    {
        [Verb("run", HelpText = "Run migrations against the database provided.")]
        public class Options
        {
            [Value(0, MetaName = "Connection String", Required = true, HelpText = "Connection string for the database to report on")]
            public string ConnectionString { get; }

            [Option('q', "quiet", HelpText = "Stops output to the console.")]
            public bool Quiet { get; }

            public Options(string connectionString, bool quiet)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
            }
        }

        public static void Execute(Options opts, Func<Build.Options, UpgradeEngineBuilder> builder)
        {
            builder(new Build.Options(opts.ConnectionString, opts.Quiet, false))
                .Build()
                .PerformUpgrade();
        }
    }
}
