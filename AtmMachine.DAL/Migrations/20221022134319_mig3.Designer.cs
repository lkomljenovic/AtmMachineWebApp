﻿// <auto-generated />
using AtmMachine.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtmMachine.DAL.Migrations
{
    [DbContext(typeof(AtmMachineDbContext))]
    [Migration("20221022134319_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AtmMachine.Model.Account", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountNumber");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountNumber = "1122334455",
                            Pin = "1234"
                        },
                        new
                        {
                            AccountNumber = "1234567890",
                            Pin = "5432"
                        });
                });

            modelBuilder.Entity("AtmMachine.Model.AccountDetails", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AccountNumber");

                    b.ToTable("AccountDetails");

                    b.HasData(
                        new
                        {
                            AccountNumber = "1122334455",
                            Balance = 1234.56m
                        },
                        new
                        {
                            AccountNumber = "1234567890",
                            Balance = 5999.56m
                        });
                });

            modelBuilder.Entity("AtmMachine.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountNumber = "1122334455",
                            Name = "Luka",
                            Surname = "Komljenovic",
                            Username = "lkomljenovic"
                        },
                        new
                        {
                            Id = 2,
                            AccountNumber = "1234567890",
                            Name = "New",
                            Surname = "User",
                            Username = "nUser"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
