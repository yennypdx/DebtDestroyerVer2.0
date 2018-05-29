using System.Collections.Generic;
using System.Text;

namespace DebtDestroyer.Model
{
    public class Account : IAccount
    {
        public int _AccountId { get; set; }
        public int _CustomerId { get; set; }
        public string _Name { get; set; }
        public float _Apr { get; set; }
        public decimal _Balance { get; set; }
        public decimal _MinPay { get; set; }
        public decimal _Payment { get; set; } //TODO: needs to be calclulated in payoff, updated after every payment
        public decimal _AvailCredit { get; set; } //optional
        public decimal _OneTimePay { get; set; } //optional
        public decimal _NextPayment { get; set; } //TODO: needs to be calculated in payoff
        public decimal DailyInterest() // this also needs to be caluculated in payoff, changes after every payment
        {
            float balance = (float)_Balance;
            return (decimal)(_Apr / 365 * balance);
        }

        public override bool Equals(object obj)
        {
            var account = obj as Account;
            return account != null &&
                   _AccountId == account._AccountId &&
                   _CustomerId == account._CustomerId &&
                   _Name == account._Name &&
                   _Apr == account._Apr &&
                   _Balance == account._Balance &&
                   _MinPay == account._MinPay;
                   
        }

        public override int GetHashCode()
        {
            var hashCode = 1081699009;
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(_AccountId);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(_CustomerId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<float>.Default.GetHashCode(_Apr);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(_MinPay); 
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(_Balance); 
            return hashCode;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("AccountID: " + _AccountId);
            builder.Append("CustomerID: " + _CustomerId);
            builder.Append("Name: " + _Name);
            builder.Append("Balance: " + _Balance);
            builder.Append("MinPay: " + _MinPay);
            builder.Append("AvailableCredit: " + _AvailCredit);
            builder.Append("OneTimePay: " + _OneTimePay);
            builder.Append("NextPayment: " + _NextPayment);
            return builder.ToString();
        }
    }
}
