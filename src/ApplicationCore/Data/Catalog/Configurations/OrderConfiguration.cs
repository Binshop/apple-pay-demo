// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Binshop.Data.Catalog.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.ProductId)
                   .IsRequired();

            builder.Property(b => b.Unit)
                   .IsRequired();

            builder.Property(b => b.Price)
                   .IsRequired(true)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
