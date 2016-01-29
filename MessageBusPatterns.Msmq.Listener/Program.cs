using System;
using System.Messaging;

namespace MessageBusPatterns.Msmq.Listener
{
    class Program
    {
        static void Main()
        {
            // Create an instance of MessageQueue. Set its formatter.
            MessageQueue mq = GetQueueReference();
            mq.Formatter = new XmlMessageFormatter(new[] { typeof(String) });

            // Add an event handler for the PeekCompleted event.
            mq.PeekCompleted += OnPeekCompleted;

            // Begin the asynchronous peek operation.
            mq.BeginPeek();

            Console.WriteLine("Listening on queue");

            Console.ReadLine();

            Console.WriteLine("Closing listener...");

            // Remove the event handler before closing the queue
            mq.PeekCompleted -= OnPeekCompleted;
            mq.Close();
            mq.Dispose();

            Console.WriteLine("Listener closed. Press any key to exit.");
            Console.ReadLine();
        }
        
        private static MessageQueue GetQueueReference()
        {
            const string queueName = @".\private$\testqueue";
            MessageQueue queue;
            if (!MessageQueue.Exists(queueName))
                queue = MessageQueue.Create(queueName, true);
            else
                queue = new MessageQueue(queueName);
            return queue;
        }

        private static void OnPeekCompleted(Object source, PeekCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // create transaction
            using (var txn = new MessageQueueTransaction())
            {
                try
                {
                    // retrieve message and process
                    txn.Begin();
                    // End the asynchronous peek operation.
                    var message = mq.Receive(txn);

                    // Display message information on the screen.
                    if (message != null)
                    {
                        Console.WriteLine("{0}: {1}", message.Label, (string)message.Body);
                    }

                    // message will be removed on txn.Commit.
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    // on error don't remove message from queue
                    Console.WriteLine(ex.ToString());
                    txn.Abort();
                }
            }

            // Restart the asynchronous peek operation.
            mq.BeginPeek();
        }
    }
}
