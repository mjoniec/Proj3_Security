using Data.Access.Model;
using Data.Access.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Data.Services.Test
{
    public class RequestServiceUntTests
    {
        private readonly IRequestRepository _substitute;
        private readonly IRequestService _sut;

        public RequestServiceUntTests()
        {
            _substitute = Substitute.For<IRequestRepository>();
            _substitute.SaveRequests(Arg.Any<IEnumerable<Request>>()).Returns(string.Empty);

            _sut = new RequestService(_substitute);
        }

        [Fact]
        public void GetRequestsTest()
        {
            var result = _sut.GetRequests();



        }
    }
}
