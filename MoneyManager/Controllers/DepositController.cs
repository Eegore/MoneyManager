using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManager.Models;
using MoneyManager.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Controllers
{
    public class DepositController : Controller
    {
        private IDepositRepository repository;

        public DepositController(IDepositRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(Int16? bankId) => View( new DepositListViewModel
        {
            Deposits = repository.Deposits.Where( d => bankId == null || d.Bank.BankId == bankId ),
            SelectedBankId = bankId
        });

        public ViewResult Edit(Int32 depositId)
        {
            ViewBag.Banks = new SelectList(repository.Banks, "BankId", "Name");
            return View(repository.Deposits.FirstOrDefault(d => d.DepositId == depositId));
        }

        [HttpPost]
        public IActionResult Edit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                repository.SaveDeposit(deposit);
                TempData["message"] = $"{deposit.Currency.ToString()} deposit has been saved";
                return RedirectToAction("List");
            }

            else
            {
                return View(deposit);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Banks = new SelectList(repository.Banks, "BankId", "Name");

            return View("Edit", new Deposit());
        }

        [HttpPost]
        public IActionResult Delete(Int32 depositId)
        {
            Deposit deletedDeposit = repository.DeleteDeposit(depositId);

            if (deletedDeposit != null)
            {
                TempData["message"] = $"{deletedDeposit.Currency.ToString()} deposit was deleted";
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult ProcessData(Byte actionId, String filePath)
        {
            switch (actionId)
            {
                case 0:
                    ExportToJson(filePath);
                    break;
                case 1:
                    ImportFromJson(filePath);
                    break;
            }

            return RedirectToAction("List");
        }

        public void ExportToJson(String filePath)
        {
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(repository.Deposits));
        }

        public void ImportFromJson(String filePath)
        {
            Int32 deleted = repository.DeleteAllDeposits();

            String json = System.IO.File.ReadAllText(filePath);
            IEnumerable<Deposit> deposits = JsonConvert.DeserializeObject<IEnumerable<Deposit>>(json);

            Int32 added = repository.AddDeposits(deposits);

            TempData["message"] = $"{deleted.ToString()} deposits were deleted. {added.ToString()} deposits were added";
        }

    }
}
