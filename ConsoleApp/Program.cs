using DebtDestoyer.UI.DataProvider;
using DebtDestroyer.DataAccess;
using DebtDestroyer.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
        //    PayoffDataService _payoffDataService = new PayoffDataService();
        //    AccountDataService _accountDataService = new AccountDataService();
        //    CustomerDataService _customerDataService = new CustomerDataService();
            

        //    int currentMonth = 0;
            
        //    //var payoffPath = "PayoffDatabase";
        //    //var accountPath = "AccountDatabase.json";
        //    //var customerPath = "PayoffDatabase.json";

        //    var accountdB = new List<Account>()
        //    {
        //        new Account {_AccountId = 1, _CustomerId = 1, _Name = "Wells Fargo",
        //            _Balance = 2000.00m, _Apr = 0.159f, _MinPay = 25.00m, _Payment = 30.00m},
        //        new Account {_AccountId = 2, _CustomerId = 1, _Name = "Chase Bank",
        //            _Balance = 3000.00m, _Apr = 0.189f, _MinPay = 27.00m, _Payment = 30.00m},
        //        new Account {_AccountId = 3, _CustomerId = 1, _Name = "Car Loan",
        //            _Balance = 4000.00m, _Apr = 0.209f, _MinPay = 225.00m, _Payment = 225.00m},
        //        new Account {_AccountId = 4, _CustomerId = 2, _Name = "Bank of America",
        //            _Balance = 1500.00m, _Apr = 0.229f, _MinPay = 25.00m, _Payment = 100.00m},
        //        new Account {_AccountId = 5, _CustomerId = 2, _Name = "Student Loan1",
        //            _Balance = 20000.00m, _Apr = 0.03f, _MinPay = 225.00m, _Payment = 225.00m},
        //        new Account {_AccountId = 6, _CustomerId = 2, _Name = "Student Loan2",
        //            _Balance = 20000.00m, _Apr = 0.06f, _MinPay = 250.00m, _Payment = 250.00m},
        //        new Account {_AccountId = 7, _CustomerId = 3, _Name = "Zales Credit",
        //            _Balance = 2700.00m, _Apr = 0.299f, _MinPay = 75.00m, _Payment = 100.00m},
        //        new Account {_AccountId = 8, _CustomerId = 3, _Name = "Capital One",
        //            _Balance = 1000.00m, _Apr = 0.139f, _MinPay = 15.00m, _Payment = 15.00m},
        //        new Account {_AccountId = 9, _CustomerId = 3, _Name = "Best Buy",
        //            _Balance = 2200.00m, _Apr = 0.169f, _MinPay = 25.00m, _Payment = 100.00m},
        //        new Account {_AccountId = 10, _CustomerId = 3, _Name = "American Express",
        //            _Balance = 3500.00m, _Apr = 0.199f, _MinPay = 25.00m, _Payment = 100.00m}
        //    };

        //    var customers = new List<Customer>()
        //    {
        //        new Customer{_CustomerId = 1, _UserName = "John", _Email = "jsmith@gmail.com",
        //            _Password = "pass1234", _AllocatedFund = 345m },
        //        new Customer{_CustomerId = 2, _UserName = "Bradly", _Email = "bj@gmail.com",
        //            _Password = "pass5678", _AllocatedFund = 250m },
        //        new Customer{_CustomerId = 3, _UserName = "Kendra", _Email = "kendra@gmail.com",
        //            _Password = "pass9876", _AllocatedFund = 150m },
        //        new Customer{_CustomerId = 4, _UserName = "Casey", _Email = "casey@gmail.com",
        //            _Password = "pass4321", _AllocatedFund = 450m },
        //        new Customer{_CustomerId = 5, _UserName = "Francois", _Email = "francis@gmail.com",
        //            _Password = "word1234", _AllocatedFund = 345m },
        //        new Customer{_CustomerId = 6, _UserName = "Danielle", _Email = "d.elle@gmail.com",
        //            _Password = "word5678", _AllocatedFund = 225m },
        //        new Customer{_CustomerId = 7, _UserName = "Moana", _Email = "moana@gmail.com",
        //            _Password = "word3456", _AllocatedFund = 520m },
        //        new Customer{_CustomerId = 8, _UserName = "Robert", _Email = "rob@gmail.com",
        //            _Password = "pass1122", _AllocatedFund = 275m },
        //        new Customer{_CustomerId = 9, _UserName = "Kobe", _Email = "kobe@gmail.com",
        //            _Password = "pass3344", _AllocatedFund = 300m },
        //        new Customer{_CustomerId = 10, _UserName = "Jasmine", _Email = "justmine@gmail.com",
        //            _Password = "pass8899", _AllocatedFund = 475m }
        //    };


        //    var customerAccounts = new List<Account>()
        //    {
        //        new Account {_AccountId = 1, _CustomerId = 1, _Name = "Wells Fargo",
        //            _Balance = 2000.00m, _Apr = 0.159f, _MinPay = 25.00m, _Payment = 0.00m},
        //        new Account {_AccountId = 2, _CustomerId = 1, _Name = "Chase Bank",
        //            _Balance = 3000.00m, _Apr = 0.189f, _MinPay = 27.00m, _Payment = 0.00m},
        //        new Account {_AccountId = 3, _CustomerId = 1, _Name = "Car Loan_1",
        //            _Balance = 4000.00m, _Apr = 0.209f, _MinPay = 125.00m, _Payment = 0.00m}
        //    };

        //    var customer = new Customer()
        //    {
        //        _CustomerId = 1,
        //        _UserName = "Tim Capehart",
        //        _Email = "tcapehart@gmail.com",
        //        _Password = "mypassword",
        //        _AllocatedFund = 350.00m,
        //        _AccountList = customerAccounts
        //    };

        //    _customerDataService.SaveToStorage(customers);

        //    _accountDataService.SaveToFile(accountdB);

        //    var customerPayoff = new Payoff(customer);
        //    var paymentStrings = new List<string>();
        //    var _paymentDb = customerPayoff.Generate();
        //    _payoffDataService.SavePaymentsToFile(_paymentDb);
        //    var sortedPayments = _paymentDb.ToList().OrderBy(p => p._Month).ThenByDescending(pay => pay._DailyInterest).ToList();
        //    foreach (var payment in sortedPayments)
        //    {
        //        if(payment._Month > currentMonth)
        //        {
        //            Console.WriteLine(" ");
        //        }
        //        Console.WriteLine(payment.ToString());
        //        paymentStrings.Add(payment.ToString());
        //        currentMonth = payment._Month;
        //    }
        //    //File.Delete(path);
        //    //File.AppendAllLines(path, paymentStrings);
        }
    }
}
