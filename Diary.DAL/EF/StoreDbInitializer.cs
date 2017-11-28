using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Diary.DAL.Entities;
using Diary.DAL.Identity;
using Diary.DAL.Util;

namespace Diary.DAL.EF
{
    public class StoreDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(db));

            User john = new User
            {
                Email = "john@mail.com",
                UserName = "john@mail.com",
                Name = "John",
                JoinDate = DateTime.Now
            };

            User bill = new User
            {
                Email = "bill@mail.com",
                UserName = "bill@mail.com",
                Name = "Bill",
                JoinDate = DateTime.Now
            };

            #region Notes init
            Note john1 = new Note
            {
                Title = "Note 1",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-20),
                User = john
            };
            Note john2 = new Note
            {
                Title = "Note 2",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-19),
                User = john
            };
            Note john3 = new Note
            {
                Title = "Note 3",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-18),
                User = john
            };
            Note john4 = new Note
            {
                Title = "Note 4",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-17),
                User = john
            };
            Note john5 = new Note
            {
                Title = "Note 5",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-16),
                User = john
            };
            Note john6 = new Note
            {
                Title = "Note 6",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-15),
                User = john
            };
            Note john7 = new Note
            {
                Title = "Note 7",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-14),
                User = john
            };
            Note john8 = new Note
            {
                Title = "Note 8",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-13),
                User = john
            };
            Note john9 = new Note
            {
                Title = "Note 9",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-12),
                User = john
            };
            Note john10 = new Note
            {
                Title = "Note 10",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-11),
                User = john
            };
            Note john11 = new Note
            {
                Title = "Note 11",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now.AddDays(-10),
                User = john
            };
            Note john12 = new Note
            {
                Title = "Note 12",
                Text = Lorem.GetLorem(450),
                CreationTime = DateTime.Now,
                User = john
            };
            Note bill1 = new Note
            {
                Title = "Note 1 Bill",
                Text = Lorem.GetLorem(300),
                CreationTime = DateTime.Now.AddDays(-1),
                User = bill
            };
            #endregion

            #region Pics init
            string startupPath = AppDomain.CurrentDomain.RelativeSearchPath;

            Picture pic12 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "1.jpg")),
                
                Name = "1",
                MIME = "image/jpeg",
                Note = john12
            };
            Picture pic11 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "2.jpg")),
                
                Name = "2",
                MIME = "image/jpeg",
                Note = john11
            };
            Picture pic10 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "3.jpg")),
                
                Name = "3",
                MIME = "image/jpeg",
                Note = john10
            };
            Picture pic9 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "4.jpg")),
                
                Name = "4",
                MIME = "image/jpeg",
                Note = john9
            }; Picture pic8 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "5.jpg")),
                Name = "5",
                MIME = "image/jpeg",
                Note = john8
            };
            Picture pic7 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "6.jpg")),
                Name = "6",
                MIME = "image/jpeg",
                Note = john7
            };
            Picture pic6 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "7.jpg")),
                Name = "7",
                MIME = "image/jpeg",
                Note = john6
            };
            Picture pic5 = new Picture
            {
                Image = File.ReadAllBytes(Path.Combine(startupPath, "Util", "Images", "8.png")),
                Name = "8",
                MIME = "image/png",
                Note = bill1
            };
            #endregion

            string pass = "pas123";
            userManager.Create(john, pass);
            userManager.Create(bill, pass);

            db.Notes.AddRange(new List<Note>
            {
                john1,john2,john3,john4,john5,john6,
                john7,john8,john9,john10,john11,john12,
                bill1
            });

            db.Pictures.AddRange(new List<Picture>
            {
                pic12,pic11,pic10,pic9,
                pic8,pic7,pic6,
                pic5
            });

            db.SaveChanges();
        }
    }
}
