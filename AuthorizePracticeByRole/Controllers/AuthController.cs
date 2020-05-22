using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.ViewModels;
using DAL.Repository.@interface;
using Newtonsoft.Json;
using SharedLibrary.Helpers;

namespace AuthorizePracticeByRole.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // 在進入需要登入的頁面時，asp.net 預設就會將原頁面的 url 放到 query string ReturnUrl 中
            // 在登入後，可以用該資料來回到原頁面
            var loginVM = new LoginForm { ReturnUrl = returnUrl };
            return View(loginVM);
        }

        [HttpPost]
        public ActionResult Login(LoginForm form)
        {
            if (ModelState.IsValid == false)
                return View(form);

            var sha256Password = form.Password.EncryptSha256();
            var user = _userRepository.Validate(form.Account, sha256Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "帳號或密碼錯誤 ~!!");
                return View(form);
            }

            var ticket = new FormsAuthenticationTicket(1,
                                                       form.Account,
                                                       DateTime.Now,
                                                       DateTime.Now.AddMinutes(30),
                                                       true,
                                                       JsonConvert.SerializeObject(user),
                                                       FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            return string.IsNullOrWhiteSpace(form.ReturnUrl)
                       ? RedirectToAction("Index", "Home") as ActionResult
                       : Redirect(form.ReturnUrl);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //清除Session中的資料
            Session.Abandon();

            //登出表單驗證
            FormsAuthentication.SignOut();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Home");
        }
    }
}