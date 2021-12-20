using NoteApp.Models.DomainModels;

namespace TripLog.Models
{
    public class ManageViewModel : DropDownViewModel
    {
        public Category Category { get; set; }
        public Title Title { get; set; }
        public Description Description { get; set; }
    } 
}
