using DebtDestroyer.DataAccess;
using DebtDestroyer.Model;
using DebtDestroyer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestoyer.UI
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerDataService CustomerService { get; set; }
        public IAccountDataService AccountService { get; set; }


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
