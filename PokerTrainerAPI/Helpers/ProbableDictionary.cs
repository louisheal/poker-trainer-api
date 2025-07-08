namespace PokerTrainerAPI.Helpers;

public class ProbableDictionary
{
    private readonly Dictionary<string, double> _weights = new();
    private double _totalWeight;

    public void Add(string key, double weight)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(weight);
        
        _weights.Add(key, weight);
        _totalWeight += weight;
    }

    public void Update(string key, double weight)
    {
        if (!_weights.ContainsKey(key))
        {
            throw new KeyNotFoundException();
        }
        ArgumentOutOfRangeException.ThrowIfNegative(weight);

        _totalWeight += weight - _weights[key];
        _weights[key] = weight;
    }

    public string GetRandom()
    {
        var rng = new Random();
        var r = rng.NextDouble() * _totalWeight;
        foreach (var key in _weights.Keys)
        {
            r -= _weights[key];
            if (r <= 0)
            {
                return key;
            }
        }
        
        Console.WriteLine($"{r} - {_totalWeight}");

        return _weights.Last().Key;
    }
}