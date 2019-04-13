using Data.Model;
using Data.Repositories;
using Mqtt.Client;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Data.Services.Test
{
    public class GoldServiceUntTests
    {
        private readonly IGoldRepository _goldRepository;
        private readonly IMqttDualTopicClient _mqttDualTopicClient;
        private readonly IGoldService _sut;

        public GoldServiceUntTests()
        {
            var goldDataModel = new GoldDataModel
            {
                OldestAvailableDate = DateTime.MinValue.ToLongDateString(),
                NewestAvailaleDate = DateTime.Now.ToLongDateString(),
                Data = new List<List<object>>
                {
                    new List<object>
                    {
                         new DateTime(2010, 06, 29),
                         15.7
                    },
                    new List<object>
                    {
                         new DateTime(2010, 06, 30),
                         25.8
                    },
                    new List<object>
                    {
                         new DateTime(2010, 07, 02),
                         35.8
                    },
                    new List<object>
                    {
                         new DateTime(2010, 07, 03),
                         18.9
                    }
                }
            };

            _goldRepository = Substitute.For<IGoldRepository>();
            _goldRepository.Get().Returns(goldDataModel);

            _mqttDualTopicClient = Substitute.For<IMqttDualTopicClient>();
            _mqttDualTopicClient.Start().Returns(false);

            _sut = new GoldService(_goldRepository, _mqttDualTopicClient);
        }

        [Fact]
        public void GetNewestPrice_ServiceInstantiated_ReturnsNonEmptyData()
        {
            var allPrices = _sut.GetDailyGoldPricesFromDatabase();

            Assert.NotEmpty(allPrices);
            Assert.Equal("2010-6-29,15.7", allPrices.First());
        }
    }
}
