using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgGroup.OnlineStore
{
    /// <summary>
    /// Represents an Product Item in the Online Store
    /// </summary>
    /// <see cref="http://schema.org/Product"/>
    public class Product : IEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Supporting third party serialization tooling. EF no longer needs public ctor. Review and remove if not needed...
        /// </remarks>
        public Product()
        {

        }

        internal Product(string productId, string name)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(productId)); // TODO Add custom messages from Resource
            System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = Guid.NewGuid();
            this.ProductId = productId;
            this.Name = name;          
        }

        #region IEntity<Guid> implementation

        public Guid Id
        {
            get;
            private set;
        }
        
        #endregion

        /// <summary>
        /// The product identifier, such as ISBN.
        /// </summary>
        /// <example>        
        /// For example: <meta itemprop='productID' content='isbn:123-456-789'/>.        
        /// </example>
        public string ProductId { get; private set; }
        
        /// <summary>
        /// The name of the item.
        /// </summary>
        /// <see cref="http://schema.org/Thing"/>
        public string Name { get; private set; }

        /// <summary>
        /// An alias for the item.
        /// </summary>
        /// <see cref="http://schema.org/Thing"/>
        public string AlternateName { get; private set; }        
        
        /// <summary>
        /// A short description of the item.
        /// </summary>
        /// <see cref="http://schema.org/Thing"/>
        public string Description { get; private set; } // TODO Enriching the Product model is considered out of the scope of this exercise
        
        /// <summary>
        /// The Stock Keeping Unit (SKU), i.e. a merchant-specific identifier for a product or service, or the product to which the offer refers.
        /// </summary>
        public string Sku { get; private set; }

        public decimal? DefaultPrice { get; set; } // TODO a lot can be said about pricing and pricing tables. Including Price in this model for the purpose of this exercise although making it optional

        #region IEntity<Guid> implementation
        object[] IEntity<Guid>.Key()
        {
            return new object[] { this.Id };
        }

        public bool Equals(IEntity<Guid> other)
        {
            if (other == null)
            {
                return false;
            }

            return (other as Product).Id == this.Id;
        }

        #endregion

        public override bool Equals(object obj)
        {            
            return this.Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}