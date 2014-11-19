using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.Common.Serialization
{
    public static class SerializationExtensions
    {
        public static byte[] Serialize<T>(this T obj) where T : ISerializableEntity
        {
            using (var ms = new MemoryStream())
            {
                new DataContractSerializer(typeof(T))
                    .WriteObject(ms, obj);

                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(this byte[] data) where T : class, ISerializableEntity
        {            
            using (var ms = new MemoryStream(data))
            {
                return new DataContractSerializer(typeof(T))
                    .ReadObject(ms) as T;                
            }
        }
    }
}
