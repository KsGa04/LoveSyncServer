using LoveSyncServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LoveSyncServer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблицы
        public DbSet<User> users { get; set; }
        public DbSet<Couple> couples { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<FinanceRecord> FinanceRecords { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Таблица users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Couple)
                .WithMany(c => c.users)
                .HasForeignKey(u => u.Couple_id)
                .OnDelete(DeleteBehavior.SetNull);

            // Таблица TaskItem
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Author)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Couple)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CoupleId);

            // Таблица ShoppingItem
            modelBuilder.Entity<ShoppingItem>()
                .HasOne(s => s.Couple)
                .WithMany(c => c.ShoppingItems)
                .HasForeignKey(s => s.CoupleId);

            // Таблица Gift
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Author)
                .WithMany(u => u.Gifts)
                .HasForeignKey(g => g.AuthorId);

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Couple)
                .WithMany(c => c.Gifts)
                .HasForeignKey(g => g.CoupleId);

            // Таблица FinanceRecord
            modelBuilder.Entity<FinanceRecord>()
                .HasOne(f => f.Payer)
                .WithMany(u => u.FinanceRecords)
                .HasForeignKey(f => f.PayerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FinanceRecord>()
                .HasOne(f => f.Couple)
                .WithMany(c => c.FinanceRecords)
                .HasForeignKey(f => f.CoupleId);

            // Таблица Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Name); // Имя — это ключ
        }
    }
}
