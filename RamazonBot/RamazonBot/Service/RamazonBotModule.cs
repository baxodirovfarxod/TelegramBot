using RamazonBot.Bll.Service;
using RamazonBot.Dal.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RamazonBot.Service;

public class RamazonBotModule
{
    private readonly ITelegramBotService _botService;
    private readonly IRamazonTimeService _timeService;
    private static string botToken = "7798401767:AAHiCjIxcRHUcRn3Ole8n-iJTcjv8Ay87uI";
    private readonly ITelegramBotClient _botClient = new TelegramBotClient(botToken);
    private readonly ReplyKeyboardMarkup _startMenu;

    public RamazonBotModule(ITelegramBotService botService, IRamazonTimeService timeService)
    {
        _botService = botService;
        _startMenu = StartMenu2();
        _timeService = timeService;
    }

    public async Task StartBot()
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync
        );

        Console.WriteLine($"Bot ishga tushdi");
        Console.ReadKey();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message || update.Message!.Type != MessageType.Text)
        {
            return; // Agar xabar text bo‘lmasa, hech narsa qilmaymiz
        }

        else if (update.Type == UpdateType.Message)
        {
            var user = update.Message.Chat;
            var message = update.Message;

            Console.WriteLine($"📩 Yangi xabar: {message.Text}");
            // for users
            if (message.Text == "/start")
            {
                var savingUser = new TelegramBot()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Username,
                    LastUpdatedAt = DateTime.UtcNow
                };

                await _botService.AddUserAsync(savingUser);
                await bot.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Ramazon kunini tanlang:",
                    replyMarkup: _startMenu
                );
            }

            else if (int.TryParse(message.Text.Replace(" kun", ""), out int day) && day >= 1 && day <= 30)
            {
                var text = await GetRamazonMessage(day);
                await bot.SendTextMessageAsync(user.Id, text, replyMarkup: _startMenu);
            }


            // For admin
            else if (message.Text == "/allUsers" && user.Id == 7636487012)
            {
                var users = await _botService.GetAllUsersAsync();
                foreach (var u in users)
                {
                    var messageToUser = $"BotId: {u.TelegramBotId}\n" +
                        $"TelegramId: {u.UserId}\n" +
                        $"FirstName: {u.FirstName}\n" +
                        $"LastName: {u.LastName}\n" +
                        $"UserName: {u.UserName}\n" +
                        $"PhoneNumber: {u.PhoneNumber}\n" +
                        $"CreatedAt: {u.CreatedAt}\n" +
                        $"LastUpdatedAt: {u.LastUpdatedAt}\n";

                    await bot.SendTextMessageAsync(user.Id, messageToUser);
                }
            }
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"❌ Xatolik: {exception.Message}");
        return Task.CompletedTask;
    }

    private InlineKeyboardMarkup StartMenu()
    {
        var buttons = new List<List<InlineKeyboardButton>>();
        var row = new List<InlineKeyboardButton>();

        for (int i = 1; i <= 30; i++)
        {
            row.Add(InlineKeyboardButton.WithCallbackData($"{i} kun", $"ramazon_{i}"));

            if (i % 5 == 0)
            {
                buttons.Add(new List<InlineKeyboardButton>(row));
                row.Clear();
            }
        }

        if (row.Count > 0)
            buttons.Add(row);

        return new InlineKeyboardMarkup(buttons);
    }

    private ReplyKeyboardMarkup StartMenu2()
    {
        var buttons = new List<List<KeyboardButton>>();
        var row = new List<KeyboardButton>();

        for (var i = 1; i <= 30; i++)
        {
            row.Add(new KeyboardButton($"{i} kun"));

            if (i % 5 == 0)
            {
                buttons.Add(new List<KeyboardButton>(row));
                row.Clear();
            }
        }
        if (row.Count > 0)
        {
            buttons.Add(row);
        }

        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true, // Tugmalar kichikroq ko‘rinishda chiqadi
            OneTimeKeyboard = false // Tugmalar doimiy ko‘rinishda qoladi
        };
    }

    private async Task<RamazonTimes> GetTimeByDay(int day)
    {
        return await _timeService.GetRamazonTimes(day);
    }

    private async Task<string> GetRamazonMessage(int day)
    {
        var ramazonTime = await GetTimeByDay(day);

        var message = $"🌙 *Ramazon {day}-kun* 🌙\n\n" +
                      $"🕰 *Suhur vaqti:* `{ramazonTime.Suhur}`\n" +
                      $"🤲 *Suhur duosi:*\n" +
                      $"`Navaytu an asuma sovmi shahri ramazona minal fajri ilal mag'ribi, xolisan lillahi ta'ala. Allohu akbar.`\n\n" +
                      $"🌅 *Iftar vaqti:* `{ramazonTime.Iftar}`\n" +
                      $"🤲 *Iftar duosi:*\n" +
                      $"`Allohumma laka sumtu va bika amantu va a'layka tavakkaltu va a'la rizqika aftortu, fag'firliy ya G'offaru ma qoddamtu va ma axxortu.`\n\n" +
                      $"🕌 Ramazon muborak! 🤍✨";

        return message;
    }

}
