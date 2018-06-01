using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.DataAccess
{
    public class PayoffDataService : IPayoffDataService
    {
        public ICustomerDataService _CustomerData;
        public IAccountDataService _AccountsData;
        //private int _CustomerId;
        //private int _AccountId;

        public void SaveToFile(IEnumerable<string> payoff)
        {
            throw new NotImplementedException();
        }

        public void stuff()
        {

        }
    }
}
