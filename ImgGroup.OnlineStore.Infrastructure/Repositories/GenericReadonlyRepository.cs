using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Infrastructure
{
    public class GenericReadOnlyRepository<TEntity, TIdentifier> : IReadOnlyRepository<TEntity, TIdentifier>
        where TEntity : class, IEntity<TIdentifier>        
    {
        readonly DbContext db;

        public GenericReadOnlyRepository(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TEntity>> RetrieveAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken ct)
        {
            return await query(this.db.Set<TEntity>())
                .ToArrayAsync(ct)
                .ConfigureAwait(false);
        }


        public async Task<TResult> RetrieveAsync<TResult>(Func<IQueryable<TEntity>, TResult> queryToScalarOrAnonymous, CancellationToken ct)
        {
            return await Task.FromResult(queryToScalarOrAnonymous(db.Set<TEntity>()))
                .ConfigureAwait(false);
        }
    }
}
