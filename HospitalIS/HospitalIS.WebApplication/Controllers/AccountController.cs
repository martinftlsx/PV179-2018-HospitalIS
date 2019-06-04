using HospitalIS.BusinessLayer.Facades;
using HospitalIS.WebApplication.Models.Account;
using HospitalISDBContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HospitalIS.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public DoctorFacade DoctorFacade { get; set; }
        public PatientFacade PatientFacade { get; set; }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            (bool success1, AccessRights roles1) = await PatientFacade.Login(model.Username, model.Password);
            (bool success2, AccessRights roles2) = await DoctorFacade.Login(model.Username, model.Password);
            if (success1)
            {
                return CreateCookie(model, returnUrl, roles1);
            }
            else if (success2)
            {
                return CreateCookie(model, returnUrl, roles2);
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        private ActionResult CreateCookie(LoginModel model, string returnUrl, AccessRights roles)
        {
            //FormsAuthentication.SetAuthCookie(model.Username, false);
            var role = Enum.GetName(roles.GetType(), roles);
            var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now,
                DateTime.Now.AddMinutes(30), false, role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);

            var decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
            {
                decodedUrl = Server.UrlDecode(returnUrl);
            }

            if (Url.IsLocalUrl(decodedUrl))
            {
                return Redirect(decodedUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}