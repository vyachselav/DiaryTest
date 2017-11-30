using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Diary.Web.Util;
using Diary.BLL.Services;
using Diary.BLL.DTO;
using AutoMapper;
using Diary.Web.Models.Api;

namespace Diary.Web.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private string userId => User.Identity.GetUserId();
        private IMapper mapper = AutoMapperConfigWeb.mvcConfig.CreateMapper();

        public NoteService NoteService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<NoteService>();
            }
        }
        public IHttpActionResult Get()
        {
            IEnumerable<NoteDTO> notesDto = NoteService.GetAllByUser(userId);
            IEnumerable<NoteApiModel> notesModel = mapper.Map<IEnumerable<NoteDTO>, IEnumerable<NoteApiModel>>(notesDto);

            return Json(notesModel);
        }

        public IHttpActionResult Get(int id)
        {
            NoteDTO noteDto = NoteService.GetNote(id, userId);
            noteDto.Picture = NoteService.GetPicture(id);

            NoteApiModel noteModel = mapper.Map<NoteDTO, NoteApiModel>(noteDto);

            //if (noteModel.Picture != null)
            //{
            //    noteModel.Picture.PathToPicture = Url.Route("Default", new { controller = "Pictures", action = "Index", id = noteModel.Id });
            //}

            return Json(noteModel);
        }
    }
}
