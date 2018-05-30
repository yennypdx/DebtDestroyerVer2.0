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
        private const string AccountStorageFile = "AccountDatabase.txt"; 

        private IList<Account> ReadFromFile()
        {
            if (!File.Exists(AccountStorageFile))
            {
                //Create dummy List<Account>

            }

            var json = File.ReadAllText(AccountStorageFile);
            return JsonConvert.DeserializeObject<List<Account>>(json);
        }

        public void AddAccount(Account newAccount)
        {
            var accounts = ReadFromFile();
            var maxId = accounts.Count == 0 ? 0 : accounts.Max(a => a._AccountId);

            newAccount._AccountId = maxId + 1;
            accounts.Add(newAccount);

            SaveToFile(accounts);
        }

        private void SaveToFile(IList<Account> accounts)
        {
            var json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(AccountStorageFile, json);
        }

        public bool DeleteAccount(Account deleteAccount)
        {
            var accounts = ReadFromFile();
            var accountToDelete = accounts.SingleOrDefault(a => a._AccountId.Equals(deleteAccount._AccountId));
            if (accountToDelete == null) return false;

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
            var editAccount = accounts.SingleOrDefault(a => a._AccountId.Equals(target._AccountId));
            if (editAccount == null) return false;
            var index = accounts.IndexOf(editAccount);
            accounts.Insert(index, target);
            accounts.Remove(editAccount);
            SaveToFile(accounts);
            return true;
        }

        public IEnumerable<Account> FindAll()
        {
            return ReadFromFile().Select(account =>
                new Account()
                {
                    _AccountId = account._AccountId,
                    _CustomerId = account._CustomerId,
                    _Name = account._Name,
                    _Apr = account._Apr,
                    _Balance = account._Balance,
                    _MinPay = account._MinPay,
                    //_Payment = account._Payment;
                });
        }

        public Account FindByID(int accountID)
        {
            return FindAll().SingleOrDefault(account => account._AccountId.Equals(accountID));
        }

        public Account FindByName(string accountName)
        {
            return FindAll().SingleOrDefault(account => account._Name.Equals(accountName));
        }

        public IEnumerable<IAccount> FindAllByCustomerId(int customerId)
        {
            
            return FindAll().Where(account => account._CustomerId.Equals(customerId));

        }

        public IList<Account> PrioritySort(IList<Account> accounts)
        {
            return FindAll().ToList().OrderByDescending(a => a.DailyInterest()).ToList();
        }
    }
}
