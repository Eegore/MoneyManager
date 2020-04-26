using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MoneyManager.Models;
using MoneyManager.Models.ViewModels;
using MoneyManager.Controllers;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManager.Tests
{
    public class DepositControllerTests
    {
        [Fact]
        public void Can_Filter_Deposits()
        {
            Mock <IDepositRepository> mock = new Mock<IDepositRepository>();
            mock.Setup(m => m.Deposits).Returns(GetFakeDeposits());

            DepositController controller = new DepositController(mock.Object);

            Deposit[] resultBoth = (controller.List(null).ViewData.Model as DepositListViewModel).Deposits.ToArray();
            Deposit[] result1 = (controller.List(1).ViewData.Model as DepositListViewModel).Deposits.ToArray();
            Deposit[] result2 = (controller.List(2).ViewData.Model as DepositListViewModel).Deposits.ToArray();

            Assert.Equal(3, resultBoth.Length);
            Assert.Single(result1);
            Assert.Equal(2, result2.Length);
        }

        [Fact]
        public void Can_Edit_Deposit()
        {
            Mock<IDepositRepository> mock = new Mock<IDepositRepository>();
            mock.Setup(m => m.Deposits).Returns(GetFakeDeposits());

            DepositController controller = new DepositController(mock.Object);

            Deposit d1 = GetViewModel<Deposit>(controller.Edit(1));
            Deposit d2 = GetViewModel<Deposit>(controller.Edit(2));
            Deposit d3 = GetViewModel<Deposit>(controller.Edit(3));

            Assert.Equal(1, d1.DepositId);
            Assert.Equal(2, d2.DepositId);
            Assert.Equal(3, d3.DepositId);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Deposit()
        {
            Mock<IDepositRepository> mock = new Mock<IDepositRepository>();
            mock.Setup(m => m.Deposits).Returns(GetFakeDeposits());

            DepositController controller = new DepositController(mock.Object);

            Deposit d = GetViewModel<Deposit>(controller.Edit(4));

            Assert.Null(d);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        private IQueryable<Deposit> GetFakeDeposits()
        {
            Bank[] banks = new Bank[] { new Bank { BankId = 1, Name = "Tinkoff" }, new Bank { BankId = 2, Name = "MKB" } };

            return new Deposit[]
            {
                new Deposit
                {
                    DepositId = 1,
                    Bank = banks[0],
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
                    DepositId = 2,
                    Bank = banks[1],
                    InitialFund = 805000m,
                    InterestRate = 5.5m,
                    CapitalizationTermsPerYear = 12,
                    Currency = Currency.RUR,
                    OpeningDate = new DateTime(2019, 2, 2),
                    ClosingDate = new DateTime(2020, 7, 26),
                    HasCapitalization = true
                },
                new Deposit
                {
                    DepositId = 3,
                    Bank = banks[1],
                    InitialFund = 500000m,
                    InterestRate = 6m,
                    CapitalizationTermsPerYear = 12,
                    Currency = Currency.RUR,
                    OpeningDate = new DateTime(2019, 2, 2),
                    ClosingDate = new DateTime(2020, 7, 26),
                    HasCapitalization = true
                }
            }.AsQueryable<Deposit>();
        }
    }
}
