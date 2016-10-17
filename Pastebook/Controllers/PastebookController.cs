using Pastebook.Mapper;
using PastebookBusinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(mapperManager.USERToProfileViewModel(userDataAccess.GetProfile(username)));
        }

        public ActionResult Friends()
        {
            return View(mapperManager.ListOfUSERsToListOfFriendsViewModel(friendDataAccess.GetListOfFriends((int)Session["user_id"])));
        }
    }
}