using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;

namespace MoneyManager.Components
{
    public class BankMenuViewComponent : ViewComponent
    {
        private IBankRepository repository;

        public BankMenuViewComponent(IBankRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.Banks);
        }
    }
}
