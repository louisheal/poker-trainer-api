using PokerTrainerAPI.DTOs;
using PokerTrainerAPI.Enums;
using PokerTrainerAPI.Models;

namespace PokerTrainerAPI.Services;

public interface IRangeService
{
    public bool IsCorrect(Hand hand, HandAction action);
    public IEnumerable<IEnumerable<TrackerCell>> GetBoard();
}