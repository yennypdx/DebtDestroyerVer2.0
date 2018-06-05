using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DebtDestroyer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerDataService CustomerService { get; set; }
        public IAccountDataService AccountService { get; set; }
        public IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID)
        {
            var customerAccounts = AccountService.FindAll().ToList().Where(a => a._CustomerId.Equals(customerID));
            if (customerAccounts == null || customerAccounts.Count().Equals(0)) throw new InvalidOperationException("Invalid customer Id");
            return customerAccounts;
        }

        public UnitOfWork(ICustomerDataService customerDataService, IAccountDataService accountDataService)
        {
            CustomerService = customerDataService;
            AccountService = accountDataService;

        }
        //public UnitOfWork()
        //{
        //    CustomerService = new CustomerDataService();
        //    AccountService = new AccountDataService();

        //}

    }
}
