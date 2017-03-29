using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly TripDAO _tripDao;

        public TripService(TripDAO tripDao)
        {
            _tripDao = tripDao;
        }

        public List<Trip> GetTripsByUser(User.User user, User.User loggedUser)
        {
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }
            return user.IsFriend(loggedUser)
                ? TripsByUser(user)
                : NoTrips();
        }

        private List<Trip> NoTrips() => new List<Trip>();

        public virtual User.User LoggedUser() => UserSession.GetInstance().GetLoggedUser();

        public virtual List<Trip> TripsByUser(User.User user)
        {
            return _tripDao.TripsByUser(user);
        }
    }
}
