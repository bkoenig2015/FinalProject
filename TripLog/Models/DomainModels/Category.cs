using System.Collections.Generic;

namespace NoteApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }           // PK 
        public string Name { get; set; }

        public ICollection<Note> Note { get; set; }     // navigation property
    }
}
