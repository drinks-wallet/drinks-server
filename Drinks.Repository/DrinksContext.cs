namespace Drinks.Repository
{
    using System;
    using System.Data.Entity;
    using Drinks.Entities;

    public interface IDrinksContext : IDisposable
    {
        DbSet<Transaction> Transactions { get; }
        DbSet<User> Users { get; }
        DbSet<Product> Products { get; }
        Database Database { get; }
        int SaveChanges();
    }

    public class DrinksContext : DbContext, IDrinksContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}