using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RamazonBot.Dal;
using RamazonBot.Bll.Service;
using RamazonBot.Service;

namespace RamazonBot;

internal class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        // DI konteynerga xizmatlarni qo'shish
        serviceCollection.AddDbContext<MainContext>();

        serviceCollection.AddScoped<ITelegramBotService, TelegramBotService>();
        serviceCollection.AddScoped<IRamazonTimeService, RamazonTimeService>();
        serviceCollection.AddScoped<RamazonBotModule>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Scope ochish
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MainContext>();

        // Migrations qo‘llash
        //dbContext.Database.Migrate();

        // Telegram botni ishga tushirish
        var ramazonBot = scope.ServiceProvider.GetRequiredService<RamazonBotModule>();
        await ramazonBot.StartBot();
    }
}
