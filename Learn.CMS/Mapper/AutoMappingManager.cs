using AutoMapper;
using Learn.Core.DataModels;
using Learn.Core.ViewModels;

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
