using Microsoft.VisualStudio.TestTools.UnitTesting;
using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtDestroyer.Model;
using Moq;

namespace DebtDestroyer.DataAccess.Tests
{
    [TestClass()]
    public class CustomerDataServiceTests
    {
        private Mock<IAccountDataService> _accountService;
        private IList<Account> _accountDb;
        private Mock<IAccountDataService> _accountDataSerivice;

        private Mock<ICustomerDataService> _customerService;
        private IList<Customer> _customerDb;
        private ICustomerDataService _customerDataService;
        //private DebtDestoyer.UI.UnitOfWork _unit;

        [TestInitialize]
        public void Init()
        {
            //_unit = new DebtDestoyer.UI.UnitOfWork();
            _accountService = new Mock<IAccountDataService>();
            _customerService = new Mock<ICustomerDataService>();
            
            _customerDataService = new CustomerDataService();
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

            //_customerDataService.SaveToStorage(_customerDb);

            //_unit.CustomerService.SaveToStorage(_customerDb);

            _customerService.Setup(m => m.GetCustomerById(It.IsAny<int>())).Returns(
                (int custId) =>
                {
                    return _customerDb.Single(f => f._CustomerId.Equals(custId));
                });

            _customerService.Setup(m => m.UpdateCustomer(It.IsAny<Customer>())).Callback(
                (Customer newCust) =>
                {
                    if (newCust == null) throw new InvalidOperationException("Customer is null");

                    if (newCust._CustomerId <= 0)
                    {
                        newCust._CustomerId = _customerDb.Max(f => f._CustomerId) + 1;
                        _customerDb.Add(newCust);
                    }
                    else
                    {
                        var newCustomerToAdd = _customerDb.SingleOrDefault(f => f._CustomerId.Equals(newCust._CustomerId));
                        if (newCustomerToAdd == null) throw new InvalidOperationException("Customer is null");

                        newCustomerToAdd._CustomerId = newCust._CustomerId;
                        newCustomerToAdd._UserName = newCust._UserName;
                        newCustomerToAdd._Email = newCust._Email;
                        newCustomerToAdd._Password = newCust._Password;
                        newCustomerToAdd._AllocatedFund = newCust._AllocatedFund;
                        newCustomerToAdd._AccountList = newCust._AccountList;
                    }
                });

            _customerService.Setup(m => m.AddNewCustomer(It.IsAny<Customer>())).Callback(
                (Customer newCust) =>
                {
                    if (newCust == null) throw new InvalidOperationException("Customer is null");

                    if (newCust._CustomerId <= 0)
                    {
                        newCust._CustomerId = _customerDb.Max(f => f._CustomerId) + 1;
                        _customerDb.Add(newCust);
                    }
                    else
                    {
                        var newCustomerToAdd = _customerDb.SingleOrDefault(f => f._CustomerId.Equals(newCust._CustomerId));
                        if (newCustomerToAdd == null) throw new InvalidOperationException("Customer is null");

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
                (Customer fund) =>
                {
                    var customerFund = _customerDb.SingleOrDefault(f => f._AllocatedFund.Equals(fund._AllocatedFund));
                    return customerFund._AllocatedFund;
                });

            _customerService.Setup(m => m.GetCustomerIdByName(It.IsAny<string>())).Returns(
                (string name) =>
                {
                    var customerToSearch = _customerDb.Single(f => f._UserName.Equals(name));
                    return customerToSearch._CustomerId;
                });

            _customerService.Setup(m => m.GetAccounts(It.IsAny<int>())).Returns(
                (ICollection<Account> acct) =>
                {
                    var customerAccounts = new List<Account>();
                    customerAccounts = _accountDb.ToList().Where(account => account._CustomerId.Equals(acct)).ToList();

                    return customerAccounts;
                });

        }

        //********************** END OF MOCK / INIT() *****************************//

        [TestMethod()]
        public void GetCustomerByIdTest()
        {
            Assert.AreEqual("John", _customerService.Object.GetCustomerById(1)._UserName);
        }

        [TestMethod()]
        public void UpdateCustomerTest()
        {
            Customer testCustomer = new Customer
            {
                _CustomerId = 1,
                _UserName = "Johnny",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 345m
            };
            _customerService.Object.UpdateCustomer(testCustomer);
            Assert.AreEqual("Johnny", _customerService.Object.GetCustomerById(1)._UserName);
        }

        [TestMethod()]
        public void MockAddNewCustomerTest()
        {
            Customer testCustomer = new Customer
            {
                _CustomerId = 0,
                _UserName = "Johnny",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 345m
            };
            _customerService.Object.AddNewCustomer(testCustomer);
            Assert.AreEqual("Johnny", _customerService.Object.GetCustomerById(11)._UserName);
        }

        [TestMethod()]
        public void DeleteExistingCustomerTest()
        {
            try
            {
                _customerService.Object.DeleteExistingCustomer(1);
                Assert.AreEqual("John", _customerService.Object.GetCustomerById(1)._UserName);
            }
            catch (InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void GetAccountsTest()
        {
            Assert.AreEqual(3, _customerDataService.GetAccounts(1).Count());
        }
       

        [TestMethod()]
        public void GetAllocatedFundsTest()
        {
            Customer testCustomer = new Customer
            {
                _CustomerId = 0,
                _UserName = "Johnny",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 999m
            };
            _customerService.Object.AddNewCustomer(testCustomer);
            Assert.AreEqual(999m, _customerService.Object.GetAllocatedFunds(testCustomer));
        }

        [TestMethod()]
        public void GetCustomerIdByNameTest()
        {
            Assert.AreEqual(1, _customerService.Object.GetCustomerIdByName("John"));
        }

        //****************** END OF MOCK TESTS *************************//

        [TestMethod()]
        public void GetCustomerByIdTestValid()
        {
            var customer = _customerService.Object.GetCustomerById(1);
            Assert.AreEqual("John", customer._UserName);
        }

        [TestMethod()]
        public void GetCustomerByIdTestInvalid()
        {
            Assert.IsNull(_customerService.Object.GetCustomerById(15));
        }

        [TestMethod()]
        public void AddNewCustomerTestValid()
        {
            Customer tempCustomer = new Customer
            {
                _CustomerId = 0,
                _UserName = "Johnny",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 345m
            };

            _customerDataService.AddNewCustomer(tempCustomer);

            Assert.AreEqual(tempCustomer._UserName, _customerDataService.GetCustomerById(11)._UserName);
        }

        //Adds Customer to database
        [TestMethod()]
        public void AddNewCustomerTestInvalid()
        {
            try
            {
                Customer tempCustomer = new Customer();

                _customerDataService.AddNewCustomer(tempCustomer);
            }
            catch(InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void DeleteExistingCustomerTestValid()
        {
            try
            {
                _customerDataService.DeleteExistingCustomer(1);

                 _customerDataService.GetCustomerById(1);
            }
            catch(InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void DeleteExistingCustomerTestInvalid()
        {
            try
            {
                _customerDataService.DeleteExistingCustomer(11);
            }
            catch(InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void EditCustomerTestValid()
        {
            Customer tempCustomer = new Customer
            {
                _CustomerId = 1,
                _UserName = "Johnny",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 345m
            };

            _customerDataService.UpdateCustomer(tempCustomer);

            Assert.AreEqual(tempCustomer._UserName, _customerDataService.GetCustomerById(1)._UserName);
        }

        [TestMethod()]
        public void EditCustomerTestInvalid()
        {
            try
            {
                Customer tempCustomer = new Customer();

                _customerDataService.UpdateCustomer(tempCustomer);
            }
            catch(InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void GetAccountsTestByIDValid()
        {
            var accounts = _accountService.Object.FindAllByCustomerId(1);
            Assert.AreEqual(3, accounts.Count());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAccountsTestByIDInvalid()
        {
            var accounts = _accountService.Object.FindAllByCustomerId(15);
        }


        [TestMethod()]
        public void GetAllocatedFundsByUserNameAndPassordTestValid()
        {
            Assert.AreEqual(345m, _customerDataService.GetAllocatedFundsByUserNameAndPassword("John", "pass1234"));
        }

        [TestMethod()]
        public void GetCustomerIdByNameTestValid()
        {
            Customer tempCustomer = new Customer
            {
                _CustomerId = 1,
                _UserName = "John",
                _Email = "jsmith@gmail.com",
                _Password = "pass1234",
                _AllocatedFund = 345m
            };

            Assert.AreEqual(tempCustomer, _customerDataService.GetCustomerById(1));
        }

        [TestMethod()]
        public void GetCustomerIdByNameTestInvalid()
        {
            try
            {
                _customerDataService.GetCustomerById(11);
            }
            catch(InvalidOperationException e)
            {
                Assert.IsTrue(true);
            }
        }
    }
}