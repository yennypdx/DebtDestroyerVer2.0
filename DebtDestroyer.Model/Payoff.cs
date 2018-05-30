using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtDestroyer;

namespace DebtDestroyer.Model
{
    public class Payoff
    {
        private ICustomer _Customer { get; set; }
        private int _CustomerId { get; set; }
        private decimal _AllocatedFunds { get; set; }
        private IEnumerable<IAccount> _Accounts { get; set; }

        //private IAccountDataService accountDataService { get; set; }

        //private IPayoffDataService payoffDataService { get; set; }

        public Payoff(ICustomer customer)
        {
            _CustomerId = customer._CustomerId;
            _AllocatedFunds = customer._AllocatedFund;
            _Accounts = customer._AccountList;
        }

        public Payoff(int customerId, int allocatedFunds, IEnumerable<IAccount> accounts)
        {
            _CustomerId = customerId;
            _AllocatedFunds = allocatedFunds;
            _Accounts = accounts;
        }

        public Payoff(int customerId, int allocatedFunds)
        {
            _CustomerId = customerId;
            _AllocatedFunds = allocatedFunds;
            _Accounts = null;
        }

        //public IEnumerable<IAccount> GetAccounts()
        //{
        //    return _Accounts = accountDataService.FindAllByCustomerId(_CustomerId);
        //}

        public void PrioretySort()
        {
            _Accounts = _Accounts.ToList().OrderBy(account => account.DailyInterest()).ToList();
        }

        public decimal TotalDailyInterest()
        {
            decimal total = 0.00m;
            foreach (var account in _Accounts)
            {
                total += account.DailyInterest();
            }
            return total;
        }

        public decimal TotalMinimumPayments()
        {
            decimal total = 0.00m;
            foreach (var account in _Accounts)
            {
                total += account._MinPay;
            }
            return total;
        }

        public decimal TotalPayments()
        {
            decimal total = 0.00m;
            foreach (var account in _Accounts)
            {
                total += account._Payment;
            }
            return total;
        }

        public decimal LeftOver()
        {
            return _AllocatedFunds - TotalPayments();
        }

        public void ApplyAllAccrued()
        {
            foreach (var account in _Accounts)
            {
                account._Balance += account.DailyInterest() * 30;
            }
        }

        public void ResetPayments()
        {
            foreach (var account in _Accounts)
            {
                account._Payment = 0.00m;
            }
        }

        public void Update()
        {
            if (_Accounts == null)
                throw new Exception("Accounts = null");
            if (_AllocatedFunds < TotalMinimumPayments())
                throw new Exception("Insufficient Funds");

            PrioretySort();

            ResetPayments();

            var allocated = _AllocatedFunds;

            var totalMin = TotalMinimumPayments();

            var totalPaid = TotalPayments();

            var totalDaily = TotalDailyInterest();

            foreach (var account in _Accounts)
            {
                var payment = account._Payment = account.DailyInterest() / totalDaily * _AllocatedFunds;

                if (payment < account._MinPay)
                {
                    payment = account._MinPay;
                }

                if (payment >= account._Balance)
                {
                    account._Payment = account._Balance;
                    totalPaid += account._Balance;
                    allocated -= account._Balance;
                    account._Balance = 0.00m;
                }
                else
                {
                    account._Payment = payment;
                    totalPaid += payment;
                    allocated -= payment;
                    account._Balance -= payment;
                }

            }

            if (allocated > 0.00m)
            {
                PrioretySort();
                foreach (var account in _Accounts)
                {

                }
            }





        }


    }
}
