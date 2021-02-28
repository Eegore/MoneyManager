using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class EFBrokerRepository : IBrokerRepository
    {
        private ApplicationDbContext context;

        public EFBrokerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Broker> Brokers => context.Brokers;
    }
}
