using FluentMigrator;

namespace FluentMigrationRepro.Migrations
{
    [Migration(201909231355)]
    public class RunOnceMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("test").WithColumn("Id").AsString();
        }
    }
}