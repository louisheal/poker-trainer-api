namespace PokerTrainerAPI.Models;

public record Card(Suit Suit, Value Value)
{
    public override string ToString()
    {
        return Value.ToString() + " of " + Suit.ToString();
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