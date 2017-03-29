using FluentAssertions;
using Xunit;

namespace TripServiceKata.Tests
{
    public class UserShould
    {
        [Fact]
        public void Not_be_friend_of_an_stranger()
        {
            var stranger = new User.User();
            var user = new User.User();
            var actual = user.IsFriend(stranger);

            actual.Should().Be(false);
        }

        [Fact]
        public void Be_friend_of_friend()
        {
            var user = new User.User();
            var friend = new User.User();
            friend.AddFriend(user);
            var actual = user.IsFriend(friend);

            actual.Should().Be(false);
        }
    }
}