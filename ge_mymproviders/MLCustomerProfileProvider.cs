using ge_core.Base;
using ge_core.Coretypes;
using ge_core.DataClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_mymproviders
{
    internal class MLCustomerProfileProvider : CustomerProfileProviderBase
    {

        private static EproResponse GetEproResponse(string JsonFilePath)
        {
            var jsonData = File.ReadAllText(JsonFilePath);
            return JsonConvert.DeserializeObject<EproResponse>(jsonData);
        }

        public override RetrieveProfileResponse RetrieveProfile()
        {
            RetrieveProfileResponse retrieveProfileResponse = null;

            try
            {
                retrieveProfileResponse = new RetrieveProfileResponse();

                EproResponse eproResponse = GetEproResponse(Constants.ML_EproRespPath);

                if (eproResponse != null)
                {
                    Dictionary<string, ge_core.Coretypes.Account> accountDictionary = null;

                    var lstOfAccts = ConvertEproRespToCoreTypesForAccts(eproResponse, out accountDictionary);

                    var lstOfAccountGroups = GetAccountGroups(eproResponse,accountDictionary);

                    var client = ConvertEproRespToCoreTypesForClient(eproResponse);

                    bool isDataPresent = false;

                    if (lstOfAccts != null && lstOfAccountGroups != null)
                    {
                        SaveAccountsInContext(lstOfAccts,lstOfAccountGroups);
                        isDataPresent = true;
                    }

                    if (client != null)
                    {
                        SaveClientInfoInContext(client);
                        isDataPresent = true;
                    }

                    retrieveProfileResponse.ResponseStatus = isDataPresent ? ResponseStatus.Success : ResponseStatus.Failure;
                }
                else
                {
                    retrieveProfileResponse.ResponseStatus = ResponseStatus.Failure;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

                retrieveProfileResponse = new RetrieveProfileResponse { ResponseStatus = ResponseStatus.Failure };
            }


            return retrieveProfileResponse;

        }

        private List<AccountGroup> GetAccountGroups(EproResponse eproResponse, Dictionary<string, ge_core.Coretypes.Account> accountDict)
        {
            List<AccountGroup> accountGroups = null;

            if(accountDict != null && accountDict.Count > 0 && eproResponse.AccountDisplayGroup != null && eproResponse.AccountDisplayGroup.Accounts != null && eproResponse.AccountDisplayGroup.Accounts.Length > 0)
            {
                accountGroups = new List<AccountGroup>();
                foreach(var item in eproResponse.AccountDisplayGroup.Categories)
                {
                    if(!string.IsNullOrEmpty(item.CategoryName))
                    {
                        var accountGrp = new AccountGroup();

                        accountGrp.GroupName = item.CategoryName;
                        accountGrp.GroupSequenceNumber = item.CategorySeqNum;

                        var accountGroupAccts = GetAccountsForAcctGrp(item.CategoryName, eproResponse.AccountDisplayGroup.Accounts, accountDict);

                        accountGrp.Accounts = accountGroupAccts != null && accountGroupAccts.Count > 0 ? accountGroupAccts.ToArray() : null;

                        accountGroups.Add(accountGrp);

                    }
                }
            }

            return accountGroups;
        }

        private List<ge_core.Coretypes.Account> GetAccountsForAcctGrp(string categoryName, Accounts[] accounts, Dictionary<string, ge_core.Coretypes.Account> accountDict)
        {
            List<ge_core.Coretypes.Account> accountsLst = null;

            if(accounts != null && accounts.Length > 0)
            {
                accountsLst = new List<ge_core.Coretypes.Account>();
                foreach(var item in accounts)
                {
                    ge_core.Coretypes.Account account = null;
                    if (string.Equals(item.AccountCategory,categoryName,StringComparison.OrdinalIgnoreCase))
                    {
                        if(accountDict.TryGetValue(item.AccountNumber, out account))
                        {
                            accountsLst.Add(account);
                        }
                    }
                }

            }

            return accountsLst != null && accountsLst.Count > 0 ? accountsLst.OrderBy(x => x.AccountNumber).ToList() : accountsLst;
        }

        private List<ge_core.Coretypes.Account> ConvertEproRespToCoreTypesForAccts(EproResponse profile, out Dictionary<string, ge_core.Coretypes.Account> accountDictionary)
        {
            List<ge_core.Coretypes.Account> accounts = null;
            accountDictionary = null;
            if (profile != null)
            {
                if (profile.accounts != null && profile.accounts.Length > 0)
                {
                    accounts = new List<ge_core.Coretypes.Account>();
                    accountDictionary = new Dictionary<string, ge_core.Coretypes.Account>();

                    foreach (var item in profile.accounts)
                    {
                        ge_core.Coretypes.Account account = new ge_core.Coretypes.Account();
                        account.AccountNumber = item.AccountNumber;
                        account.AccountType = item.AccountType;
                        account.ADX = item.ADX;
                        account.Balance = item.Balance;

                        accountDictionary.Add(item.AccountNumber, account);
                        accounts.Add(account);
                    }
                }

            }

            return accounts;
        }

        private void SaveClientInfoInContext(Client clientData)
        {
            PlatformContextAccessor.ClientData = clientData;
        }

        private Client ConvertEproRespToCoreTypesForClient(EproResponse profile)
        {
            Client client = null;
            if (profile != null)
            {
                if (profile.customer != null)
                {
                    client = new Client();

                    client.FullName = profile.customer.FullName;
                    client.PrimaryEmailAddress = profile.customer.PrimaryEmailAddress;
                }

            }

            return client;
        }

        private void SaveAccountsInContext(List<ge_core.Coretypes.Account> lstOfAccts, List<AccountGroup> lstOfAccountGroups)
        {
            PlatformContextAccessor.AccountGroupContextItemsData = new AccountGroupContextItems { AllAccounts = lstOfAccts, AccountGroups = lstOfAccountGroups };
        }
    }
}
