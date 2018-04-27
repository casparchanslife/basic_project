using AutoMapper;
using Learn.DataModel.Models;
using Learn.ViewModel;

namespace Learn.Web
{
    public class AutoMappingManager
    {
        public static void Init()
        {
            Mapper.CreateMap<NoteViewModel, Note>();
            Mapper.CreateMap<Note, NoteViewModel>();

            Mapper.CreateMap<ApplicationRole, RoleBindingModel>();

            Mapper.CreateMap<ApplicationUser, UserViewModel>();

        }
    }
}
