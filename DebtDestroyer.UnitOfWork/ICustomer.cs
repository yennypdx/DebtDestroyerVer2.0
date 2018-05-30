using System.Collections.Generic;

namespace DebtDestroyer.UnitOfWork
{
    public interface ICustomer
    {
        int _CustomerId { get; set; }
        string _UserName { get; set; }
        string _Email { get; set; }
        string _Password { get; set; }
        decimal _AllocatedFund { get; set; }
        ICollection<Account> _AccountList { get; set; }
       

    }
}