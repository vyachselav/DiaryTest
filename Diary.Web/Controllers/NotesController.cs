using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Diary.Web.Models.Note;
using Diary.Web.Util;
using Diary.BLL.Services;
using Diary.BLL.DTO;
using AutoMapper;

namespace Diary.Web.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private string userId => User.Identity.GetUserId();
        private IMapper mapper = AutoMapperConfigWeb.mvcConfig.CreateMapper();
        private NoteService NoteService => HttpContext.GetOwinContext().GetUserManager<NoteService>();

        [HttpGet]
        public ActionResult Index(string from = "", string to = "", int page = 1)
        {
            int pageSize = 6;
            int totalItems = 0;
            IEnumerable<NoteDTO> notesDto = NoteService.GetNotesInRange(userId, from, to, page, pageSize, out totalItems);
            
            IEnumerable<NotePreviewModel> notesPerPage = mapper.Map<IEnumerable<NoteDTO>,
                IEnumerable<NotePreviewModel>>(notesDto);
            
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            PageModel currentPage = new PageModel
            {
                PageInfo = pageInfo,
                Notes = notesPerPage,
                From = from,
                To = to
            };
            return View(currentPage);
        }

        public ActionResult Note(int noteId)
        {
            NoteDTO noteDto = NoteService.GetNote(noteId, userId);

            if (noteDto.Id == -1)
            {
                return HttpNotFound();
            }

            NoteModel noteModel = mapper.Map<NoteDTO, NoteModel>(noteDto);
            return View(noteModel);
        }

        public ActionResult DeleteNote(int? noteId)
        {
            if (noteId != null)
            {
                NoteService.DeleteNote(noteId.Value);
            }
            return RedirectToAction("Index", "Notes");
        }

        [HttpGet]
        public ActionResult NewNote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewNote(NoteCreateModel newNoteView, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                NoteDTO newNote = mapper.Map<NoteCreateModel, NoteDTO>(newNoteView);
                newNote.UserId = userId;
                newNote.CreationTime = DateTime.Now;

                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }

                    PictureDTO newPicture = new PictureDTO
                    {
                        Name = uploadImage.FileName,
                        MIME = uploadImage.ContentType,
                        Image = imageData
                    };

                    newNote.Picture = newPicture;
                }

                NoteService.CreateNote(newNote);

                return RedirectToAction("Index", "Notes");
            }
            return View(newNoteView);
        }
        protected override void Dispose(bool disposing)
        {
            NoteService.Dispose();
            base.Dispose(disposing);
        }
    }
}