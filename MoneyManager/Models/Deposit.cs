using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MoneyManager.Models
{
    public class Deposit
    {
        [JsonIgnore]
        public Int32 DepositId { get; set; }

        public Int16 BankId { get; set; }

        [JsonIgnore]
        public Bank Bank { get; set; }

        public Decimal InitialFund { get; set; }

        public Decimal InterestRate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public Byte CapitalizationTermsPerYear { get; set; }

        public Boolean HasCapitalization { get; set; }

        [JsonIgnore]
        public Int16 Term => (Int16)(ClosingDate - OpeningDate).Days;

        [JsonIgnore]
        public Int16 DaysToEnd => (Int16)(ClosingDate - DateTime.Now).Days;

        [JsonIgnore]
        public Decimal EffectiveInterestRate => Decimal.Round((Decimal)(Math.Pow((Double)(1 + InterestRate / (100m * CapitalizationTermsPerYear)), (Double)((Term / 365.25m) * CapitalizationTermsPerYear)) - 1) * 100 / (Term / 365.25m), 2);

        [JsonIgnore]
        public Dictionary<DateTime, Decimal> Incomes => GetIncomes();

        private Dictionary<DateTime, Decimal> GetIncomes()
        {
            Dictionary<DateTime, Decimal> incomes = new Dictionary<DateTime, Decimal>();

            DateTime date = OpeningDate;
            Decimal balance = InitialFund;
            Decimal monthlyIncome = 0;

            while (date < ClosingDate)
            {
                Byte daysInCurrentMonth = (Byte)DateTime.DaysInMonth(date.Year, date.Month);
                date = date.AddMonths(1);

                if (date > ClosingDate)
                {
                    daysInCurrentMonth = (Byte)(ClosingDate - date.AddMonths(-1)).Days;
                    date = ClosingDate;
                }

                if (HasCapitalization) balance += monthlyIncome;
                Decimal MonthyRate = (InterestRate / (DateTime.IsLeapYear(date.Year) == true ? 366 : 365)) * daysInCurrentMonth;
                monthlyIncome = (MonthyRate / 100) * balance;

                incomes.Add(date, Decimal.Round(monthlyIncome, 2));
            }

            return incomes;
        }
    }
}
