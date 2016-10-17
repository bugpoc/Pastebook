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
        MapperManager mapperManager = new MapperManager();

        public ActionResult Index(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        public ActionResult Friends()
        {
            return View(mapperManager.ListOfUSERsToListOfFriendsViewModel(friendDataAccess.GetListOfFriends((int)Session["user_id"])));
        }
    }
}