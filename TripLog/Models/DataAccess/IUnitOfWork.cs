namespace TripLog.Models
{
    public interface IUnitOfWork
    {
        Repository<Note> Notes { get; }
        Repository<Destination> Destinations { get; }
        Repository<Accommodation> Accommodations { get; }
        void Save();
    }
}
