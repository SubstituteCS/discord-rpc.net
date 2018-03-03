using System;
using System.Threading.Tasks;
using discord_rpc;

namespace discord_rpc_tests
{
    class Program
    {
        static bool ready = false;
        static void Main(string[] args)
        {
            Task.Run(Run);
            Console.WriteLine("Presence Started");
            while (true)
            {
                Console.Read();
            }

        }
        static async Task Run()
        {
            DiscordEventHandlers handlers = new DiscordEventHandlers();
            handlers.ready += Ready;
            handlers.disconnected += DC;
            while (true)
            {
                Discord.RunCallbacks();
                if (ready)
                {
                    
                }
                else
                {
                    await Discord.InitializeAsync("419236472705515520", handlers, 1, "");
                }
                System.Threading.Thread.Sleep(15000);

            }
        }

        static void DC(int errorCode, string message)
        {
            Console.WriteLine("...DISCONNECTED... {0}", message);
        }
        static void Ready()
        {
            Console.WriteLine("READY");
            ready = true;
            Update();
        }


        static void Update()
        {
            Random rng = new Random();
            RichPresence BB = new RichPresence { };

            BB.Details = "IRL";
            BB.State = String.Format("Sleeping 💤💤💤");
            BB.largeImageKey = "sleep";
            BB.largeImageText = "😴";
            BB.startTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            BB.endTimestamp = DateTimeOffset.Now.AddHours(8).ToUnixTimeSeconds();
            Discord.UpdatePresence(BB);


        }



    }


}
