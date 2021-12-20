using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Models
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

        public void Save() => context.SaveChanges();
    }
}
