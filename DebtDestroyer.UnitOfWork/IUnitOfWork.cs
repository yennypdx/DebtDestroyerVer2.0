using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerDataService customerService { get; set; }
        IAccountDataService accountService { get; set; }
        IEnumerable<DebtDestroyer.Model.IAccount> getCustomerAccounts(int customerID);
    }

    //public class UnitOfWork : IUnitOfWork
    //{
    //    public ICustomerDataService _CustomerService { get; set; }
    //    public IAccountDataService _AccountService { get; set; }
    // }
}
