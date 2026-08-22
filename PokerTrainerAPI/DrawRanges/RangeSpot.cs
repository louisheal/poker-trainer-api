namespace PokerTrainerApi.DrawRanges;

public record RangeSpot(IReadOnlyList<ActionSpot> Sequence, Dictionary<PokerPosition, PokerAction> Range);

public record ActionSpot(PokerPosition Position, PokerAction Action);