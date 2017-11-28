using Diary.Web.Models.Note;
using Diary.BLL.DTO;
using Diary.Web.Models.Account;
using Diary.Web.Models.Api;
using AutoMapper;

namespace Diary.Web.Util
{
    public class AutoMapperConfigWeb
    {
        public static MapperConfiguration mvcConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RegisterModel, UserDTO>();
            cfg.CreateMap<LoginModel, UserDTO>();

            cfg.CreateMap<NoteDTO, NotePreviewModel>()
                .ForMember(dest => dest.Text,
                    opts => opts.MapFrom(src => src.Text.Length < 100
                    ? src.Text : src.Text.Substring(0, 100) + "..."));
            
            cfg.CreateMap<NoteCreateModel, NoteDTO>();
            cfg.CreateMap<PictureDTO, PictureModel>();
            cfg.CreateMap<NoteDTO, NoteModel>().ReverseMap();

            cfg.CreateMap<NoteDTO, NoteApiModel>();
            cfg.CreateMap<PictureDTO, PictureApiModel>();

        });
    }
}