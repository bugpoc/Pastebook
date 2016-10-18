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
        MapperManager mapperManager = new MapperManager();
        
        public ActionResult Index()
        {

            //userDataAccess.GetUser(null, username);
            return View();
        }

        public ActionResult UserProfile(string username)
        {
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
            return View(mapperManager.ListOfUSERsToListOfFriendsViewModel(friendDataAccess.GetListOfFriends((int)Session["user_id"])));
        }
    }
}