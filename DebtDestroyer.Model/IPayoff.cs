using System.Collections.Generic;

namespace DebtDestroyer.Model
{
    public interface IPayoff
    {
        void ApplyAllAccrued();
        IList<Payment> Generate();
        decimal LeftOver();
        void PrioretySort();
        void ResetPayments();
        decimal TotalDailyInterest();
        decimal TotalMinimumPayments();
        decimal TotalPayments();
        void Update();
    }
}