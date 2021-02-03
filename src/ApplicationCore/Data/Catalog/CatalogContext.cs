// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Binshop.Data.Catalog
{
    public class CatalogContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<PaymentTransaction> Transactions { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                                                         type => type.Namespace == nameof(Configurations));
        }
    }
}
