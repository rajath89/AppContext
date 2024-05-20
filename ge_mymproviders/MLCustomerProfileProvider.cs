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

                    var lstOfAccts = ConvertEproRespToCoreTypesForAccts(eproResponse);

                    var client = ConvertEproRespToCoreTypesForClient(eproResponse);

                    bool isDataPresent = false;

                    if (lstOfAccts != null && lstOfAccts.Count > 0)
                    {
                        SaveAccountsInContext(lstOfAccts);
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


        private List<ge_core.Coretypes.Account> ConvertEproRespToCoreTypesForAccts(EproResponse profile)
        {
            List<ge_core.Coretypes.Account> accounts = null;
            if (profile != null)
            {
                if (profile.accounts != null && profile.accounts.Length > 0)
                {
                    accounts = new List<ge_core.Coretypes.Account>();

                    foreach (var item in profile.accounts)
                    {
                        ge_core.Coretypes.Account account = new ge_core.Coretypes.Account();
                        account.AccountNumber = item.AccountNumber;
                        account.AccountType = item.AccountType;
                        account.ADX = item.ADX;
                        account.Balance = item.Balance;

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

        private void SaveAccountsInContext(List<ge_core.Coretypes.Account> lstOfAccts)
        {
            PlatformContextAccessor.AccountGroupContextItemsData = new AccountGroupContextItems { AllAccounts = lstOfAccts };
        }
    }
}
