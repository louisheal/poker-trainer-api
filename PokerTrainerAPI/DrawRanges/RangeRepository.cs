namespace PokerTrainerApi.DrawRanges;

public interface IRangeRepository
{
    Dictionary<PokerPosition, PokerAction> GetRange(PokerPosition position);
}

public class RangeRepository : IRangeRepository
{
    private readonly Dictionary<PokerPosition, string> Files = new()
    {
        { PokerPosition.LJ, "lowjack.json" },
        { PokerPosition.HJ, "hijack.json" },
        { PokerPosition.CO, "cutoff.json" },
        { PokerPosition.BTN, "button.json" },
        { PokerPosition.SB, "smallblind.json" },
    };

    private readonly Dictionary<PokerPosition, Dictionary<PokerPosition, PokerAction>> _ranges = null!;

    public RangeRepository()
    {
        foreach (var (pos, path) in Files)
        {
            LoadRange(pos, path);
        }
    }

    public Dictionary<PokerPosition, PokerAction> GetRange(PokerPosition position)
    {
        return _ranges[position];
    }

    private void LoadRange(PokerPosition position, string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(path);
        }

        var json = File.ReadAllText(path);
        var rawDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        if (rawDict == null)
        {
            throw new InvalidOperationException("Failed to deserialize the range file.");
        }

        var range = new Dictionary<PokerPosition, PokerAction>();
        foreach (var kvp in rawDict)
        {
            var pos = Enum.Parse<PokerPosition>(kvp.Key, ignoreCase: true);
            var action = Enum.Parse<PokerAction>(kvp.Value, ignoreCase: true);
            range[pos] = action;
        }

        _ranges[position] = range;
    }
}