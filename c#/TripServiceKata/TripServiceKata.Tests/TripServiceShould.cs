using System;
using FluentAssertions;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        [Fact]
        public void Thow_an_exception_when_user_is_not_logged_in()
        {
            var tripService = new TripServiceTest();
             
            Action call = () => tripService.GetTripsByUser(null);

            call.ShouldThrow<UserNotLoggedInException>();
        }

        internal class TripServiceTest : TripService
        {
            protected override User.User GetLoggedUser() => null;
        }
    }
}
