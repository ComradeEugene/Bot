using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.IO;

namespace OldBot
{
    class MainClass
    {
        private static string token = System.IO.File.ReadAllText(
            @"/Users/evgenijvolkov/Desktop/C#/token.txt");
        private static TelegramBotClient botClient;

        public static void Main(string[] args)
        {
            botClient = new TelegramBotClient(token);
            botClient.StartReceiving();
            botClient.OnMessage += OnMessageHendler;
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static  void OnMessageHendler(object sender,
            MessageEventArgs e)
        {
            var mes = e.Message;
            switch (mes.Type)
            {
                case MessageType.Text:
                    Console.WriteLine($"Пришло сообщение от: " +
                        $"{mes.Chat.FirstName} с текстом: {mes.Text}");
                    TextCommands(mes);
                    break;
                default:
                    break;
            }
            //if (mes.Text == "/start")
            //{
            //    botClient.SendTextMessageAsync(mes.Chat.Id,
            //        "Ты че попутал пес?", replyToMessageId: mes.MessageId);
            //}
        }

        static async void DownLoad(string fileID, string path)
        {
            var file = await botClient.GetFileAsync(fileID);
            FileStream fs = new FileStream("xXx" + path, FileMode.Create);
            await botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();
            fs.Dispose();
        }

        static async void TextCommands(Message message)
        {
            if (message.Text.ToLower() == "hi" ||
                message.Text.ToLower() == "hello")
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    $"hi {message.Chat.FirstName}");
            else if (message.Text == "/start")
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Go go go");
            else
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    message.Text, replyToMessageId: message.MessageId);
        }
    }
}
