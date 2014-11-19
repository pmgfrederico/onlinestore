using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.Common.Entities
{
    public interface ISerializableEntity // Initially just a Marker interface
    {
        bool CanSerialize(); // Some logic to check if a type meets the requirements e.g. DataContract attribute present
    }
}
