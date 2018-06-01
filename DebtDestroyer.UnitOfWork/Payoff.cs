using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtDestroyer;
using DebtDestroyer.DataAccess;

namespace DebtDestroyer.UnitOfWork
{
    public class Payoff
    {
        //private ICustomer _Customer { get; set; }
        private int _CustomerId { get; set; }
        private decimal _AllocatedFunds { get; set; }
        private IEnumerable<IAccount> _Accounts { get; set; }
        private IUnitOfWork _UnitOfWork { get; set; }

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

        public IEnumerable<DebtDestroyer.Model.IAccount> GetAccounts()
        {
            return _UnitOfWork._AccountService.FindAllByCustomerId(_CustomerId);                            
        }

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
                account._Balance += account.DailyInterest() * 30.42m;
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

            ApplyAllAccrued();

            PrioretySort();

            ResetPayments();

            var allocated = _AllocatedFunds;

            var totalMin = TotalMinimumPayments();

            var totalPaid = TotalPayments();

            var totalDaily = TotalDailyInterest();

            foreach (var account in _Accounts)
            {
                var payment = account._Payment = account.DailyInterest() / totalDaily * _AllocatedFunds;

                if (payment < account._MinPay) // must make at least minimum payment
                {
                    payment = account._MinPay;
                }

                if (payment >= account._Balance) // payment more than balance? Just payoff the balance
                {
                    account._Payment = account._Balance;
                    totalPaid += account._Balance;
                    allocated -= account._Balance;
                    account._Balance = 0.00m;
                }
                else // otherwise, make the payment
                {
                    account._Payment = payment;
                    totalPaid += payment;
                    allocated -= payment;
                    account._Balance -= payment;
                    
                }

            }

            if (allocated > 0.00m) // if any money is left over, pay it to the first account that has at least that much balance
            {
                PrioretySort();
                foreach (var account in _Accounts)
                {
                    if (account._Balance >= allocated)
                    {
                        account._Payment += allocated;
                        totalPaid += allocated;
                        account._Balance -= allocated;
                        break;
                    }
                }
            }
        }
        
        public IList<DebtDestroyer.Model.Payment> Generate()
        {
            var payments = new List<DebtDestroyer.Model.Payment>();
            var done = false;
            int month = 0;

            foreach (var account in _Accounts)
            {
                payments.Add(new Model.Payment
                {
                    _Month = month,
                    _CustomerId = account._CustomerId,
                    _AccountId = account._AccountId,
                    _AccountName = account._Name,
                    _Balance = account._Balance,
                    _Payment = account._Payment,
                    _DailyInterest = account.DailyInterest()
                });
            }

            while (!done)
            {
                month++;
                Update();
                foreach (var account in _Accounts)
                {
                    payments.Add(new Model.Payment
                    {
                        _Month = month,
                        _CustomerId = account._CustomerId,
                        _AccountId = account._AccountId,
                        _AccountName = account._Name,
                        _Balance = account._Balance,
                        _Payment = account._Payment,
                        _DailyInterest = account.DailyInterest()
                    });
                }
                _Accounts.ToList().OrderByDescending(a => a._Balance).ToList();
                if (_Accounts.First()._Balance == 0.00m)
                {
                    done = true;
                }

            }
            return payments;
        }
    }
}
