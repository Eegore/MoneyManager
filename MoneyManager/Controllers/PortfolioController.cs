using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Models.ViewModels;

namespace MoneyManager.Controllers
{
    public class PortfolioController : Controller
    {
        private IAssetRepository assetRepository;

        public PortfolioController(IAssetRepository repo)
        {
            assetRepository = repo;
        }

        public async Task<IActionResult> AssetList()
        {
            await assetRepository.ImportAssetsAsync();
            return View("Assets/List", new PortfolioViewModel{ Assets = assetRepository.Assets });
        }

    }
}
