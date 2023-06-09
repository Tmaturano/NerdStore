﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Orders.Domain;

namespace NS.Orders.Infra.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.Code)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Vouchers");
    }
}