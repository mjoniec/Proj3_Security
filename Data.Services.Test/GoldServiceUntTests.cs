using Data.Model;
using Data.Repositories;
//using Mqtt.Client;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Data.Services.Test
{
    public class GoldServiceUntTests
    {
        private readonly IGoldRepository _substitute;
        private readonly IGoldService _sut;

        public GoldServiceUntTests()
        {
            var goldDataModel = new GoldDataModel
            {
                OldestAvailableDate = DateTime.MinValue.ToLongDateString(),
                NewestAvailaleDate = DateTime.Now.ToLongDateString(),
                DailyGoldDataUnparsed = new List<List<object>>
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

            _substitute = Substitute.For<IGoldRepository>();
            _substitute.Get().Returns(goldDataModel);

           // var mqttDualTopicClient = Substitute.For<MqttDualTopicClient>();

            _sut = new GoldService(_substitute, null/*, mqttDualTopicClient*/);
        }

        [Fact]
        public void GetNewestPrice_ServiceInstantiated_ReturnsNonEmptyData()
        {
            var allPrices = _sut.GetAllPrices();

            Assert.NotEmpty(allPrices);
            Assert.Equal("2010-6-29,15.7", allPrices.First());
        }
    }
}
