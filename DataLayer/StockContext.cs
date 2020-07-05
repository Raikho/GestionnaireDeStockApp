
using DataTransfertObject;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class StockContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LoginSession> LoginSessions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockDB;Integrated Security=True;");
        }
    }
}
