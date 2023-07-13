using Telegram.Bot;
using Telegram.Bot.Types;
using SkillFactoryMyBot.Services;
using Telegram.Bot.Types.Enums;

namespace SkillFactoryMyBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;
        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;
            _memoryStorage.GetSession(callbackQuery.From.Id).SelectMode = callbackQuery.Data;
            string userMode = callbackQuery.Data switch
            {
                "chars" => "chars. Подсчёт символов",
                "numbers" => "numbers. Сложение чисел",
                _ => String.Empty
            };
            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбран режим - {userMode}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
