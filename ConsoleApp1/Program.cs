using Newtonsoft.Json;
using System;
using ge_core.Base;
using ge_core.Context;
using ge_core.Coretypes;
using ge_core.DataClass;
using ge_core.Util;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var obj = Util.GetConfigObj();

            var cache = SimpleCache<Session>.Instance;
            var sessionId = Guid.NewGuid().ToString();
            if (obj != null && cache != null && cache.Get(Constants.SESSION_OBJ) == null)
            {
                var sessionObj = new Session { SessionId = sessionId, User = obj.User, Site = obj.CurrentSite };
                cache.Set(Constants.SESSION_OBJ, sessionObj, TimeSpan.FromMinutes(10));
            }


            var accountContext = new AccountContext();
            var clientContext = new ClientContext();
            Console.WriteLine($"--first attempt with sessionId : {sessionId}  --");

            var accounts = accountContext.GetAllAccounts();
            Console.WriteLine(JsonConvert.SerializeObject(accounts));
            var client = clientContext.GetClientDetails();
            Console.WriteLine(JsonConvert.SerializeObject(client));

            Console.WriteLine();

            Console.WriteLine($"--second attempt with sessionId : {sessionId}  --");

            var accountsFromCache = accountContext.GetAllAccounts();
            Console.WriteLine(JsonConvert.SerializeObject(accountsFromCache));
            var clientFromCache = clientContext.GetClientDetails();
            Console.WriteLine(JsonConvert.SerializeObject(clientFromCache));

            Console.WriteLine();

            cache.ClearByType<Session>(); //clear & gen new sess

            sessionId = Guid.NewGuid().ToString();
            if (obj != null && cache != null && cache.Get(Constants.SESSION_OBJ) == null)
            {
                var sessionObj = new Session { SessionId = sessionId, User = obj.User, Site = obj.CurrentSite };
                cache.Set(Constants.SESSION_OBJ, sessionObj, TimeSpan.FromMinutes(10));
            }

            Console.WriteLine($"--third attempt with sessionId : {sessionId}  --");

            var accountsFromCache2 = accountContext.GetAllAccounts();
            Console.WriteLine(JsonConvert.SerializeObject(accountsFromCache2));
            var clientFromCache2 = clientContext.GetClientDetails();
            Console.WriteLine(JsonConvert.SerializeObject(clientFromCache2));

            Console.WriteLine();

            Console.WriteLine($"--fourth attempt with sessionId : {sessionId}  --");

            var accountsFromCache3 = accountContext.GetAllAccounts();
            Console.WriteLine(JsonConvert.SerializeObject(accountsFromCache3));
            var clientFromCache3 = clientContext.GetClientDetails();
            Console.WriteLine(JsonConvert.SerializeObject(clientFromCache3));

            Console.WriteLine();

            Console.WriteLine("end");



            /*
             Hello, World!
            --first attempt with sessionId : 5ea2d849-8a0e-41de-bb0f-3140c8708ccd  --
            session is not present, calling provider
            [{"AccountNumber":"1234","ADX":"asdf123","Balance":1000.0,"AccountType":"UST"},{"AccountNumber":"5678","ADX":"asdf568","Balance":2000.0,"AccountType":"UST"},{"AccountNumber":"3456","ADX":"asdf345","Balance":6000.0,"AccountType":"UST"}]
            session is present
            {"FullName":"testUserUST1","PrimaryEmailAddress":"testUserUST1@gmail.com"}

            --second attempt with sessionId : 5ea2d849-8a0e-41de-bb0f-3140c8708ccd  --
            session is present
            [{"AccountNumber":"1234","ADX":"asdf123","Balance":1000.0,"AccountType":"UST"},{"AccountNumber":"5678","ADX":"asdf568","Balance":2000.0,"AccountType":"UST"},{"AccountNumber":"3456","ADX":"asdf345","Balance":6000.0,"AccountType":"UST"}]
            session is present
            {"FullName":"testUserUST1","PrimaryEmailAddress":"testUserUST1@gmail.com"}

            --third attempt with sessionId : 8e98e765-be4b-442b-9928-b3c9f76beb61  --
            session is not present, calling provider
            [{"AccountNumber":"1234","ADX":"asdf123","Balance":1000.0,"AccountType":"UST"},{"AccountNumber":"5678","ADX":"asdf568","Balance":2000.0,"AccountType":"UST"},{"AccountNumber":"3456","ADX":"asdf345","Balance":6000.0,"AccountType":"UST"}]
            session is present
            {"FullName":"testUserUST1","PrimaryEmailAddress":"testUserUST1@gmail.com"}

            --fourth attempt with sessionId : 8e98e765-be4b-442b-9928-b3c9f76beb61  --
            session is present
            [{"AccountNumber":"1234","ADX":"asdf123","Balance":1000.0,"AccountType":"UST"},{"AccountNumber":"5678","ADX":"asdf568","Balance":2000.0,"AccountType":"UST"},{"AccountNumber":"3456","ADX":"asdf345","Balance":6000.0,"AccountType":"UST"}]
            session is present
            {"FullName":"testUserUST1","PrimaryEmailAddress":"testUserUST1@gmail.com"}

            end
             */

        }
    }
}
