using System.Collections.Generic;

namespace DebtDestroyer.UnitOfWork
{
    public class Customer : ICustomer
    {
        public int _CustomerId { get; set; }
        public string _UserName { get; set; }
        public string _Email { get; set; }
        public string _Password { get; set; }
        public decimal _AllocatedFund { get; set; }
        public ICollection<Account> _AccountList { get; set; }
        //public IPayoff _Payoff { get; set; }
        private IUnitOfWork data { get; set; }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   _CustomerId == customer._CustomerId &&
                   _UserName == customer._UserName &&
                   _Email == customer._Email &&
                   _Password == customer._Password &&
                   _AllocatedFund == customer._AllocatedFund &&
                   EqualityComparer<ICollection<Account>>.Default.Equals(_AccountList, customer._AccountList);
        }

        public override int GetHashCode()
        {
            var hashCode = 1615748143;
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(_CustomerId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(_AllocatedFund);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Account>>.Default.GetHashCode(_AccountList);
            return hashCode;
        }

        public override string ToString()
        {
            string outCustomer = _CustomerId + " | " + _UserName + " | " + _Email + " | " + _Password + " | " + _AllocatedFund + " | " + _AccountList;

            return outCustomer;
        }
    }
}
