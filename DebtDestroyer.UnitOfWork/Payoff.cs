﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtDestroyer;
using DebtDestroyer.DataAccess;

namespace DebtDestroyer.UnitOfWork
{
    public class Payoff : IPayoff
    {
        public int _CustomerId { get; set; }
        public decimal _AllocatedFunds { get; set; }
        public IEnumerable<Model.IAccount> _Accounts { get; set; }
        public IUnitOfWork _UnitOfWork { get; set; }

        public Payoff(Model.ICustomer customer)
        {
            _CustomerId = customer._CustomerId;
            _AllocatedFunds = customer._AllocatedFund;
            _Accounts = customer._AccountList;
            _UnitOfWork = new UnitOfWork(new CustomerDataService(), new AccountDataService());
        }

        public Payoff(int customerId, decimal allocatedFunds, IUnitOfWork unitOfWork)
        {
            _CustomerId = customerId;
            _AllocatedFunds = allocatedFunds;
            _Accounts = null;
            _UnitOfWork = unitOfWork;
        }

        public Payoff(int customerId, decimal allocatedFunds)
        {
            _CustomerId = customerId;
            _AllocatedFunds = allocatedFunds;
            _Accounts = null;
        }

        public IEnumerable<DebtDestroyer.Model.IAccount> GetAccounts()
        {
            _Accounts = _UnitOfWork.AccountService.FindAllByCustomerId(_CustomerId);
            return _Accounts;
        }

        public void PrioretySort()
        {
            _Accounts = _Accounts.ToList().OrderByDescending(account => account.DailyInterest()).ToList();
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
                // must make at least minimum payment
                if (payment < account._MinPay) 
                {
                    payment = account._MinPay;
                }
                // payment more than balance? Just payoff the balance
                if (payment >= account._Balance) 
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
            // if any money is left over, pay it to the first account that has at least that much balance
            if (allocated > 0.00m) 
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
            // if paid too much, re-adjust funds
            if (totalPaid > _AllocatedFunds)
            {
                var diff = totalPaid - _AllocatedFunds;
                foreach (var account in _Accounts)
                {
                    if ((account._Payment - account._MinPay) > diff)
                    {
                        account._Payment -= diff;
                        account._Balance += diff;
                        totalPaid -= diff;
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
                
                done = true;
                foreach (var account in _Accounts)
                {
                    if (!account._Balance.Equals(0.00m))
                        done = false;
                }
                
            }
            return payments;
        }
    }
}
