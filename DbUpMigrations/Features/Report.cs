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

            [Option('q', "quiet", HelpText = "Stops output to the console.")]
            public bool Quiet { get; }

            [Option('p', "path", Default = ".\\Report.html", MetaValue = "[String]", HelpText = "Path to where the report will be published.")]
            public string ReportPath { get; }

            public Options(string connectionString, bool quiet, string reportPath)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
                ReportPath = reportPath;
            }
        }

        public static void Execute(Options opts, Func<Build.Options, UpgradeEngine> builder)
        {
            builder(new Build.Options(opts.ConnectionString, opts.Quiet, false))
                .GenerateUpgradeHtmlReport(opts.ReportPath);
        }
    }
}
