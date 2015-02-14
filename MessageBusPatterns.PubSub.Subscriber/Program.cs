using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriber = new Subscriber();

            Console.ReadLine();

            Console.WriteLine("Unsubscribing...");

            subscriber.UnSubscribe();

            Console.WriteLine("Unsubscribed. Press any key to exit.");
            Console.ReadLine();
        }
    }

    class Subscriber : IPublishingService
    {
        SubscriptionClient _subscriptionClient;

        public Subscriber()
        {
            _subscriptionClient = new SubscriptionClient(this);
            _subscriptionClient.Subscribe();
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
