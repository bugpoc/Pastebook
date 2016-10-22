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
using PastebookDataAccess;

namespace Pastebook.Controllers
{

    public class PastebookController : Controller
    {
        FriendDataAccess friendDataAccess = new FriendDataAccess();
        UserDataAccess userDataAccess = new UserDataAccess();
        PostDataAccess postDataAccess = new PostDataAccess();
        LikeDataAccess likeDataAccess = new LikeDataAccess();
        CommentDataAccess commentDataAccess = new CommentDataAccess();
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
            if (user.GENDER == "M")
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
            return PartialView("Timeline", mapperManager.ListOfPOSTsToListOfPostViewModel(postDataAccess.GetUserTimeline(username)));
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

        public ActionResult AddFriendPartial(string username)
        {
            return PartialView("AddFriendPartialView", friendDataAccess.GetPendingAndApprovedFriends((int)Session["user_id"]));
        }

        public ActionResult FriendRequest()
        {
            return View();
        }

        public ActionResult FriendRequestPartial()
        {
            return PartialView("FriendRequestPartialView", friendDataAccess.GetPendingFriends((int)Session["user_id"]));
        }

        public JsonResult SavePost(string content, string username)
        {
            var user = new USER();

            if (username != null)
            {
                user = userDataAccess.GetUser(null, username);
            }
            else
            {
                user.ID = (int)Session["user_id"];
            }

            int result = postDataAccess.SavePost(new POST() { CONTENT = content, POSTER_ID = (int)Session["user_id"], PROFILE_OWNER_ID = user.ID, CREATED_DATE = DateTime.Now });

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

        public JsonResult LikePost(int postID, string status)
        {
            int result = 0;
            GenericDataAccess<LIKE> dataAccessLike = new GenericDataAccess<LIKE>();
            LIKE like = new LIKE()
            {
                LIKED_BY = (int)Session["user_id"],
                POST_ID = postID
            };

            if (status == "like")
            {
                result = dataAccessLike.Create(like);
            }
            else
            {
                result = dataAccessLike.Delete(likeDataAccess.GetLike(like));
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommentToPost(string content, int postID)
        {
            int result = commentDataAccess.SaveComment(content, postID, (int)Session["user_id"]);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFriend(string username)
        {
            USER user = new USER();
            user = userDataAccess.GetUser(null, username);
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            FRIEND friend = new FRIEND()
            {
                USER_ID = (int)Session["user_id"],
                FRIEND_ID = user.ID,
                REQUEST = "Y",
                CREATED_DATE = DateTime.Now,
                BLOCKED = "N"
            };
            int result = dataAccessFriend.Create(friend);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptRejectRequest(int friendID, string status)
        {
            FRIEND friend = new FRIEND();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            int result = 0;

            friend = friendDataAccess.GetFriendID(friendID, (int)Session["user_id"]);

            if (status == "Confirm")
            {
                friend.REQUEST = "N";
                
                result = dataAccessFriend.Update(friend);
            }
            else
            {
                result = dataAccessFriend.Delete(friend);
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}