using DebtDestroyer.Model;
using System.Collections.Generic;

namespace DebtDestroyer.DataAccess
{
    public interface ICustomerDataService
    {
        ICustomer GetCustomerById(int customerID);
        void AddNewCustomer(Customer newCustomer);
        void DeleteExistingCustomer(int customerId);
        ICollection<Account> GetAccounts(int customerId);
        decimal GetAllocatedFunds(Customer customer);
        int GetCustomerIdByName(string customerName);
    }
}