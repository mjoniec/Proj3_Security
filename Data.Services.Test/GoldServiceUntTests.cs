using Data.Repositories;
using Mqtt.Client;
using NSubstitute;
using Xunit;

namespace Data.Services.Test
{
    public class GoldServiceUntTests
    {
        private readonly IGoldRepository _goldRepository;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private IGoldService _sut;
        private double ExampleMockValue => GoldRepository.ExampleMockValue;

        //inits test like setup im moq
        public GoldServiceUntTests()
        {
            //reuse when repo actually implemented
            //_goldRepository = Substitute.For<IGoldRepository>();
            //_goldRepository.Get().Returns(goldDataModel);

            _goldRepository = new GoldRepository();
            _mqttDualTopicClient = Substitute.For<IMqttDualTopicClient>();
        }

        [Fact]
        public void GetDailyGoldPrices_InvokedWithZeroParameterAndMqttClientInstantiated_ReturnsNonEmptyDataFromRepoitory()
        {
            _mqttDualTopicClient.Start().Returns(true);
            _sut = new GoldService(_goldRepository, _mqttDualTopicClient);

            var goldDataDaily = _sut.GetDailyGoldPrices("0");

            Assert.True(_sut.IsMqttConnected);
            Assert.NotEmpty(goldDataDaily);
            Assert.Contains(goldDataDaily, goldDataDay => goldDataDay.Value == ExampleMockValue);
        }

        [Fact]
        public void GetDailyGoldPrices_InvokedWithEmptyParameterAndMqttClientInstantiated_ReturnsNonEmptyDataFromRepoitory()
        {
            _mqttDualTopicClient.Start().Returns(true);
            _sut = new GoldService(_goldRepository, _mqttDualTopicClient);

            var goldDataDaily = _sut.GetDailyGoldPrices(string.Empty);

            Assert.True(_sut.IsMqttConnected);
            Assert.NotEmpty(goldDataDaily);
            Assert.Contains(goldDataDaily, goldDataDay => goldDataDay.Value == ExampleMockValue);
        }

        [Fact]
        public void GetDailyGoldPrices_InvokedWithNonZeroParameterAndMqttClientNotInstantiated_ReturnsNonEmptyDataFromRepoitory()
        {
            _mqttDualTopicClient.Start().Returns(false);
            _sut = new GoldService(_goldRepository, _mqttDualTopicClient);

            var goldDataDaily = _sut.GetDailyGoldPrices("1");

            Assert.False(_sut.IsMqttConnected);
            Assert.NotEmpty(goldDataDaily);
            Assert.Contains(goldDataDaily, goldDataDay => goldDataDay.Value == ExampleMockValue);
        }
    }
}
