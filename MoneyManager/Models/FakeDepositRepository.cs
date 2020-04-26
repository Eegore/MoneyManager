using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class FakeDepositRepository //: IDepositRepository
    {
        public IQueryable<Deposit> Deposits => new List<Deposit>
        {
            new Deposit
            {
                Bank = new Bank { Name = "Tinkoff" },
                InitialFund = 1000m,
                InterestRate = 2.5m,
                CapitalizationTermsPerYear = 12,
                Currency = Currency.USD,
                OpeningDate = new DateTime(2019, 4, 4),
                ClosingDate = new DateTime(2020, 4, 4),
                HasCapitalization = false
            },
            new Deposit
            {
                Bank = new Bank { Name = "MKB" },
                InitialFund = 805000m,
                InterestRate = 5.5m,
                CapitalizationTermsPerYear = 12,
                Currency = Currency.RUR,
                OpeningDate = new DateTime(2019, 2, 2),
                ClosingDate = new DateTime(2020, 7, 26),
                HasCapitalization = true
            }
        }.AsQueryable<Deposit>();
    }
}
