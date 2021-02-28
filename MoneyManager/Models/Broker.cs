using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace MoneyManager.Models
{
    public class Broker
    {
        [BindNever]
        public Int16 BrokerId { get; set; }

        [BindNever]
        public String Name { get; set; }
    }
}
