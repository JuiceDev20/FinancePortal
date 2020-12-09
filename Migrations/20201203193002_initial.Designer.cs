﻿// <auto-generated />
using System;
using FinancePortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FinancePortal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201203193002_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FinancePortal.Models.CategoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("ActualAmount")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<decimal>("TargetAmount")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.ToTable("CategoryItem");
                });

            modelBuilder.Entity("FinancePortal.Models.FPUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("AvatarFile")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FinancePortal.Models.Household", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Greeting")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("Household");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ContentType")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<byte[]>("FileData")
                        .HasColumnType("bytea")
                        .HasMaxLength(4000);

                    b.Property<string>("FileName")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdAttachment");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdBankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccountType")
                        .HasColumnType("integer")
                        .HasMaxLength(35);

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.Property<string>("FPUserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("StartingBalance")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdBankAccount");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdCategory");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<string>("Body")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<Guid>("Code")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("RoleName")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdInvitation");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Body")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HouseholdId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Subject")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdNotification");
                });

            modelBuilder.Entity("FinancePortal.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int>("CategryItemId")
                        .HasColumnType("integer");

                    b.Property<string>("ContentType")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HouseholdBankAccountId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Memo")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FPUserId")
                        .HasColumnType("character varying(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdAttachment", b =>
                {
                    b.HasOne("FinancePortal.Models.Household", null)
                        .WithMany("Attachments")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdBankAccount", b =>
                {
                    b.HasOne("FinancePortal.Models.Household", null)
                        .WithMany("BankAccounts")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdCategory", b =>
                {
                    b.HasOne("FinancePortal.Models.Household", null)
                        .WithMany("Categories")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdInvitation", b =>
                {
                    b.HasOne("FinancePortal.Models.Household", null)
                        .WithMany("Invitations")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancePortal.Models.HouseholdNotification", b =>
                {
                    b.HasOne("FinancePortal.Models.Household", null)
                        .WithMany("Notifications")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FinancePortal.Models.FPUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FinancePortal.Models.FPUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancePortal.Models.FPUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FinancePortal.Models.FPUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
