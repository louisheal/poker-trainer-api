using Microsoft.EntityFrameworkCore;

namespace PokerTrainerApi.DrawRanges.Repository;

public class PokerDbContext : DbContext
{
    public PokerDbContext(DbContextOptions<PokerDbContext> options) : base(options)
    {
    }

    public DbSet<PokerRangeDao> PokerRanges => Set<PokerRangeDao>();
    public DbSet<PokerRangeEntryDao> RangeEntries => Set<PokerRangeEntryDao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokerRangeEntryDao>()
            .HasKey(x => new { x.RangeId, x.HandKey });

        modelBuilder.Entity<PokerRangeDao>()
            .Property(x => x.SpotKey)
            .HasMaxLength(255);

        modelBuilder.Entity<PokerRangeDao>()
            .HasIndex(x => x.SpotKey)
            .IsUnique();

        modelBuilder.Entity<PokerRangeDao>()
            .HasMany(x => x.Entries)
            .WithOne()
            .HasForeignKey(x => x.RangeId);
    }
}