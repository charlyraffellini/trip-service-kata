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
            var tripService = new TripServiceTest();
            Friend.AddFriend(RegisteredUser);

            var trips = tripService.GetTripsByUser(Friend, RegisteredUser);

            trips.Should().HaveCount(2);
        }

        [Fact]
        public void Not_return_any_trip_when_users_are_not_friends()
        {
            var tripService = new TripServiceTest();

            var  trips = tripService.GetTripsByUser(Friend, RegisteredUser);

            trips.Should().BeEmpty();
        }

        [Fact]
        public void Thow_an_exception_when_user_is_not_logged_in()
        {
            var tripService = new TripServiceTest();

            Action call = () => tripService.GetTripsByUser(NoUser, Guest);

            call.ShouldThrow<UserNotLoggedInException>();
        }

        class TripServiceTest : TripService
        {
            public TripServiceTest() : base(new TripDAO())
            {
            }

            public override List<Trip.Trip> TripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
