using PastebookEntityLibrary;
using System;
using System.Web;
using System.Web.Mvc;
using PastebookDataAccessLibrary;

namespace Pastebook.Controllers
{

    public class PastebookController : Controller
    {
        private FriendDataAccess friendDataAccess = new FriendDataAccess();
        private UserDataAccess userDataAccess = new UserDataAccess();
        private PostDataAccess postDataAccess = new PostDataAccess();
        private LikeDataAccess likeDataAccess = new LikeDataAccess();

        public ActionResult Index(string username)
        {
            return View();
        }

        public ActionResult UserProfile(string username)
        {
            return View(userDataAccess.GetUser(null, username));
        }

        public ActionResult CredentialPartial(string username)
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

            return PartialView("CredentialPartialView", user);
        }

        public ActionResult Friends()
        {
            return View(friendDataAccess.GetListOfFriends((int)Session["user_id"]));
        }

        public ActionResult TimelinePartial(string username)
        {
            return PartialView("TimelinePartialView", postDataAccess.GetUserTimeline(username));
        }

        public ActionResult NewsFeed()
        {
            return View();
        }

        public ActionResult NewsFeedPartial()
        {
            var listOfUserFriends = friendDataAccess.GetListOfFriends((int)Session["user_id"]);

            return PartialView("NewsFeedPartialView", postDataAccess.GetUserNewsFeed(listOfUserFriends, (int)Session["user_id"]));
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

        public ActionResult UploadPhoto(string username, HttpPostedFileBase file)
        {
            USER user = new USER();
            user = userDataAccess.GetUser(null, username);
            if (file.ContentLength > 0)
            {
                user.PROFILE_PIC = new byte[file.ContentLength];
                file.InputStream.Read(user.PROFILE_PIC, 0, file.ContentLength);
                GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();
                dataAccessUser.Update(user);
            }
            return RedirectToAction("UserProfile", "Pastebook", new { username = username });
        }

        public JsonResult SavePost(string content, string username)
        {
            var user = new USER();
            GenericDataAccess<POST> dataAccessPost = new GenericDataAccess<POST>();

            if (username != null)
            {
                user = userDataAccess.GetUser(null, username);
            }
            else
            {
                user.ID = (int)Session["user_id"];
            }

            int result = dataAccessPost.Create(new POST() { CONTENT = content, POSTER_ID = (int)Session["user_id"], PROFILE_OWNER_ID = user.ID, CREATED_DATE = DateTime.Now });

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAboutMe(string aboutMe, string username)
        {
            var user = new USER();
            GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();

            user = userDataAccess.GetUser(null, username);
            user.ABOUT_ME = aboutMe;

            int result = dataAccessUser.Update(user);

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
            GenericDataAccess<COMMENT> dataAccessComment = new GenericDataAccess<COMMENT>();

            int result = dataAccessComment.Create(new COMMENT()
            {
                CONTENT = content,
                DATE_CREATED = DateTime.Now,
                POST_ID = postID,
                POSTER_ID = (int)Session["user_id"]
            });
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFriend(string username)
        {
            USER user = new USER();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();

            user = userDataAccess.GetUser(null, username);
            int result = dataAccessFriend.Create(new FRIEND()
            {
                USER_ID = (int)Session["user_id"],
                FRIEND_ID = user.ID,
                REQUEST = "Y",
                CREATED_DATE = DateTime.Now,
                BLOCKED = "N"
            });
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
    }
}