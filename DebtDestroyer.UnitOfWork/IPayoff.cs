using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.UnitOfWork
{
    public interface IPayoff
    {
        void ApplyAllAccrued();
        IList<Payment> Generate();
        IEnumerable<Model.IAccount> GetAccounts();
        decimal LeftOver();
        void PrioretySort();
        void ResetPayments();
        decimal TotalDailyInterest();
        decimal TotalMinimumPayments();
        decimal TotalPayments();
        void Update();
    }
}