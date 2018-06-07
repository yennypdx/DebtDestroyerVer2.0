using DebtDestroyer.Model;
using DebtDestroyer.UnitOfWork;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDestroyerVer2.ViewModel
{
    public class CustomerEditViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IEventAggregator _eventAggregator;

        public CustomerEditViewModel(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public void LoadCustomer(int? customerId)
        {
            var customer = customerId.HasValue
                ? _unitOfWork.CustomerService.GetCustomerById(customerId.Value)
                : new Customer();

            
        }
    }

    
}
