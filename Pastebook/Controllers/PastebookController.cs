using Pastebook.Mapper;
using Pastebook.ViewModels;
using PastebookBusinessLogicLibrary;
using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{

    public class PastebookController : Controller
    {
        FriendDataAccess friendDataAccess = new FriendDataAccess();
        UserDataAccess userDataAccess = new UserDataAccess();
        PostDataAccess postDataAccess = new PostDataAccess();
        MapperManager mapperManager = new MapperManager();

        public ActionResult Index(string username)
        {
            return View();
        }

        public ActionResult UserProfile(string username)
        {
            return View();
        }

        public ActionResult Credential(string username)
        {
            var user = new USER();
            user = userDataAccess.GetUser(null, username);
            if(user.GENDER == "M")
            {
                user.GENDER = "Male";
            }
            else if (user.GENDER == "F")
            {
                user.GENDER = "Female";
            }
            else
            {
                user.GENDER = "Unspecified";
            }
            return PartialView("CredentialPartialView", mapperManager.USERToProfileViewModel(user));
        }

        public ActionResult Friends()
        {
            return View(mapperManager.ListOfUSERsToListOfFriendViewModel(friendDataAccess.GetListOfFriends((int)Session["user_id"])));
        }

        public ActionResult Timeline(string username)
        {
            return PartialView("Timeline",mapperManager.ListOfPOSTsToListOfPostViewModel(postDataAccess.GetUserTimeline(username)));
        }

        public ActionResult NewsFeed()
        {
            return View();
        }

        public ActionResult NewsFeedPartial()
        {
            var listOfUserFriends = friendDataAccess.GetListOfFriends((int)Session["user_id"]);

            return PartialView("NewsFeedPartialView", mapperManager.ListOfPOSTsToListOfPostViewModel(postDataAccess.GetUserNewsFeed(listOfUserFriends, (int)Session["user_id"])));
        }

        public JsonResult SavePost(string content, string username)
        {
            var user = new USER();

            if(username!="")
            { 
            user = userDataAccess.GetUser(null, username);
            }
            else
            {
                user.ID = (int)Session["user_id"];
            }

            int result = postDataAccess.SavePost(new POST() { CONTENT = content, POSTER_ID = (int)Session["user_id"], PROFILE_OWNER_ID = user.ID, CREATED_DATE = DateTime.Now});

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAboutMe(string aboutMe, string username)
        {
            var user = new USER();
            user = userDataAccess.GetUser(null, username);
            user.ABOUT_ME = aboutMe;

            int result = userDataAccess.UpdateAboutMe(user);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}