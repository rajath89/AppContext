using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public class DataProviderManager<T> where T : BaseDataProvider
    {

        static DataProviderManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var cache = SimpleCache<Session>.Instance;

            if (cache != null)
            {
                var session = cache.Get(Constants.SESSION_OBJ);
                _providerSite = session != null && session.Site != null ? session.Site : "UST";
            }
            else
            {
                _providerSite = "UST";
            }
        }

        public static T Provider
        {
            get
            {

                try
                {

                    if (_providerSite == "ML")
                    {
                        //return ML instance
                        Assembly mlAssembly = Assembly.LoadFrom(Constants.GE_mymProvPath);

                        var mlaccountTypes = mlAssembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                        .ToList();

                        Type mltype = mlaccountTypes.FirstOrDefault();

                        return (T)Activator.CreateInstance(mltype);

                    }


                    //default
                    //Assembly pluginAssembly = Assembly.LoadFrom("ge_ustproviders, Version=0.0.0.0, Culture=neutral, PublicKeyToken=17242887c5aa9d81");

                    Assembly pluginAssembly = Assembly.LoadFrom(Constants.GE_ustProvPath);

                    var accountTypes = pluginAssembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                    .ToList();

                    Type type = accountTypes.FirstOrDefault();

                    return (T)Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

                
                return default(T);

            }
        }

        private static string _providerSite;


    }
}
