using System;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class ManageController : Controller
    {
        private UnitOfWork data { get; set; }
        public ManageController(NoteLogContext ctx) => data = new UnitOfWork(ctx);

        [HttpGet]
        public ViewResult Index()
        {
            var vm = new ManageViewModel();
            LoadDropDownData(vm);
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(ManageViewModel vm)
        {
            bool needsSave = false;
            string notifyMsg = "";

            if (!string.IsNullOrEmpty(vm.Destination.Name)) {
                data.Destinations.Insert(vm.Destination);
                notifyMsg = $"{notifyMsg} {vm.Destination.Name}, ";
                needsSave = true;
            }
            if (!string.IsNullOrEmpty(vm.Accommodation.Name)) {
                data.Accommodations.Insert(vm.Accommodation);
                notifyMsg = $"{notifyMsg} {vm.Accommodation.Name}, ";
                needsSave = true;
            }
            if (needsSave) {
                data.Save();
                TempData["message"] = notifyMsg + " added";
            }
                
            return RedirectToAction("Confirm");
        }

        [HttpPost]
        public IActionResult Delete(ManageViewModel vm)
        {
            bool needsSave = false;
            string notifyMsg = "";

            /******************************************************
             * On delete, retrieve full entity from database first
             * so have Name value for notification message
             ******************************************************/

            if (vm.Destination.DestinationId > 0) {
                vm.Destination = data.Destinations.Get(vm.Destination.DestinationId);
                data.Destinations.Delete(vm.Destination);
                notifyMsg = $"{notifyMsg} {vm.Destination.Name}, ";
                needsSave = true;
            }
            if (vm.Accommodation.AccommodationId > 0) {
                vm.Accommodation = data.Accommodations.Get(vm.Accommodation.AccommodationId);
                data.Accommodations.Delete(vm.Accommodation);
                notifyMsg = $"{notifyMsg} {vm.Accommodation.Name}, ";
                needsSave = true;
            }
            /**************************************************************************************
             * If try to delete a destination that's associated with a trip, will get an exception,
             * bc FK delete behavior is set to Restrict. No exception for an accommodation, bc FK
             * delete behavior is set to SetNull, and no exception for activity bc just removes 
             * entry from linking table. So catch block is only concerned with a destination.
             **************************************************************************************/
            if (needsSave) {
                try {
                    data.Save();
                    TempData["message"] = notifyMsg + " deleted";
                } 
                catch {
                    TempData["message"] = $"Unable to delete {vm.Destination.Name} because it's associated with a Trip.";
                    LoadDropDownData(vm);
                    return View("Index", vm);
                }
            }
                
            return RedirectToAction("Confirm");
        }

        public ViewResult Confirm() => View();

        private void LoadDropDownData(ManageViewModel vm)
        {
            vm.Destinations = data.Destinations.List(new QueryOptions<Destination> {
                OrderBy = d => d.Name
            });
            vm.Accommodations = data.Accommodations.List(new QueryOptions<Accommodation> {
                OrderBy = a => a.Name
            });
            
        }
    }
}