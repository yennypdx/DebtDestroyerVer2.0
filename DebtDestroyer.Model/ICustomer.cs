using System.Collections.Generic;

namespace DebtDestroyer.Model
{
    public interface ICustomer
    {
        int _CustomerId { get; set; }
        string _UserName { get; set; }
        string _Email { get; set; }
        string _Password { get; set; }
        decimal _AllocatedFund { get; set; }
        IEnumerable<IAccount> _AccountList { get; set; }
       

    }
}