using Microsoft.EntityFrameworkCore;
using RectatSystem.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using RectatSystem.Models;

namespace RectatSystem.Services;

public class TelegramBotService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramBotService()
    {
        _botClient = new TelegramBotClient("Telegram bot token");
    }

    public async Task SetWebhookAsync(string webhookUrl)
    {
        await _botClient.SetWebhookAsync(webhookUrl);
    }

    public async Task SendMessageAsync(long chatId, string text)
    {
        await _botClient.SendTextMessageAsync(chatId, text);
    }

    public async Task HandleUpdateAsync(Update update, TelegramBotContext dbContext)
    {
        if (update.Message != null)
        {
            var message = update.Message;
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.ChatId == message.Chat.Id);

            if (user == null)
            {
                user = new Models.User
                {
                    ChatId = message.Chat.Id,
                    Username = message.From.Username,
                    FirstName = message.From.FirstName,
                    LastName = message.From.LastName,
                    LastInteraction = DateTimeOffset.UtcNow
                };
                dbContext.Users.Add(user);
            }
            else
            {
                user.LastInteraction = DateTimeOffset.UtcNow;
            }

            await dbContext.SaveChangesAsync();
            await SendMessageAsync(message.Chat.Id, "Thank you for starting a conversation with our bot!");
        }
    }
}
