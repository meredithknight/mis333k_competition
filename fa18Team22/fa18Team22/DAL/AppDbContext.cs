using System;
using fa18Team22.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fa18Team22.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //db set for each model class
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Procurement> Procurements { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //AppUser is implicitly set
    }


}
