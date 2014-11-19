using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.Common.Entities
{
    public abstract class Composition<TEntity, TIdentifier>
        where TEntity : class, IEntity<TIdentifier>
        where TIdentifier : struct
    {
        /// <summary>
        /// Where Aggregate read the type name of TEntity e.g. Company and CompanyId
        /// </summary>
        public TIdentifier AggregateId { get; internal set; }
        /// <summary>
        /// Where Aggregate read the type name of TEntity e.g. Company and CompanyId
        /// </summary>
        public TEntity Aggregate { get; internal set; }
    }
}
