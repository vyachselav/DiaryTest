using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Diary.DAL.Entities;
using Diary.DAL.EF;

namespace Diary.DAL.Repositories
{
    public class NoteRepository 
    {
        private ApplicationContext db;

        public NoteRepository(ApplicationContext context)
        {
            db = context;
        }
       
        public IEnumerable<Note> GetAllByUser(string userId)
        {
            return db.Notes.Where(n=>n.UserId == userId)
                .Include(n => n.Picture)
                .OrderByDescending(n => n.CreationTime);
        }

        public Note Get(int id)
        {
            return db.Notes
                //.Include(n => n.Picture)
                .FirstOrDefault(n => n.Id == id);
        }

        public void Create(Note note)
        {
            db.Notes.Add(note);
        }

        public void Update(Note note)
        {
            db.Entry(note).State = EntityState.Modified;
        }

        public IEnumerable<Note> Find(Func<Note, Boolean> predicate)
        {
            return db.Notes
               // .Include(u => u.Picture)
                .Where(predicate).ToList();
        }
        
        public IEnumerable<Note> GetPageInDateRange(string userId, int pageSize, int page, out int totalItems)
        {
            IEnumerable<Note> notes = db.Notes.Where(u => u.UserId == userId)
               // .Include(u => u.Picture)
                .OrderByDescending(n => n.CreationTime).ToList();

            totalItems = notes.Count();
            notes = notes.Skip((page - 1) * pageSize).Take(pageSize);
            return notes;
        }

        public IEnumerable<Note> GetPageInDateRange(string userId, DateTime fromDate, DateTime toDate, int pageSize, int page, out int totalItems)
        {
            IEnumerable<Note> notes = db.Notes.Where(u=>u.UserId==userId)
                .Where(n => n.CreationTime >= fromDate && n.CreationTime <= toDate)
                //.Include(u => u.Picture)
                .OrderByDescending(n => n.CreationTime).ToList();

            totalItems = notes.Count();
            notes = notes.Skip((page - 1) * pageSize).Take(pageSize);
            return notes;
        }
        
        public void Delete(int id)
        {
            Note note = db.Notes.Find(id);
            if (note != null)
                db.Notes.Remove(note);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
