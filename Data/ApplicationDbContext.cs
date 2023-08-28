using Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetail { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
		public DbSet<PaymentAccept> PaymentAccept { get; set; }

	}
}