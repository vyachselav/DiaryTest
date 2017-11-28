using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Diary.DAL.Entities;


namespace Diary.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        static ApplicationContext()
        {
            //Database.SetInitializer<ApplicationContext>(new StoreDbInitializer());
        }

        public ApplicationContext() : base("DefaultConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}
