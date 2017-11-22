using System.Messaging;

namespace MessageBusPatterns.MessageBus.Shared
{
    public static class QueueHelper
    {
        public static MessageQueue GetQueueReference(string queueName)
        {
            if (!MessageQueue.Exists(queueName))
                return MessageQueue.Create(queueName, true);
            return new MessageQueue(queueName);
        }

    }
}
