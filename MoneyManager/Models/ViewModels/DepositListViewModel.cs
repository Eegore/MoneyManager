using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManager.Models;

namespace MoneyManager.Models.ViewModels
{
    public class DepositListViewModel
    {
        public IEnumerable<Deposit> Deposits { get; set; }

        public Int16? SelectedBankId { get; set; }
    }
}
