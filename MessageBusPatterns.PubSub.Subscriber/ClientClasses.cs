using System;
using System.ServiceModel;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Subscriber
{
    class Subscriber : IPublishingService
    {
        SubscriptionClient _subscriptionClient;

        public Subscriber()
        {
            _subscriptionClient = new SubscriptionClient(this);
            _subscriptionClient.Subscribe();
            Console.WriteLine("Subscribed");
        }

        public void Publish(PubSubMessage e)
        {
            Console.WriteLine("Message {0}: {1}", e.MessageNum, e.Content);
        }

        public void UnSubscribe()
        {
            _subscriptionClient.Unsubscribe();
        }
    }

    class SubscriptionClient : DuplexClientBase<ISubscriptionService>, ISubscriptionService
    {
        public SubscriptionClient(object inputInstance)
            : base(inputInstance)
        { }

        public void Subscribe()
        {
            Channel.Subscribe();
        }
        public void Unsubscribe()
        {
            Channel.Unsubscribe();
        }
    }

}
