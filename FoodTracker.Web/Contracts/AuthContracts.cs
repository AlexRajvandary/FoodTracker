using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Web.Contracts;

public sealed class RegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required, MinLength(1)]
    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }
}

public sealed class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public sealed class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}

public sealed class TelegramAuthRequest
{
    [Required]
    public string InitData { get; set; } = string.Empty;
}

public sealed class LogoutRequest
{
    /// <summary>Если передан — отзывается только этот refresh; иначе при авторизованном запросе отзываются все refresh пользователя.</summary>
    public string? RefreshToken { get; set; }
}

public sealed class AuthTokensResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public AuthUserDto User { get; set; } = null!;
}

public sealed class AuthUserDto
{
    public string Id { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Role { get; set; } = "user";
    public string? AvatarUrl { get; set; }
}
