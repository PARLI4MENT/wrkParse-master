using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TlgSrv
{
    internal class StartedClassTlg
    {
        public static TelegramBotClient botClient { get; private set; }
        public static string Token { get; private set; } = "1279990267:AAFME7qmFYV74GbhA3gXTcKMnoEDuw5gx8g";

        StartedClassTlg() { }
        StartedClassTlg(string keyToken)
        {
            _ = Token;
            Start();
        }

        public async void Start()
        {
            botClient = new TelegramBotClient(Token);
            botClient.StartReceiving();
            botClient.OnMessage += OnMessageHandler;
        }
        public async void Start(string ketTocken)
        {
            botClient = new TelegramBotClient(ketTocken);
            botClient.StartReceiving();
            botClient.OnMessage += OnMessageHandler;
        }
        public void Stop()
        {
            botClient.StopReceiving();
        }

        private void OnMessageHandler(object sender, MessageEventArgs e)
        {
            if (e.Message != null &&
                e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text &&
                string.IsNullOrEmpty(e.Message.Text))
            {

            }
        }
    }
}
