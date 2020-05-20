using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using DatingApp.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                        .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                        .HasOne<Role>(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                        .HasOne<User>(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId);

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
        public DbSet<UserDescription> UserDescriptions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}