using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Models;

namespace WebApplication2.Data
{
    public class EducationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Museum> Museums { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ExhibitionItem> ExhibitionItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=11223344ag")
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exhibition>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExhibitionItem>().Property(eI => eI.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Museum>().Property(museum => museum.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(order => order.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Museum>().HasMany(m => m.Exhibitions).WithOne(e => e.Museum);
            modelBuilder.Entity<Exhibition>().HasMany(e => e.Orders).WithOne(order => order.Exhibition);
            modelBuilder.Entity<Exhibition>().HasMany(e => e.ExhibitionItems).WithOne(eI => eI.Exhibition);
            modelBuilder.Entity<User>().HasMany(user => user.Orders).WithOne(order => order.User);
            modelBuilder.Entity<User>().HasMany(user => user.Orders).WithOne(order => order.User);
        }
    }
}
