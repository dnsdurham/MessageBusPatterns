using System;
using System.Collections.Generic;
using System.Messaging;
using MessageBusPatterns.MessageBus.Shared;

namespace MessageBusPatterns.MessageBus.Server
{
    class MessageBus
    {
        private MessageQueue _mq;
        /// <summary>
        /// The constructor is used to setup the peek on the message queue so 
        /// that incoming messages are processed
        /// </summary>
        public MessageBus()
        {

            // Create an instance of MessageQueue. Set its formatter.
            _mq = new MessageQueue(@".\private$\mbp.message");
            _mq.Formatter = new XmlMessageFormatter(new[] { typeof(String) });

            // Add an event handler for the PeekCompleted event.
            _mq.PeekCompleted += OnPeekCompleted;

            // Begin the asynchronous peek operation.
            _mq.BeginPeek();
        }

        public void Close()
        {
            // Remove the event handler before closing the queue
            _mq.PeekCompleted -= OnPeekCompleted;
            _mq.Close();
            _mq.Dispose();
        }

        private void OnPeekCompleted(object source, PeekCompletedEventArgs asyncResult)
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
                        // determine the topic
                        var topic = (TopicType) Enum.Parse(typeof (TopicType), message.Label);

                        Console.WriteLine("{0} message received", topic);

                        if (ProcessCommand(message, topic))
                        {
                            // message will be removed on txn.Commit.
                            txn.Commit();
                        }
                        else
                        {
                            // Problem sending message on so put back in the queue
                            txn.Abort();
                        }
                    }
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

        private bool ProcessCommand(Message message, TopicType topic)
        {
            // get the subscribers
            var subscriptions = new SubscriptionManager();
            List<string> subscribers = subscriptions.GetSubscribers(topic);

            // loop through the subscribers and send the message
            using (var trn = new MessageQueueTransaction())
            {
                foreach (var subscriberQueue in subscribers)
                {
                    try
                    {
                        // Create queue object
                        using (var queue = new MessageQueue(subscriberQueue))
                        {
                            queue.Formatter = new XmlMessageFormatter();

                            // push message onto queue (inside of a transaction)
                            trn.Begin();
                            queue.Send((string)message.Body, topic.ToString(), trn);
                            trn.Commit();

                            Console.WriteLine("{0} message queued on {1}", topic, subscriberQueue);
                        }
                    }
                    catch
                    {
                        trn.Abort(); // rollback the transaction
                        return false;
                    }
                }
            }
            return true; // successfully sent the message on to subscribers
        }
    }
}
