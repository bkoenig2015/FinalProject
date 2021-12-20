using NoteApp.Models.DomainModels;

namespace TripLog.Models
{
    public interface IUnitOfWork
    {
        Repository<Note> Notes { get; }
        Repository<Category> Categories { get; }
        Repository<Title> Titles { get; }
        Repository<Description> Descriptions { get; }
        void Save();
    }
}
