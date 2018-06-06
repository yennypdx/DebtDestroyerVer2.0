using DebtDestroyer.DataAccess;
using DebtDestroyer.Model;
using DebtDestroyer.UnitOfWork;
using System;
using System.Collections.Generic;

namespace DebtDestroyer.UI.DataProvider
{
    public interface INavigationDataProvider
    {
        ICustomerDataService CustomerService { get; set; }
        IAccountDataService AccountService { get; set; }
        IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID);
    }

    public class NavigationDataProvider : INavigationDataProvider
    {
        private Func<IUnitOfWork> _dataServiceCreator;

        public NavigationDataProvider(Func<IUnitOfWork> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public ICustomerDataService CustomerService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAccountDataService AccountService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IAccount> GetCustomerAccounts(int customerID)
        {
            throw new NotImplementedException();
        }
    }
}
