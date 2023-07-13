using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using SkillFactoryMyBot.Services;

namespace SkillFactoryMyBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчёт символов" , $"chars"),
                        InlineKeyboardButton.WithCallbackData($"Сложение чисел" , $"numbers")
                    });
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b> Наш бот умеет считать количество символов в строке и складывать числа.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Для работы с ботом нужно выбрать действие.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    switch (_memoryStorage.GetSession(message.Chat.Id).SelectMode)
                    {
                        case "chars":
                            await _telegramBotClient.SendTextMessageAsync(message.From.Id, $"в вашем сообщении {message.Text.Length} символов", cancellationToken: ct);
                            break;
                        case "numbers":
                            string[] input = message.Text.Split(' ');
                            int result = default;
                            try
                            {
                                foreach (string inputItem in input)
                                {
                                    result += Convert.ToInt32(inputItem);
                                }
                                await _telegramBotClient.SendTextMessageAsync(message.From.Id, $"сумма чисел: {result}", cancellationToken: ct);
                            }
                            catch (FormatException)
                            {
                                await _telegramBotClient.SendTextMessageAsync(message.From.Id, $"введите целые числа через пробел", cancellationToken: ct);
                            }
                            catch (Exception)
                            {
                                await _telegramBotClient.SendTextMessageAsync(message.From.Id, $"непредвиденная ошибка", cancellationToken: ct);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
