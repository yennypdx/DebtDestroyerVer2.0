using DebtDestroyer.DataAccess;
using System.Collections.Generic;

namespace DebtDestroyer.UnitOfWork
{
    public interface IUnitOfWork: System.IDisposable
    {
        ICustomerDataService CustomerService { get; set; }
        IAccountDataService AccountService { get; set; }
        IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID);
    }
}
