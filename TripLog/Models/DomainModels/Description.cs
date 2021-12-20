using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripLog.Models;

namespace NoteApp.Models.DomainModels
{
    public class Description
    {
        public int DescriptionId { get; set; }            

        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
