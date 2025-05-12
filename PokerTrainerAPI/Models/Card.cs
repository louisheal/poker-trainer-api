using PokerTrainerAPI.Enums;

namespace PokerTrainerAPI.Models;

public record Card(Suit Suit, CardValue Value)
{
    public string ToNotation()
    {
        return Value switch
        {
            CardValue.Two => "2",
            CardValue.Three => "3",
            CardValue.Four => "4",
            CardValue.Five => "5",
            CardValue.Six => "6",
            CardValue.Seven => "7",
            CardValue.Eight => "8",
            CardValue.Nine => "9",
            CardValue.Ten => "T",
            CardValue.Jack => "J",
            CardValue.Queen => "Q",
            CardValue.King => "K",
            CardValue.Ace => "A",
            _ => throw new ArgumentOutOfRangeException(nameof(Value), $"Unsupported card value: {Value}")
        };
    }
    
    public override string ToString()
    {
        return Value + " of " + Suit;
    }

    public virtual bool Equals(Card? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return Suit == other.Suit && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}