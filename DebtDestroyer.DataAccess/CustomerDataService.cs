using System;
using System.Collections.Generic;
using DebtDestroyer.Model;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace DebtDestroyer.DataAccess
{
    public class CustomerDataService : ICustomerDataService
    {
        
        //TODO: Replace with real database
        private const string CustStorage = "CustomerDatabase.json";
        private const string AccStorage = "AccountDatabase.json";

        public IList<Customer> ReadFromCustomerDb()
        {
            if (!File.Exists(CustStorage))
            {
                throw new InvalidOperationException("File not found");
            }
            var json = File.ReadAllText(CustStorage);
            return JsonConvert.DeserializeObject<List<Customer>>(json);
        }

        public IList<Account> ReadFromAccountDb()
        {
            if (!File.Exists(AccStorage))
            {
                throw new InvalidOperationException("File not found");
            }
            var json = File.ReadAllText(AccStorage);
            return JsonConvert.DeserializeObject<List<Account>>(json);
        }

        public ICustomer GetCustomerById(int customerID)
        {
             var ICustomer = ReadFromCustomerDb();
             return ICustomer.SingleOrDefault(customer => customer._CustomerId.Equals(customerID));
        }

        public void UpdateCustomer(Customer customer)
        {
            var customers = ReadFromCustomerDb();
            var customerToUpdate = customers.SingleOrDefault(f => f._CustomerId.Equals(customer._CustomerId));

            var custIndex = customers.IndexOf(customerToUpdate);
            customers.Insert(custIndex, customer);
            customers.Remove(customerToUpdate);

            SaveToStorage(customers);
        }

        public void InsertCustomer(Customer customer)
        {
            var customers = ReadFromCustomerDb();
            var maxCustId = customers.Count == 0 ? 0 : customers.Max(f => f._CustomerId);

            customer._CustomerId = maxCustId + 1;

            SaveToStorage(customers);
        }

        public void SaveToStorage(IList<Customer> customer)
        {
            var json = JsonConvert.SerializeObject(customer, Formatting.Indented);
            File.WriteAllText(CustStorage, json);
        }

        public void AddNewCustomer(Customer newCustomer)
        {
            if (newCustomer._CustomerId <= 0)
            {
                InsertCustomer(newCustomer);
            }
            else
            {
                UpdateCustomer(newCustomer);
            }
        }

        public void DeleteExistingCustomer(int customerId)
        {
            var customers = ReadFromCustomerDb();
            var existingCustIdToDelete = customers.Single(f => f._CustomerId == customerId);

            customers.Remove(existingCustIdToDelete);

            SaveToStorage(customers);
        }

        public IEnumerable<Account> GetAccounts(int customerId)
        {
            var accounts = ReadFromAccountDb();
            var customerAccounts = accounts.ToList().Where(a => a._CustomerId.Equals(customerId));
            if (customerAccounts == null || customerAccounts.Count().Equals(0)) throw new InvalidOperationException("Customer accounts not found");
            return customerAccounts;
        }

        public void EditCustomer(Customer customer)
        {
            var customers = ReadFromCustomerDb();
            var updateCustomer = customers.Single(c => c._CustomerId.Equals(customer._CustomerId));
            var index = customers.IndexOf(updateCustomer);
            customers.Insert(index, customer);
            customers.Remove(updateCustomer);
            SaveToStorage(customers);
        }
        public Decimal GetAllocatedFunds(Customer customer) // get by custId, get by username/password
        {
            var customers = ReadFromCustomerDb();
            var fundCustomer = customers.SingleOrDefault(c => c._CustomerId.Equals(customer._CustomerId));
            if (fundCustomer == null) throw new InvalidOperationException("Customer not found");
            return fundCustomer._AllocatedFund;
        }

        public Decimal GetAllocatedFundsByUserNameAndPassword(string name, string password)
        {
            var customers = ReadFromCustomerDb();
            var customer = customers.SingleOrDefault(c => c._UserName.Equals(name));
            if (customer == null) throw new InvalidOperationException("Username not found");
            return customer._AllocatedFund;
        }

        public int GetCustomerIdByName(string customerName)
        {
            var customers = ReadFromCustomerDb();
            var existingCustId = 1;

            foreach (var cust in customers)
            {
                if (cust._UserName.Equals(customerName))
                {
                    existingCustId = cust._CustomerId;
                }
            }
            return existingCustId;
        }
    }
}
