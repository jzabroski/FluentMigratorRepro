using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace FluentMigrationRepro.Profiles
{
    [Profile("")]
    class TestProfile : ForwardOnlyMigration
    {
        public override void Up()
        {
            
        }
    }
}
