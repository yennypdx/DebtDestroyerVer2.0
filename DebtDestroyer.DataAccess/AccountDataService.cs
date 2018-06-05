using DebtDestroyer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.DataAccess
{
    public class AccountDataService : IAccountDataService
    {
        //TODO: Replace with real database
        private const string AccountStorageFile = @"C:\Users\slaug\Desktop\CST\CST 236 - System Testing\DebtDestroyer2\DebtDestroyer.DataAccess\AccountDatabase.json"; 

        public IList<Account> ReadFromFile()
        {
            if (!File.Exists(AccountStorageFile))
            {
                //throw exception

            }

            var json = File.ReadAllText(AccountStorageFile);
            return JsonConvert.DeserializeObject<List<Account>>(json);
        }

        public void AddAccount(Account newAccount)
        {
            if (newAccount == null) throw new InvalidOperationException("newAccount was null");
            var accounts = ReadFromFile();
            var maxId = accounts.Count == 0 ? 0 : accounts.Max(a => a._AccountId);

            newAccount._AccountId = maxId + 1;
            accounts.Add(newAccount);

            SaveToFile(accounts);
        }

        public void SaveToFile(IList<Account> accounts)
        {
            var json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(AccountStorageFile, json);
        }

        public bool DeleteAccount(Account deleteAccount)
        {
            var accounts = ReadFromFile();
            var accountToDelete = accounts.ToList().SingleOrDefault(a => a._AccountId.Equals(deleteAccount._AccountId));
            if (accountToDelete == null) throw new InvalidOperationException("Account not found");

            accounts.Remove(accountToDelete);

            SaveToFile(accounts);

            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool EditAccount(Account target)
        {
            var accounts = ReadFromFile();
            var editAccount = accounts.ToList().SingleOrDefault(a => a._AccountId.Equals(target._AccountId));
            if (editAccount == null) throw new InvalidOperationException("Account not found");
            var index = accounts.IndexOf(editAccount);
            accounts.Insert(index, target);
            accounts.Remove(editAccount);
            SaveToFile(accounts);
            return true;
        }

        public IEnumerable<Account> FindAll()
        {
            //return ReadFromFile().Select(account =>
            //    new Account()
            //    {
            //        _AccountId = account._AccountId,
            //        _CustomerId = account._CustomerId,
            //        _Name = account._Name,
            //        _Apr = account._Apr,
            //        _Balance = account._Balance,
            //        _MinPay = account._MinPay,
            //    });
            return ReadFromFile();
        }

        public Account FindByID(int accountID)
        {
            return FindAll().SingleOrDefault(account => account._AccountId.Equals(accountID));
        }

        public Account FindByName(string accountName)
        {
            return FindAll().ToList().SingleOrDefault(account => account._Name.Equals(accountName));
        }

        public IEnumerable<IAccount> FindAllByCustomerId(int customerId)
        {
            var accounts = FindAll().ToList().Where(account => account._CustomerId.Equals(customerId)).ToList();
            if (accounts == null || accounts.Count.Equals(0)) throw new InvalidOperationException("Customer ID not found");
            return accounts;

        }

        //public IList<Account> PrioritySort(IList<Account> accounts)
        //{
        //    return FindAll().ToList().OrderByDescending(a => a.DailyInterest()).ToList();
        //}
    }
}
