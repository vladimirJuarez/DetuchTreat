using System;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasData(new Order(){
                    Id = 1,
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345"
                });
            

            // builder.Entity<Product>()
            //   .Property(p => p.Price)
            //   .HasColumnType("decimal(18,2)");

            // builder.Entity<OrderItem>()
            //   .Property(p => p.UnitPrice)
            //   .HasColumnType("decimal(18,2)");
        }
    }
}
