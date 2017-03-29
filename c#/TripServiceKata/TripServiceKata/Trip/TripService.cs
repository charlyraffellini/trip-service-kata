using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly TripDAO _tripDao = new TripDAO();

        public List<Trip> GetTripsByUser(User.User user)
        {
            if (LoggedUser() == null)
            {
                throw new UserNotLoggedInException();
            }
            return user.IsFriend(LoggedUser())
                ? TripsByUser(user)
                : NoTrips();
        }

        private List<Trip> NoTrips() => new List<Trip>();

        protected virtual User.User LoggedUser() => UserSession.GetInstance().GetLoggedUser();

        public virtual List<Trip> TripsByUser(User.User user)
        {
            return _tripDao.TripsByUser(user);
        }
    }
}
