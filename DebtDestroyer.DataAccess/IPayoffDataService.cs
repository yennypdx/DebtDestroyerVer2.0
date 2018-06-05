using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.DataAccess
{
    public interface IPayoffDataService
    {
        IEnumerable<Payment> FindAllByAccountId(int accountId);
        IEnumerable<Payment> FindAllByCustomerId(int customerId);
        IEnumerable<Payment> FindAllByMonth(int month);
        Payment FindPaymentByMonthAndAccountId(int month, int accountId);
        IList<Payment> ReadFromFile();
        IEnumerable<Payment> FindAll();
        void SaveToFile(IList<Payment> payments);
    }
}