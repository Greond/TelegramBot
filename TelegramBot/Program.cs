using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class Program
    {
        private static string BotToekn { get; set; } = "7291651770:AAEv8OYCghgNMyhY34VPcGQ-RJLPPe9pr30";
        // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
        private static ITelegramBotClient _botClient;

        // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
        private static ReceiverOptions _receiverOptions;
        static async Task Main(string[] args)
        {
            _botClient = new TelegramBotClient(BotToekn);
            _receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                },
                DropPendingUpdates = true,
            };
            var cts = new CancellationTokenSource();
            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"{me.FirstName} been started");
            await Task.Delay(-1);
        }

        [Obsolete]
        private static async Task UpdateHandler(ITelegramBotClient botClient,Update update,CancellationToken cancellationToken)
        {
            try
            {
               

                switch (update.Type)
                {
                        case UpdateType.Message:
                        {
                            // эта переменная будет содержать в себе все связанное с сообщениями
                            var message = update.Message;
                            // From - это от кого пришло сообщение (или любой другой Update)
                            var user = message.From;

                            // Выводим на экран то, что пишут нашему боту, а также небольшую информацию об отправителе
                            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                            // Chat - содержит всю информацию о чате
                            var chat = message.Chat;

                            // Добавляем проверку на тип Message
                            switch (message.Type)
                            {
                                    //текстовый тип
                                    case MessageType.Text:
                                    {
                                        // тут обрабатываем команду /start, остальные аналогично
                                        if (message.Text == "поехали братуха, 82!!" || message.Text == "/start" || message.Text == "start" 
                                            || message.Text == "/начать" || message.Text == "начать" )
                                        {
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Выбери путь:\n" +
                                                "Схомякнуть по сылочке\n" +
                                                "чи\n"+
                                                "Пабазарить\n");
                                            var replyKeyboard = new ReplyKeyboardMarkup(
                                                new List<KeyboardButton[]>()
                                                {
                                                    new KeyboardButton[]
                                                    {
                                                        new KeyboardButton("Схомякнуть по сылочке"),
                                                        new KeyboardButton("Пабазарить")
                                                    },
                                                })
                                            { ResizeKeyboard = true };
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "?",
                                                replyMarkup: replyKeyboard
                                                );
                                            return;
                                        }
                                        if (message.Text == "Схомякнуть по сылочке")
                                        {
                                            // Тут создаем нашу клавиатуру
                                            var inlineKeyboard = new InlineKeyboardMarkup(
                                                new List<InlineKeyboardButton[]>() // здесь создаем лист (массив), который содрежит в себе массив из класса кнопок
                                                {
                                                    // Каждый новый массив - это дополнительные строки,
                                                    // а каждая дополнительная строка (кнопка) в массиве - это добавление ряда

                                                    new InlineKeyboardButton[] // тут создаем массив кнопок
                                                    {
                                                        InlineKeyboardButton.WithUrl("Ds server", "https://discord.gg/G24uQeCb"),
                                                        InlineKeyboardButton.WithUrl("Скачать крутой калькулятор", "https://drive.google.com/file/d/1UXXLF8KmOKR8bZyBISheJOTUBMdlW9IV/view?usp=drive_link"),
                                                    },
                                                    new InlineKeyboardButton[]
                                                    {
                                                        InlineKeyboardButton.WithCallbackData("А это по приколу тут", "inlineKeyboard.emptyButton"),
                                                    },
                                                });

                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Это спонсраловская клавиатура!",
                                                replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                        }
                                        if (message.Text == "Пабазарить")
                                        {
                                            var replyKeyboard = new ReplyKeyboardMarkup(
                                                new List<KeyboardButton[]>()
                                                {
                                                    new KeyboardButton[]
                                                    {
                                                        new KeyboardButton("Пуриветь!"),
                                                        new KeyboardButton("Bye Bye")
                                                    },
                                                    new KeyboardButton[]
                                                    {
                                                        new KeyboardButton("Выйдём 1 на 1, хам")
                                                    },
                                                    new KeyboardButton[]
                                                    {
                                                        new KeyboardButton("Эй книга братан идика сюдаа")
                                                    }
                                                })
                                            { ResizeKeyboard = true };
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "базарная клавиатура",
                                                replyMarkup: replyKeyboard
                                                );
                                            return;
                                        }
                                        if (message.Text == "Пуриветь!")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                  chat.Id,
                                                  "Привет земляк, с тобой Homyak"
                                                  );
                                            return;
                                        }
                                        if ( message.Text == "Bye Bye")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                  chat.Id,
                                                  "Давай все, обнял, приподнял, в лобик поцеловал. Pyk"
                                                  );
                                           
                                        }
                                        if (message.Text == "Выйдём 1 на 1, хам")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                  chat.Id,
                                                  "сорян, брат, пока нимагу"
                                                  );
                                            return;
                                        }
                                        if (message.Text == "Эй книга братан идика сюдаа")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                  chat.Id,
                                                  "Да ди нахукй бляяя",
                                                  replyParameters:message.MessageId);
                                            return;
                                        }
                                            //меня для старта
                                            var startReplyKeyboard = new ReplyKeyboardMarkup(
                                            new List<KeyboardButton[]>()
                                            {
                                            new KeyboardButton[]
                                            {
                                                new KeyboardButton("поехали братуха, 82!!"),
                                            },
                                            })
                                            { ResizeKeyboard = true };
                                            await botClient.SendTextMessageAsync(
                                                update.Message.Chat.Id,
                                                "ПЕРВЫЙ брат",
                                                replyMarkup: startReplyKeyboard
                                                );
                                            return;
                                    }
                                    default:
                                    {
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Давай пока тока текстом");
                                        return;
                                    }
                            }
                            return;
                        }
                        case UpdateType.CallbackQuery:
                        {
                            // Переменная, которая будет содержать в себе всю информацию о кнопке, которую нажали
                            var callbackQuery = update.CallbackQuery;

                            // Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.
                            var user = callbackQuery.From;
                            // Выводим на экран нажатие кнопки
                            Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                            // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                            // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                            var chat = callbackQuery.Message.Chat;

                            switch(callbackQuery.Data)
                            {
                                case "inlineKeyboard.emptyButton":
                                    {
                                        // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id,"Ахометь она работает",showAlert:true);
                                        // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            $"Хопа ");
                                        return;
                                    }
                            }
                            return;
                        }

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erorr: {ex.ToString()}");
            }
        }
        private static Task ErrorHandler(ITelegramBotClient botClient,Exception error,CancellationToken cancellationToken)
        {
            string ErrorMessage;

            if (error is ApiRequestException apiRequestException)
            {
                ErrorMessage = $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}";
            }
            else
            {
                ErrorMessage = error.ToString();
            }
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
