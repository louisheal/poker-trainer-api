using PokerTrainerAPI.Models;

namespace PokerTrainerAPI.Services;

public interface IPokerService
{
    public Hand GenerateRandomHand();
}