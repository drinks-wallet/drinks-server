namespace Drinks.Repository
{
    using System;
    using System.Data.Entity;
    using Drinks.Entities;
    using Drinks.Entities.Logging;

    public interface IDrinksContext : IDisposable
    {
        DbSet<Transaction> Transactions { get; }
        DbSet<User> Users { get; }
        DbSet<Product> Products { get; }
        DbSet<LogItem> Log { get; }
        Database Database { get; }
        // ReSharper disable once UnusedMethodReturnValue.Global
        int SaveChanges();
    }

    public class DrinksContext : DbContext, IDrinksContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LogItem> Log { get; set; }
    }
}