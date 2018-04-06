using AutoMapper;
using Learn.Core;
using Learn.Core.Extensions;
using Learn.DataModel.Models;
using Learn.lib.Models.ViewModels;
using Learn.Web.Service.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace Learn.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
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
        public ActionResult Create(NoteViewModel createNote)
        {
            var note = Mapper.Map<NoteViewModel, Note>(createNote);
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
            User.Identity.GetUserId();
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
