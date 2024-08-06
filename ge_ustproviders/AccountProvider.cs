using ge_core.Base;
using ge_core.Coretypes;
using ge_core.DataClass;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ge_ustproviders
{
    public class AccountProvider : AccountProviderBase
    {
        public override List<AccountGroup> GetAccountGroups()
        {
            throw new System.NotImplementedException();
        }

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
                if(profile != null && profile.ResponseStatus == ResponseStatus.Success)
                {
                    accounts = PlatformContextAccessor.AccountGroupContextItemsData.AllAccounts;
                }
            }
            
            return accounts;

        }

    }
}