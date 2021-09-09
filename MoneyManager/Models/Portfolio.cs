using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class Portfolio
    {
        public Broker Broker { get; set; }

        public IEnumerable<Asset> Assets { get; }

        public Portfolio (Broker broker, IEnumerable<Asset> assets)
        {
            Broker = broker;
            Assets = assets;
        }

    }
}
