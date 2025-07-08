using System.Text;
using PokerTrainerAPI.Enums;

namespace PokerTrainerAPI.Models;

public record Hand(Card FirstCard, Card SecondCard)
{
    public string ToNotation()
    {
        var builder = new StringBuilder();

        if (FirstCard.Value > SecondCard.Value)
        {
            builder.Append(FirstCard.ToNotation());
            builder.Append(SecondCard.ToNotation());
        }
        else
        {
            builder.Append(SecondCard.ToNotation());
            builder.Append(FirstCard.ToNotation());
        }

        if (FirstCard.Value == SecondCard.Value)
        {
            return builder.ToString();
        }

        builder.Append(FirstCard.Suit == SecondCard.Suit ? "s" : "o");
        return builder.ToString();
    }

    public static Hand FromNotation(string notation)
    {
        var first = CardValueExtensions.FromNotation(notation[0]);
        var second = CardValueExtensions.FromNotation(notation[1]);

        var random = new Random();
        var suits = Enum.GetValues(typeof(Suit));
        var suit = (Suit)suits.GetValue(random.Next(suits.Length))!;

        if (notation.Length >= 3 && notation[2] == 's')
        {
            return new Hand(new Card(suit, first), new Card(suit, second));
        }
        return new Hand(new Card(suit, first), new Card((Suit)(((int)suit + 1) % suits.Length), second));
    }
}