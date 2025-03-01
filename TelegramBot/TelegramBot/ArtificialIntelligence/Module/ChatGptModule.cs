using TelegramBot.ArtificialIntelligence.Service;

namespace TelegramBot.ArtificialIntelligence.Module;

public class ChatGptModule
{
    private readonly IAiService aiService;
    public ChatGptModule(IAiService aiService)
    {
        this.aiService = aiService;
    }

    public async Task<string> GetAnswer(string message)
    {
        return await aiService.GetChatResponse(message);
    }
}
