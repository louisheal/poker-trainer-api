using Microsoft.EntityFrameworkCore;

namespace PokerTrainerApi.DrawRanges.Repository;

public interface IRangeRepository
{
    Task<PokerRange> GetRange(string spotKey);
    Task<PokerRange> UpdateRange(string spotKey, PokerRange range);
}

public class RangeRepository : IRangeRepository
{
    private readonly PokerDbContext _db;

    public RangeRepository(PokerDbContext db)
    {
        _db = db;
    }

    public async Task<PokerRange> GetRange(string spotKey)
    {
        var range = await _db.PokerRanges
            .Include(x => x.Entries)
            .SingleAsync(x => x.SpotKey == spotKey);

        return new PokerRange(
            range.Entries.ToDictionary(
                x => x.HandKey,
                x => x.Action
            )
        );
    }

    public async Task<PokerRange> UpdateRange(string spotKey, PokerRange range)
    {
        var dbRange = await _db.PokerRanges
            .Include(x => x.Entries)
            .SingleOrDefaultAsync(x => x.SpotKey == spotKey);

        if (dbRange == null)
        {
            dbRange = new PokerRangeDao
            {
                SpotKey = spotKey
            };

            _db.PokerRanges.Add(dbRange);
        }

        dbRange.Entries.Clear();

        foreach (var (handKey, action) in range)
        {
            dbRange.Entries.Add(new PokerRangeEntryDao
            {
                HandKey = handKey,
                Action = action
            });
        }

        await _db.SaveChangesAsync();
        return await GetRange(spotKey);
    }
}