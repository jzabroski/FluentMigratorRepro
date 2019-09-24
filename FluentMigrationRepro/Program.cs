using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluentMigrationRepro
{
    class Program
    {
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            const string connectionString = @"Server=.;Database=Cv;Trusted_Connection=true";
            _ = services
                .AddLogging(lb => lb.SetMinimumLevel(LogLevel.Debug).AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .AddSingleton<IAssemblySourceItem>(serviceProvider => new AssemblySourceItem(typeof(Program).Assembly))
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
