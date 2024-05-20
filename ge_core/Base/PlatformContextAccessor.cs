using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public sealed class PlatformContextAccessor
    {
        //private static string _sessionId = null;

        private static SimpleCache<Session> _sessionCache = SimpleCache<Session>.Instance;

        private static SimpleCache<PlatformContextData> _platformCache = null;
        static PlatformContextAccessor() 
        {
            
        }

        public static Client ClientData
        {
            get
            {
                Client result = null;

                try
                {
                    result = GetContext().ClientData;
                }
                catch (Exception)
                {

                    throw;
                }

                return result;
            }

            set
            {
                try
                {
                    PlatformContextData platformContext = GetContext();
                    platformContext.ClientData = value;

                    SetContext(platformContext);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        public static AccountGroupContextItems AccountGroupContextItemsData
        {
            get
            {
                AccountGroupContextItems result = null;

                try
                {

                    var context = GetContext();

                    if(context != null)
                    {
                        result = context.AccountGroupContextItems;
                    }
                    else
                    {
                        Console.Error.WriteLine("context is null");
                    }

                    //result = GetContext().AccountGroupContextItems;
                }
                catch (Exception)
                {

                    throw;
                }

                return result;
            }

            set
            {
                try
                {
                    PlatformContextData platformContext = GetContext();
                    platformContext.AccountGroupContextItems = value;

                    SetContext(platformContext);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private static PlatformContextData GetContext()
        {
            PlatformContextData result = null;

            try
            {
                if(_platformCache == null)
                {
                    _platformCache = SimpleCache<PlatformContextData>.Instance;
                }

                var contextObj = _platformCache.Get(GetSessionId());

                if (_platformCache != null &&  contextObj == null)
                {
                    result = new PlatformContextData();

                    SetContext(result);
                }
                else
                {
                    result = contextObj;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        private static void SetContext(PlatformContextData result)
        {
            _platformCache.Set(GetSessionId(), result, TimeSpan.FromMinutes(10));
        }

        private static string GetSessionId()
        {
            string sessionId = null;
            
            if (_sessionCache != null)
            {
                var sessionObj = _sessionCache.Get(Constants.SESSION_OBJ);
                sessionId = sessionObj != null && sessionObj.SessionId != null ? sessionObj.SessionId : null;
            }

            return sessionId;
        }
    }
}
