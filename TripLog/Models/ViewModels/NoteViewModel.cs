namespace TripLog.Models
{
    public class NoteViewModel : DropDownViewModel
    {
        public Note Note { get; set; }

        public int PageNumber { get; set; }
        
        public string CategoryName { get; set; }
    }
}
