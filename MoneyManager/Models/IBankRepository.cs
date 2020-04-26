using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public interface IBankRepository
    {
        IQueryable<Bank> Banks { get; }
    }
}
