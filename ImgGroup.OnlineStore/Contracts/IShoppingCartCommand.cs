using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    public interface IShoppingCartCommand : IDomainCommand { }

    [DataContract]
    [KnownType(typeof(AddItemCommand))]
    public class AddItemCommand : IShoppingCartCommand
    {
        [DataMember]
        public string ShopperId { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public double Quantity { get; set; }
    }

    [DataContract]
    [KnownType(typeof(UpdateItemCommand))]
    public class UpdateItemCommand : IShoppingCartCommand
    {
        [DataMember]
        public string ShopperId { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public double Quantity { get; set; }
    }

    [DataContract]
    [KnownType(typeof(RemoveItemCommand))]
    public class RemoveItemCommand : IShoppingCartCommand
    {
        [DataMember]
        public string ShopperId { get; set; }
        [DataMember]
        public string ProductId { get; set; }        
    }
}
