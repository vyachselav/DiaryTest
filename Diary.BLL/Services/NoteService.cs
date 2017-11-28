using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Identity;
using Diary.DAL.Entities;
using Diary.BLL.DTO;
using Diary.BLL.Infrastructure;
using Diary.DAL.Repositories;
using AutoMapper;

namespace Diary.BLL.Services
{
    public class NoteService : IDisposable
    {
        private IdentityUnitOfWork Database { get; set; }
        IMapper mapper = AutoMapperConfigBLL.dtoConfig.CreateMapper();
        public NoteService()
        {
            Database = IdentityUnitOfWork.Create();
        }

        public static NoteService Create()
        {
            return new NoteService();
        }

        #region Account
        public async Task<IdentityResult> Create(UserDTO userDto)
        {
            User user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = mapper.Map<UserDTO, User>(userDto);

                IdentityResult result = await Database.UserManager.CreateAsync(user, userDto.Password);
                await Database.SaveAsync();
                return result;
            }
            else
            {
                return new IdentityResult();
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            User user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        #endregion

        #region Notes
        public NoteDTO GetNote(int id, string userId)
        {
            Note note = Database.UserManager.FindById(userId).Notes.FirstOrDefault(n => n.Id == id);
            NoteDTO noteDto;
            if (note != null)
            {
                bool deletable = CheckDeletability(note.CreationTime);

                noteDto = mapper.Map<Note, NoteDTO>(note);
                noteDto.Deletable = deletable;
            }
            else
            {
                noteDto =  new NoteDTO
                {
                    Id = -1
                };
            }

            return noteDto;
        }

        public IEnumerable<NoteDTO> GetAllByUser(string userId)
        {
            IEnumerable<Note> notes = Database.Notes.GetAllByUser(userId);

            return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);

        }

        public IEnumerable<NoteDTO> GetNotesInRange(string userId, string from, string to, int page, int pageSize, out int totalItems)
        {
            IEnumerable<Note> notes;
            DateTime fromDate, toDate;

            fromDate = from == "" ? DateTime.MinValue
                : Convert.ToDateTime(from.Replace('-', '/') + " 0:00:00");

            toDate = to == "" ? DateTime.MaxValue
                : Convert.ToDateTime(to.Replace('-', '/') + " 23:59:59");

            notes = (from == "" && to == "") ? Database.Notes.GetPageInDateRange(userId, pageSize, page, out totalItems) :
                Database.Notes.GetPageInDateRange(userId, fromDate, toDate, pageSize, page, out totalItems);

            return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
        }

        public void CreateNote(NoteDTO newNoteDto)
        {
            Note newNote = mapper.Map<NoteDTO, Note>(newNoteDto);

            Database.Notes.Create(newNote);

            Database.Save();
        }

        public void DeleteNote(int noteId)
        {
            if (Database.Pictures.CheckIsExist(noteId))
            {
                Database.Pictures.Delete(noteId);
            }

            Database.Notes.Delete(noteId);

            Database.Save();
        }
        #endregion

        public void Dispose()
        {
            Database.Dispose();
        }
        
        public bool CheckDeletability(DateTime creationTime)
        {
            int daysAgo = (DateTime.Now - creationTime).Days;
            return daysAgo < 1;
        }
    }
}
