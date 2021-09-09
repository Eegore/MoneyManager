using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Network;
using Tinkoff.Trading.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MoneyManager.Models
{
    public class EFAssetRepository : IAssetRepository
    {
        private ApplicationDbContext context;

        private readonly Client client;

        public EFAssetRepository(ApplicationDbContext ctx, IOptions<Client> options)
        {
            context = ctx;
            client = options.Value;
        }

        public IQueryable<Asset> Assets => context.Assets;

        public async Task<Int32> ImportAssetsAsync()
        {
            context.Assets.RemoveRange(context.Assets);

            var portfolio = await ConnectionFactory.GetConnection(client.Token).Context.PortfolioAsync();

            var broker = context.Brokers.FirstOrDefault();

            foreach (var position in portfolio.Positions)
            {
                Asset asset = new Asset
                {
                    Broker = broker,
                    AssetType = (AssetType)position.InstrumentType,
                    Currency = (Currency)position.AveragePositionPrice.Currency,
                    AveragePrice = position.AveragePositionPrice.Value,
                    ExpectedYield = position.ExpectedYield.Value,
                    Quantity = position.Balance,
                    Name = position.Name,
                    Figi = position.Figi,
                    Isin = position.Isin,
                    Ticker = position.Ticker
                };

                context.Assets.Add(asset);
            }

            return context.SaveChanges();
        }
    }
}
