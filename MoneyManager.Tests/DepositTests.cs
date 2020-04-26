using System;
using System.Collections.Generic;
using System.Text;
using MoneyManager.Models;
using Xunit;
using Moq;

namespace MoneyManager.Tests
{
    public class DepositTests
    {
        [Fact]
        public void Can_Calculate_Incomes()
        {
            Deposit deposit = new Deposit
            {
                Bank = new Bank { Name = "FakeBank" },
                InitialFund = 1000m,
                InterestRate = 2.5m,
                CapitalizationTermsPerYear = 12,
                Currency = Currency.USD,
                OpeningDate = new DateTime(2019, 4, 4),
                ClosingDate = new DateTime(2019, 10, 4),
                HasCapitalization = false
            };

            Dictionary<DateTime, Decimal> testIncomes = new Dictionary<DateTime, Decimal>
            {
                { new DateTime(2019, 5, 4), 2.05m },
                { new DateTime(2019, 6, 4), 2.12m },
                { new DateTime(2019, 7, 4), 2.05m },
                { new DateTime(2019, 8, 4), 2.12m },
                { new DateTime(2019, 9, 4), 2.12m },
                { new DateTime(2019, 10, 4), 2.05m }
            };

            Assert.Equal(testIncomes, deposit.Incomes);
        }
    }
}
