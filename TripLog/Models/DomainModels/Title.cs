using System.Collections.Generic;

namespace TripLog.Models
{
    public class Title
    {
        public int TitleId { get; set; }              // PK 
        public string Name { get; set; }
        

        public ICollection<Note> Notes { get; set; }          // navigation property
    }
}
