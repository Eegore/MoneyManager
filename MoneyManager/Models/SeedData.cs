using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace MoneyManager.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            //context.Database.Migrate();

            if (!context.Deposits.Any())
            {
                context.Deposits.AddRange(
                    new Deposit
                    {
                        Bank = context.Banks.FirstOrDefault(b => b.Name == "Tinkoff"),
                        InitialFund = 1000m,
                        InterestRate = 2.5m,
                        CapitalizationTermsPerYear = 12,
                        Currency = Enum.Parse<Currency>("USD"),
                        OpeningDate = new DateTime(2019, 4, 4),
                        ClosingDate = new DateTime(2020, 4, 4),
                        HasCapitalization = false
                    },
                    new Deposit
                    {
                        Bank = context.Banks.FirstOrDefault(b => b.Name == "MKB"),
                        InitialFund = 500000m,
                        InterestRate = 5.5m,
                        CapitalizationTermsPerYear = 12,
                        Currency = Enum.Parse<Currency>("RUR"),
                        OpeningDate = new DateTime(2019, 2, 2),
                        ClosingDate = new DateTime(2020, 7, 26),
                        HasCapitalization = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
