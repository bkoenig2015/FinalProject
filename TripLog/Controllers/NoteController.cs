using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class NoteController : Controller
    {
        private UnitOfWork data { get; set; }
        public NoteController(NoteLogContext ctx) => data = new UnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add(string id = "")
        {
            var vm = new NoteViewModel();

                // get data for drop-downs
                vm.Destinations = data.Destinations.List(new QueryOptions<Destination> { 
                    OrderBy = d => d.Name
                });
                vm.Accommodations = data.Accommodations.List(new QueryOptions<Accommodation> { 
                    OrderBy = a => a.Name
                });


                return View("Add1", vm);
               
        }

        [HttpPost]
        public IActionResult Add(NoteViewModel vm)
        {
            
                if (ModelState.IsValid) // only page 1 has required data
                {
                    // Store data in TempData 
                    TempData[nameof(Note.DestinationId)] = vm.Note.DestinationId;
                    TempData[nameof(Note.StartDate)] = vm.Note.StartDate;
                    TempData[nameof(Note.EndDate)] = vm.Note.EndDate;

                    int destId = (int)TempData.Peek(nameof(Note.DestinationId));
                    vm.DestinationName = data.Destinations.Get(destId).Name;

                    // only store accommodation if user has selected an item from the drop-down
                    if (vm.Note.AccommodationId > 0)
                        TempData[nameof(Note.AccommodationId)] = vm.Note.AccommodationId;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // get data for drop-downs
                    vm.Destinations = data.Destinations.List(new QueryOptions<Destination> { 
                        OrderBy = d => d.Name
                    });
                    vm.Accommodations = data.Accommodations.List(new QueryOptions<Accommodation> { 
                        OrderBy = a => a.Name
                    });

                    return View("Add1", vm);
                }
            
        }

        public RedirectToActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public RedirectToActionResult Delete(int id)
        {
            Note note = data.Notes.Get(id);
            Destination dest = data.Destinations.Get(note.DestinationId); // for notification message

            data.Notes.Delete(note);
            data.Save();

            TempData["message"] = $"Trip to {dest.Name} deleted.";
            return RedirectToAction("Index", "Home");
        }
    }
}