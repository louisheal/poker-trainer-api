namespace PokerTrainerApi.DrawRanges.Repository;

public class PokerRangeDao
{
    public int Id { get; set; }
    public string SpotKey { get; set; } = "";

    public ICollection<PokerRangeEntryDao> Entries { get; set; } = [];
}