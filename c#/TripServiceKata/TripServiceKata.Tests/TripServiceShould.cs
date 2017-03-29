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
        private User.User _loggedUser;

        [Fact]
        public void Thow_an_exception_when_user_is_not_logged_in()
        {
            var tripService = new TripServiceTest(_loggedUser);
            _loggedUser = Guest;

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
