using BookApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Data.Database
{
    public class DBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DBContext()
        {
            Database.EnsureCreated();
        }
        string connString = "";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.BookId, x.AuthorId });
            modelBuilder.Entity<Book>().HasMany(b => b.Tags).WithMany(t => t.Books).UsingEntity("BooksTags");
        }
    }
}
