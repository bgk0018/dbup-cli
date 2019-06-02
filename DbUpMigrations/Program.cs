using CommandLine;
using DbUp;
using DbUp.Builder;
using DbUp.Helpers;
using DbUpMigrations.Features;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbUpMigrations
{
    class Program
    {
        private static void Catch(IEnumerable<Error> obj)
        {
            Console.WriteLine("Exited without any execution.");
        }

        public static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Report.Options, Run.Options, Script.Options>(args)
                    .WithParsed<Report.Options>(opts => Report.Execute(opts, Build.Execute))
                    .WithParsed<Run.Options>(opts => Run.Execute(opts, Build.Execute))
                    .WithParsed<Script.Options>(opts => Script.Execute(opts, Build.Execute))
                    .WithNotParsed(Catch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
