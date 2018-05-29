﻿using System;
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
        private const string CustStorage = "Customer.txt";
        private const string AccStorage = "Account.txt";

        private IList<Customer> ReadFromCustomerDb()
        {
            if (!File.Exists(CustStorage))
            {
                return new List<Customer>
                {
                    new Customer
                    { _CustomerId=1,
                        _UserName ="YeonJae",
                        _Email ="yeonssi@gmail.com",
                        _Password="trouble123",
                        _AllocatedFund=350m,
                        //_AccountList=GetAccounts(1)
                    }
                };
            }
            var json = File.ReadAllText(CustStorage);
            return JsonConvert.DeserializeObject<List<Customer>>(json);
        }

        private IList<Account> ReadFromAccountDb()
        {
            if (!File.Exists(AccStorage))
            {
                return new List<Account>
                {
                    new Account
                    {
                        //TODO: Fill out this!
                         _AccountId = 1,
                         _CustomerId = 1,
                         _Name = "Wells Fargo",
                         _Balance = 8450m,
                         _MinPay = 149m
                    }
                };
            }
            var json = File.ReadAllText(AccStorage);
            return JsonConvert.DeserializeObject<List<Account>>(json);
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

        public ICollection<Account> GetAccounts(int customerId)
        {
            //Note: This method need access to the account database
            throw new NotImplementedException();
        }
        public void EditCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public void GetAllocatedFunds(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int GetCustomerId(string customerName)
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
