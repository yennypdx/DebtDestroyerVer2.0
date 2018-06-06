using DebtDestroyer.DataAccess;
using DebtDestroyer.UnitOfWork;
using System.Collections.Generic;

namespace DebtDestoyer.UI.DataProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerDataService CustomerService { get; set; }
        public IAccountDataService AccountService { get; set; }

        private Func<ICustomerDataService> CustomerServiceCreator;
        private Func<IAccountDataService> AccountServiceCreator;
        private Func<IPayoffDataService> PayoffServiceCreator;

       
        public UnitOfWork(Func<ICustomerDataService> customerServiceCreator, Func<IAccountDataService> accountServiceCreator, Func<IPayoffDataService> payoffServiceCreator)
        {
            CustomerServiceCreator = customerServiceCreator;
            AccountServiceCreator = accountServiceCreator;
            PayoffServiceCreator = payoffServiceCreator;
        }
        public IEnumerable<DebtDestroyer.Model.IAccount> GetCustomerAccounts(int customerID)
        {
            var accounts = AccountService.FindAllByCustomerId(customerID);
            var returnAccounts = new List<DebtDestroyer.Model.IAccount>();
            foreach (var account in accounts)
            {
                returnAccounts.Add(new DebtDestroyer.Model.Account
                {
                    _CustomerId = account._CustomerId,
                    _AccountId = account._AccountId,
                    _Balance = account._Balance,
                    _Apr = account._Apr,
                    _MinPay = account._MinPay,
                    _Name = account._Name,
                    _Payment = account._Payment
                });
            }
            return returnAccounts;
        }

        public ICustomer GetCustomerById(int id)
        {
            using (var customerService = CustomerServiceCreator())
            {
                return customerService.GetCustomerById(id);
            }
        }

        public void AddNewCustomer(Customer newCustomer)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.AddNewCustomer(newCustomer);
            }
        }

        public void DeleteExistingCustomer(int customerId)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.DeleteExistingCustomer(customerId);
            }
        }

        public void EditCustomer(Customer customerId)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.EditCustomer(customerId);
            }
        }

        public IEnumerable<Account> GetAccounts(int customerId)
        {
            using (var customerService = CustomerServiceCreator())
            {
                return customerService.GetAccounts(customerId);
            }
        }

        public Decimal GetAllocatedFunds(Customer customer)
        {
            using (var customerService = CustomerServiceCreator())
            {
                return customerService.GetAllocatedFunds(customer);
            }
        }

        public Decimal GetAllocatedFundsByUserNameAndPassword(string name, string password)
        {
            using (var customerService = CustomerServiceCreator())
            {
                return customerService.GetAllocatedFundsByUserNameAndPassword(name, password);
            }
        }

        public int GetCustomerIdByName(string customerName)
        {
            using (var customerService = CustomerServiceCreator())
            {
                return customerService.GetCustomerIdByName(customerName);
            }
        }

        public void InsertCustomer(Customer customer)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.InsertCustomer(customer);
            }
        }

        public void SaveToStorage(System.Collections.Generic.IList<Customer> customer)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.SaveToStorage(customer);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var customerService = CustomerServiceCreator())
            {
                customerService.UpdateCustomer(customer);
            }
        }

        //*** Account ***//

        public void SaveToFile(IList<Account> accounts)
        {
            using (var accountService = AccountServiceCreator())
            {
                accountService.SaveToFile(accounts);
            }
        }

        public void AddAccount(Account newAccount)
        {
            using (var accountService = AccountServiceCreator())
            {
                accountService.AddAccount(newAccount);
            }
        }

        public bool DeleteAccount(Account deleteAccount)
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.DeleteAccount(deleteAccount);
            }
        }

        public bool EditAccount(Account target)
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.EditAccount(target);
            }
        }

        public IEnumerable<Account> FindAll()
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.FindAll();
            }
        }

        public Account FindByID(int accountID)
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.FindByID(accountID);
            }
        }

        public Account FindByName(string accountName)
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.FindByName(accountName);
            }
        }

        public IEnumerable<IAccount> FindAllByCustomerId(int customerId)
        {
            using (var accountService = AccountServiceCreator())
            {
                return accountService.FindAllByCustomerId(customerId);
            }
        }

        //*****Payoff ******//
        public IEnumerable<Payment> FindAllByAccountId(int accountId)
        {
            using (var payoffService = PayoffServiceCreator())
            {
                return payoffService.FindAllByAccountId(accountId);
            }
        }

        public IEnumerable<Payment> FindAllPaymentsByCustomerId(int customerId)
        {
            using (var payoffService = PayoffServiceCreator())
            {
                return payoffService.FindAllPaymentsByCustomerId(customerId);
            }
        }

        public IEnumerable<Payment> FindAllByMonth(int month)
        {
            using (var payoffService = PayoffServiceCreator())
            {
                return payoffService.FindAllByMonth(month);
            }
        }

        public Payment FindPaymentByMonthAndAccountId(int month, int accountId)
        {
            using (var payoffService = PayoffServiceCreator())
            {
                return payoffService.FindPaymentByMonthAndAccountId(month, accountId);
            }
        }

        public IEnumerable<Payment> FindAllPayments()
        {
            using (var payoffService = PayoffServiceCreator())
            {
                return payoffService.FindAllPayments();
            }
        }

        public void SavePaymentsToFile(IList<Payment> payments)
        {
            using (var payoffService = PayoffServiceCreator())
            {
                payoffService.SavePaymentsToFile(payments);
            }
        }

        public void Dispose()
        {
           
        }
    }
}
