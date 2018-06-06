using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DebtDestroyer.DataAccess;
using DebtDestroyer.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace DebtDestroyer.DataAccess.Tests
{
    [TestClass()]
    public class AccountDataServiceTests
    {
        private Mock<IAccountDataService> _mockAccountService;
        private IList<Account> _accountDb;
        private IAccountDataService _accountDataService;
        private IList<Account> _accountJson;
       
        

        [TestInitialize()]
        public void Init()
        {
            _accountDataService = new AccountDataService();
           
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
                    _Balance = 3500.00m, _Apr = 0.199f, _MinPay = 25.00m, _Payment = 100.00m}
            };

            _accountDataService.SaveToFile(_accountDb);

            _mockAccountService = new Mock<IAccountDataService>();

            _accountJson = _accountDataService.ReadFromFile();

            //_mockAccountService.Object.SavePaymentsToFile(_accountDb);

            _mockAccountService.Setup(a => a.FindAll()).Returns(
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

            _mockAccountService.Setup(a => a.AddAccount(It.IsAny<Account>())).Callback(
                (Account account) =>
                {
                    if (account == null) throw new InvalidOperationException("Add Account - account was null");
                    if (account._AccountId <= 0)
                    {
                        account._AccountId = _accountDb.Max(a => a._AccountId) + 1;
                        _accountDb.Add(account);
                    }

                });

            _mockAccountService.Setup(a => a.DeleteAccount(It.IsAny<Account>())).Callback(
                (Account account) =>
                {
                    var deleteAccount = _accountDb.SingleOrDefault(a => a._AccountId.Equals(account._AccountId));
                    _accountDb.Remove(deleteAccount);
                })/*.Verifiable()??*/;

            _mockAccountService.Setup(a => a.EditAccount(It.IsAny<Account>())).Callback(
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

            _mockAccountService.Setup(a => a.FindByName(It.IsAny<string>())).Returns(
                (string name) =>
                {
                    return _accountDb.SingleOrDefault(a => a._Name.Equals(name));

                });

            _mockAccountService.Setup(a => a.FindByID(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return _accountDb.SingleOrDefault(a => a._AccountId.Equals(id));

                });

            _mockAccountService.Setup(a => a.FindAllByCustomerId(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    var customerAccounts = new List<Account>();
                    customerAccounts = _accountDb.ToList().Where(account => account._CustomerId.Equals(id)).ToList();
                    if (customerAccounts.Count().Equals(0)) throw new InvalidOperationException("No accounts with that id");
                    return customerAccounts;
                });
        }

        //********************** END OF MOCK / INIT() **************************//

        [TestMethod()]
        public void MockAddAccountTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 0,
                _CustomerId = 3,
                _Name = "American Express",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _mockAccountService.Object.AddAccount(tempAccount);

            Assert.AreEqual(_mockAccountService.Object.FindAll().Count(), 11);
        }

        [TestMethod()]
        public void MockAddAccountFailTest()
        {
            try
            {
                Account tempAccount = new Account();
                _mockAccountService.Object.AddAccount(tempAccount);
            }
            catch (InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void MockDeleteAccountTest()
        {
            Assert.AreEqual(_mockAccountService.Object.FindAll().Count(), 10);

            Account tempAccount = new Account
            {
                _AccountId = 10,
                _CustomerId = 3,
                _Name = "American Express",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _mockAccountService.Object.DeleteAccount(tempAccount);

            Assert.AreEqual(_mockAccountService.Object.FindAll().Count(), 9);
        }

        [TestMethod()]
        public void MockDeleteAccountFailTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 11,
                _CustomerId = 3,
                _Name = "American Express",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _mockAccountService.Object.DeleteAccount(tempAccount);

            Assert.AreEqual(_mockAccountService.Object.FindAll().Count(), 10);
        }

        [TestMethod()]
        public void MockEditAccountTest()
        {
            Assert.AreEqual("American Express", _mockAccountService.Object.FindByID(10)._Name.ToString());

            Account tempAccount = new Account
            {
                _AccountId = 10,
                _CustomerId = 3,
                _Name = "Student Loan3",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _mockAccountService.Object.EditAccount(tempAccount);

            Assert.AreEqual("Student Loan3", _mockAccountService.Object.FindByID(10)._Name.ToString());
        }

        [TestMethod()]
        public void MockEditAccountNullFailTest()
        {
            try
            {
                Account tempAccount = new Account();

                _mockAccountService.Object.EditAccount(tempAccount);
            }
            catch (InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof (InvalidOperationException))]
        public void MockEditAccountCantFindFailTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 11,
                _CustomerId = 3,
                _Name = "Student Loan3",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _mockAccountService.Object.EditAccount(tempAccount);
        }

        [TestMethod()]
        public void MockFindAllTest()
        {
            Assert.AreEqual(10, _mockAccountService.Object.FindAll().Count());
        }

        [TestMethod()]
        public void MockFindAllFailTest()
        {
            Assert.AreNotEqual(11, _mockAccountService.Object.FindAll().Count());
        }

        [TestMethod()]
        public void MockFindByIDTest()
        {
            Assert.AreEqual("American Express", _mockAccountService.Object.FindByID(10)._Name.ToString());
        }

        [TestMethod()]
        public void MockFindByIDFailTest()
        {
            Assert.AreNotEqual("American Express", _mockAccountService.Object.FindByID(9)._Name.ToString());
        }

        [TestMethod()]
        public void MockFindByNameTest()
        {
            Assert.AreEqual("American Express", _mockAccountService.Object.FindByName("American Express")._Name.ToString());
        }

        [TestMethod()]
        public void MockFindByNameFailTest()
        {
            var account = _mockAccountService.Object.FindByName("Some Account");
            Assert.IsNull(account);
        }

        [TestMethod()]
        public void MockFindAllByCustomerIdTest()
        {
            Assert.AreEqual(4, _mockAccountService.Object.FindAllByCustomerId(3).Count());
        }

        [TestMethod()]
        [ExpectedException(typeof (InvalidOperationException))]
        public void MockFindAllByCustomerIdFailTest()
        {
            _mockAccountService.Object.FindAllByCustomerId(5);
        }

        // ********** END OF MOCK TESTS **************//

        [TestMethod()]
        public void FindAllTest()
        {
            Assert.AreEqual(10, _accountDataService.FindAll().Count());
        }

        [TestMethod()]
        public void FindAllFailTest()
        {
            Assert.AreNotEqual(11, _accountDataService.FindAll().Count());
        }

        [TestMethod()]
        public void FindByIDTest()
        {
            Assert.AreEqual("American Express", _accountDataService.FindByID(10)._Name.ToString());
        }

        [TestMethod()]
        public void FindByIDFailTest()
        {
            Assert.AreNotEqual("American Express", _accountDataService.FindByID(9)._Name.ToString());
        }

        [TestMethod()]
        public void FindByNameTest()
        {
            Assert.AreEqual("American Express", _accountDataService.FindByName("American Express")._Name.ToString());
        }

        [TestMethod()]
        public void FindByNameFailTest()
        {
            var account = _accountDataService.FindByName("Some Account");
            Assert.IsNull(account);
        }

        [TestMethod()]
        public void FindAllByCustomerIdTest()
        {
            var accounts = _accountDataService.FindAllByCustomerId(2);
            Assert.AreEqual(3, accounts.Count());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindAllByCustomerIdFailTest()
        {
            _accountDataService.FindAllByCustomerId(5);
        }
        //*************************//

        [TestMethod()]
        public void AddAccountTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 0,
                _CustomerId = 3,
                _Name = "American Express",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _accountDataService.AddAccount(tempAccount);
            var size = _accountDataService.FindAll().Count();

            Assert.AreEqual(11,size);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddAccountFailTest()
        {
            Account tempAccount = null;
            _accountDataService.AddAccount(tempAccount);
        }

        [TestMethod()]
        public void DeleteAccountTest()
        {
           
            var deleteAccount = _accountDataService.FindAll().ToList().Last();

            _accountDataService.DeleteAccount(deleteAccount);

            Assert.AreEqual(9, _accountDataService.FindAll().Count());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteAccountFailTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 11,
                _CustomerId = 3,
                _Name = "American Express",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _accountDataService.DeleteAccount(tempAccount);

        }

        [TestMethod()]
        public void EditAccountTest()
        {

            Account tempAccount = new Account
            {
                _AccountId = 10,
                _CustomerId = 3,
                _Name = "Student Loan3",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _accountDataService.EditAccount(tempAccount);

            Assert.AreEqual("Student Loan3", _accountDataService.FindByID(10)._Name.ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof (InvalidOperationException))]
        public void EditAccountNullFailTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 11,
                _CustomerId = 3,
                _Name = "Student Loan3",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };
            _accountDataService.EditAccount(tempAccount);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EditAccountCantFindFailTest()
        {
            Account tempAccount = new Account
            {
                _AccountId = 11,
                _CustomerId = 3,
                _Name = "Student Loan3",
                _Balance = 3500.00m,
                _Apr = 0.199f,
                _MinPay = 25.00m,
                _Payment = 100.00m
            };

            _accountDataService.EditAccount(tempAccount);
        }
    }

        

        
}