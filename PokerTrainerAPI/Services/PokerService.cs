using PokerTrainerAPI.Models;
using PokerTrainerAPI.Services;

namespace PokerTrainerAPI.Helpers;

public class PokerService : IPokerService
{
    private readonly Random _random;
    
    public PokerService(Random random)
    {
        _random = random;
    }
    
    public Hand GenerateRandomHand()
    {
        var first = GenerateRandomCard();
        var second = GenerateRandomCard();

        while (first == second)
        {
            second = GenerateRandomCard();
        }

        return new Hand(first, second);
    }

    private Card GenerateRandomCard()
    {
        var suits = Enum.GetValues(typeof(Suit));
        var values = Enum.GetValues(typeof(Value));

        var suit = (Suit)suits.GetValue(_random.Next(suits.Length))!;
        var value = (Value)values.GetValue(_random.Next(values.Length))!;

        return new Card(suit, value);
    }
}