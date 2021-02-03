// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Binshop.Data.Catalog.Configurations
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Order)
                   .WithOne()
                   .HasForeignKey(nameof(PaymentTransaction.OrderId))
                   .IsRequired()
                   .HasConstraintName("FK_transaction2order")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
