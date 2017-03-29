using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = LoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }
            var isFriend = user.IsFriend(loggedUser);
            if (isFriend)
            {
                tripList = FindTripsByUser(user);
            }
            return tripList;
        }

        protected virtual User.User LoggedUser() => UserSession.GetInstance().GetLoggedUser();

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
