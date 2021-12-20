using System;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
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

            

            if (vm.Category.CategoryId > 0) {
                vm.Category = data.Categories.Get(vm.Category.CategoryId);
                data.Categories.Delete(vm.Category);
                notifyMsg = $"{notifyMsg} {vm.Category.Name}, ";
                needsSave = true;
            }


            
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