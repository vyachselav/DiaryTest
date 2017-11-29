using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.BLL.Services;
using Diary.Web.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;
using Diary.BLL.DTO;
using Diary.Web.Models.Note;

namespace Diary.Web.Controllers
{
    public class PicturesController : Controller
    {
        private IMapper mapper = AutoMapperConfigWeb.mvcConfig.CreateMapper();
        private NoteService NoteService => HttpContext.GetOwinContext().GetUserManager<NoteService>();

        public ActionResult Index(int id)
        {
            PictureDTO picDto = NoteService.GetPicture(id);
            PictureModel pic = mapper.Map<PictureDTO, PictureModel>(picDto);
            return View(pic);
        }
    }
}