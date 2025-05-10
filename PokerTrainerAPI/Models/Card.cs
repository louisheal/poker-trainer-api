namespace PokerTrainerAPI.Models;

public record Card(Suit Suit, Value Value)
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

public enum Value
{
    Ace,
    King,
    Queen,
    Jack,
    Ten,
    Nine,
    Eight,
    Seven,
    Six,
    Five,
    Four,
    Three,
    Two,
}

public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades,
}