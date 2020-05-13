using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne<Password>(u => u.Password)
                        .WithOne(p => p.User)
                        .HasForeignKey<Password>(p => p.UserId);

            modelBuilder.Entity<User>()
                        .HasMany<Photo>(u => u.Photos)
                        .WithOne()
                        .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                        .HasOne<UserDescription>(u => u.UserDescription)
                        .WithOne()
                        .HasForeignKey<UserDescription>(descr => descr.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDescription> UserDescriptions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}