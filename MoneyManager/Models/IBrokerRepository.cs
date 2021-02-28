using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public interface IBrokerRepository
    {
        IQueryable<Broker> Brokers { get; }
    }
}
