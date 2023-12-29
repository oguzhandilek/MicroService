using CustomerAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Context;

public class MyDbContext : DbContext
{

    public MyDbContext() : base() { }
    public MyDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host = localhost; Port = 6432; Database =etrade; User Id = root; Password = 12345;");
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
