using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    { 
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<Credit>()
        .HasOne<Product>(s => s.Product)
        .WithMany(g => g.Credits)
        .HasForeignKey(s => s.ProductId);

        modelBuilder.Entity<Credit>()
        .HasOne<Customer>(s => s.Customer)
        .WithMany(g => g.Credits)
        .HasForeignKey(s => s.CustomerId);
    }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products {get; set;}

}