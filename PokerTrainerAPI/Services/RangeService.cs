using System.ComponentModel;
using PokerTrainerAPI.DTOs;
using PokerTrainerAPI.Enums;
using PokerTrainerAPI.Helpers;
using PokerTrainerAPI.Models;

namespace PokerTrainerAPI.Services;

public class RangeService
{
    private readonly Dictionary<string, HandAction> _range;
    private readonly Dictionary<string, Tuple<int,int>> _tracker;
    private readonly List<string> _keys;

    private readonly string _label;

    private readonly ProbableDictionary _dist;
    
    public RangeService(string filePath, string label)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(filePath);
        }
        
        var json = File.ReadAllText(filePath);
        var rawDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        
        if (rawDict == null)
        {
            throw new InvalidOperationException("Failed to deserialize the range file.");
        }

        _keys = new List<string>();
        _range = new Dictionary<string, HandAction>();
        _tracker = new Dictionary<string, Tuple<int, int>>();
        _dist = new ProbableDictionary();
        _label = label;
        
        foreach (var kvp in rawDict)
        {
            _keys.Add(kvp.Key);
            _range[kvp.Key] = Enum.Parse<HandAction>(kvp.Value, ignoreCase: true);
            _tracker[kvp.Key] = new Tuple<int, int>(0, 0);
            _dist.Add(kvp.Key, 1);
        }
    }

    public Hand GenerateRandomHand()
    {
        var notation = _dist.GetRandom();
        return Hand.FromNotation(notation);
    }

    public bool IsCorrect(Hand hand, HandAction action)
    {
        var key = hand.ToNotation();
        if (!_range.TryGetValue(hand.ToNotation(), out var expected))
        {
            throw new InvalidEnumArgumentException($"Range does not contain the hand {hand}");
        }

        var newCorrect = _tracker[hand.ToNotation()].Item1 + (action == expected ? 1 : 0);
        var newTotal = _tracker[hand.ToNotation()].Item2 + 1;

        _tracker[hand.ToNotation()] = new Tuple<int, int>(newCorrect, newTotal);
        
        var weight = (newTotal - newCorrect + 0.1) / newTotal;
        _dist.Update(key,  weight);
        
        return action == expected;
    }

    public IEnumerable<IEnumerable<TrackerCell>> GetBoard()
    {
        var count = 1;
        var result = new List<List<TrackerCell>>();
        var current = new List<TrackerCell>();
        foreach (var key in _keys)
        {
            current.Add(new TrackerCell(key, _tracker[key].Item1, _tracker[key].Item2));
            if (count % 13 == 0)
            {
                result.Add(current);
                current = new List<TrackerCell>();
            }
            count++;
        }

        return result;
    }

    public override string ToString()
    {
        return _label;
    }
}