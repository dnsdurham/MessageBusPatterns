using System;
using System.Threading;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Publisher
{
    class Program
    {
        private static int _messageNum;
        static PublishingClient _publishingClient;

        static void Main()
        {
            bool exitApp = false;

            _publishingClient = new PublishingClient();

            // get the first menu selection
            int menuSelection = InitConsoleMenu();

            while (menuSelection != 99)
            {
                switch (menuSelection)
                {
                    case 1: // Send single message
                        SendOneMessage();
                        break;
                    case 2: // Send multiple messages
                        SendMultipleMessages();
                        break;
                    case 99:
                        exitApp = true;
                        break;
                }

                // check to see if we want to exit the app
                if (exitApp)
                {
                    break; // exit the while loop
                }

                // re-initialize the menu selection
                menuSelection = InitConsoleMenu();
            }

        }

        private static void SendMultipleMessages()
        {
            for (int i = 0; i < 5; i++)
            {
                SendOneMessage();
            }
            
        }

        private static void SendOneMessage()
        {
            PubSubMessage message = new PubSubMessage()
            {
                MessageNum = _messageNum++,
                Content = "[Message content here]"
            };

            Thread.Sleep(1000); // Pause one seconnds between messages
            _publishingClient.Publish(message);
        }

        private static int InitConsoleMenu()
        {
            int result;

            Console.WriteLine("");
            Console.WriteLine("Select desired option:");
            Console.WriteLine(" 1: Send 1 message");
            Console.WriteLine(" 2: Send multiple messages");
            Console.WriteLine("99: exit");
            string selection = Console.ReadLine();
            if (int.TryParse(selection, out result) == false)
            {
                result = 0;
            }

            return result;
        }
    }

}
