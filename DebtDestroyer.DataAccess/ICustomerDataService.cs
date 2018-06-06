using DebtDestroyer.Model;
using System;
using System.Collections.Generic;

namespace DebtDestroyer.DataAccess
{
    public interface ICustomerDataService : IDisposable
    {
        IList<Customer> ReadFromCustomerDb();
        IList<Account> ReadFromAccountDb();
        ICustomer GetCustomerById(int customerID);
        void AddNewCustomer(Customer newCustomer);
        void DeleteExistingCustomer(int customerId);
        void EditCustomer(Customer customer);
        IEnumerable<Account> GetAccounts(int customerId);
        Decimal GetAllocatedFunds(Customer customer);
        Decimal GetAllocatedFundsByUserNameAndPassword(string name, string password);
        int GetCustomerIdByName(string customerName);
        void InsertCustomer(Customer customer);
        void SaveToStorage(System.Collections.Generic.IList<Customer> customer);
        void UpdateCustomer(Customer customer);
    }
}