using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoneyManager.Models
{
    public class Bank
    {
        [BindNever]
        public Int16 BankId { get; set; }

        [BindNever]
        public String Name { get; set; }
    }
}
