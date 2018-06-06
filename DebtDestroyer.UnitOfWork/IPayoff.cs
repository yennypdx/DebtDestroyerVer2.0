using System.Collections.Generic;
using DebtDestroyer.Model;

namespace DebtDestroyer.UnitOfWork
{
    public interface IPayoff
    {
        int _CustomerId { get; set; }
        decimal _AllocatedFunds { get; set; }
        IEnumerable<Model.IAccount> _Accounts { get; set; }
        IUnitOfWork _UnitOfWork { get; set; }
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