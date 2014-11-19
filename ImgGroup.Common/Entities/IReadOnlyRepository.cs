using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImgGroup.Common.Entities
{
    public interface IReadOnlyRepository<TEntity, TIdentifier> where TEntity : IEntity<TIdentifier>
    {
        Task<IEnumerable<TEntity>> RetrieveAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken ct);
        Task<TResult> RetrieveAsync<TResult>(Func<IQueryable<TEntity>, TResult> queryToScalarOrAnonymous, CancellationToken ct);
    }
}
