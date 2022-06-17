﻿// <auto-generated />
using System;
using CleaningService.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleaningService.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CleaningService.Models.CleanPricing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("PriceForM2")
                        .HasColumnType("money");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time(0)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CleanPricing", (string)null);
                });

            modelBuilder.Entity("CleaningService.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CleanSizeM2")
                        .HasColumnType("int");

                    b.Property<int>("IdCleanPrice")
                        .HasColumnType("int");

                    b.Property<int>("IdPayList")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCleanPrice");

                    b.HasIndex("IdPayList");

                    b.HasIndex("IdUser");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CleaningService.Models.PayList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Sum")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("PayList", (string)null);
                });

            modelBuilder.Entity("CleaningService.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CleaningService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdRole");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CleaningService.Models.Order", b =>
                {
                    b.HasOne("CleaningService.Models.CleanPricing", "IdCleanPriceNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("IdCleanPrice")
                        .IsRequired()
                        .HasConstraintName("Orders_CleanPricing_Id_FK");

                    b.HasOne("CleaningService.Models.PayList", "IdPayListNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("IdPayList")
                        .IsRequired()
                        .HasConstraintName("Orders_PayList_Id_FK");

                    b.HasOne("CleaningService.Models.User", "IdUserNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("IdUser")
                        .IsRequired()
                        .HasConstraintName("Orders_User_Id_FK");

                    b.Navigation("IdCleanPriceNavigation");

                    b.Navigation("IdPayListNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("CleaningService.Models.User", b =>
                {
                    b.HasOne("CleaningService.Models.Role", "IdRoleNavigation")
                        .WithMany("Users")
                        .HasForeignKey("IdRole")
                        .IsRequired()
                        .HasConstraintName("Users_Roles_Id_FK");

                    b.Navigation("IdRoleNavigation");
                });

            modelBuilder.Entity("CleaningService.Models.CleanPricing", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CleaningService.Models.PayList", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CleaningService.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CleaningService.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
