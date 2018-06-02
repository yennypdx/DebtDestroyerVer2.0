namespace DebtDestroyer.Model
{
    public interface IPayoff
    {
        void ApplyAllAccrued();
        decimal LeftOver();
        void PrioretySort();
        void ResetPayments();
        decimal TotalDailyInterest();
        decimal TotalMinimumPayments();
        decimal TotalPayments();
        void Update();
    }
}