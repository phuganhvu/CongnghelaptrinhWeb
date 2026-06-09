using Lesson3_CNLTWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson3_CNLTWeb.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(b => b.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(b => b.Author)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(b => b.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(b => b.PublishDate)
                    .HasColumnType("datetime");
            });
        }
    }
}
