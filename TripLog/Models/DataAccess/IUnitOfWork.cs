namespace TripLog.Models
{
    public interface IUnitOfWork
    {
        Repository<Note> Notes { get; }
        Repository<Category> Categories { get; }
        void Save();
    }
}
