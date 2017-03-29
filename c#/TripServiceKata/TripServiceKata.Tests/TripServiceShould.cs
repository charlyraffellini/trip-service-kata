using System;
using System.Collections.Generic;
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
        private static readonly User.User RegisteredUser = new User.User();
        private User.User Friend = new User.User();
        private Trip.Trip ToBarcelona = new Trip.Trip();
        private Trip.Trip ToLondon = new Trip.Trip();

        public TripServiceShould()
        {
            Friend.AddTrip(ToBarcelona);
            Friend.AddTrip(ToLondon);
        }

        [Fact]
        public void Return_trips_when_users_are_friends()
        {
            var tripService = new TripServiceTest(RegisteredUser);
            Friend.AddFriend(RegisteredUser);

            var trips = tripService.GetTripsByUser(Friend);

            trips.Should().HaveCount(2);
        }

        [Fact]
        public void Not_return_any_trip_when_users_are_not_friends()
        {
            var tripService = new TripServiceTest(RegisteredUser);

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

            public TripServiceTest(User.User loggedUser) : base(new TripDAO())
            {
                _loggedUser = loggedUser;
            }

            protected override User.User LoggedUser() => _loggedUser;

            public override List<Trip.Trip> TripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
