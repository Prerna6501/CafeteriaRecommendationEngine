using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServerSide.Entity;

namespace ServerSide.Data
{
    public class CafeteriaDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public CafeteriaDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<FixedMeal> FixedMeals { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MenuItemType> MenuItemTypes { get; set; }
        public DbSet<VotingResult> VotingResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("REDatabase"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasMany(f => f.Feedbacks)
                .WithOne()
                .HasForeignKey(f => f.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuItem>()
                .HasOne(f => f.MenuItemType)
                .WithMany()
                .HasForeignKey(f => f.MenuItemTypeId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasMany(f => f.Feedbacks)
                .WithOne()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FixedMeal>()
                .HasOne(fm => fm.MenuItem)
                .WithMany()
                .HasForeignKey(fm => fm.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FixedMeal>()
                .HasOne(fm => fm.MealType)
                .WithMany()
                .HasForeignKey(fm => fm.MealTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.NotificationType)
                .WithMany()
                .HasForeignKey(n => n.NotificationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VotingResult>()
                .HasOne(v => v.MenuItem)
                .WithMany()
                .HasForeignKey(v => v.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
