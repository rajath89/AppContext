using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ge_core.Base;
using ge_core.Coretypes;
using ge_core.DataClass;

namespace ge_mymproviders
{
    public class AccountProvider : AccountProviderBase
    {
        public override List<ge_core.Coretypes.Account> GetAccounts()
        {
            List<ge_core.Coretypes.Account> accounts = null;

            var data = PlatformContextAccessor.AccountGroupContextItemsData;

            if (data != null)
            {
                accounts = data.AllAccounts;
            }
            else
            {
                var profile = DataProviderManager<CustomerProfileProviderBase>.Provider.RetrieveProfile();
                if (profile != null && profile.ResponseStatus == ResponseStatus.Success)
                {
                    accounts = PlatformContextAccessor.AccountGroupContextItemsData.AllAccounts;
                }
            }

            return accounts;

        }

    }
}
