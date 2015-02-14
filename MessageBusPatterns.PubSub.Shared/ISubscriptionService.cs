using System.ServiceModel;

namespace MessageBusPatterns.PubSub.Shared
{
    [ServiceContract(CallbackContract = typeof(IPublishingService))]
    public interface ISubscriptionService
    {
      [OperationContract]
      void Subscribe();

      [OperationContract]
      void Unsubscribe();
    }
}
