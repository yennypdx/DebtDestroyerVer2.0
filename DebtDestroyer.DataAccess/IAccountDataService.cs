using System;
using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.DataAccess
{
    public interface IAccountDataService : IDisposable
    {
        IList<Account> ReadFromFile();
        void SaveToFile(IList<Account> accounts);
        void AddAccount(Account newAccount);
        bool DeleteAccount(Account deleteAccount);
        void Dispose();
        bool EditAccount(Account target);
        IEnumerable<Account> FindAll();
        Account FindByID(int accountID);
        Account FindByName(string accountName);
        IEnumerable<IAccount> FindAllByCustomerId(int customerId);
        //IList<Account> PrioritySort(IList<Account> accounts);
    }
}