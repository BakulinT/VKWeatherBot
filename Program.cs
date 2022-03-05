using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using Newtonsoft.Json;

namespace VkChatBot
{
    class Program
    {
        private static string AcessToken = "";
        private static string GroupUrl = "https://vk.com/club123456780";
        private static WeatherBot weather;
        private static VkBot bot;
        private static OpenWheater.OpenWheater dw;
        static void Main(string[] args)
        {
            bot = new VkBot(AcessToken, GroupUrl);

            bot.OnMessageReceived += MessageReceivedChat;

            Console.WriteLine("Бот запущен...");

            bot.Start();
        }
        static void MessageReceivedChat(object sender, MessageReceivedEventArgs args)
        {
            Console.WriteLine($"Message Id {args.Message.PeerId}: {args.Message.Text}");

            string[] message = args.Message.Text.Split(" ").Select(x => x.ToLower()).ToArray();

            if (message is null) return;

            if ( message[0] == "погода" )
            {
                try
                {
                    weather = new WeatherBot();
                    weather.Load(message[1]);

                    dw = JsonConvert.DeserializeObject<OpenWheater.OpenWheater>(weather.dw);

                    PrintMessage(args, DateTime.Now);
                }
                catch (System.Net.WebException)
                {
                    bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                    {
                        Message = $"❗Мне ну удалось найти город \"{message[1]}\".",
                        PeerId = args.Message.PeerId,
                        RandomId = Environment.TickCount
                    });
                }
            }
            else if (message[0] == "помощь")
            {
                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    Message = $"Прогноз погоды можно узнать, если ввести фразу «Прогноз ВашГород». Чтобы узнать информацию о боте, введите «помощь».",
                    PeerId = args.Message.PeerId,
                    RandomId = Environment.TickCount
                });
            }
        }
        private static void PrintMessage(MessageReceivedEventArgs args, DateTime dataTime)
        {
            bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                Message = $"Прогноз погоды для города {dw.name} [{dataTime.ToString("MM/dd/yyyy HH:mm")}]🌤\n" +
                    $"🌡Температура: {Math.Round(dw.main.temp, 2)}\u00B0C\n" +
                    $"📥Атмосферное давление наж уровнем моря: {dw.main.sea_level} гПа\n" +
                    $"💨Скорость ветра: {dw.wind.speed} м/с\n" +
                    $"💧Влажность: {dw.main.humidity}%",
                PeerId = args.Message.PeerId,
                RandomId = Environment.TickCount
            });
        }
    }
}
