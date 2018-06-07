using DebtDestroyer.Model;
using DebtDestroyer.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyerVer2.Wrapper
{
    public class CustomerWrapper : ViewModelBase
    {
        private Customer _customer;
        private bool _isChanged;

        public CustomerWrapper(Customer customer)
        {
            _customer = customer;
        }

        public Customer Model { get { return _customer; } }

        public bool IsChanged
        {
            get { return _isChanged; }
            private set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        public int Id
        {
            get { return _customer._CustomerId; }

        }

        public string UserName
        {
            get { return _customer._UserName; }
            set
            {
                _customer._UserName = value;
                OnPropertyChanged();
            }
        }

        public decimal AllocatedFunds
        {
            get { return _customer._AllocatedFund; }
            set
            {
                _customer._AllocatedFund = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _customer._Email; }
            set
            {
                _customer._Email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _customer._Password; }
            set
            {
                _customer._Password = value;
                OnPropertyChanged();
            }
        }




        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
        }
    }
}
