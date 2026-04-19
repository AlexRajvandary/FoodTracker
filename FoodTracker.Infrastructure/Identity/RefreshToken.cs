namespace FoodTracker.Infrastructure.Identity;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public byte[] TokenHash { get; set; } = Array.Empty<byte>();
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string? CreatedByIp { get; set; }
    public DateTime? RevokedAtUtc { get; set; }
}