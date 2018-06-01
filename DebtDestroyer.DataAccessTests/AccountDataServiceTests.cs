using Microsoft.VisualStudio.TestTools.UnitTesting;
using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DebtDestroyer.Model;

namespace DebtDestroyer.DataAccess.Tests
{
    [TestClass()]
    public class AccountDataServiceTests
    {

        private Mock<IAccountDataService> _accountService;
        private IList<Account> _accountDb;


        [TestInitialize]
        public void Init()
        {
            _accountDb = new List<Account>()
            {
                new Account {_AccountId = 1, _CustomerId = 1, _Name = "Wells Fargo",
                    _Balance = 2000.00m, _Apr = 0.159f, _MinPay = 25.00m, _Payment = 30.00m},
                new Account {_AccountId = 2, _CustomerId = 1, _Name = "Chase Bank",
                    _Balance = 3000.00m, _Apr = 0.189f, _MinPay = 27.00m, _Payment = 30.00m},
                new Account {_AccountId = 3, _CustomerId = 1, _Name = "Car Loan",
                    _Balance = 4000.00m, _Apr = 0.209f, _MinPay = 225.00m, _Payment = 225.00m},
                new Account {_AccountId = 4, _CustomerId = 2, _Name = "Bank of America",
                    _Balance = 1500.00m, _Apr = 0.229f, _MinPay = 25.00m, _Payment = 100.00m},
                new Account {_AccountId = 5, _CustomerId = 2, _Name = "Student Loan1",
                    _Balance = 20000.00m, _Apr = 0.03f, _MinPay = 225.00m, _Payment = 225.00m},
                new Account {_AccountId = 6, _CustomerId = 2, _Name = "Student Loan2",
                    _Balance = 20000.00m, _Apr = 0.06f, _MinPay = 250.00m, _Payment = 250.00m},
                new Account {_AccountId = 7, _CustomerId = 3, _Name = "Zales Credit",
                    _Balance = 2700.00m, _Apr = 0.299f, _MinPay = 75.00m, _Payment = 100.00m},
                new Account {_AccountId = 8, _CustomerId = 3, _Name = "Capital One",
                    _Balance = 1000.00m, _Apr = 0.139f, _MinPay = 15.00m, _Payment = 15.00m},
                new Account {_AccountId = 9, _CustomerId = 3, _Name = "Best Buy",
                    _Balance = 2200.00m, _Apr = 0.169f, _MinPay = 25.00m, _Payment = 100.00m},
                new Account {_AccountId = 10, _CustomerId = 3, _Name = "American Express",
                    _Balance = 3500.00m, _Apr = 0.199f, _MinPay = 25.00m, _Payment = 100.00m},
            };

            _accountService = new Mock<IAccountDataService>();

            _accountService.Setup(a => a.FindAll()).Returns(
                () =>
                {
                    var accounts = new List<Account>();

                    _accountDb.ToList().ForEach(account =>
                    {
                        accounts.Add(new Account()
                        {
                            _AccountId = account._AccountId,
                            _CustomerId = account._CustomerId,
                            _Name = account._Name,
                            _Balance = account._Balance,
                            _Apr = account._Apr,
                            _MinPay = account._MinPay,
                            _Payment = account._Payment
                        });
                    });

                    return accounts;
                });

            _accountService.Setup(a => a.AddAccount(It.IsAny<Account>())).Callback(
                (Account account) =>
                {
                    if (account == null) throw new InvalidOperationException("Add Account - account was null");
                    if (account._AccountId <= 0)
                    {
                        account._AccountId = _accountDb.Max(a => a._AccountId) + 1;
                        _accountDb.Add(account);
                    }

                });

            _accountService.Setup(a => a.DeleteAccount(It.IsAny<Account>())).Callback(
                (Account account) =>
            {
                var deleteAccount = _accountDb.SingleOrDefault(a => a._AccountId.Equals(account._AccountId));
                _accountDb.Remove(deleteAccount);
            })/*.Verifiable()??*/;

            _accountService.Setup(a => a.EditAccount(It.IsAny<Account>())).Callback(
                (Account account) =>
                {
                    if (account == null) throw new InvalidOperationException("Edit Account - account was null");
                    var updateAccount = _accountDb.SingleOrDefault(a => a._AccountId.Equals(account._AccountId));
                    if (updateAccount == null) throw new InvalidOperationException("Account was null");
                    updateAccount._CustomerId = account._CustomerId;
                    updateAccount._Name = account._Name;
                    updateAccount._Balance = account._Balance;
                    updateAccount._Apr = account._Apr;
                    updateAccount._MinPay = account._MinPay;
                    updateAccount._Payment = account._Payment;
                });

            _accountService.Setup(a => a.FindByName(It.IsAny<string>())).Returns(
                (string name) =>
                {
                    return _accountDb.SingleOrDefault(a => a._Name.Equals(name));

                });

            _accountService.Setup(a => a.FindByID(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return _accountDb.SingleOrDefault(a => a._Name.Equals(id));

                });

            _accountService.Setup(a => a.FindAllByCustomerId(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var customerAccounts = new List<Account>();
                    customerAccounts = _accountDb.ToList().Where(account => account._CustomerId.Equals(id)).ToList();
                    return customerAccounts;
                });
        }

        [TestMethod()]
        public void AddAccountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteAccountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditAccountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindByIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindByNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllByCustomerIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PrioritySortTest()
        {
            Assert.Fail();
        }
    }
}