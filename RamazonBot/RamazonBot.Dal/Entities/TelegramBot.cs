namespace RamazonBot.Dal.Entities;
public class TelegramBot
{
    public long TelegramBotId { get; set; }
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsBlocked { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }
}
