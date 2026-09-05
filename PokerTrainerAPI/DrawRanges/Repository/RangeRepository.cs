using PokerTrainerApi.DrawRanges;

namespace PokerTrainerAPI.DrawRanges.Repository;

public interface IRangeRepository
{
    PokerRange GetRange(string spotKey);
    void SetRange(string spotKey, PokerRange range);
}

public class RangeRepository : IRangeRepository
{
    private readonly PokerDbContext _db;

    public RangeRepository(Dictionary<PokerPosition, string> files, PokerDbContext db)
    {
        _db = db;
    }

    public PokerRange GetRange(string spotKey)
    {
        throw new NotImplementedException();
    }

    public void SetRange(string spotKey, PokerRange range)
    {
        throw new NotImplementedException();
    }
}