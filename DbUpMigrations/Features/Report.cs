using CommandLine;
using DbUp.Builder;
using DbUp.Engine;
using DbUp.Helpers;
using System;

namespace DbUpMigrations.Features
{
    public static class Report
    {
        [Verb("report", HelpText = "Generate a report on what scripts will be run against the database provided.")]
        public class Options
        {
            [Value(0, MetaName = "Connection String", Required = true, HelpText = "Connection string for the database to report on")]
            public string ConnectionString { get; }

            [Option('q', "quiet", MetaValue = "[true, false]", HelpText = "Stops output to the console. Default is false.")]
            public bool Quiet { get; }

            [Option('p', "path", MetaValue = "[String]", HelpText = "Path to where the report will be published.")]
            public string ReportPath { get; }

            public Options(string connectionString, bool quiet, string reportPath)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
                ReportPath = reportPath;
            }
        }

        public static void Execute(Options opts, Func<Build.Options, UpgradeEngineBuilder> builder)
        {
            builder(new Build.Options(opts.ConnectionString, opts.Quiet))
                .Build()
                .GenerateUpgradeHtmlReport(opts.ReportPath);
        }
    }
}
