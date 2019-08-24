using Mqtt.Client;
using NSubstitute;
using Xunit;

namespace Data.Services.Test
{
    public class GoldServiceUntTests
    {
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private IGoldService _sut;

        public GoldServiceUntTests()
        {
            _mqttDualTopicClient = Substitute.For<IMqttDualTopicClient>();
        }

        [Fact]
        public void StartPreparingData_ReturnsUshortValue_WhenInvokedWithMockConnectedMqttClient()
        {
            _mqttDualTopicClient.Start().Returns(true);
            _sut = new GoldService(_mqttDualTopicClient);

            var requestId = _sut.StartPreparingData();

            Assert.NotEqual(ushort.MinValue, requestId);
        }

        [Fact]
        public void StartPreparingData_ReturnsZero_WhenInvokedWithMockNotConnectedMqttClient()
        {
            _mqttDualTopicClient.Start().Returns(false);
            _sut = new GoldService(_mqttDualTopicClient);

            var requestId = _sut.StartPreparingData();

            Assert.Equal(ushort.MinValue, requestId);
        }

        //TODO #34
        //[Fact]
        //public void GetDailyGoldPrices_()
        //{
        //    _mqttDualTopicClient.Start().Returns(true);
        //    _sut = new GoldService(_mqttDualTopicClient);

        //    var goldDataDaily = _sut.GetDailyGoldPrices("0");


        //}
    }
}
