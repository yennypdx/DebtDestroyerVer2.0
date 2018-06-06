using System;
using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.DataAccess
{
    public interface IPayoffDataService : IDisposable
    {
        IEnumerable<Payment> FindAllByAccountId(int accountId);
        IEnumerable<Payment> FindAllPaymentsByCustomerId(int customerId);
        IEnumerable<Payment> FindAllByMonth(int month);
        Payment FindPaymentByMonthAndAccountId(int month, int accountId);
        IList<Payment> ReadFromFile();
        IEnumerable<Payment> FindAllPayments();
        void SavePaymentsToFile(IList<Payment> payments);
    }
}