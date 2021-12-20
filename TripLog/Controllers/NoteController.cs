using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
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
                int destId = (int)TempData.Peek(nameof(Note.CategoryId));
                vm.CategoryName = data.Categories.Get(destId).Name;

                // get data for drop-down
                
                return View("Add2", vm);
            }
            else
            {
                vm.PageNumber = 1;

                // get data for drop-downs
                vm.Categories = data.Categories.List(new QueryOptions<Category>
                {
                    OrderBy = d => d.Name
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
                    TempData[nameof(Note.CategoryId)] = vm.Note.CategoryId;
                    TempData[nameof(Note.DateCreated)] = vm.Note.DateCreated;
                    TempData[nameof(Note.DueDate)] = vm.Note.DueDate;
                    TempData[nameof(Note.Title)] = vm.Note.Title;
                    TempData[nameof(Note.Description)] = vm.Note.Description;


                    // only store accommodation if user has selected an item from the drop-down


                    return RedirectToAction("Add", new { id = "Page2" });
                }
                else
                {
                    // get data for drop-downs
                    vm.Categories = data.Categories.List(new QueryOptions<Category>
                    {
                        OrderBy = d => d.Name
                    });
                    

                    return View("Add1", vm);
                }
            }
            else if (vm.PageNumber == 2)
            {
                // get saved data from TempData 
                vm.Note = new Note
                {
                    CategoryId = (int)TempData[nameof(Note.CategoryId)],
                    DateCreated = (DateTime)TempData[nameof(Note.DateCreated)],
                    DueDate = (DateTime)TempData[nameof(Note.DueDate)],
                    Title = (string)TempData[nameof(Note.Title)],
                    Description = (string)TempData[nameof(Note.Description)],


                };
                // only get accommodation if there's something in TempData
                

                

                data.Notes.Insert(vm.Note);
                data.Save();

                // get destination data for notification message
                var dest = data.Categories.Get(vm.Note.CategoryId);
                TempData["message"] = $"Note to {dest.Name} added.";

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
            Note note = data.Notes.Get(id);
            Category dest = data.Categories.Get(note.CategoryId); // for notification message

            data.Notes.Delete(note);
            data.Save();

            TempData["message"] = $"Note to {dest.Name} deleted.";
            return RedirectToAction("Index", "Home");
        }
    }
}