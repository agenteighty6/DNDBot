using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DNDBot
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            string TokenPath = @"d:\DNDBot\token.txt";  //File containing bot token

            string token = File.ReadAllText(TokenPath);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            Random roll = new Random();

            if (message.Content == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
            else if (message.Content == "!pong")
            {
                await message.Channel.SendMessageAsync("Ping!");
            }
            else if (message.Content == "!roll")
            {
                int result = roll.Next(1, 21);  //rolls a D20
                await message.Channel.SendMessageAsync("You rolled a " + result + "!");
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}