using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MoneyManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:MoneyManager:ConnectionString"]));
            services.AddScoped<IDepositRepository, EFDepositRepository>();
            services.AddScoped<IBankRepository, EFBankRepository>();
            services.AddScoped<IBrokerRepository, EFBrokerRepository>();
            services.AddScoped<IAssetRepository, EFAssetRepository>();
            services.AddRazorPages();
            services.Configure<Client>(Configuration.GetSection("Data:MoneyManager:Client"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("default", "{controller=Deposit}/{action=List}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Portfolio}/{action=AssetList}");
                endpoints.MapControllers();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
