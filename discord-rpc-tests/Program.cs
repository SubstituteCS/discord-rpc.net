using System;
using System.Threading.Tasks;
using discord_rpc;

namespace discord_rpc_tests
{
    class Program
    {
        static bool ready = false;
        static RichPresence RP = new RichPresence { Details = "Discord-RPC.net" };

        static void Main(string[] args)
        {
            Task.Run(Callbacks);
            Run().GetAwaiter().GetResult();
        }

        static async Task Run()
        {
            Console.WriteLine("Presence Started");
            string str = String.Empty;
            do
            {
                if (ready)
                {
                    Console.WriteLine("Enter Next State:");
                    str = Console.ReadLine();
                    RP.State = str;
                    await Discord.UpdatePresenceAsync(RP);
                }
                else
                {
                    Console.Write(".");
                    DiscordEventHandlers handlers = new DiscordEventHandlers { };
                    handlers.ready = Ready;
                    handlers.disconnected = Disconnected;
                    await Discord.InitializeAsync("418842770057461762", handlers, 1, "");
                }
                await Task.Delay(100);
            } while (str.ToLower() != "c");
            await Discord.ShutdownAsync();
        }

        static async Task Callbacks()
        {
            while (true)
            {
                Discord.RunCallbacks();
                await Task.Delay(100);
            }
        }

        static void Disconnected(int errorCode, string message)
        {
            ready = false;
            Console.WriteLine("...DISCONNECTED... {0}", message);
        }
        static void Ready()
        {
            Console.WriteLine("READY");
            ready = true;
            RP.Details = "Discord-RPC.net";
            RP.State = "👉😎👉 Zoop!";
            RP.startTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            Discord.UpdatePresence(RP);
        }
    }
}
