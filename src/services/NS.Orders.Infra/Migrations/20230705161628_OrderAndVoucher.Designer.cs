﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NS.Orders.Infra.Data;

#nullable disable

namespace NS.Orders.Infra.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20230705161628_OrderAndVoucher")]
    partial class OrderAndVoucher
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("MySequence")
                .StartsAt(1000L);

            modelBuilder.Entity("NS.Orders.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MySequence");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("VoucherAlreadyUsed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("NS.Orders.Domain.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductImage")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitaryValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("NS.Orders.Domain.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<decimal?>("DiscountValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UseDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Used")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Vouchers", (string)null);
                });

            modelBuilder.Entity("NS.Orders.Domain.Orders.Order", b =>
                {
                    b.HasOne("NS.Orders.Domain.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherId");

                    b.OwnsOne("NS.Orders.Domain.Orders.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("City");

                            b1.Property<string>("Complement")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Complement");

                            b1.Property<string>("Neighborhood")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Neighborhood");

                            b1.Property<string>("Number")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Number");

                            b1.Property<string>("State")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("NS.Orders.Domain.Orders.OrderItem", b =>
                {
                    b.HasOne("NS.Orders.Domain.Orders.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("NS.Orders.Domain.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}