using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Note> data { get; set; }
        public HomeController(NoteLogContext ctx) => data = new Repository<Note>(ctx);

        public ViewResult Index()
        {
            var options = new QueryOptions<Note> { 
                Includes = "Destination, Accommodation",
                OrderBy = t => t.StartDate
            };

            var notes = data.List(options);
            return View(notes);
        }

    }
}
