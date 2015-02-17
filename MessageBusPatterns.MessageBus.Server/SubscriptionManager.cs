using System.Collections.Generic;
using MessageBusPatterns.MessageBus.Shared;

namespace MessageBusPatterns.MessageBus.Server
{
    /// <summary>
    /// This class simulates the management of the subscribers for the commands.
    /// Normally, the list of subscribers would be persisted somewhere and would
    /// be dynamically added and removed. 
    /// </summary>
    class SubscriptionManager
    {
        Dictionary<TopicType, List<string>> _subscribersList = new Dictionary<TopicType, List<string>>();

        /// <summary>
        /// The constructor is used to simuldate the population of the subscribers from some
        /// persistence
        /// </summary>
        public SubscriptionManager()
        {
            // Add subscribers to NewOrder commands
            List<string> subscriberQueues = new List<string>();
            subscriberQueues.Add(@".\private$\mbp.payment");
            subscriberQueues.Add(@".\private$\mbp.inventory");
            subscriberQueues.Add(@".\private$\mbp.notification");
            _subscribersList.Add(TopicType.NewOrder, subscriberQueues);

            // Add subscribers to Shiiped commands
            subscriberQueues = new List<string>();
            subscriberQueues.Add(@".\private$\mbp.payment");
            subscriberQueues.Add(@".\private$\mbp.notification");
            _subscribersList.Add(TopicType.Shipped, subscriberQueues);
        
            // Add subscribers to Return commands
            subscriberQueues = new List<string>();
            subscriberQueues.Add(@".\private$\mbp.payment");
            subscriberQueues.Add(@".\private$\mbp.inventory");
            _subscribersList.Add(TopicType.Return, subscriberQueues);

        }

        public Dictionary<TopicType, List<string>> SubscribersList
        {
            get
            {
                return _subscribersList;
            }

        }
        public List<string> GetSubscribers(TopicType topic)
        {
            if (SubscribersList.ContainsKey(topic))
            {
                return SubscribersList[topic];
            }
            return null;
        }
    }
}
