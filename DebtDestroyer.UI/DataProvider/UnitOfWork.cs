using DebtDestroyer.DataAccess;
using DebtDestroyer.UnitOfWork;
using System.Collections.Generic;

namespace DebtDestoyer.UI.DataProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerDataService CustomerService { get; set; }
        public IAccountDataService AccountService { get; set; }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID)
        {
            var accounts = AccountService.FindAllByCustomerId(customerID);
            var returnAccounts = new List<DebtDestroyer.Model.IAccount>();
            foreach (var account in accounts)
            {
                returnAccounts.Add(new DebtDestroyer.Model.Account
                {
                    _CustomerId = account._CustomerId,
                    _AccountId = account._AccountId,
                    _Balance = account._Balance,
                    _Apr = account._Apr,
                    _MinPay = account._MinPay,
                    _Name = account._Name,
                    _Payment = account._Payment
                });
            }
            return returnAccounts;
        }
    }
}
