namespace PokerTrainerApi.DrawRanges;

public record RangeSpot(IReadOnlyList<ActionSpot> Sequence, PokerRange Range);

public record ActionSpot(PokerPosition Position, PokerAction Action);