using System.Runtime.Serialization;

namespace MessageBusPatterns.PubSub.Shared
{
    [DataContract]
    public class PubSubMessage
    {
        [DataMember]
        public int MessageNum { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}
