using System;

namespace MessageBusPatterns.MessageBus.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageBus messageBus = new MessageBus();
            Console.WriteLine("Message bus listening on queue");

            Console.ReadLine();

            Console.WriteLine("Closing message bus...");
            messageBus.Close();

            Console.WriteLine("Message bus closed. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
