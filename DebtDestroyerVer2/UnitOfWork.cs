using DebtDestroyer.DataAccess;
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
        public ICustomerDataService customerService { get; set; }
        public IAccountDataService accountService { get; set; }


        public IEnumerable<DebtDestroyer.Model.IAccount> getCustomerAccounts(int customerID)
        {
            var accounts = accountService.FindAllByCustomerId(customerID);
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
