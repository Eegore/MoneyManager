using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Models
{
    public interface IDepositRepository
    {
        IQueryable<Deposit> Deposits { get; }

        IQueryable<Bank> Banks { get; }

        void SaveDeposit(Deposit deposit);

        Deposit DeleteDeposit(Int32 depositId);

        Int32 DeleteAllDeposits();

        Int32 AddDeposits(IEnumerable<Deposit> deposits);
    }
}
