using ImgGroup.OnlineStore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace ImgGroup.OnlineStore.Infrastructure.Repositories
{
    public class ShoppingSessionRepository : GenericReadOnlyRepository<ShoppingSession, Guid>, IShoppingSessionRepository
    {
        readonly OnlineStoreDbContext db;

        public ShoppingSessionRepository(OnlineStoreDbContext db)
            : base(db)
        {
            this.db = db;
        }

        public async Task<ShoppingSession> RetrieveShopperSessionAsync(string shopperId, CancellationToken ct)
        {
            return await this.RetrieveAsync<ShoppingSession>(q => q.OrderByDescending(ss => ss.CreatedOn).FirstOrDefault(ss => ss.ShopperId == shopperId), ct); 
        }

        public async Task SaveShopperSessionAsync(ShoppingSession session, CancellationToken ct)
        {
            this.db.Set<ShoppingSession>()
                .AddOrUpdate(session);            
            
            await this.db.SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
