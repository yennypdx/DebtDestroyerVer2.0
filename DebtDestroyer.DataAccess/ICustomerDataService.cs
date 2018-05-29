using DebtDestroyer.Model;
using System.Collections.Generic;

namespace DebtDestroyer.DataAccess
{
    public interface ICustomerDataService
    {
        void AddNewCustomer(Customer newCustomer);
        void DeleteExistingCustomer(int customerId);
        void EditCustomer(Customer customer);
        ICollection<Account> GetAccounts(int customerId);
        void GetAllocatedFunds(Customer customer);
        int GetCustomerId(string customerName);
        void InsertCustomer(Customer customer);
        void SaveToStorage(System.Collections.Generic.IList<Customer> customer);
        void UpdateCustomer(Customer customer);
    }
}