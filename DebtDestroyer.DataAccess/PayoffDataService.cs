using DebtDestroyer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.DataAccess
{
    public class PayoffDataService : IPayoffDataService // This only allows payoff DB to store payments list for ONE customer
    {
     
        private const string PayoffDataBase = "PayoffDatabase.json";

        public IList<Payment> ReadFromFile()
        {
            if (!File.Exists(PayoffDataBase))
            {
                throw new InvalidOperationException("File not found");
            }

            var json = File.ReadAllText(PayoffDataBase);
            return JsonConvert.DeserializeObject<List<Payment>>(json);
        }

        public IEnumerable<Payment> FindAllPayments()
        {
            //return ReadFromFile().Select(payment =>
            //new Payment
            //{
            //    _Month = payment._Month,
            //    _CustomerId = payment._CustomerId,
            //    _AccountId = payment._AccountId,
            //    _AccountName = payment._AccountName,
            //    _Balance = payment._Balance,
            //    _DailyInterest = payment._DailyInterest

            //});
            return ReadFromFile();

        }

        public IEnumerable<Payment> FindAllPaymentsByCustomerId(int customerId)
        {
            return FindAllPayments().ToList().Where(payment => payment._CustomerId.Equals(customerId)).ToList();
        }

        public IEnumerable<Payment> FindAllByAccountId(int accountId)
        {
            return FindAllPayments().ToList().Where(payment => payment._AccountId.Equals(accountId)).ToList();
        }

        public IEnumerable<Payment> FindAllByMonth(int month)
        {
            return FindAllPayments().ToList().Where(payment => payment._Month.Equals(month)).ToList();
        }

        public Payment FindPaymentByMonthAndAccountId(int month, int accountId)
        {
            var paymentsByMonth = FindAllByMonth(month);
            if (paymentsByMonth == null) throw new InvalidOperationException("Payments by month is null");
            return paymentsByMonth.SingleOrDefault(payment => payment._AccountId.Equals(accountId));
        }
        public void SavePaymentsToFile(IList<Payment> payments)
        {
            var json = JsonConvert.SerializeObject(payments, Formatting.Indented);
            File.WriteAllText(PayoffDataBase, json);
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

