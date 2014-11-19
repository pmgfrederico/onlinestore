using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.DesignPatterns.Examples;

namespace ImgGroup.OnlineStore.Infrastructure
{
    public static class RepositoryExtensions
    {
        public static async Task<IEnumerable<TEntity>> RetrieveAllAsync<TEntity, TIdentifier>(this IReadOnlyRepository<TEntity, TIdentifier> repository,
            CancellationToken ct) where TEntity : class, IEntity<TIdentifier>
        {
            return await repository.RetrieveAsync(q => q, ct);
        }

        public static async Task<IEnumerable<TEntity>> PaginateAsync<TEntity, TIdentifier>(this IReadOnlyRepository<TEntity, TIdentifier> repository,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sorter,
            int pageIndex,
            int pageSize, CancellationToken ct) where TEntity : class, IEntity<TIdentifier>
        {
            var skip = pageIndex * pageSize;

            return await repository.RetrieveAsync(q => sorter(q).Skip(skip).Take(pageSize), ct);
        }

        public static async Task<PagedCollection<TEntity>> PaginateWithCountAsync<TEntity, TIdentifier>(this IReadOnlyRepository<TEntity, TIdentifier> repository,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sorter,
           int pageIndex,
           int pageSize, CancellationToken ct) where TEntity : class, IEntity<TIdentifier>
        {
            var skip = pageIndex * pageSize;

            var groups = await repository.RetrieveAsync(
               q =>
               {
                   return sorter(q)
                       .Skip(skip)
                       .Take(pageSize)
                       .GroupBy(g => new
                               {
                                   Total = q.Count()
                               });
               },
               ct);

            // return first group
            var result = groups.FirstOrDefault();

            if (result == null)
                return new PagedCollection<TEntity>(Enumerable.Empty<TEntity>(), 0L);

            return new PagedCollection<TEntity>(result, result.Key.Total);
        }
    }
}
