using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System.Reflection.Emit;

namespace ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Ноутбук", Price = 79990, Category = "Электроника", Stock = 15 },
            new Product { Id = 2, Name = "Мышь", Price = 1290, Category = "Аксессуары", Stock = 50 },
            new Product { Id = 3, Name = "Клавиатура", Price = 3490, Category = "Аксессуары", Stock = 30 }
        );
    }
}