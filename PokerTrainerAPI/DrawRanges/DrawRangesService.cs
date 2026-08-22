namespace PokerTrainerApi.DrawRanges;

public interface IDrawRangesService
{
    RangeSpot GetRangeSpot();
}

public class DrawRangesService : IDrawRangesService
{
    private readonly IRangeRepository _ranges;
    private readonly Random _random;

    public DrawRangesService(IRangeRepository ranges)
    {
        _ranges = ranges;
        _random = new Random();
    }

    public RangeSpot GetRangeSpot()
    {
        var positions = Enum.GetValues<PokerPosition>();
        var position = positions[_random.Next(positions.Length)];

        var sequence = GenerateRfiSequence(position);
        var range = _ranges.GetRange(position);

        return new RangeSpot(sequence, range);
    }

    private static ActionSpot[] GenerateRfiSequence(PokerPosition position) =>
        Enum.GetValues<PokerPosition>()
            .Where(p => p < position)
            .Select(p => new ActionSpot(p, PokerAction.Fold))
            .ToArray();
}