using ge_core.Base;
using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Context
{
    public class AccountContext
    {

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = null;

            var accountGrpContextItems = PlatformContextAccessor.AccountGroupContextItemsData;

            if(accountGrpContextItems != null)
            {
                Console.WriteLine("session is present");
                accounts = accountGrpContextItems.AllAccounts;
            }
            else
            {
                Console.WriteLine("session is not present, calling provider");
                accounts = DataProviderManager<AccountProviderBase>.Provider.GetAccounts();
            }


            return accounts;
        }

        public List<AccountGroup> GetAccountGroups()
        {
            List<AccountGroup> accountGrp = null;

            var accountGrpContextItems = PlatformContextAccessor.AccountGroupContextItemsData;

            if (accountGrpContextItems != null)
            {
                Console.WriteLine("session is present");
                accountGrp = accountGrpContextItems.AccountGroups;
            }
            else
            {
                Console.WriteLine("session is not present, calling provider");
                accountGrp = DataProviderManager<AccountProviderBase>.Provider.GetAccountGroups();
            }


            return accountGrp;
        }

    }
}
