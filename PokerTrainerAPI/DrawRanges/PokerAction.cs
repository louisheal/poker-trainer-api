using System.Text.Json.Serialization;

namespace PokerTrainerApi.DrawRanges;

public enum PokerAction
{
    [JsonStringEnumMemberName("fold")]
    Fold,
    [JsonStringEnumMemberName("raise")]
    Raise
}