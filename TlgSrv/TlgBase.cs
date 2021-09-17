using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TlgSrv
{
    internal class TlgBase
    {
        public static string Key { private get; set; }
        private static TelegramBotClient botClient { set; get; }
        private TlgBase() { }
        public TlgBase(string key)
        {
            Key = key;
            botClient = new TelegramBotClient(Key);
        }

        public async void StartSrv()
        {
            try
            {

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Start prg
        /// </summary>
        public async void Start()
        {
            try
            {
                var me = await botClient.GetMeAsync();

                Console.WriteLine($"Hello, World {me.Id}, {me.Username}, {me.FirstName}, {me.LastName}");

                using var cts = new CancellationTokenSource();

                /// StartReceiving does not block the caller thread. Receving is done on the ThreadPool.
                botClient.StartReceiving(
                    new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                    cts.Token);

                Console.WriteLine($"Start listening for @{me.Username}");
                Console.ReadLine();

                /// Send cancellation request to stop bot
                Start();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        [Obsolete]
        public void Stop()
        {
            try
            {
                botClient.StopReceiving();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken)
        {
            var ErrorMessage = ex switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n[{apiRequestException.Message}]",
                _ => ex.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }
            if (update.Message.Type != MessageType.Text)
            {
                return;
            }

            var chatID = update.Message.Chat.Id;
            
            Console.WriteLine($"Recieved a '{update.Message.Text}' message in chat {chatID}.");

            await botClient.SendTextMessageAsync(
                chatId: chatID,
                text: "You said:\n" + update.Message.Text);
        }

        async Task HandleGetMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Trying *all the parameters* of 'sendMessage' method",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyToMessageId: update.Message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    "Check sendMessage method",
                    "https://core.telegram.org/bots/api#sendmessage")));
        }

        public async void GetSendingTextMessage(TelegramBotClient botClient, ChatId chatId, object ?sender)
        {
            var message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Hello World"
                );
        }
        public async void GetSendingStickerMessage(TelegramBotClient botClient, ChatId chatid, object ?sender)
        {
            await botClient.SendStickerAsync(
                chatId: chatid,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp");
        }

        public async void GetSendingVideoMessage(TelegramBotClient botClient, ChatId chatid, object ?sender)
        {
            await botClient.SendVideoAsync(
                chatId: chatid,
                video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4");
        }

        public async void GetSendingVideoNoteMessage(TelegramBotClient botClient, ChatId chatid, object ?sender)
        {
            await botClient.SendVideoNoteAsync(
                chatId: chatid,
                videoNote: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4");
        }

    }
}