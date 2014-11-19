using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.Common.Entities
{
    public interface IEntity<TIdentifier> : IEquatable<IEntity<TIdentifier>>
    {
        TIdentifier Id { get; }
        
        object[] Key();
    }
}
