using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public interface IAssetRepository
    {
        IQueryable<Asset> Assets { get; }

        Task<Int32> ImportAssetsAsync();
    }
}
