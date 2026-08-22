namespace PokerTrainerApi.DrawRanges;

public record RangeSpot(IReadOnlyList<ActionSpot> Sequence, Dictionary<string, PokerAction> Range);

public record ActionSpot(PokerPosition Position, PokerAction Action);