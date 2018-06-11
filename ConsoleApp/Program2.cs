using Autofac;
using ConsoleApp.Startup;
using DebtDestroyer.DataAccess;
using DebtDestroyer.Model;
using DebtDestroyer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program2
    {
        static void Main(string[] args)
        {
            
            var bootStrapper = new Bootstrapper();
            var container = bootStrapper.BootStrap();
            var customerRepository = container.Resolve<ICustomerDataService>();
            var accountRepository = container.Resolve<IAccountDataService>();
            var payoffRepository = container.Resolve<IPayoffDataService>();


            //Clear Database Files
            customerRepository.Dispose();
            accountRepository.Dispose();
            payoffRepository.Dispose();

            // Accounts to populate AccountDatabase.json
            var accountdB = new List<Account>()
            {
                new Account {_AccountId = 1, _CustomerId = 1, _Name = "Wells Fargo",
                    _Balance = 2000.00m, _Apr = 0.159f, _MinPay = 25.00m, _Payment = 0.00m},
                new Account {_AccountId = 2, _CustomerId = 1, _Name = "Chase Bank ",
                    _Balance = 3000.00m, _Apr = 0.189f, _MinPay = 27.00m, _Payment = 0.00m},
                new Account {_AccountId = 3, _CustomerId = 1, _Name = "Car Loan   ",
                    _Balance = 4000.00m, _Apr = 0.209f, _MinPay = 125.00m, _Payment = 0.00m},
                new Account {_AccountId = 4, _CustomerId = 2, _Name = "Bank of America",
                    _Balance = 1500.00m, _Apr = 0.229f, _MinPay = 25.00m, _Payment = 0.00m},
                new Account {_AccountId = 5, _CustomerId = 2, _Name = "Example Loan1",
                    _Balance = 2000.00m, _Apr = 0.03f, _MinPay = 50.00m, _Payment = 0.00m},
                new Account {_AccountId = 6, _CustomerId = 2, _Name = "Example Loan2",
                    _Balance = 3000.00m, _Apr = 0.06f, _MinPay = 69.00m, _Payment = 0.00m},
                new Account {_AccountId = 7, _CustomerId = 3, _Name = "Zales Credit",
                    _Balance = 2700.00m, _Apr = 0.299f, _MinPay = 75.00m, _Payment = 0.00m},
                new Account {_AccountId = 8, _CustomerId = 3, _Name = "Capital One",
                    _Balance = 1000.00m, _Apr = 0.139f, _MinPay = 15.00m, _Payment = 0.00m},
                new Account {_AccountId = 9, _CustomerId = 3, _Name = "Best Buy",
                    _Balance = 2200.00m, _Apr = 0.169f, _MinPay = 25.00m, _Payment = 0.00m},
                new Account {_AccountId = 10, _CustomerId = 3, _Name = "American Express",
                    _Balance = 3500.00m, _Apr = 0.199f, _MinPay = 25.00m, _Payment = 0.00m}
            };

            // Add Accounts to Account Database
            foreach (var account in accountdB)
            {
                accountRepository.AddAccount(account);
            }

            // Customers to populate CustomerDatabase.json
            var customers = new List<Customer>()
            {
                new DebtDestroyer.Model.Customer{_CustomerId = 1, _UserName = "John", _Email = "jsmith@gmail.com",
                    _Password = "pass1234", _AllocatedFund = 300m },
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

            // Add Customers to Customer Database
            foreach (var c in customers)
            {
                customerRepository.AddNewCustomer(c);
            }

            // Retrieve Customer 2 from Customer Database
            var customer = customerRepository.GetCustomerById(2);

            // Give customer info to Payoff
            var customerPayoff = new DebtDestroyer.UnitOfWork.Payoff(customer);

            // Get Customer's accounts from Account Database
            customer._AccountList = customerPayoff.GetAccounts();

            // Generate Payoff plan
            var _paymentDb = customerPayoff.Generate();
            
            // Write Payoff plan to Payoff Database 
            payoffRepository.SavePaymentsToFile(_paymentDb);

            // For display
            var sortedPayments = payoffRepository.FindAllPayments().ToList().OrderBy(p => p._Month).ThenByDescending(pay => pay._DailyInterest).ToList();

            var paymentsByMonth = payoffRepository.FindAllByMonth(6);

            var paymentsByAccount_1 = payoffRepository.FindAllByAccountId(4);
            var paymentsByAccount_2 = payoffRepository.FindAllByAccountId(5);
            var paymentsByAccount_3 = payoffRepository.FindAllByAccountId(6);

            var paymentByMonthAndAccount = payoffRepository.FindPaymentByMonthAndAccountId(6, 5);
            
            // Display
            int currentMonth = 0;
            Console.WriteLine("*** All Payments on All Accounts *** \n");
            foreach (var payment in sortedPayments)
            {
                if (payment._Month > currentMonth)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine(payment.ToString());
                currentMonth = payment._Month;
            }

            Console.WriteLine("\n\n*** All Payments for Account 4 *** \n");
            foreach (var payment in paymentsByAccount_1)
            {
                Console.WriteLine(payment.ToString());
            }

            Console.WriteLine("\n\n*** All Payments for Account 5 *** \n");
            foreach (var payment in paymentsByAccount_2)
            {
                Console.WriteLine(payment.ToString());
            }

            Console.WriteLine("\n\n*** All Payments for Account 6 *** \n");
            foreach (var payment in paymentsByAccount_3)
            {
                Console.WriteLine(payment.ToString());
            }

            Console.WriteLine("\n\n*** All Payments for Month 6 *** \n");
            foreach (var payment in paymentsByMonth)
            {
                Console.WriteLine(payment.ToString());
            }

            Console.WriteLine("\n\n*** Payment: Month 6, Account 2 *** \n");
            Console.WriteLine(paymentByMonthAndAccount.ToString());


            

            Console.WriteLine("\n\n*** All Accounts after delete Account 5 *** \n");

            Console.ReadKey();

            // Delete an account
            accountRepository.DeleteAccount(accountRepository.FindByID(5));

            var updatedAccounts = accountRepository.FindAllByCustomerId(2);

            foreach (var account in updatedAccounts)
            {
                Console.WriteLine(account.ToString());
            }
            
            var newAccount = new Account
            {
                _AccountId = 5,
                _CustomerId = 2,
                _Name = "Capital One",
                _Balance = 1000.00m,
                _Apr = 0.139f,
                _MinPay = 15.00m,
                _Payment = 0.00m
            };

            // Add an account
            accountRepository.AddAccount(newAccount);
            
            Console.WriteLine("\n\n*** All Accounts after adding another Account *** \n");

            Console.ReadKey();

            updatedAccounts = accountRepository.FindAllByCustomerId(2);

            foreach (var account in updatedAccounts)
            {
                Console.WriteLine(account.ToString());
            }

            Console.ReadKey();


            // make a new Payoff plan
            var newPayoff = new DebtDestroyer.UnitOfWork.Payoff(customer);
            customer._AccountList = newPayoff.GetAccounts();

            var newPaymentPlan = newPayoff.Generate();

            payoffRepository.Dispose();

            payoffRepository.SavePaymentsToFile(newPaymentPlan);

            sortedPayments = payoffRepository.FindAllPayments().ToList().OrderBy(p => p._Month).ThenByDescending(pay => pay._DailyInterest).ToList();

            currentMonth = 0;
            Console.WriteLine("\n\n*** All Payments on All Accounts Updated *** \n");

            Console.ReadKey();

            foreach (var payment in sortedPayments)
            {
                if (payment._Month > currentMonth)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine(payment.ToString());
                currentMonth = payment._Month;
            }

        }
    }
}
