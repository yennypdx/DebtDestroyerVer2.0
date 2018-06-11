using Autofac;
using DebtDestroyer.DataAccess;

namespace ConsoleApp.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CustomerDataService>().As<ICustomerDataService>();
            builder.RegisterType<AccountDataService>().As<IAccountDataService>();
            builder.RegisterType<PayoffDataService>().As<IPayoffDataService>();
            return builder.Build();
        }
    }
}
