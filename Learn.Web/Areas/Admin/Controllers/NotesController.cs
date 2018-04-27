using AutoMapper;
using Learn.Core;
using Learn.Core.Extensions;
using Learn.lib.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Learn.lib.Attributes;
using Learn.Web.Controllers;
using Learn.Web.Services;
using Learn.Web.Interface.Services;
using Learn.Web.Manager;
using Learn.DataModel;

namespace Learn.Web.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles =" Writter")]
    public class NotesController : BaseController
    {
        private readonly INoteService noteService;
        private readonly IApplicationUserService ApplicationUserService;
        private readonly ApplicationUserManager UserManager;

        public NotesController(INoteService noteService, IApplicationUserService applicationUserService, ApplicationUserManager userManger)
        {
            this.noteService = noteService;
            this.ApplicationUserService = applicationUserService;
            this.UserManager = userManger;
        }

        public ActionResult Index()
        {
            var notes = noteService.GetNotes();
            var notesViewModel = Mapper.Map<IEnumerable<Note>, IEnumerable<NoteViewModel>>(notes);
            return View(notesViewModel);
        }


        public ActionResult Create()
        {
            var createNote = new NoteViewModel();
            return View(createNote);
        }

        [HttpPost]
        public  ActionResult Create(NoteViewModel createNote)
        {
            var note = Mapper.Map<NoteViewModel, Note>(createNote);
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            note.CreatedBy = currentUser;
            note.UpdatedBy = currentUser;
            IEnumerable<ValidationResult> errors = noteService.CanAddNote(note);
            ModelState.AddModelErrors(errors);
            if (ModelState.IsValid)
            {
                noteService.CreateNote(note);
                return RedirectToAction("Index", new { id = note.Id });
            }
            return View("Create", createNote);
        }

        public ActionResult Edit(string id)
        {
            var note = noteService.GetNote(Guid.Parse(id));
            var editNote = Mapper.Map<Note, NoteViewModel>(note);
            if (note == null)
            {
                return HttpNotFound(); 
            }
            return View(editNote);
        }

        [HttpPost]
        public ActionResult Edit(NoteViewModel editNote)
        {
            var noteToEdit = Mapper.Map<NoteViewModel, Note>(editNote);
            IEnumerable<ValidationResult> errors = noteService.CanAddNote(noteToEdit);
            ModelState.AddModelErrors(errors);
            if (ModelState.IsValid)
            {
                noteService.EditNote(noteToEdit);
                return RedirectToAction("Index", new { id = noteToEdit.Id });
            }
            else
                return View(editNote);
        }

        public ActionResult Delete(string id)
        {
            var note = noteService.GetNote(Guid.Parse(id));
            if (note == null)
            {
                return HttpNotFound();
            }
            var noteToDelete = Mapper.Map<Note, NoteViewModel>(note);
            return View(noteToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            var goal = noteService.GetNote(Guid.Parse(id));
            if (goal == null)
            {
                return HttpNotFound();
            }

            noteService.DeleteNote(Guid.Parse(id));
            return RedirectToAction("Index", "Home");
        }
    }



}
