using PokerTrainerAPI.Enums;

namespace PokerTrainerAPI.DTOs;

public record ActionRequest(Guid Id, HandAction Action);