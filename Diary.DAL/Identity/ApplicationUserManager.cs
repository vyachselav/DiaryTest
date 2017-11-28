using Diary.DAL.Entities;
using Microsoft.AspNet.Identity;
namespace Diary.DAL.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store) { }
        
    }
}
