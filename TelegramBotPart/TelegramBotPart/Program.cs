using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using BadoonBotLogic_2._0_;
using BadoonBotLogic_2._0_.objects;
using BadoonBotLogic_2._0_.objects.Grid;
using TelegramBotPart.Menu;
using System.Linq.Expressions;

namespace TelegramBotPart
{
    class Program
    {
        static ITelegramBotClient botClient;
        static Point lb;
        static Point rt;
        static GridMap map;
        
        static void Main()
        {
            lb = new Point(49.754760, 23.860770);
            rt = new Point(lb.x + ((double)50 / 111), lb.y + ((double)50 / 111));
            Console.WriteLine($"{lb.ToString()},{rt.ToString()}");
            map = new GridMap(Resolution: 3,RightTop: rt,LeftBottom: lb);
            map.MatrixDispl();
            botClient = new TelegramBotClient("982918024:AAGb1agXjVHLdk1F_3j8Wuyxmv267nA6CUw");
            botClient.OnMessage += AnswerAsync;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        static async void AnswerAsync(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Кинь локу!");
                return;
            }
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Location)
            {
                await Task.Run(() => {
                    try
                    {
                        string text = Search.getUsersInRange(map, $"{e.Message.Chat.Id}", 5);
                        if (text == null)
                            map.Add(new Person(
                                x: e.Message.Location.Latitude,
                                y: e.Message.Location.Longitude,
                                id: e.Message.Chat.Id.ToString(),
                                name: e.Message.Chat.FirstName
                                )); ;
                        text = Search.getUsersInRange(map, $"{e.Message.Chat.Id}", 5);
                        botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: text);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    });
                
            
                
            }
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat, 
                    text: "Ку-ку?");
            }
        }

    }
}
