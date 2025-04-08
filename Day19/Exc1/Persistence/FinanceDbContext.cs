using Exc1.Models;
using Microsoft.EntityFrameworkCore;

namespace Exc1.Persistence;

public class FinanceDbContext : DbContext
{
    private readonly string _dbPath = "finance.db";

    public DbSet<TransactionModel> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<TransactionModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Type).IsRequired().HasMaxLength(20);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);

            entity.Ignore(e => e.UserName);

            entity.Property(e => e.CategoryId).IsRequired();

            entity.HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public void InitializeDatabase()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing database: {ex.Message}");
        }
    }
}