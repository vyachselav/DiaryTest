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
using Newtonsoft.Json;

namespace Diary.Web.Controllers
{
    public class PicturesController : Controller
    {
        private IMapper mapper = AutoMapperConfigWeb.mvcConfig.CreateMapper();
        private NoteService NoteService => HttpContext.GetOwinContext().GetUserManager<NoteService>();

        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                PictureDTO picDto = NoteService.GetPicture(id.Value);
                if (picDto != null)
                {
                    PictureModel pic = mapper.Map<PictureDTO, PictureModel>(picDto);
                    return View(pic);
                }
                return HttpNotFound("There is not such pic. Sorry.");
            }
            return RedirectToAction("Index", "Notes");

        }

        public string GetPic(int? id)
        {
            if (id.HasValue)
            {
                PictureDTO picDto = NoteService.GetPicture(id.Value);
                if (picDto != null)
                {
                    PictureModel pic = mapper.Map<PictureDTO, PictureModel>(picDto);
                    return JsonConvert.SerializeObject(pic);
                }
            }
            return null;
        }
    }
}