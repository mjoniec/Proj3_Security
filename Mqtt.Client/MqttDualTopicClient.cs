using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.Client
{
    public class MqttDualTopicClient
    {
        private readonly MqttDualTopicData _mqttDualTopicData;
        private readonly IMqttClient _client = new MqttFactory().CreateMqttClient();

        public event EventHandler<MessageEventArgs> RaiseMessageReceivedEvent;

        public MqttDualTopicClient(MqttDualTopicData mqttDualTopicData)
        {
            _mqttDualTopicData = mqttDualTopicData;
            _client.ApplicationMessageReceived += (o, mqttEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(mqttEventArgs.ApplicationMessage.Payload);

                OnRaiseMessageReceivedEvent(new MessageEventArgs(message));
            };
        }

        private void OnRaiseMessageReceivedEvent(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = RaiseMessageReceivedEvent;

            //no subscribers
            if (handler == null) return;
            
            handler(this, e);
        }

        public async Task<bool> Start()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttDualTopicData.Ip, _mqttDualTopicData.Port)
                .Build();

            await _client.ConnectAsync(options);

            var subscribeResults = await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(_mqttDualTopicData.TopicReceiver).Build());

            if (subscribeResults.Any(r=>r.ReturnCode == MQTTnet.Protocol.MqttSubscribeReturnCode.Failure)) return false;

            return true;
        }

        public async void Send(string message)
        {
            var mqttApplicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(_mqttDualTopicData.TopicSender)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _client.PublishAsync(mqttApplicationMessageBuilder);
        }
    }
}
