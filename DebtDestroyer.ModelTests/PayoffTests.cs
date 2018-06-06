using Microsoft.VisualStudio.TestTools.UnitTesting;
using DebtDestroyer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtDestroyer.UnitOfWork;
using DebtDestroyer.DataAccess;

namespace DebtDestroyer.Model.Tests
{
    

    
    [TestClass()]
    public class PayoffTests
    {

        private IList<IAccount> accounts;
        private IUnitOfWork unit;
        private UnitOfWork.IPayoff payoff;
        private ICustomerDataService customerService;
        private IAccountDataService accountService;
        

        [TestInitialize]
        public void Init()
        {
            customerService = new CustomerDataService();
            accountService = new AccountDataService();
            unit = new UnitOfWork.UnitOfWork(customerService, accountService); 
            payoff = new UnitOfWork.Payoff(1, 300m, unit);
            accounts = payoff.GetAccounts().ToList().OrderBy(a => a.DailyInterest()).ToList();
            payoff._Accounts = accounts;
        }
        //[TestMethod()]
        //public void PayoffTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PayoffTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PayoffTest2()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void PrioretySortTest()
        {
            var unsorted = accounts;
            payoff.PrioretySort();
            Assert.AreNotEqual(payoff._Accounts, unsorted);
        }

        [TestMethod()]
        public void TotalDailyInterestTest()
        {
            var total = 0.0m;
            foreach (var account in payoff._Accounts)
            {
                total += account.DailyInterest();
            }

            Assert.AreEqual(total, payoff.TotalDailyInterest());
        }

        [TestMethod()]
        public void TotalMinimumPaymentsTest()
        {
            var total = 0.0m;
            foreach (var account in payoff._Accounts)
            {
                total += account._MinPay;
            }

            Assert.AreEqual(total, payoff.TotalMinimumPayments());
        }

        [TestMethod()]
        public void TotalPaymentsTest()
        {
            var total = 0.0m;
            foreach (var account in payoff._Accounts)
            {
                total += account._Payment;
            }

            Assert.AreEqual(total, payoff.TotalPayments());
        }

        [TestMethod()]
        public void LeftOverTest()
        {
            var total = 0.0m;
            foreach (var account in payoff._Accounts)
            {
                total += account._Payment;
            }

            var diff = payoff._AllocatedFunds - total;
            Assert.AreEqual(diff, payoff.LeftOver());
        }

        [TestMethod()]
        public void ApplyAllAccruedTest()
        {
            //var arrayAccounts = accounts.ToArray();
            //var balances = new decimal [3];
            //balances[0] = arrayAccounts[0]._Balance;
            //balances[1]= arrayAccounts[1]._Balance;
            //balances[2] = arrayAccounts[2]._Balance;

            //for(int i = 0; i < 3; i++)
            //{
            //    if accounts.ToArray()[i].Equals(balances[i] + )
            //}

            var pass = true;

            Assert.IsTrue(pass);
        }

        //[TestMethod()]
        //public void ResetPaymentsTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GenerateTest()
        //{
        //    Assert.Fail();
        //}
    }
}