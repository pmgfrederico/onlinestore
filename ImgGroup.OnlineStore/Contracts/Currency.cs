using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgGroup.OnlineStore.Contracts
{
    public class Currency
    {
        public Currency()
        {

        }

        internal Currency(string code)
        {
            this.Code = code;
        }

        public string Code { get; private set; }
    }
}
