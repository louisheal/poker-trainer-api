namespace PokerTrainerApi.Admin;

public static class AdminAuthDefaults
{
    public const string CookieName = "poker_trainer_admin";
    public const string Issuer = "poker-trainer-api";
    public const string Audience = "poker-trainer-admin";
    public const string Role = "Admin";
    public const string LoginRateLimitPolicy = "admin-login";
}