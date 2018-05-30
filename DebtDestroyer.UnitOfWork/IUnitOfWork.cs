using DebtDestroyer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerDataService customerService { get; set; }
        IAccountDataService accountService { get; set; }

    }

    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerDataService customerService { get; set; }
        public IAccountDataService accountService { get; set; }


        public Model.Customer

    }
}
