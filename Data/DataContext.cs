using System;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Models;


namespace RealPetApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {

        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Handler> Handlers { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ClubSport> ClubSports { get; set; }
        public DbSet<HandlerSport> HandlerSports { get; set; }
        public DbSet<DogSport> DogSports { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<DogTitle> DogTitles { get; set; }
        public DbSet<WallPost> Wallposts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<DogPhoto> DogPhotos { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubSport>()
                .HasKey(pc => new { pc.ClubId, pc.SportId });
            modelBuilder.Entity<ClubSport>()
                .HasOne(p => p.Club)
                .WithMany(pc => pc.ClubSports)
                .HasForeignKey(p => p.ClubId);
            modelBuilder.Entity<ClubSport>()
                .HasOne(p => p.Sport)
                .WithMany(pc => pc.ClubSports)
                .HasForeignKey(c => c.SportId);

            modelBuilder.Entity<HandlerSport>()
               .HasKey(po => new { po.UserProfileId, po.SportId });
            modelBuilder.Entity<HandlerSport>()
                .HasOne(p => p.UserProfile)
                .WithMany(pc => pc.HandlerSports)
                .HasForeignKey(p => p.UserProfileId);
            modelBuilder.Entity<HandlerSport>()
                .HasOne(p => p.Sport)
                .WithMany(pc => pc.HandlerSports)
                .HasForeignKey(c => c.SportId);

            modelBuilder.Entity<DogSport>()
               .HasKey(po => new { po.DogId, po.SportId });
            modelBuilder.Entity<DogSport>()
                .HasOne(p => p.Dog)
                .WithMany(pc => pc.DogSports)
                .HasForeignKey(p => p.DogId);
            modelBuilder.Entity<DogSport>()
                .HasOne(p => p.Sport)
                .WithMany(pc => pc.DogSports)
                .HasForeignKey(c => c.SportId);

            modelBuilder.Entity<DogTitle>()
               .HasKey(po => new { po.DogId, po.TitleId });
            modelBuilder.Entity<DogTitle>()
                .HasOne(p => p.Dog)
                .WithMany(pc => pc.DogTitles)
                .HasForeignKey(p => p.DogId);
            modelBuilder.Entity<DogTitle>()
                .HasOne(p => p.Title)
                .WithMany(pc => pc.DogTitles)
                .HasForeignKey(c => c.TitleId);
        }
    }
}

