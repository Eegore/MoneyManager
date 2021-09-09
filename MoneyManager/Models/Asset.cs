using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class Asset
    {
        public Int32 AssetId { get; set; }

        public Int16 BrokerId { get; set; }

        public Broker Broker { get; set; }

        public AssetType AssetType { get; set; }

        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public Decimal AveragePrice { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public Decimal ExpectedYield { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Quantity { get; set; }

        public Decimal Balance => AveragePrice * Quantity + ExpectedYield;

        public String Name { get; set; }

        public String Figi { get; set; }

        public String Isin { get; set; }

        public String Ticker { get; set; }
    }
}
