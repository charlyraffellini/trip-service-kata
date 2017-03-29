using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            if (LoggedUser() == null)
            {
                throw new UserNotLoggedInException();
            }
            return user.IsFriend(LoggedUser())
                ? FindTripsByUser(user)
                : new List<Trip>();
        }

        protected virtual User.User LoggedUser() => UserSession.GetInstance().GetLoggedUser();

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
