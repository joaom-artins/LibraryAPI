using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Book>? Books { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Book>().HasKey(i => i.ID); 
            mb.Entity<Book>().Property(i=>i.Title).HasMaxLength(125).IsRequired();
            mb.Entity<Book>().Property(i => i.Description).HasMaxLength(255).IsRequired();
            mb.Entity<Book>().Property(i => i.Author).HasMaxLength(125);
            mb.Entity<Book>().Property(i => i.Price).HasPrecision(10, 2).IsRequired();
        }
    }
}
