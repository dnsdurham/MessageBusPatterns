using System;
using System.ServiceModel;
using MessageBusPatterns.PubSub.Shared;
using System.Reflection;

namespace MessageBusPatterns.PubSub.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class PublishingService : IPublishingService
    {
        public void Publish(PubSubMessage e)
        {
            if (SubscriptionManager.Subscribers == null) return;

            Type type = typeof(IPublishingService);
            MethodInfo publishMethodInfo = type.GetMethod("Publish");

            foreach (var subscriber in SubscriptionManager.Subscribers)
            {
                try
                {
                    publishMethodInfo.Invoke(subscriber, new object[] { e });
                }
                catch
                {
                    Console.WriteLine("Error publishing to subscriber");
                }

            }

        }
    }
}
