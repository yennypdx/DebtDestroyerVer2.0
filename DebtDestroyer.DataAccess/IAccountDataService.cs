using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.DataAccess
{
    public interface IAccountDataService
    {
        void AddAccount(Account newAccount);
        bool DeleteAccount(Account deleteAccount);
        void Dispose();
        bool EditAccount(Account target);
        IEnumerable<Account> FindAll();
        Account FindByID(int accountID);
        Account FindByName(string accountName);
        IList<Account> PrioritySort(IList<Account> accounts);
    }
}