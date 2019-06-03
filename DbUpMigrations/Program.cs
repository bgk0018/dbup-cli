using CommandLine;
using DbUpMigrations.Features;
using System;
using System.Collections.Generic;

namespace DbUpMigrations
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Report.Options, Run.Options>(args)
                    .WithParsed<Report.Options>(opts => Report.Execute(opts, Build.Execute))
                    .WithParsed<Run.Options>(opts => Run.Execute(opts, Build.Execute));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
