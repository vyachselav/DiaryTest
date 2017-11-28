using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Diary.DAL.Entities;
using Diary.DAL.EF;

namespace Diary.DAL.Repositories
{
    public class PictureRepository 
    {
        private ApplicationContext db;

        public PictureRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public void Create(Picture picture)
        {
            db.Pictures.Add(picture);
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
        }

        public IEnumerable<Picture> Find(Func<Picture, Boolean> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public bool CheckIsExist(int id)
        {
            return db.Pictures.Find(id) != null;
        }
        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
