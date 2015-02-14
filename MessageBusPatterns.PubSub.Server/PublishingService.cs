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

            if (SubscriptionManager.Subscribers.Count > 0)
            {
                foreach (var subscriber in SubscriptionManager.Subscribers)
                {
                    try
                    {
                        publishMethodInfo.Invoke(subscriber, new object[] { e });
                        Console.WriteLine("Message {0} send to subscriber", e.MessageNum);
                    }
                    catch
                    {
                        Console.WriteLine("Error publishing to subscriber");
                    }

                }                
            }
            else
            {
                Console.WriteLine("No subscribers for message {0}", e.MessageNum);                
            }
        }
    }
}
