namespace FoodTracker.Domain.Auth;

/// <summary>Связь пользователя с внешним идентификатором провайдера (уникальна по паре Kind + Key).</summary>
public class UserAuthProvider
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AuthProviderKind ProviderKind { get; set; }
    public string ProviderKey { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
}
