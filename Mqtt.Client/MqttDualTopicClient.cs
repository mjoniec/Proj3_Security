using System;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.Client
{
    public class MqttDualTopicClient
    {
        private readonly string _topicSender;
        private readonly IMqttClient _client = new MqttFactory().CreateMqttClient();

        public event EventHandler<MessageEventArgs> RaiseMessageReceivedEvent;

        public MqttDualTopicClient(string ip, int port, string topicReceiver, string topicSender)
        {
            _topicSender = topicSender;
            _client.ApplicationMessageReceived += (o, mqttEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(mqttEventArgs.ApplicationMessage.Payload);

                OnRaiseMessageReceivedEvent(new MessageEventArgs(message));
            };

            Start(ip, port, topicReceiver);
        }

        private void OnRaiseMessageReceivedEvent(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = RaiseMessageReceivedEvent;

            //no subscribers
            if (handler == null) return;
            
            handler(this, e);            
        }

        private async void Start(string ip, int port, string topicReceiver)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(ip, port)
                .Build();

            await _client.ConnectAsync(options);
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topicReceiver).Build());
        }

        public async void Send(string message)
        {
            var mqttApplicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(_topicSender)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _client.PublishAsync(mqttApplicationMessageBuilder);
        }
    }
}
