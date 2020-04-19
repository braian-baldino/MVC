﻿// <auto-generated />
using System;
using Accountant.MVC.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accountant.MVC.Migrations
{
    [DbContext(typeof(AccountantContext))]
    partial class AccountantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Accountant.MVC.Models.AnualBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("AnualBalanceResult")
                        .HasColumnType("float");

                    b.Property<bool>("Positive")
                        .HasColumnType("bit");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AnualBalances");
                });

            modelBuilder.Entity("Accountant.MVC.Models.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnualBalanceId")
                        .HasColumnType("int");

                    b.Property<double?>("BalanceResult")
                        .HasColumnType("float");

                    b.Property<int?>("MonthId")
                        .HasColumnType("int");

                    b.Property<bool>("Positive")
                        .HasColumnType("bit");

                    b.Property<double?>("TotalIncomes")
                        .HasColumnType("float");

                    b.Property<double?>("TotalSpendings")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AnualBalanceId");

                    b.HasIndex("MonthId");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("Accountant.MVC.Models.DropDowns.EIncomeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IncomeTypes");
                });

            modelBuilder.Entity("Accountant.MVC.Models.DropDowns.EMonth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("Accountant.MVC.Models.DropDowns.ESpendingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SpendingTypes");
                });

            modelBuilder.Entity("Accountant.MVC.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BalanceId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Accountant.MVC.Models.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BalanceId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("Spendings");
                });

            modelBuilder.Entity("Accountant.MVC.Models.Balance", b =>
                {
                    b.HasOne("Accountant.MVC.Models.AnualBalance", null)
                        .WithMany("Balances")
                        .HasForeignKey("AnualBalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accountant.MVC.Models.DropDowns.EMonth", "Month")
                        .WithMany()
                        .HasForeignKey("MonthId");
                });

            modelBuilder.Entity("Accountant.MVC.Models.Income", b =>
                {
                    b.HasOne("Accountant.MVC.Models.Balance", null)
                        .WithMany("Incomes")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accountant.MVC.Models.Spending", b =>
                {
                    b.HasOne("Accountant.MVC.Models.Balance", null)
                        .WithMany("Spendings")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
