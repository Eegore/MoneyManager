using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class EFBankRepository : IBankRepository
    {
        private ApplicationDbContext context;

        public EFBankRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Bank> Banks => context.Banks;
    }
}
