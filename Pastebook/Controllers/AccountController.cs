﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pastebook.Mapper;
using Pastebook.ViewModels;
using PastebookBusinessLogicLibrary;
using PastebookEntityLibrary;

namespace Pastebook.Controllers
{
    public class AccountController : Controller
    {
        PasswordManager passwordManager = new PasswordManager();
        UserDataAccess userDataAccess = new UserDataAccess();
        CountryDataAccess countryDataAccess = new CountryDataAccess();
        MapperManager mapperManager = new MapperManager();
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (userDataAccess.CheckUsername(model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
            }

            if (userDataAccess.CheckEmail(model.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address already exists.");
            }

            if (ModelState.IsValid)
            {
                string salt = null;

                model.Password = passwordManager.GeneratePasswordHash(model.Password, out salt);
                model.Salt = salt;

                userDataAccess.SaveUser(mapperManager.RegisterViewModelToUSER(model));

                return RedirectToAction("Register");
            }

            IEnumerable<SelectListItem> countryListItems;

            countryListItems = countryDataAccess.GetCountryList().Select(i => new SelectListItem()
            {
                Value = i.ID.ToString(),
                Text = i.COUNTRY
            });

            ViewBag.CountryList = countryListItems;

            return View("Register", model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = new USER();

            if (ModelState.IsValid)
            {
                user = userDataAccess.GetUser(model.EmailAddress);
                if (user != null)
                {
                    if (passwordManager.IsPasswordMatch(model.Password, user.SALT, user.PASSWORD))
                    {
                        Session["user_id"] = user.ID;

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Index", model);
        }

        public JsonResult CheckUsername(string username)
        {
            bool result = userDataAccess.CheckUsername(username);

            return Json(new {Result = result}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmail(string email)
        {
            bool result = userDataAccess.CheckEmail(email);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}