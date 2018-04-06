using Learn.Core;
using Learn.DataModel.Models;
using System;
using System.Collections.Generic;

namespace Learn.Web.Service.Services
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotes();

        IEnumerable<ValidationResult> CanAddNote(Note newNote);

        void CreateNote(Note note);

        void EditNote(Note note);

        void DeleteNote(Guid id);

        Note GetNote(Guid id);

        void SaveNote();
    }
}
