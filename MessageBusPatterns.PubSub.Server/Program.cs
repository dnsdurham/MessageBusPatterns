using System;
using System.ServiceModel;

namespace MessageBusPatterns.PubSub.Server
{
    class Program
    {
        private static ServiceHost _publishingServiceHost;
        private static ServiceHost _subscriptionServiceHost;

        static void Main()
        {
            _publishingServiceHost = new ServiceHost(typeof(PublishingService));
            _publishingServiceHost.Open();

            Console.WriteLine("Publishing service started on {0}", _publishingServiceHost.Description.Endpoints[0].Address.Uri);

            _subscriptionServiceHost = new ServiceHost(typeof(SubscriptionService));
            _subscriptionServiceHost.Open();

            Console.WriteLine("Subscription service started on {0}", _subscriptionServiceHost.Description.Endpoints[0].Address.Uri);

            Console.ReadLine();

            Console.WriteLine("Closing service hosts...");
            _publishingServiceHost.Close();
            _subscriptionServiceHost.Close();

            Console.WriteLine("Hosts closed. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
