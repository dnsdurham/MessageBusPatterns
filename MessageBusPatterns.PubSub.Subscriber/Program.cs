using System;

namespace MessageBusPatterns.PubSub.Subscriber
{
    class Program
    {
        static void Main()
        {
            var subscriber = new Subscriber();

            Console.ReadLine();

            Console.WriteLine("Unsubscribing...");

            subscriber.UnSubscribe();

            Console.WriteLine("Unsubscribed. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
