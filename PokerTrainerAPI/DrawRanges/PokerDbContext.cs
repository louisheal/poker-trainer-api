using Microsoft.EntityFrameworkCore;

namespace PokerTrainerApi.DrawRanges;

public class PokerDbContext : DbContext
{
    public PokerDbContext(DbContextOptions<PokerDbContext> options) : base(options)
    {
    }

    public DbSet<PokerRange> PokerRanges => Set<PokerRange>();
    public DbSet<PokerRangeEntry> RangeEntries => Set<PokerRangeEntry>();
}