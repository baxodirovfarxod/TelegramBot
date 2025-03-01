using Microsoft.EntityFrameworkCore;
using RamazonBot.Dal;
using RamazonBot.Dal.Entities;
using System;

namespace RamazonBot.Bll.Service;

public class TelegramBotService : ITelegramBotService
{
    private readonly MainContext mainContext;
    public TelegramBotService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task AddUserAsync(TelegramBot user)
    {
        var exist = await mainContext.TelegramBots.FirstOrDefaultAsync(x => x.UserId == user.UserId);

        if (exist is not null)
        {
            Console.WriteLine($"{user.UserId} lik foydalanuvchi bazada bor !");
            return;
        }

        try
        {
            mainContext.TelegramBots.Add(user);
            await mainContext.SaveChangesAsync();
            Console.WriteLine("Saqlandi!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<TelegramBot> GetUserByIdAsync(long userId)
    {
        var userFromDB = await mainContext.TelegramBots.FirstOrDefaultAsync(u => u.UserId == userId);

        if (userFromDB == null)
        {
            throw new Exception($"{userId} lik user topilmadi");
        }

        return userFromDB;
    }

    public async Task UpdateUserAsync(TelegramBot user)
    {
        var dbUser = await mainContext.TelegramBots.FirstOrDefaultAsync(u => u.TelegramBotId == user.TelegramBotId);
        if (dbUser == null) return;

        dbUser.FirstName = user.FirstName;
        dbUser.LastName = user.LastName;
        dbUser.UserName = user.UserName;
        dbUser.PhoneNumber = user.PhoneNumber;
        dbUser.IsBlocked = user.IsBlocked;

        await mainContext.SaveChangesAsync();
    }

    public async Task<ICollection<TelegramBot>> GetAllUsersAsync()
    {
        return mainContext.TelegramBots.ToList();
    }

}
