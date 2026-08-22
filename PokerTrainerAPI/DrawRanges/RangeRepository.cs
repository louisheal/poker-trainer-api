namespace PokerTrainerApi.DrawRanges;

public interface IRangeRepository
{
    Dictionary<string, PokerAction> GetRange(PokerPosition position);
}

public class RangeRepository : IRangeRepository
{
    private readonly Dictionary<PokerPosition, Dictionary<string, PokerAction>> _ranges = [];

    public RangeRepository(Dictionary<PokerPosition, string> files)
    {
        foreach (var (pos, path) in files)
        {
            LoadRange(pos, path);
        }
    }

    public Dictionary<string, PokerAction> GetRange(PokerPosition position) => _ranges[position];

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

        var range = new Dictionary<string, PokerAction>();
        foreach (var kvp in rawDict)
        {
            var action = Enum.Parse<PokerAction>(kvp.Value, ignoreCase: true);
            range[kvp.Key] = action;
        }

        _ranges[position] = range;
    }
}