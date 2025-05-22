using Microsoft.EntityFrameworkCore;
using TaxApp.Models;

namespace TaxApp.Data
{
    public class TaxContext : DbContext
    {
        public DbSet<TaxRecord> TaxRecords { get; set; } = null!;

        public TaxContext() { }

        public TaxContext(DbContextOptions<TaxContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=taxes.db");
            }
        }
    }
}