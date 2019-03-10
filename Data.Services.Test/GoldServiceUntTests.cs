using Data.Model;
using Data.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
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
                         DateTime.Now,
                         5.0
                    }
                }
            };

            _substitute = Substitute.For<IGoldRepository>();
            _substitute.Get().Returns(goldDataModel);

            _sut = new GoldService(_substitute);
        }

        [Fact]
        public void GetRequestsTest_ResultXmlEqualsExample()
        {
            var result = _sut.GetOldestDay();

            Assert.Equal(DateTime.MinValue.Date, result.Date);
        }
    }
}
