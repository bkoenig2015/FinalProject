namespace TripLog.Models
{
    public interface IUnitOfWork
    {
        Repository<Trip> Trips { get; }
        Repository<Destination> Destinations { get; }
        Repository<Accommodation> Accommodations { get; }
        Repository<Activity> Activities { get; }

        void Save();
    }
}
