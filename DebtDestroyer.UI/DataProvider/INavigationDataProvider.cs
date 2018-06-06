using DebtDestroyer.DataAccess;
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

       
    }
}
