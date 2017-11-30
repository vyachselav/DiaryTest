using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using Diary.BLL.Services;
using Diary.BLL.DTO;
using Diary.Web.Models.Account;
using Diary.Web.Util;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace Diary.Web.Controllers
{
    public class AccountController : Controller
    {
        private IMapper mapper = AutoMapperConfigWeb.mvcConfig.CreateMapper();
        private string userId => User.Identity.GetUserId();
        private NoteService NoteService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<NoteService>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            if (!userId.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Notes");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password
                };
                ClaimsIdentity claim = await NoteService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Notes");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            if (!userId.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Notes");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = mapper.Map<RegisterModel, UserDTO>(model);
                userDto.JoinDate = DateTime.Now;
                userDto.UserName = model.Email;

                IdentityResult result = await NoteService.Create(userDto);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.EmailTaken = "Email address already in use.";
                    return View();
                }
            }
            return View(model);
        }
    }
}