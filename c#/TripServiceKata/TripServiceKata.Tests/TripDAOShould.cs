using System;
using FluentAssertions;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripDAOShould
    {
        [Fact]
        public void Throw_exception()
        {
            Action call = () => new TripDAO().TripsByUser(null);

            call.ShouldThrow<DependendClassCallDuringUnitTestException>();
        }
    }
}