namespace PokerTrainerApi.DrawRanges.Repository;

public class PokerRangeEntryDao
{
    public int RangeId { get; set; }
    public PokerHandKey HandKey { get; set; }
    public PokerAction Action { get; set; }
}