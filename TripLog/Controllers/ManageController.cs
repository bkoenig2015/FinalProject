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

            if (!string.IsNullOrEmpty(vm.Category.Name)) {
                data.Categories.Insert(vm.Category);
                notifyMsg = $"{notifyMsg} {vm.Category.Name}, ";
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

            if (vm.Category.CategoryId > 0) {
                vm.Category = data.Categories.Get(vm.Category.CategoryId);
                data.Categories.Delete(vm.Category);
                notifyMsg = $"{notifyMsg} {vm.Category.Name}, ";
                needsSave = true;
            }
            /*
            if (vm.Title.TitleId > 0) {
                vm.Title = data.Titles.Get(vm.Title.TitleId);
                data.Titles.Delete(vm.Title);
                notifyMsg = $"{notifyMsg} {vm.Title.Name}, ";
                needsSave = true;
            }
            if (vm.Description.DescriptionId > 0)
            {
                vm.Description = data.Descriptions.Get(vm.Description.DescriptionId);
                data.Descriptions.Delete(vm.Description);
                notifyMsg = $"{notifyMsg} {vm.Description.Name}, ";
                needsSave = true;
            }*/
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
                    TempData["message"] = $"Unable to delete {vm.Category.Name} because it's associated with a Note.";
                    LoadDropDownData(vm);
                    return View("Index", vm);
                }
            }
                
            return RedirectToAction("Confirm");
        }

        public ViewResult Confirm() => View();

        private void LoadDropDownData(ManageViewModel vm)
        {
            vm.Categories = data.Categories.List(new QueryOptions<Category> {
                OrderBy = d => d.Name
            });
           
        }
    }
}