using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public abstract class AccountProviderBase : BaseDataProvider
    {
        protected AccountProviderBase() { }

        public abstract List<Account> GetAccounts();
    }
}
