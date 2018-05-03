using Learn.Core;
using Learn.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Learn.CMS.Controllers;
using Learn.Core.Services;
using Learn.Core.Attributes;
using Learn.Core.ViewModels;
using Learn.Core.DataEnum;

namespace Learn.CMS.Areas.News.Controllers
{
    public class NotesController : BaseController
    {
        private readonly INoteService noteService;
        private readonly IUserService ApplicationUserService;

        public NotesController(INoteService noteService, IUserService applicationUserService)
        {
            this.noteService = noteService;
            this.ApplicationUserService = applicationUserService;
        }

        [HttpGet]
        [AuthorizeUser(accessLevel = AccessLevelType.Read, functionID = FunctionIDs.NotesgMgt)]
        public ActionResult Index()
        {
            var notes = noteService.GetNotes();
            return View(notes);
        }

        [HttpGet]
        [AuthorizeUser(accessLevel = AccessLevelType.Insert, functionID = FunctionIDs.NotesgMgt)]
        public ActionResult Create()
        {
            return View(new NoteViewModel());
        }

        [HttpPost]
        [AuthorizeUser(accessLevel = AccessLevelType.Insert, functionID = FunctionIDs.NotesgMgt)]
        public ActionResult Create(NoteViewModel model)
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

        [HttpGet]
        [AuthorizeUser(accessLevel = AccessLevelType.Update, functionID = FunctionIDs.NotesgMgt)]
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
        [AuthorizeUser(accessLevel = AccessLevelType.Update, functionID = FunctionIDs.NotesgMgt)]
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

        [HttpGet]
        [AuthorizeUser(accessLevel = AccessLevelType.Delete, functionID = FunctionIDs.NotesgMgt)]
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
        [AuthorizeUser(accessLevel = AccessLevelType.Delete, functionID = FunctionIDs.NotesgMgt)]
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
