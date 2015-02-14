using System.ServiceModel;
using MessageBusPatterns.PubSub.Shared;

namespace MessageBusPatterns.PubSub.Publisher
{
    class PublishingClient : ClientBase<IPublishingService>, IPublishingService
    {
        public void Publish(PubSubMessage e)
        {
            Channel.Publish(e);
        }
    }
}
