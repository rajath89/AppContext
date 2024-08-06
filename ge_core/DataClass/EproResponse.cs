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

        public AccountDisplayGroup AccountDisplayGroup { get; set; }

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

    public class AccountDisplayGroup
    {
        public AccountCategory[] Categories { get; set; }
        public Accounts[] Accounts { get; set; }
    }

    public class AccountCategory
    {
        public string CategoryName { get; set; }

        public int CategorySeqNum { get; set; }
    }

    public class Accounts
    {
        public string AccountCategory { get; set; }

        public string AccountNumber { get; set; }
    }

}
