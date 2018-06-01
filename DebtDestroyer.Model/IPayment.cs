namespace DebtDestroyer.Model
{
    public interface IPayment
    {
        int _AccountId { get; set; }
        string _AccountName { get; set; }
        decimal _Balance { get; set; }
        int _CustomerId { get; set; }
        decimal _DailyInterest { get; set; }
        int _Month { get; set; }
        decimal _Payment { get; set; }
    }
}