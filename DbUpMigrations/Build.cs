using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DbUpMigrations
{
    public static class Build
    {
        public class Options
        {
            public string ConnectionString { get; }

            public bool Quiet { get; }

            public bool LogScriptResult { get; }

            public Options(string connectionString, bool quiet, bool logScriptResult)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
                LogScriptResult = logScriptResult;
            }
        }

        public static UpgradeEngine Execute(Options opts)
        {
            EnsureDatabase.For.SqlDatabase(opts.ConnectionString);

            var builder = DeployChanges.To
                .SqlDatabase(opts.ConnectionString, null)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction();

            builder = opts.Quiet ?
                builder.LogToNowhere() :
                builder.LogToConsole();

            builder = opts.LogScriptResult ?
                builder.LogScriptOutput() :
                builder;

            return builder.Build();
        }
    }
}
