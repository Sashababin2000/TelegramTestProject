using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot
{
    class Program
    {
        private static string Token { get; set; } = "6178728774:AAHsZblR0F5szqVNpQGtlvzFcO-sFOnJrOY";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {


            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();


        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {

            var msg = e.Message;

            List<string> answer = new List<string>() { "Да ", "Нет", "Сесть на бутылку" };
            Random rnd = new Random();
            int randIndex = rnd.Next(answer.Count);



            if (msg.Text != null)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {msg.Text}");
                switch (msg.Text)
                {
                    case "Стикер":
                        await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://cdn.tlgrm.app/stickers/dc7/a36/dc7a3659-1457-4506-9294-0d28f529bb0a/192/1.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Картинка":
                        await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: "https://png.pngtree.com/thumb_back/fw800/background/20210403/pngtree-office-desktop-background-image_602173.jpg",
                            replyMarkup: GetButtons());
                        break;


                    case "Уходить с работы?":
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            answer[randIndex],
                            replyMarkup: GetButtons());
                        break;

                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Выберите команду: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стикер"}, new KeyboardButton { Text = "Картинка"} },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Уходить с работы?"}, new KeyboardButton { Text = "456"} }
                }
            };
        }

    }

    public enum DayName
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public class Day
    {
        public Day(DayName name, ushort number)
        {
            Name = name; Number = number;
        }
        public DayName Name { get; set; }
        public ushort Number { get; set; }
    }
    public enum MonthName
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public class Month
    {
        public Month(MonthName monthName, uint year)
        {
            Name = monthName;
            Year = year;
            var leapyear = Year % 4 == 0;
            var days = Name == MonthName.February ? (leapyear ? 29 : 28) : (Name == MonthName.April || Name == MonthName.June || Name == MonthName.September || Name == MonthName.November ? 30 : 31);
            Days = new Day[days];
            var firstday = year * 365 + (leapyear ? -1 : 0) + (((year - (year % 4)) / 4)) - (((year - (year % 400)) / 400)) + 3;
            var month = (int)monthName;
            firstday += month < 1 ? 0 : 31;
            firstday += month < 2 ? 0 : (leapyear ? 29 : 28);
            firstday += month < 3 ? 0 : 31;
            firstday += month < 4 ? 0 : 30;
            firstday += month < 5 ? 0 : 31;
            firstday += month < 6 ? 0 : 30;
            firstday += month < 7 ? 0 : 31;
            firstday += month < 8 ? 0 : 31;
            firstday += month < 9 ? 0 : 30;
            firstday += month < 10 ? 0 : 31;
            firstday += month < 11 ? 0 : 30;
            firstday = firstday % 7;
            for (int i = 0; i < Days.Length; i++)
                Days[i] = new Day((DayName)((i + firstday) % 7), (ushort)(i + 1));
        }
        public uint Year { get; set; }
        public MonthName Name { get; set; }
        public Day[] Days { get; set; }
        public ushort Weeks
        {
            get
            {
                var days = (int)Days[0].Name + Days.Length - 1;
                return (ushort)(((days - (days % 7)) / 7) + (days % 7 > 0 ? 1 : 0));
            }
        }
    }



}





