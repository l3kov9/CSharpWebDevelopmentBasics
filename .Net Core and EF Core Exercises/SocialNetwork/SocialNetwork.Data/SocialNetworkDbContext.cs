namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<FriendShip> FriendShips { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=SocialNetworkDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<FriendShip>()
                .HasKey(f => new
                {
                    f.FromUserId,
                    f.ToUserId
                });

            builder
                .Entity<User>()
                .HasMany(u => u.FromFriends)
                .WithOne(f => f.FromUser)
                .HasForeignKey(f => f.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<User>()
                .HasMany(u => u.ToFriends)
                .WithOne(f => f.ToUser)
                .HasForeignKey(f => f.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Album>()
                .HasOne(a => a.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(a => a.UserId);

            builder
                .Entity<AlbumPicture>()
                .HasKey(ap => new
                {
                    ap.AlbumId,
                    ap.PictureId
                });

            builder
                .Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Album)
                .HasForeignKey(p => p.AlbumId);

            builder
                .Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithOne(a => a.Picture)
                .HasForeignKey(a => a.PictureId);

            builder
                .Entity<AlbumTag>()
                .HasKey(a => new
                {
                    a.TagId,
                    a.AlbumId
                });

            builder
                .Entity<Tag>()
                .HasMany(t => t.Albums)
                .WithOne(a => a.Tag)
                .HasForeignKey(a => a.TagId);

            builder
                .Entity<Album>()
                .HasMany(a => a.Tags)
                .WithOne(t => t.Album)
                .HasForeignKey(t => t.AlbumId);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }
        
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
