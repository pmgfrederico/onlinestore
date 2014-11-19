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
        #region IEntity<Guid> implementation

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

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
