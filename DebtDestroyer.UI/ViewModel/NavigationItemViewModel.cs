using DebtDestroyer.Model;
using System.Collections.Generic;

namespace DebtDestroyer.UI.ViewModel
{
    class NavigationItemViewModel: ViewModelBase
    {
        //Customer
        int _CustomerId { get; set; }
        string _UserName { get; set; }
        string _Email { get; set; }
        string _Password { get; set; }
        decimal _AllocatedFund { get; set; }
        ICollection<Account> _AccountList { get; set; }

        //Account --- customerID only used once
        decimal DailyInterest { get; }
        int _AccountId { get; set; }
        string _Name { get; set; }
        float _Apr { get; set; }
        decimal _Balance { get; set; }
        decimal _MinPay { get; set; }
        decimal _Payment { get; set; }
    }
}
