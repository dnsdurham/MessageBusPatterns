using System.Collections.Generic;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Server
{
    class SubscriptionManager
    {
        static List<IPublishingService> _subscriberList = new List<IPublishingService>();
        static object _locker = new object();

        public static List<IPublishingService> Subscribers
        {
            get
            {
                lock (_locker)
                {
                    return _subscriberList;
                }
            }

        }

        static public void AddSubscriber(IPublishingService subscriberCallbackReference)
        {
            lock (_locker)
            {
                if (!Subscribers.Contains(subscriberCallbackReference))
                {
                    Subscribers.Add(subscriberCallbackReference);
                }
            }

        }

        static public void RemoveSubscriber(IPublishingService subscriberCallbackReference)
        {
            lock (_locker)
            {
                if (Subscribers.Contains(subscriberCallbackReference))
                {
                    Subscribers.Remove(subscriberCallbackReference);
                }
            }
        }
    }
}
