using PokerTrainerApi.DrawRanges.Models;

namespace PokerTrainerApi.DrawRanges;

public class PokerRange : Dictionary<PokerHandKey, PokerAction>
{
    public PokerRange()
    {
    }

    public PokerRange(IDictionary<PokerHandKey, PokerAction> dictionary) : base(dictionary)
    {
    }
}