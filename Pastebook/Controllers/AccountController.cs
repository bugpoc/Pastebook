using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PastebookBusinessLogicLibrary;
using PastebookEntityLibrary;
using PastebookDataAccessLibrary;
using System;

namespace Pastebook.Controllers
{
    public class AccountController : Controller
    {
        PasswordManager passwordManager = new PasswordManager();
        UserDataAccess userDataAccess = new UserDataAccess();
        CountryDataAccess countryDataAccess = new CountryDataAccess();

        [HttpGet]
        public ActionResult Index()
        {
            Session["user_id"] = 1;
            Session["username"] = "jayquiambao";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            IEnumerable<SelectListItem> countryListItems;

            countryListItems = countryDataAccess.GetCountryList().Select(i => new SelectListItem()
            {
                Value = i.ID.ToString(),
                Text = i.COUNTRY
            });

            ViewBag.CountryList = countryListItems;
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(USER model, string CONFIRM_PASSWORD)
        {
            GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();

            if (model.PASSWORD != CONFIRM_PASSWORD)
            {
                ModelState.AddModelError("PASSWORD", "Password do not match");
            }
            if (userDataAccess.CheckUsername(model.USER_NAME))
            {
                ModelState.AddModelError("USER_NAME", "Username already exists.");
            }

            if (userDataAccess.CheckEmail(model.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email Address already exists.");
            }

            if (ModelState.IsValid)
            {
                string salt = null;

                model.PASSWORD = passwordManager.GeneratePasswordHash(model.PASSWORD, out salt);
                model.SALT = salt;
                model.DATE_CREATED = DateTime.Now;
                
                dataAccessUser.Create(model);

                Session["user_id"] = model.ID;
                Session["username"] = model.USER_NAME;

                return RedirectToAction("Index","Pastebook");
            }

            IEnumerable<SelectListItem> countryListItems;

            countryListItems = countryDataAccess.GetCountryList().Select(i => new SelectListItem()
            {
                Value = i.ID.ToString(),
                Text = i.COUNTRY
            });

            model.PASSWORD = null;
            ViewBag.CountryList = countryListItems;

            return View("Register", model);
        }

        [HttpPost]
        public ActionResult Login(USER model)
        {
            var user = new USER();

            if (model.EMAIL_ADDRESS!=null && model.PASSWORD!=null)
            {
                user = userDataAccess.GetUser(model.EMAIL_ADDRESS, null);
                if (user != null)
                {
                    if (passwordManager.IsPasswordMatch(model.PASSWORD, user.SALT, user.PASSWORD))
                    {
                        Session["user_id"] = user.ID;
                        Session["username"] = user.USER_NAME;

                        return RedirectToAction("Index", "Pastebook");
                    }
                }
            }

            ModelState.AddModelError("PASSWORD", "Username/Password is incorrect.");

            return View("Index", model);
        }

        public JsonResult CheckUsername(string username)
        {
            bool result = userDataAccess.CheckUsername(username);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmail(string email)
        {
            bool result = userDataAccess.CheckEmail(email);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}