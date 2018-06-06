using Microsoft.VisualStudio.TestTools.UnitTesting;
using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DebtDestroyer.Model;
using DebtDestroyer.UnitOfWork;
using DebtDestoyer.UI;

namespace DebtDestroyer.DataAccess.Tests
{
    [TestClass()]
    public class PayoffDataServiceTests
    {

        private Mock<IPayoffDataService> _mockService;
        private IList<Payment> _paymentDb;
        private IPayoffDataService _payoffService;
        private IList<Payment> _paymentsJson;

        [TestInitialize]
        public void Init()
        {
            _payoffService = new PayoffDataService();
            _paymentsJson = new List<Payment>();
            _paymentsJson = _payoffService.ReadFromFile();

            var customerAccounts = new List<Account>()
            {
                new Account {_AccountId = 1, _CustomerId = 1, _Name = "Wells Fargo",
                    _Balance = 2000.00m, _Apr = 0.159f, _MinPay = 25.00m, _Payment = 0.00m},
                new Account {_AccountId = 2, _CustomerId = 1, _Name = "Chase Bank",
                    _Balance = 3000.00m, _Apr = 0.189f, _MinPay = 27.00m, _Payment = 0.00m},
                new Account {_AccountId = 3, _CustomerId = 1, _Name = "Car Loan",
                    _Balance = 4000.00m, _Apr = 0.209f, _MinPay = 225.00m, _Payment = 0.00m}
            };
            var customer = new Customer() { _CustomerId = 1, _UserName = "Tim Capehart", _Email = "tcapehart@gmail.com",
                    _Password = "mypassword", _AllocatedFund = 350.00m, _AccountList = customerAccounts };

            var customerPayoff = new UnitOfWork.Payoff(customer);

            _paymentDb = customerPayoff.Generate();

            
            _mockService = new Mock<IPayoffDataService>();
            //_mockService.Object.SaveToFile(_paymentDb);



            _mockService.Setup(p => p.FindAll()).Returns(
                () =>
                {
                    var payments = new List<Payment>();
                    _paymentDb.ToList().ForEach(payment =>
                    {
                        payments.Add(new Payment()
                        {
                            _Month = payment._Month,
                            _CustomerId = payment._CustomerId,
                            _AccountId = payment._AccountId,
                            _AccountName = payment._AccountName,
                            _Balance = payment._Balance,
                            _Payment = payment._Payment,
                            _DailyInterest = payment._DailyInterest
                        });
                    });
                    return payments;
                });

            _mockService.Setup(p => p.FindAllByCustomerId(It.IsAny<int>())).Returns(
                (int customerId) =>
                {
                    return _paymentDb.ToList().Where(p => p._CustomerId.Equals(customerId)).ToList();

                });

            _mockService.Setup(p => p.FindAllByAccountId(It.IsAny<int>())).Returns(
                (int accountId) =>
                {
                    return _paymentDb.ToList().Where(p => p._AccountId.Equals(accountId)).ToList();

                });

            _mockService.Setup(p => p.FindAllByMonth(It.IsAny<int>())).Returns(
                (int month) =>
                {
                    return _paymentDb.ToList().Where(p => p._Month.Equals(month)).ToList();

                });

            _mockService.Setup(p => p.FindPaymentByMonthAndAccountId(It.IsAny<int>(), It.IsAny<int>())).Returns(
                (int month, int accountId) =>
                {
                    var monthPayments = new List<Payment>();
                    monthPayments = _paymentDb.ToList().Where(p => p._Month.Equals(month)).ToList();
                    return monthPayments.SingleOrDefault(p => p._AccountId.Equals(accountId));

                });



        }

        // ******************** End of Mock Setup / Init() **************************//

        [TestMethod()]
        public void MockFindAllTest() 
        {
            var payments = _mockService.Object.FindAll();
            Assert.IsTrue(payments.ToList().LastOrDefault()._Balance == 0.00m && payments.ToList().FirstOrDefault()._Balance > 0.00m);
        }

        [TestMethod()]
        public void MockFindAllByCustomerIdTestValid()
        {
            var payments = _mockService.Object.FindAllByCustomerId(1);
            bool pass = true;

            if (payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._CustomerId.Equals(1))
                    pass = false;
            }

            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void MockFindAllByCustomerIdTestInvalidId()
        {
            var payments = _mockService.Object.FindAllByCustomerId(2);
            bool pass = true;

            if (payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._CustomerId.Equals(2))
                    pass = false;
            }

            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void MockFindAllByAccountIdTestValid()
        {
            var payments = _mockService.Object.FindAllByAccountId(2);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._AccountId.Equals(2))
                    pass = false;
            }
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void MockFindAllByAccountIdTestInvalid()
        {
            var payments = _mockService.Object.FindAllByAccountId(4);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._AccountId.Equals(4))
                    pass = false;
            }
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void MockFindAllByMonthTestValid()
        {
            var payments = _mockService.Object.FindAllByMonth(8);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._Month.Equals(8))
                    pass = false;
            }
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void MockFindAllByMonthTestInvalid()
        {
            var payments = _mockService.Object.FindAllByMonth(100);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._Month.Equals(100))
                    pass = false;
            }
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void MockFindPaymentByMonthAndAccountIdTestValid()
        {
            var payment = _mockService.Object.FindPaymentByMonthAndAccountId(8, 3);
            
            Assert.IsTrue(payment._Month.Equals(8) && payment._AccountId.Equals(3));
        }

        [TestMethod()]
        public void MockFindPaymentByMonthAndAccountIdTestInvalidMonth()
        {
            var payment = _mockService.Object.FindPaymentByMonthAndAccountId(-1, 3);

            Assert.IsNull(payment);
        }

        [TestMethod()]
        public void MockFindPaymentByMonthAndAccountIdTestInvalidAccount()
        {
            var payment = _mockService.Object.FindPaymentByMonthAndAccountId(8, 5);

            Assert.IsNull(payment);
        }

        // END OF MOCK TESTS //

        [TestMethod()]
        public void ReadFromFileTest()
        {
            // ReadFromFile() is called in Init()
            Assert.IsTrue(_paymentsJson.Count().Equals(81));
        }

        [TestMethod()]
        public void FindAllTest()
        {
            var payments = _payoffService.FindAll();
            Assert.IsTrue(payments.Count().Equals(81));
        }

        [TestMethod()]
        public void FindAllByCustomerIdTestValid()
        {
            var payments = _payoffService.FindAllByCustomerId(1);
            bool pass = true;

            if (payments == null || payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._CustomerId.Equals(1))
                    pass = false;
            }

            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void FindAllByCustomerIdTestInvalidId()
        {
            var payments = _payoffService.FindAllByCustomerId(2);
            bool pass = true;

            if (payments == null || payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._CustomerId.Equals(2))
                    pass = false;
            }

            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void FindAllByAccountIdTestValid()
        {
            var payments = _payoffService.FindAllByAccountId(2);
            bool pass = true;
            if (payments == null || payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._AccountId.Equals(2))
                    pass = false;
            }
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void FindAllByAccountIdTestInvalid()
        {
            var payments = _payoffService.FindAllByAccountId(4);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;

            foreach (var p in payments)
            {
                if (!p._AccountId.Equals(4))
                    pass = false;
            }
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void FindAllByMonthTestValid()
        {
            var payments = _payoffService.FindAllByMonth(8);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._Month.Equals(8))
                    pass = false;
            }
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void FindAllByMonthTestInvalid()
        {
            var payments = _payoffService.FindAllByMonth(100);
            bool pass = true;
            if (payments.Count().Equals(0)) pass = false;
            foreach (var p in payments)
            {
                if (!p._Month.Equals(100))
                    pass = false;
            }
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void FindPaymentByMonthAndAccountIdTestValid()
        {
            var payment = _payoffService.FindPaymentByMonthAndAccountId(8, 3);

            Assert.IsTrue(payment._Month.Equals(8) && payment._AccountId.Equals(3));
        }

        [TestMethod()]
        public void FindPaymentByMonthAndAccountIdTestInvalidMonth()
        {
            var payment = _payoffService.FindPaymentByMonthAndAccountId(-1, 3);

            Assert.IsNull(payment);
        }

        [TestMethod()]
        public void FindPaymentByMonthAndAccountIdTestInvalidAccount()
        {
            var payment = _payoffService.FindPaymentByMonthAndAccountId(8, 5);

            Assert.IsNull(payment);
        }

    }
}