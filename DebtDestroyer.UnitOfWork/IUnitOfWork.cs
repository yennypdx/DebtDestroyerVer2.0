using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerDataService CustomerService { get; set; }
        IAccountDataService AccountService { get; set; }
        IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID);
    }
}
