using System.Collections.Generic;
using System.Linq;
using Learn.Core.Infrastructure.Interface;
using Learn.Core.DataModels;
using System;
using AutoMapper;
using Learn.Core.Manager;
using Microsoft.AspNet.Identity;
using System.Web;
using Learn.Core.ViewModels;
using Learn.Core.Repositories;

namespace Learn.Core.Services
{
    #region Interface
    public interface INoteService
    {
        IEnumerable<NoteViewModel> GetNotes();

        IEnumerable<ValidationResult> CanAddNote(NoteViewModel model);

        void CreateNote(NoteViewModel note);

        void EditNote(NoteViewModel note);

        void DeleteNote(Guid id);

        NoteViewModel GetNote(Guid id);

        void SaveNote();
    }
    #endregion

    public class NoteService : ServiceBase, INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationUserManager UserManager;

        public NoteService(INoteRepository noteRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManger)
        {
            this.noteRepository = noteRepository;
            this.unitOfWork = unitOfWork;
            this.UserManager = userManger;
        }

        public IEnumerable<NoteViewModel> GetNotes()
        {
            var notes = noteRepository.GetAll().OrderByDescending(g => g.CreateDate);
            var notesViewModel = Mapper.Map<IEnumerable<Note>, IEnumerable<NoteViewModel>>(notes);
            return notesViewModel;
        }

        public IEnumerable<ValidationResult> CanAddNote(NoteViewModel model)
        {
            var note = noteRepository.Get(n => n.Message == model.Message);
            if (note != null)
            {
                yield return new ValidationResult("Message", "Resources.NoteExists");
            }
        }

        public void CreateNote(NoteViewModel model)
        {
            var currentUser = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            var note = Mapper.Map<NoteViewModel, Note>(model);
            note.CreatedBy = currentUser;
            note.UpdatedBy = currentUser;
            noteRepository.Add(note);
            SaveNote();
        }

        public void EditNote(NoteViewModel model)
        {
            var currentUser = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            var note = Mapper.Map<NoteViewModel, Note>(model);
            note.UpdatedBy = currentUser;
            noteRepository.Update(note);
            SaveNote();
        }

        public void SaveNote()
        {
            unitOfWork.Commit();
        }

        public NoteViewModel GetNote(Guid id)
        {
            var note = noteRepository.GetById(id);
            return Mapper.Map<Note, NoteViewModel>(note);
        }

        public void DeleteNote(Guid id)
        {
            var note = noteRepository.GetById(id);
            noteRepository.Delete(note);
            SaveNote();
        }
    }

}
