using Learn.Lib;
using Learn.DataModel.Models;
using System;
using System.Collections.Generic;
using Learn.ViewModel;

namespace Learn.Web.Service.Services
{
    public interface INoteService
    {
        IEnumerable<NoteViewModel> GetNotes();

        IEnumerable<ValidationResult> CanAddNote(Note newNote);

        void CreateNote(Note note);

        void EditNote(Note note);

        void DeleteNote(Guid id);

        Note GetNote(Guid id);

        void SaveNote();
    }
}
