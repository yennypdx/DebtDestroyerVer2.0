namespace DebtDestroyer.Model
{
    public interface IAccount
    {
        decimal DailyInterest();
        int _AccountId { get; set; }
        int _CustomerId { get; set; }
        string _Name { get; set; }
        float _Apr { get; set; }
        decimal _Balance { get; set; }
        decimal _MinPay { get; set; }
        decimal _Payment { get; set; } //TODO: needs to be calclulated in payoff, updated after every payment
        decimal _NextPayment { get; set; } //TODO: needs to be calculated in payoff
    }
}