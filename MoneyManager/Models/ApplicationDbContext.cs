using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyManager.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Broker> Brokers { get; set; }
    }
}
