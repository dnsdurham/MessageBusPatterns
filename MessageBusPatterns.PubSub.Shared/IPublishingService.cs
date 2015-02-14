using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MessageBusPatterns.PubSub.Shared
{
    [ServiceContract]
    public interface IPublishingService
    {
        [OperationContract(IsOneWay = true)]
        void Publish(PubSubMessage e);
    }
}
