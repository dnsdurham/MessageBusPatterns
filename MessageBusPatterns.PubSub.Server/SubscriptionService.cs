using System;
using System.ServiceModel;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class SubscriptionService : ISubscriptionService
    {
        public void Subscribe()
        {
            IPublishingService subscriber = OperationContext.Current.GetCallbackChannel<IPublishingService>();
            SubscriptionManager.AddSubscriber(subscriber);
            Console.WriteLine("Subscriber added");
        }

        public void Unsubscribe()
        {
            IPublishingService subscriber = OperationContext.Current.GetCallbackChannel<IPublishingService>();
            SubscriptionManager.RemoveSubscriber(subscriber);
            Console.WriteLine("Subscriber removed");
        }
    }
}
