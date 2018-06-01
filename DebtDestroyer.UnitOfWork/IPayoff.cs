using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.UnitOfWork
{
    public interface IPayoff
    {
        IList<DebtDestroyer.Model.Payment> Generate();
    }
}
