namespace TelegramBot.ArtificialIntelligence.Service;
public interface IAiService
{
    Task<string> GetChatResponse(string userMessage);
}
