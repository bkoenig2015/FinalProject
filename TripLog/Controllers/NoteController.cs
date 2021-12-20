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

            if (id.ToLower() == "page2")
            {
                vm.PageNumber = 2;

                // get destination name for display by view
                int destId = (int)TempData.Peek(nameof(Note.DestinationId));
                vm.DestinationName = data.Destinations.Get(destId).Name;

                // get data for drop-down
                
                return View("Add2", vm);
            }
            else
            {
                vm.PageNumber = 1;

                // get data for drop-downs
                vm.Destinations = data.Destinations.List(new QueryOptions<Destination>
                {
                    OrderBy = d => d.Name
                });
                vm.Accommodations = data.Accommodations.List(new QueryOptions<Accommodation>
                {
                    OrderBy = a => a.Name
                });

                return View("Add1", vm);
            }
        }

        [HttpPost]
        public IActionResult Add(NoteViewModel vm)
        {
            if (vm.PageNumber == 1)
            {
                if (ModelState.IsValid) // only page 1 has required data
                {
                    // Store data in TempData 
                    TempData[nameof(Note.DestinationId)] = vm.Note.DestinationId;
                    TempData[nameof(Note.StartDate)] = vm.Note.StartDate;
                    TempData[nameof(Note.EndDate)] = vm.Note.EndDate;

                    // only store accommodation if user has selected an item from the drop-down
                    if (vm.Note.AccommodationId > 0)
                        TempData[nameof(Note.AccommodationId)] = vm.Note.AccommodationId;

                    return RedirectToAction("Add", new { id = "Page2" });
                }
                else
                {
                    // get data for drop-downs
                    vm.Destinations = data.Destinations.List(new QueryOptions<Destination>
                    {
                        OrderBy = d => d.Name
                    });
                    vm.Accommodations = data.Accommodations.List(new QueryOptions<Accommodation>
                    {
                        OrderBy = a => a.Name
                    });

                    return View("Add1", vm);
                }
            }
            else if (vm.PageNumber == 2)
            {
                // get saved data from TempData 
                vm.Note = new Note
                {
                    DestinationId = (int)TempData[nameof(Note.DestinationId)],
                    StartDate = (DateTime)TempData[nameof(Note.StartDate)],
                    EndDate = (DateTime)TempData[nameof(Note.EndDate)]
                };
                // only get accommodation if there's something in TempData
                if (TempData.Keys.Contains(nameof(Note.AccommodationId)))
                    vm.Note.AccommodationId = (int)TempData[nameof(Note.AccommodationId)];

                

                data.Notes.Insert(vm.Note);
                data.Save();

                // get destination data for notification message
                var dest = data.Destinations.Get(vm.Note.DestinationId);
                TempData["message"] = $"Trip to {dest.Name} added.";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
            Note trip = data.Notes.Get(id);
            Destination dest = data.Destinations.Get(trip.DestinationId); // for notification message

            data.Notes.Delete(trip);
            data.Save();

            TempData["message"] = $"Trip to {dest.Name} deleted.";
            return RedirectToAction("Index", "Home");
        }
    }
}