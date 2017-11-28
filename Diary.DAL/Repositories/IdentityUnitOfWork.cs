using System;
using System.Threading.Tasks;
using Diary.DAL.Identity;
using Diary.DAL.EF;
using Diary.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Diary.DAL.Repositories
{
    public class IdentityUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private NoteRepository noteRepository;
        private PictureRepository pictureRepository;

        public IdentityUnitOfWork()
        {
            db = ApplicationContext.Create();
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            noteRepository = new NoteRepository(db);
            pictureRepository = new PictureRepository(db);
        }

        public static IdentityUnitOfWork Create()
        {
            return new IdentityUnitOfWork();
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public NoteRepository Notes
        {
            get
            {
                if (noteRepository == null)
                {
                    noteRepository = new NoteRepository(db);
                }
                return noteRepository;
            }
        }

        public PictureRepository Pictures
        {
            get
            {
                if (pictureRepository == null)
                {
                    pictureRepository = new PictureRepository(db);
                }
                return pictureRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
