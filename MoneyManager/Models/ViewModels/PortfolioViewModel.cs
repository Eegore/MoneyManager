using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManager.Models;


namespace MoneyManager.Models.ViewModels
{
    public class PortfolioViewModel
    {
        public IEnumerable<Asset> Assets { get; set; }
    }
}
