using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgGroup.OnlineStore.Contracts
{
    /// <summary>
    /// A Value Object - Used class instead of struct thus supporting EF (baahh)
    /// </summary>
    public class Address // TODO Did not worried about addressing normalization and rules - It's just a dumb representation
    {
        public string Line { get; set; }
        public string ExtraLine { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
