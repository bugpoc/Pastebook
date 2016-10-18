using Pastebook.Mapper;
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
            if (username == "Friends")
            {
                return RedirectToAction("Friends");
            }

            var user = new USER();
            user = userDataAccess.GetUser(null, username);
            if (user != null)
            {
                return View(mapperManager.USERToProfileViewModel(user));
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Friends()
        {
            return View(mapperManager.ListOfUSERsToListOfFriendViewModel(friendDataAccess.GetListOfFriends((int)Session["user_id"])));
        }

        public ActionResult Timeline()
        {
            return View(mapperManager.ListOfPOSTsToListOfPostViewModel(postDataAccess.GetUserTimeline((int)Session["user_id"])));
        }

        public ActionResult NewsFeed()
        {
            var listOfUsers = new List<USER>();
            listOfUsers = friendDataAccess.GetListOfFriends((int)Session["user_id"]);
            return View(mapperManager.ListOfPOSTsToListOfPostViewModel(postDataAccess.GetUserNewsFeed(listOfUsers, (int)Session["user_id"])));
        }
    }
}