using PokerTrainerApi.Enums;

namespace PokerTrainerApi.DTOs;

public record ActionRequest(Guid Id, HandAction Action);