using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.Model
{
    public class Payment : IPayment
    {
        public int _Month { get; set; }
        public int _CustomerId { get; set; }
        public int _AccountId { get; set; }
        public string _AccountName { get; set; }
        public decimal _Balance { get; set; }
        public decimal _Payment { get; set; }
        public decimal _DailyInterest { get; set; }

        public override bool Equals(object obj)
        {
            var payment = obj as Payment;
            return payment != null &&
                   _Month == payment._Month &&
                   _CustomerId == payment._CustomerId &&
                   _AccountId == payment._AccountId &&
                   _AccountName == payment._AccountName &&
                   _Balance == payment._Balance &&
                   _Payment == payment._Payment &&
                   _DailyInterest == payment._DailyInterest;
        }

        public override int GetHashCode()
        {
            var hashCode = 1550772908;
            hashCode = hashCode * -1521134295 + _Month.GetHashCode();
            hashCode = hashCode * -1521134295 + _CustomerId.GetHashCode();
            hashCode = hashCode * -1521134295 + _AccountId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_AccountName);
            hashCode = hashCode * -1521134295 + _Balance.GetHashCode();
            hashCode = hashCode * -1521134295 + _Payment.GetHashCode();
            hashCode = hashCode * -1521134295 + _DailyInterest.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Month: " + _Month);
            builder.Append(" CID: " + _CustomerId);
            builder.Append(" AID: " + _AccountId);
            builder.Append(" Name: " + _AccountName);
            builder.Append("\tBalance: " + _Balance.ToString("$0.00" + "  "));
            builder.Append("\tPayment: " + _Payment.ToString("$0.00") + "  ");
            builder.Append("\tDailyInt: " + _DailyInterest.ToString("$0.00"));
            return builder.ToString();
        }
    }
}

