using AutoMapper;
using Learn.DataModel.Models;
using Learn.lib.Models.ViewModels;

namespace Learn.Web
{
    public class AutoMappingManager
    {
        public static void Init()
        {
            Mapper.CreateMap<NoteViewModel, Note>();
            Mapper.CreateMap<Note, NoteViewModel>();
        }
    }
}
