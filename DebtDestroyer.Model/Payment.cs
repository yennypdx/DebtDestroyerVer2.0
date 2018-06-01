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

    }
}
