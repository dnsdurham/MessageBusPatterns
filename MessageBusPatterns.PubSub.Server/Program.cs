using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusPatterns.PubSub.Server
{
    class Program
    {
        private static ServiceHost _publishingServiceHost;
        private static ServiceHost _subscriptionServiceHost;

        static void Main(string[] args)
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
        }
    }
}
