using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMigrationRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            const string connectionString = @"Server=.\SQLEXPRESS;Database=Cv;Trusted_Connection=true";
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    x => x.AddSqlServer2016()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Program).Assembly).For.Migrations()).AddLogging(lb => lb.AddFluentMigratorConsole());
            var provider = services.BuildServiceProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}
