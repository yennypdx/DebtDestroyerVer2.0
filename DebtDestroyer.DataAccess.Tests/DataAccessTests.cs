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
        private Mock<IAccountDataService> _accountService;
        private IList<Account> _accountDb;

        private Mock<ICustomerDataService> _customerService;
        private IList<Customer> _customerDb;

        [TestInitialize]
        public void Init()
        {
            /*------------- Customer Mocking Starts Here ---------------*/
            _customerDb = new List<Customer>()
            {   //need to add one more param to each cust: _AccountList
                new Customer{_CustomerId = 1, _UserName = "John", _Email = "jsmith@gmail.com",
                    _Password = "pass1234", _AllocatedFund = 345m },
                new Customer{_CustomerId = 2, _UserName = "Bradly", _Email = "bj@gmail.com",
                    _Password = "pass5678", _AllocatedFund = 250m },
                new Customer{_CustomerId = 3, _UserName = "Kendra", _Email = "kendra@gmail.com",
                    _Password = "pass9876", _AllocatedFund = 150m },
                new Customer{_CustomerId = 4, _UserName = "Casey", _Email = "casey@gmail.com",
                    _Password = "pass4321", _AllocatedFund = 450m },
                new Customer{_CustomerId = 5, _UserName = "Francois", _Email = "francis@gmail.com",
                    _Password = "word1234", _AllocatedFund = 345m },
                new Customer{_CustomerId = 6, _UserName = "Danielle", _Email = "d.elle@gmail.com",
                    _Password = "word5678", _AllocatedFund = 225m },
                new Customer{_CustomerId = 7, _UserName = "Moana", _Email = "moana@gmail.com",
                    _Password = "word3456", _AllocatedFund = 520m },
                new Customer{_CustomerId = 8, _UserName = "Robert", _Email = "rob@gmail.com",
                    _Password = "pass1122", _AllocatedFund = 275m },
                new Customer{_CustomerId = 9, _UserName = "Kobe", _Email = "kobe@gmail.com",
                    _Password = "pass3344", _AllocatedFund = 300m },
                new Customer{_CustomerId = 10, _UserName = "Jasmine", _Email = "justmine@gmail.com",
                    _Password = "pass8899", _AllocatedFund = 475m }
            };

            _customerService = new Mock<ICustomerDataService>();

            _customerService.Setup(m => m.GetCustomerById(It.IsAny<int>())).Returns(
                (int custId) =>
                {
                    return _customerDb.Single(f => f._CustomerId.Equals(custId));
                });

            _customerService.Setup(m => m.AddNewCustomer(It.IsAny<Customer>())).Callback(
                (Customer newCust) =>
                {
                    if (newCust == null) throw new InvalidOperationException("Customer is null");

                    if(newCust._CustomerId <= 0)
                    {
                        newCust._CustomerId = _customerDb.Max(f => f._CustomerId) + 1;
                        _customerDb.Add(newCust);
                    }
                    else
                    {
                        var newCustomerToAdd = _customerDb.SingleOrDefault(f => f._CustomerId.Equals(newCust._CustomerId));
                        if(newCustomerToAdd == null) throw new InvalidOperationException("Customer is null");

                        newCustomerToAdd._CustomerId = newCust._CustomerId;
                        newCustomerToAdd._UserName = newCust._UserName;
                        newCustomerToAdd._Email = newCust._Email;
                        newCustomerToAdd._Password = newCust._Password;
                        newCustomerToAdd._AllocatedFund = newCust._AllocatedFund;
                        newCustomerToAdd._AccountList = newCust._AccountList;
                    }
                });

            _customerService.Setup(m => m.DeleteExistingCustomer(It.IsAny<int>())).Callback(
                (int custId) =>
                {
                    var customerToDelete = _customerDb.SingleOrDefault(f => f._CustomerId.Equals(custId));
                    _customerDb.Remove(customerToDelete);
                }).Verifiable();

             _customerService.Setup(m => m.GetAllocatedFunds(It.IsAny<Customer>())).Returns(
                 (decimal fund) =>
                 {
                     var customerFund = _customerDb.SingleOrDefault(f => f._AllocatedFund.Equals(fund));
                     return customerFund._AllocatedFund;
                 });

            _customerService.Setup(m => m.GetCustomerIdByName(It.IsAny<string>())).Returns(
                (int id) =>
                {
                    var customerToSearch = _customerDb.Single(f => f._UserName.Equals(id));
                    return customerToSearch._CustomerId;
                });

            _customerService.Setup(m => m.GetAccounts(It.IsAny<int>())).Returns(
                (ICollection<Account> acct) =>
                {
                    var customerAccounts = new List<Account>();
                    customerAccounts = _accountDb.ToList().Where(account => account._CustomerId.Equals(acct)).ToList();

                    return customerAccounts;
                });

            /*-------------- Account Mocking Starts Here -------------------*/
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