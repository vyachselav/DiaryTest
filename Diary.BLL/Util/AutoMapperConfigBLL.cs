using Diary.DAL.Entities;
using Diary.BLL.DTO;
using AutoMapper;

namespace Diary.BLL.Infrastructure
{
    public class AutoMapperConfigBLL
    {
        public static MapperConfiguration dtoConfig = new MapperConfiguration(cfg =>
        {
            {
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(x => x.Email, o => o.MapFrom(x => x.Email))
                    .ForMember(x => x.UserName, o => o.MapFrom(x => x.Email))
                    .ForMember(x => x.JoinDate, o => o.MapFrom(x => x.JoinDate))
                    .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
                    .ForAllOtherMembers(x => x.Ignore());

                cfg.CreateMap<Note, NoteDTO>().ReverseMap();
                cfg.CreateMap<PictureDTO, Picture>().ReverseMap();
            }
        });
    }
}
