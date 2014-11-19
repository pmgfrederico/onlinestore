using ImgGroup.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    //TODO Discuss Customer as a role of fiscal entities, not persons. Considered out of the scope of the exercise
    public class Customer : IEntity<Guid>
    {
        public Customer()
        {

        }

        internal Customer(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        #region IEntity<Guid> implementation

        public Guid Id
        {
            get;
            private set;
        }

        #endregion

        public string Name { get; internal set; }
        public string FiscalNumber { get; internal set; }

        //TODO Manage addresses - Already exemplified with the Order Entity

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
            return this.Equals(obj as Customer);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
