using AutoMapper;
using Learn.DataModel.Models;
using Learn.ViewModel;

namespace Learn.CMS
{
    public class AutoMappingManager
    {
        public static void Init()
        {
            Mapper.CreateMap<NoteViewModel, Note>();
            Mapper.CreateMap<Note, NoteViewModel>();

            Mapper.CreateMap<ApplicationRole, RoleBindingModel>();
            Mapper.CreateMap<RoleBindingModel, ApplicationRole>();

            Mapper.CreateMap<ApplicationUser, UserViewModel>();
            Mapper.CreateMap<UserViewModel, ApplicationUser>();
        }
    }
}
