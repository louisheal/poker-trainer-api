using System.Text.Json.Serialization;

namespace PokerTrainerApi.DrawRanges.Models;

public enum PokerAction
{
    [JsonStringEnumMemberName("fold")]
    Fold,
    [JsonStringEnumMemberName("raise")]
    Raise
}