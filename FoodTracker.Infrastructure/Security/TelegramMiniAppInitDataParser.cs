using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FoodTracker.Infrastructure.Security;

public static class TelegramMiniAppInitDataParser
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Проверка подписи <see href="https://core.telegram.org/bots/webapps#validating-data-received-via-the-mini-app">initData</see> (схема HMAC-SHA256).
    /// </summary>
    public static bool TryValidate(
        string initData,
        string botToken,
        TimeSpan maxAge,
        [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out IReadOnlyDictionary<string, string>? fields)
    {
        fields = null;
        if (string.IsNullOrWhiteSpace(initData) || string.IsNullOrWhiteSpace(botToken))
        {
            return false;
        }

        var pairs = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var segment in initData.Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var eq = segment.IndexOf('=');
            if (eq <= 0)
            {
                continue;
            }

            var key = Uri.UnescapeDataString(segment[..eq]);
            var value = Uri.UnescapeDataString(segment[(eq + 1)..]);
            pairs[key] = value;
        }

        if (!pairs.TryGetValue("hash", out var hashHex))
        {
            return false;
        }

        pairs.Remove("hash");
        pairs.Remove("signature");

        if (!pairs.TryGetValue("auth_date", out var authDateRaw)
            || !long.TryParse(authDateRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var authUnix))
        {
            return false;
        }

        var authTime = DateTimeOffset.FromUnixTimeSeconds(authUnix);
        if (DateTimeOffset.UtcNow - authTime > maxAge)
        {
            return false;
        }

        var dataCheckString = string.Join(
            '\n',
            pairs.OrderBy(p => p.Key, StringComparer.Ordinal).Select(p => $"{p.Key}={p.Value}"));

        using var hmacSecret = new HMACSHA256(Encoding.UTF8.GetBytes(botToken));
        var secretKey = hmacSecret.ComputeHash(Encoding.UTF8.GetBytes("WebAppData"));

        using var hmac = new HMACSHA256(secretKey);
        var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
        var computedHex = Convert.ToHexString(computed).ToLowerInvariant();
        if (!string.Equals(computedHex, hashHex, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        fields = pairs;
        return true;
    }

    public static bool TryGetTelegramUserId(IReadOnlyDictionary<string, string> fields, out long telegramUserId)
    {
        telegramUserId = 0;
        if (!fields.TryGetValue("user", out var userJson))
        {
            return false;
        }

        try
        {
            using var doc = JsonDocument.Parse(userJson);
            if (!doc.RootElement.TryGetProperty("id", out var idEl) || idEl.ValueKind != JsonValueKind.Number)
            {
                return false;
            }

            telegramUserId = idEl.GetInt64();
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    public static void TryFillNamesFromUserJson(IReadOnlyDictionary<string, string> fields, out string? firstName, out string? lastName)
    {
        firstName = null;
        lastName = null;
        if (!fields.TryGetValue("user", out var userJson))
        {
            return;
        }

        try
        {
            var u = JsonSerializer.Deserialize<TelegramUserJson>(userJson, JsonOptions);
            firstName = u?.FirstName;
            lastName = u?.LastName;
        }
        catch (JsonException)
        {
            // ignore
        }
    }
}
