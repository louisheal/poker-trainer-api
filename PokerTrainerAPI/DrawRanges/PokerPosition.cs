using System.Text.Json.Serialization;

namespace PokerTrainerApi.DrawRanges;

public enum PokerPosition
{
    [JsonStringEnumMemberName("lj")]
    LJ,
    [JsonStringEnumMemberName("hj")]
    HJ,
    [JsonStringEnumMemberName("co")]
    CO,
    [JsonStringEnumMemberName("btn")]
    BTN,
    [JsonStringEnumMemberName("sb")]
    SB,
    [JsonStringEnumMemberName("bb")]
    BB
}