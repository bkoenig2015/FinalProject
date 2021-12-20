using NoteApp.Models.DomainModels;
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

        private Repository<Category> categories;
        public Repository<Category> Categories {
            get {
                if (categories == null) categories = new Repository<Category>(context);
                return categories;
            }
        }

        private Repository<Title> titles;
        public Repository<Title> Titles {
            get {
                if (titles == null) titles = new Repository<Title>(context);
                return titles;
            }
        }

        private Repository<Description> descriptions;
        public Repository<Description> Descriptions
        {
            get
            {
                if (descriptions == null) descriptions = new Repository<Description>(context);
                return descriptions;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
