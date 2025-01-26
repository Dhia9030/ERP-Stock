﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockManagement.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("StockManagement.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("StockManagement.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("StockManagement.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("StockManagement.Models.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ManufacturerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("StockManagement.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DelayedDates")
                        .HasColumnType("longtext");

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("double");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<DateTime>("RealExecutionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");

                    b.HasDiscriminator<string>("OrderType").HasValue("Order");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StockManagement.Models.OrderProducts", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("OrderProductId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("StockManagement.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("ProductType").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StockManagement.Models.ProductBlock", b =>
                {
                    b.Property<int>("ProductBlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProductBlockId"));

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("double");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("ProductBlockType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("varchar(21)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductBlockId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductBlocks");

                    b.HasDiscriminator<string>("ProductBlockType").HasValue("ProductBlock");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StockManagement.Models.ProductItem", b =>
                {
                    b.Property<int>("ProductItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProductItemId"));

                    b.Property<int?>("ProductBlockId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductItemId");

                    b.HasIndex("ProductBlockId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("StockManagement.Models.ProductSupplier", b =>
                {
                    b.Property<int>("ProductSupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProductSupplierId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ProductSupplierId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("ProductSupplier");
                });

            modelBuilder.Entity("StockManagement.Models.StockMovement", b =>
                {
                    b.Property<int>("StockMovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StockMovementId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("DestinationLocationId")
                        .HasColumnType("int");

                    b.Property<int>("DestinationProductBlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MovementDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MovementType")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SourceLocationId")
                        .HasColumnType("int");

                    b.Property<int>("SourceProductBlockId")
                        .HasColumnType("int");

                    b.HasKey("StockMovementId");

                    b.HasIndex("DestinationLocationId");

                    b.HasIndex("DestinationProductBlockId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SourceLocationId");

                    b.HasIndex("SourceProductBlockId");

                    b.ToTable("StockMovements");
                });

            modelBuilder.Entity("StockManagement.Models.StockMovementItems", b =>
                {
                    b.Property<int>("StockMovementItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StockMovementItemId"));

                    b.Property<int>("ProductItemId")
                        .HasColumnType("int");

                    b.Property<int>("StockMovementId")
                        .HasColumnType("int");

                    b.HasKey("StockMovementItemId");

                    b.HasIndex("ProductItemId");

                    b.HasIndex("StockMovementId");

                    b.ToTable("StockMovementItems");
                });

            modelBuilder.Entity("StockManagement.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("StockManagement.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("WarehouseId"));

                    b.Property<string>("Location")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("StockManagement.Models.BuyOrder", b =>
                {
                    b.HasBaseType("StockManagement.Models.Order");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasIndex("SupplierId");

                    b.HasDiscriminator().HasValue("BuyOrder");
                });

            modelBuilder.Entity("StockManagement.Models.SellOrder", b =>
                {
                    b.HasBaseType("StockManagement.Models.Order");

                    b.HasDiscriminator().HasValue("SellOrder");
                });

            modelBuilder.Entity("StockManagement.Models.ClothingProduct", b =>
                {
                    b.HasBaseType("StockManagement.Models.Product");

                    b.Property<string>("FabricType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Size")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Clothing");
                });

            modelBuilder.Entity("StockManagement.Models.ElectronicsProduct", b =>
                {
                    b.HasBaseType("StockManagement.Models.Product");

                    b.Property<string>("EnergyClass")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("WarrantyYears")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Electronics");
                });

            modelBuilder.Entity("StockManagement.Models.FoodProduct", b =>
                {
                    b.HasBaseType("StockManagement.Models.Product");

                    b.Property<int>("StorageTemperature")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Food");
                });

            modelBuilder.Entity("StockManagement.Models.FoodProductBlock", b =>
                {
                    b.HasBaseType("StockManagement.Models.ProductBlock");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.HasDiscriminator().HasValue("FoodProductBlock");
                });

            modelBuilder.Entity("StockManagement.Models.Location", b =>
                {
                    b.HasOne("StockManagement.Models.Warehouse", "Warehouse")
                        .WithMany("Locations")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("StockManagement.Models.Order", b =>
                {
                    b.HasOne("StockManagement.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");
                });

            modelBuilder.Entity("StockManagement.Models.OrderProducts", b =>
                {
                    b.HasOne("StockManagement.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StockManagement.Models.Product", b =>
                {
                    b.HasOne("StockManagement.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("StockManagement.Models.ProductBlock", b =>
                {
                    b.HasOne("StockManagement.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.Product", "Product")
                        .WithMany("ProductBlocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StockManagement.Models.ProductItem", b =>
                {
                    b.HasOne("StockManagement.Models.ProductBlock", "ProductBlock")
                        .WithMany("ProductItems")
                        .HasForeignKey("ProductBlockId");

                    b.Navigation("ProductBlock");
                });

            modelBuilder.Entity("StockManagement.Models.ProductSupplier", b =>
                {
                    b.HasOne("StockManagement.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("StockManagement.Models.StockMovement", b =>
                {
                    b.HasOne("StockManagement.Models.Location", "DestinationLocation")
                        .WithMany()
                        .HasForeignKey("DestinationLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.ProductBlock", "DestinationProductBlock")
                        .WithMany()
                        .HasForeignKey("DestinationProductBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.Order", "Order")
                        .WithMany("StockMovements")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StockManagement.Models.Location", "SourceLocation")
                        .WithMany()
                        .HasForeignKey("SourceLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.ProductBlock", "SourceProductBlock")
                        .WithMany()
                        .HasForeignKey("SourceProductBlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationLocation");

                    b.Navigation("DestinationProductBlock");

                    b.Navigation("Order");

                    b.Navigation("SourceLocation");

                    b.Navigation("SourceProductBlock");
                });

            modelBuilder.Entity("StockManagement.Models.StockMovementItems", b =>
                {
                    b.HasOne("StockManagement.Models.ProductItem", "ProductItem")
                        .WithMany()
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockManagement.Models.StockMovement", "StockMovement")
                        .WithMany()
                        .HasForeignKey("StockMovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductItem");

                    b.Navigation("StockMovement");
                });

            modelBuilder.Entity("StockManagement.Models.BuyOrder", b =>
                {
                    b.HasOne("StockManagement.Models.Supplier", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("StockManagement.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StockManagement.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StockManagement.Models.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StockManagement.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("StockMovements");
                });

            modelBuilder.Entity("StockManagement.Models.Product", b =>
                {
                    b.Navigation("ProductBlocks");
                });

            modelBuilder.Entity("StockManagement.Models.ProductBlock", b =>
                {
                    b.Navigation("ProductItems");
                });

            modelBuilder.Entity("StockManagement.Models.Supplier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StockManagement.Models.Warehouse", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
