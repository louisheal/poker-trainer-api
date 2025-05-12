using System.Text;

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
}