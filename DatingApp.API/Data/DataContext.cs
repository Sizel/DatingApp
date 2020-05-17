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

            modelBuilder.Entity<User>()
                        .HasMany<Like>(u => u.Likees)
                        .WithOne(l => l.Liker)
                        .HasForeignKey(l => l.LikerId);

            modelBuilder.Entity<User>()
                        .HasMany<Like>(u => u.Likers)
                        .WithOne(l => l.Likee)
                        .HasForeignKey(l => l.LikeeId);

            modelBuilder.Entity<User>()
                        .HasMany<Message>(u => u.MessagesRecieved)
                        .WithOne(m => m.Recipient)
                        .HasForeignKey(m => m.RecipientId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                        .HasMany<Message>(u => u.MessagesSent)
                        .WithOne(m => m.Sender)
                        .HasForeignKey(m => m.SenderId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Like>()
                        .HasKey(l => new { l.LikerId, l.LikeeId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDescription> UserDescriptions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}