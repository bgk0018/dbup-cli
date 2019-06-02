using DbUp;
using DbUp.Builder;
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

            public Options(string connectionString, bool quiet)
            {
                ConnectionString = connectionString;
                Quiet = quiet;
            }
        }

        public static UpgradeEngineBuilder Execute(Options opts)
        {
            EnsureDatabase.For.SqlDatabase(opts.ConnectionString);

            var builder = DeployChanges.To.SqlDatabase(opts.ConnectionString, null)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly());

            builder = opts.Quiet ?
                builder.LogToNowhere() :
                builder.LogToConsole();

            return builder;
        }
    }
}
