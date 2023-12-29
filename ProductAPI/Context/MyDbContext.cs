using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class MyDbContext:DbContext
    {
        public MyDbContext() : base() { }
        public MyDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost; Port = 6432; Database =etrade2; User Id = root; Password = 12345;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
