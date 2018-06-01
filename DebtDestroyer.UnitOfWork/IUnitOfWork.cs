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
        ICustomerDataService _CustomerService { get; set; }
        IAccountDataService _AccountService { get; set; }

    }

    //public class UnitOfWork : IUnitOfWork
    //{
    //    public ICustomerDataService _CustomerService { get; set; }
    //    public IAccountDataService _AccountService { get; set; }


    //    public Model.Customer

    //}
}
