using System.Collections.Generic;
using System.Linq;
using Learn.Lib.Infrastructure.Interface;
using Learn.Data.Infrastructure.Repositories.Interface;
using Learn.Core;
using System;
using Learn.Web.Interface.Services;
using Learn.DataModel;

namespace Learn.Web.Services
{
    public class NoteService : ServiceBase, INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly IUnitOfWork unitOfWork;

        public NoteService(INoteRepository noteRepository, IUnitOfWork unitOfWork)
        {
            this.noteRepository = noteRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Note> GetNotes()
        {
            var notes = noteRepository.GetAll().OrderByDescending(g => g.CreateDate);
            return notes;
        }

        public IEnumerable<ValidationResult> CanAddNote(Note newNote)
        {
            var note = noteRepository.Get(n => n.Message == newNote.Message);
            if (note != null)
            {
                yield return new ValidationResult("Message", "Resources.NoteExists");
            }
        }

        public void CreateNote(Note note)
        {
            noteRepository.Add(note);
            SaveNote();
        }

        public void EditNote(Note noteToEdit)
        {
            noteRepository.Update(noteToEdit);
            SaveNote();
        }

        public void SaveNote()
        {
            unitOfWork.Commit();
        }

        public Note GetNote(Guid id)
        {
            return noteRepository.GetById(id);
        }

        public void DeleteNote(Guid id)
        {
            var note = noteRepository.GetById(id);
            noteRepository.Delete(note);
            SaveNote();
        }
    }

}
