using RamazonBot.Dal.Entities;

namespace RamazonBot.Bll.Service;

public interface ITelegramBotService
{
    Task AddUserAsync(TelegramBot user);
    Task<TelegramBot> GetUserByIdAsync(long userId);
    Task UpdateUserAsync(TelegramBot user);
    Task<ICollection<TelegramBot>> GetAllUsersAsync();
}