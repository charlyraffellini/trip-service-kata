using System;
using FluentAssertions;
using Moq;
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
        private Mock<TripDAO> tripDaoMock;
        private TripService tripService;

        public TripServiceShould()
        {
            Friend.AddTrip(ToBarcelona);
            Friend.AddTrip(ToLondon);
            tripDaoMock = new Mock<TripDAO>();
            tripService = new TripService(tripDaoMock.Object);
        }

        [Fact]
        public void Return_trips_when_users_are_friends()
        {
            tripDaoMock
                .Setup(t => t.TripsByUser(Friend))
                .Returns(Friend.Trips);
            Friend.AddFriend(RegisteredUser);

            var trips = tripService.GetTripsByUser(Friend, RegisteredUser);

            trips.Should().HaveCount(2);
        }

        [Fact]
        public void Not_return_any_trip_when_users_are_not_friends()
        {
            var  trips = tripService.GetTripsByUser(Friend, RegisteredUser);

            trips.Should().BeEmpty();
        }

        [Fact]
        public void Thow_an_exception_when_user_is_not_logged_in()
        {
            Action call = () => tripService.GetTripsByUser(NoUser, Guest);

            call.ShouldThrow<UserNotLoggedInException>();
        }
    }
}
