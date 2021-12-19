using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripLog.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private NoteLogContext context { get; set; }
        public UnitOfWork(NoteLogContext ctx) => context = ctx;

        private Repository<Note> notes;
        public Repository<Note> Notes {
            get {
                if (notes == null) notes = new Repository<Note>(context);
                return notes;
            }
        }

        private Repository<Destination> destinations;
        public Repository<Destination> Destinations {
            get {
                if (destinations == null) destinations = new Repository<Destination>(context);
                return destinations;
            }
        }

        private Repository<Accommodation> accommodations;
        public Repository<Accommodation> Accommodations {
            get {
                if (accommodations == null) accommodations = new Repository<Accommodation>(context);
                return accommodations;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
