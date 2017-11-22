using System;
using System.Messaging;
using MessageBusPatterns.MessageBus.Shared;

namespace MessageBusPatterns.MessageBus.Sender
{
    class Program
    {
        static void Main()
        {
            bool exitApp = false;

            // get the first menu selection
            int menuSelection = InitConsoleMenu();

            while (menuSelection != 99)
            {
                switch (menuSelection)
                {
                    case 1: // Send new order message
                        SendMessage(TopicType.NewOrder);
                        break;
                    case 2: // Send shipped message
                        SendMessage(TopicType.Shipped);
                        break;
                    case 3: // Send return message
                        SendMessage(TopicType.Return);
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

        static void SendMessage(TopicType topic)
        {
            // Create a transaction because we are using a transactional queue.
            using (var trn = new MessageQueueTransaction())
            {
                try
                {
                    // Create queue object
                    using (var queue = QueueHelper.GetQueueReference(@".\private$\mbp.message"))
                    {
                        queue.Formatter = new XmlMessageFormatter();

                        // push message onto queue (inside of a transaction)
                        trn.Begin();
                        queue.Send(String.Format("{0} message", topic), topic.ToString(), trn);
                        trn.Commit();

                        Console.WriteLine("===============================");
                        Console.WriteLine("{0} message queued", topic);
                        Console.WriteLine("===============================");
                        Console.WriteLine();
                    }
                }
                catch
                {
                    trn.Abort(); // rollback the transaction
                }
            }            
        }

        private static int InitConsoleMenu()
        {
            int result;

            Console.WriteLine("");
            Console.WriteLine("Select desired option:");
            Console.WriteLine(" 1: Send New Order message");
            Console.WriteLine(" 2: Send Shipped messages");
            Console.WriteLine(" 3: Send Return messages");
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
