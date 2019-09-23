using System;
using System.Linq;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluentMigrationRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            const string connectionString = @"Server=.;Database=Cv;Trusted_Connection=true";
            _ = services
                .AddLogging(lb => lb.SetMinimumLevel(LogLevel.Debug).AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .PostConfigure<RunnerOptions>(x => x.Tags = new string[] {
                    "FluentMigratorIssue1016Workaround"})
                .AddSingleton<IAssemblySourceItem>((IServiceProvider serviceProvider) =>
                {
                    return new AssemblySourceItem(typeof(Program).Assembly); })
                .ConfigureRunner(
                    x => x.AddSqlServer2016()
                        .WithGlobalConnectionString(connectionString)
                );
            var provider = services.BuildServiceProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

    }
}
