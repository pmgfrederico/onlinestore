using ImgGroup.Common.Infrastructure;
using ImgGroup.OnlineStore.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Infrastructure
{
    public class EntityFrameworkBootstrapper : IEntityFrameworkBootstrapper
    {
        public EntityFrameworkBootstrapper()
        {            
        }

        public void Bootstrap()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OnlineStoreDbContext, Configuration>());
        }
    }
}
