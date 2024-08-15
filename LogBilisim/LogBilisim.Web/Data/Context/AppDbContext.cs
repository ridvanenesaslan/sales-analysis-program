using LogBilisim.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Data.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<User>Users { get; set; }
    public DbSet<Customer>Customers { get; set; }
    public DbSet<Order>Orders { get;set; }
    public DbSet<OrderDetail>OrderDetails { get; set; }
    public DbSet<Payment>Payments { get; set; }
    public DbSet<Product>Products { get; set; }
    public DbSet<Category>Categories { get; set; }

}
