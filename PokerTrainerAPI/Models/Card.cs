using PokerTrainerAPI.Enums;

namespace PokerTrainerAPI.Models;

public record Card(Suit Suit, CardValue Value)
{
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