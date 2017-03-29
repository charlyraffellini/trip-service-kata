using System;
using FluentAssertions;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        private const User.User Guest = null;
        private const User.User NoUser = null;
        private User.User _loggedInUser;
        private static readonly User.User RegisteredUser = new User.User();
        private User.User Friend = new User.User();
        private Trip.Trip ToBarcelona = new Trip.Trip();

        [Fact]
        public void Not_return_any_trip_when_users_are_not_friends()
        {
            var tripService = new TripServiceTest(RegisteredUser);
            Friend.AddTrip(ToBarcelona);

            var  trips = tripService.GetTripsByUser(Friend);

            trips.Should().BeEmpty();
        }

        [Fact]
        public void Thow_an_exception_when_user_is_not_logged_in()
        {
            var tripService = new TripServiceTest(Guest);

            Action call = () => tripService.GetTripsByUser(NoUser);

            call.ShouldThrow<UserNotLoggedInException>();
        }

        class TripServiceTest : TripService
        {
            private readonly User.User _loggedUser;

            public TripServiceTest(User.User loggedUser)
            {
                _loggedUser = loggedUser;
            }

            protected override User.User LoggedUser() => _loggedUser;
        }
    }
}
