namespace Mqtt.Client
{
    public class MqttDualTopicData
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly string _topicReceiver;
        private readonly string _topicSender;

        public string Ip => _ip;
        public int Port => _port;
        public string TopicReceiver => _topicReceiver;
        public string TopicSender => _topicSender;

        public MqttDualTopicData(string ip, int port, string topicReceiver, string topicSender)
        {
            _ip = ip;
            _port = port;
            _topicReceiver = topicReceiver;
            _topicSender = topicSender;
        }
    }
}
