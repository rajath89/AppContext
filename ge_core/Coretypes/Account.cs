using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Coretypes
{
    public class Account
    {
        public string AccountDisplayName { get; set; }
        public string AccountNumber { get; set; }

        public string ADX { get; set; }

        public decimal Balance { get; set; }

        public string AccountType { get; set; }
    }
}
