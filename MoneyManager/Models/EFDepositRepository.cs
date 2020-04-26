using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Models
{
    public class EFDepositRepository : IDepositRepository
    {
        private ApplicationDbContext context;

        public EFDepositRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Deposit> Deposits => context.Deposits.Include(d => d.Bank);

        public IQueryable<Bank> Banks => context.Banks;

        public void SaveDeposit(Deposit deposit)
        {
            Bank bank = context.Banks.FirstOrDefault(b => b.BankId == deposit.BankId);

            if (deposit.DepositId == 0)
            {
                deposit.Bank = bank;
                context.Deposits.Add(deposit);
            }

            else
            {
                Deposit dbEntry = context.Deposits.FirstOrDefault(d => d.DepositId == deposit.DepositId);

                if (dbEntry != null)
                {
                    dbEntry.Bank = bank;
                    dbEntry.Currency = deposit.Currency;
                    dbEntry.InitialFund = deposit.InitialFund;
                    dbEntry.InterestRate = deposit.InterestRate;
                    dbEntry.CapitalizationTermsPerYear = deposit.CapitalizationTermsPerYear;
                    dbEntry.OpeningDate = deposit.OpeningDate;
                    dbEntry.ClosingDate = deposit.ClosingDate;
                    dbEntry.HasCapitalization = deposit.HasCapitalization;
                }
            }

            context.SaveChanges();
        }

        public Deposit DeleteDeposit(Int32 depositId)
        {
            Deposit dbEntry = context.Deposits.FirstOrDefault(d => d.DepositId == depositId);

            if (dbEntry != null)
            {
                context.Deposits.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public Int32 DeleteAllDeposits()
        {
            context.Deposits.RemoveRange(context.Deposits);
            return context.SaveChanges();
        }

        public Int32 AddDeposits(IEnumerable<Deposit> deposits)
        {
            foreach(Deposit d in deposits)
            {
                d.Bank = context.Banks.FirstOrDefault(b => b.BankId == d.BankId);
            }

            context.Deposits.AddRange(deposits);

            return context.SaveChanges();
        }

    }
}
