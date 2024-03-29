﻿// <auto-generated />
using System;
using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220704232541_AddWagesPayRateAndExpensesPayRateColumnsInSalaryTable")]
    partial class AddWagesPayRateAndExpensesPayRateColumnsInSalaryTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InternationalWagesManager.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<float>("ExpensesRate")
                        .HasColumnType("real");

                    b.Property<decimal>("GrossPay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NetPay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<float>("WageRate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.SalaryComponents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("BonusHours")
                        .HasColumnType("real");

                    b.Property<float>("BonusWage")
                        .HasColumnType("real");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<float>("Expenses")
                        .HasColumnType("real");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalHours")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SalariesComponents");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.WorkConditions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Deductions")
                        .HasColumnType("real");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("ExpensesCurrencyId")
                        .HasColumnType("int");

                    b.Property<float>("PayRate")
                        .HasColumnType("real");

                    b.Property<int>("WageCurrencyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ExpensesCurrencyId");

                    b.HasIndex("WageCurrencyId");

                    b.ToTable("WorkConditions");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.Payment", b =>
                {
                    b.HasOne("InternationalWagesManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.Salary", b =>
                {
                    b.HasOne("InternationalWagesManager.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternationalWagesManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.SalaryComponents", b =>
                {
                    b.HasOne("InternationalWagesManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("InternationalWagesManager.Models.WorkConditions", b =>
                {
                    b.HasOne("InternationalWagesManager.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternationalWagesManager.Models.Currency", "ExpensesCurrency")
                        .WithMany()
                        .HasForeignKey("ExpensesCurrencyId");

                    b.HasOne("InternationalWagesManager.Models.Currency", "WageCurrency")
                        .WithMany()
                        .HasForeignKey("WageCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("ExpensesCurrency");

                    b.Navigation("WageCurrency");
                });
#pragma warning restore 612, 618
        }
    }
}
