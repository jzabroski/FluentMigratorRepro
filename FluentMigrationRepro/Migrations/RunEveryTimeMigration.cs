

namespace FluentMigrationRepro.Migrations
{
    using FluentMigrator;
    [Maintenance(MigrationStage.AfterAll)]
    public class RunEveryTimeMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql("CREATE OR ALTER PROC a AS BEGIN SELECT 1 END");
        }
    }
}
