using ConsoleApp1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //[ServiceContract]
    //public interface IEmployeeService
    //{
    //    [OperationContract]
    //    EmployeeResponse GetDataUsingDataContract(Employee empObj);
    //}

    public abstract class EmployeeServiceProviderBase : ServiceProviderBase
    {
        public abstract EmployeeResponse GetDataUsingDataContract(Employee empObj);
    }


    public class EmployeeServiceProvider : EmployeeServiceProviderBase
    {
        public override EmployeeResponse GetDataUsingDataContract(Employee empObj)
        {
            Console.WriteLine("test");
            IService1 proxy = null;
            EmployeeResponse emp = null;
            //GetGoalsResponse platformCallResponse;
            try
            {
                proxy = base.CreateProxyInterface<IService1>();
                emp = proxy.GetDataUsingDataContract(empObj);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                base.DisposeProxy(proxy);
            }

            return emp;
        }
    }
}
