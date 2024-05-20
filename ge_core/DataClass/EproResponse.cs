using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.DataClass
{
    public class EproResponse
    {
        public Account[] accounts { get; set; }
        public Customer customer { get; set; }

        public string GUID { get; set; }
    }

    public class Customer
    {
        public string FullName { get; set; }
        public string PrimaryEmailAddress { get; set; }
    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public string ADX { get; set; }
        public int Balance { get; set; }
        public string AccountType { get; set; }
    }
}
