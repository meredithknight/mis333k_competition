﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fa18Team22.DAL;

namespace fa18Team22.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181204235420_updateformatofavgrating")]
    partial class updateformatofavgrating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("fa18Team22.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreditCard1")
                        .HasMaxLength(16);

                    b.Property<string>("CreditCard2")
                        .HasMaxLength(16);

                    b.Property<string>("CreditCard3")
                        .HasMaxLength(16);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserStatus");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("fa18Team22.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<decimal>("BookCost");

                    b.Property<string>("BookDetail");

                    b.Property<int?>("GenreID");

                    b.Property<int>("Inventory");

                    b.Property<bool>("IsDiscontinued");

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("ReplenishMinimum");

                    b.Property<decimal>("SalesPrice");

                    b.Property<string>("Title");

                    b.Property<int>("UniqueID");

                    b.HasKey("BookID");

                    b.HasIndex("GenreID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("fa18Team22.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName");

                    b.HasKey("GenreID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("fa18Team22.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerId");

                    b.Property<bool>("IsComplete");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderNumber");

                    b.Property<string>("Payment")
                        .HasMaxLength(16);

                    b.Property<int?>("PromoID");

                    b.Property<decimal>("ShippingCost");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromoID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("fa18Team22.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID");

                    b.Property<int?>("OrderID");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("BookID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("fa18Team22.Models.Procurement", b =>
                {
                    b.Property<int>("ProcurementID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID");

                    b.Property<string>("EmployeeId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ProcurementDate");

                    b.Property<bool?>("ProcurementStatus");

                    b.Property<short>("Quantity");

                    b.HasKey("ProcurementID");

                    b.HasIndex("BookID");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Procurements");
                });

            modelBuilder.Entity("fa18Team22.Models.Promo", b =>
                {
                    b.Property<int>("PromoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DiscountAmount");

                    b.Property<decimal>("MinimumSpend");

                    b.Property<string>("PromoCode")
                        .HasMaxLength(20);

                    b.Property<bool>("ShippingWaiver");

                    b.Property<bool>("Status");

                    b.HasKey("PromoID");

                    b.ToTable("Promos");
                });

            modelBuilder.Entity("fa18Team22.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("ApprovalStatus");

                    b.Property<string>("ApproverId");

                    b.Property<string>("AuthorId");

                    b.Property<int?>("BookID");

                    b.Property<decimal>("Rating");

                    b.Property<string>("RejecterId");

                    b.Property<string>("ReviewText")
                        .HasMaxLength(100);

                    b.HasKey("ReviewID");

                    b.HasIndex("ApproverId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookID");

                    b.HasIndex("RejecterId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("fa18Team22.Models.Book", b =>
                {
                    b.HasOne("fa18Team22.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreID");
                });

            modelBuilder.Entity("fa18Team22.Models.Order", b =>
                {
                    b.HasOne("fa18Team22.Models.AppUser", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("fa18Team22.Models.Promo", "Promo")
                        .WithMany("Orders")
                        .HasForeignKey("PromoID");
                });

            modelBuilder.Entity("fa18Team22.Models.OrderDetail", b =>
                {
                    b.HasOne("fa18Team22.Models.Book", "Book")
                        .WithMany("OrderDetails")
                        .HasForeignKey("BookID");

                    b.HasOne("fa18Team22.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("fa18Team22.Models.Procurement", b =>
                {
                    b.HasOne("fa18Team22.Models.Book", "Book")
                        .WithMany("Procurements")
                        .HasForeignKey("BookID");

                    b.HasOne("fa18Team22.Models.AppUser", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("fa18Team22.Models.Review", b =>
                {
                    b.HasOne("fa18Team22.Models.AppUser", "Approver")
                        .WithMany("ReviewsApproved")
                        .HasForeignKey("ApproverId");

                    b.HasOne("fa18Team22.Models.AppUser", "Author")
                        .WithMany("ReviewsWritten")
                        .HasForeignKey("AuthorId");

                    b.HasOne("fa18Team22.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookID");

                    b.HasOne("fa18Team22.Models.AppUser", "Rejecter")
                        .WithMany("ReviewsRejected")
                        .HasForeignKey("RejecterId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("fa18Team22.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("fa18Team22.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("fa18Team22.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("fa18Team22.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
