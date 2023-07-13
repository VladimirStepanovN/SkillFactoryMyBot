using Telegram.Bot;
using Telegram.Bot.Types;

namespace SkillFactoryMyBot.Controllers
{
    public class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: ct);
        }
    }
}
