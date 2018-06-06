using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;

namespace DebtDestroyer.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerDataService CustomerService { get; set; }
        IAccountDataService AccountService { get; set; }
        IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID);
    }
}
