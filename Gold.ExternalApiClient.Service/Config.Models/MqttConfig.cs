namespace Gold.ExternalApiClient.Service
{
    public class MqttConfig
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string TopicReceiver { get; set; }
        public string TopicSender { get; set; }
    }
}
