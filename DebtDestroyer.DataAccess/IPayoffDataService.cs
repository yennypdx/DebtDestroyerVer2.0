using System.Collections.Generic;

namespace DebtDestroyer.DataAccess
{
    public interface IPayoffDataService
    {
        void stuff();
        void SaveToFile(IEnumerable<string> payoff);
    }
}