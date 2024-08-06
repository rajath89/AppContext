using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Coretypes
{
    public class AccountGroup
    {
        public string GroupName { get; set; }

        public int GroupSequenceNumber { get; set; }

        public Account[] Accounts { get; set; }
    }
}
