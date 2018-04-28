using AutoMapper;
using Learn.Lib;
using Learn.Lib.Extensions;
using Learn.DataModel.Models;
using Learn.CMS.Service.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Learn.Service;
using Learn.lib.Attributes;
using Learn.CMS.Controllers;
using Learn.ViewModel;

namespace Learn.CMS.Areas.News.Controllers
{
    [CustomAuthorize(Roles =" Writter")]
    public class NotesController : BaseController
    {
        private readonly INoteService noteService;
        private readonly IUserService ApplicationUserService;

        public NotesController(INoteService noteService, IUserService applicationUserService)
        {
            this.noteService = noteService;
            this.ApplicationUserService = applicationUserService;
        }

        public ActionResult Index()
        {
            var notes = noteService.GetNotes();
            return View(notes);
        }


        public ActionResult Create()
        {
            return View(new NoteViewModel());
        }

        [HttpPost]
        public  ActionResult Create(NoteViewModel model)
        {
            IEnumerable<ValidationResult> errors = noteService.CanAddNote(model);
            if (ModelState.IsValid)
            {
                noteService.CreateNote(model);
                return RedirectToAction("Index", new { id = model.Id });
            }
            else
            {
                ModelState.AddModelErrors(errors);
            }
            return View("Create", model);
        }

        public ActionResult Edit(string id)
        {
            var model = noteService.GetNote(Guid.Parse(id));
            if (model == null)
            {
                return HttpNotFound(); 
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NoteViewModel model)
        {
            IEnumerable<ValidationResult> errors = noteService.CanAddNote(model);
            if (ModelState.IsValid)
            {
                noteService.EditNote(model);
                return RedirectToAction("Index", new { id = model.Id });
            }
            else
            {
                ModelState.AddModelErrors(errors);
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var model = noteService.GetNote(Guid.Parse(id));
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            var model = noteService.GetNote(Guid.Parse(id));
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                noteService.DeleteNote(Guid.Parse(id));
                return RedirectToAction("Index", "Notes");
            }
        }
    }



}
