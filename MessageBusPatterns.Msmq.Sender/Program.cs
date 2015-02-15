using System;
using System.Messaging;
using System.Threading;

namespace MessageBusPatterns.Msmq.Sender
{
    class Program
    {
        private static int _messageNum;

        static void Main(string[] args)
        {
            bool exitApp = false;

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

        static void SendOneMessage()
        {
            Thread.Sleep(1000); // Pause one seconnds between messages

            // Create a transaction because we are using a transactional queue.
            using (var trn = new MessageQueueTransaction())
            {
                try
                {
                    // Create queue object
                    using (var queue = new MessageQueue(@".\private$\testqueue"))
                    {
                        queue.Formatter = new XmlMessageFormatter();

                        // push message onto queue (inside of a transaction)
                        trn.Begin();
                        queue.Send("[Message content here]", String.Format("Message {0}",_messageNum++), trn);
                        trn.Commit();

                        Console.WriteLine("Message {0} queued", _messageNum);
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
