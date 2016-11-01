using PastebookEntityLibrary;
using System;
using System.Web;
using System.Web.Mvc;
using PastebookDataAccessLibrary;
using System.Collections.Generic;
using System.Linq;
using PastebookBusinessLogicLibrary;
using Pastebook.ViewModels;

namespace Pastebook.Controllers
{
    [CustomAuthetication]
    public class PastebookController : Controller
    {
        private FriendDataAccess friendDataAccess = new FriendDataAccess();
        private UserDataAccess userDataAccess = new UserDataAccess();
        private PostDataAccess postDataAccess = new PostDataAccess();
        private NotificationDataAccess notificationDataAccess = new NotificationDataAccess();
        private CountryDataAccess countryDataAccess = new CountryDataAccess();
        private PastebookManager pastebookManager = new PastebookManager();
        private PasswordManager passwordManager = new PasswordManager();

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{username}")]
        public ActionResult UserProfile(string username)
        {
            return View(userDataAccess.GetUser(null, username));
        }

        [HttpGet]
        public ActionResult CredentialPartial(string username)
        {
            var user = new USER();

            user = pastebookManager.GetDetailedUserInfomration(username);

            return PartialView("CredentialPartialView", user);
        }

        [HttpGet]
        [Route("Friends")]
        public ActionResult Friends()
        {
            return View(friendDataAccess.GetListOfFriends((int)Session["user_id"]));
        }

        [HttpGet]
        public ActionResult TimelinePartial(string username)
        {
            return PartialView("TimelinePartialView", postDataAccess.GetUserTimeline(username));
        }

        [HttpGet]
        public ActionResult NewsFeedPartial()
        {
            var listOfFriendsID = friendDataAccess.GetListOfFriendsID((int)Session["user_id"]);
            listOfFriendsID.Add((int)Session["user_id"]);

            return PartialView("NewsFeedPartialView", postDataAccess.GetUserNewsFeed(listOfFriendsID));
        }

        [HttpGet]
        public ActionResult AddFriendPartial(string username)
        {
            USER user = new USER();
            FRIEND friend = new FRIEND();

            user = userDataAccess.GetUser(null, username);

            if (user.ID == (int)Session["user_id"])
            {
                friend.USER1 = user;
                return PartialView("AddFriendPartialView", friend);
            }

            return PartialView("AddFriendPartialView", friendDataAccess.GetPendingAndApprovedFriends((int)Session["user_id"], user.ID));

        }

        [HttpGet]
        [Route("FriendRequest")]
        public ActionResult FriendRequest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FriendRequestPartial()
        {
            return PartialView("FriendRequestPartialView", friendDataAccess.GetPendingFriends((int)Session["user_id"]));
        }

        [HttpPost]
        public ActionResult UploadPhoto(string username, HttpPostedFileBase file)
        {
            USER user = new USER();
            user = userDataAccess.GetUser(null, username);

            if (file.ContentLength > 0 && file.ContentLength < 2097152)
            {
                if(file.ContentType == "image/jpg" || file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                {
                    user.PROFILE_PIC = new byte[file.ContentLength];
                    file.InputStream.Read(user.PROFILE_PIC, 0, file.ContentLength);
                    GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();
                    dataAccessUser.Update(user);
                }
            }

            return RedirectToAction("UserProfile", "Pastebook", new { username = username });
        }

        [HttpGet]
        public ActionResult NotificationPartial()
        {
            return PartialView("NotificationPartialView", notificationDataAccess.GetTopSixListOfNotifications((int)Session["user_id"]));
        }

        [HttpGet]
        [Route("SearchUser/{name}")]
        public ActionResult SearchUser(string name)
        {
            return View(userDataAccess.GetListOfUsers(name, (int)Session["user_id"]));
        }

        [HttpGet]
        [Route("EditInformation")]
        public ActionResult EditInformation()
        {
            IEnumerable<SelectListItem> countryListItems;

            countryListItems = countryDataAccess.GetCountryList().Select(i => new SelectListItem()
            {
                Value = i.ID.ToString(),
                Text = i.COUNTRY
            });

            ViewBag.CountryList = countryListItems;

            return View(new SettingsViewModel
            {
                USER = userDataAccess.GetUser(null, Session["username"].ToString())
            });
        }

        [HttpGet]
        [Route("EditEmail")]
        public ActionResult EditEmail()
        {
            SettingsViewModel settings = new SettingsViewModel();
            settings.USER = userDataAccess.GetUser(null, Session["username"].ToString());
            settings.USER.PASSWORD = null;

            return View(settings);
        }

        [HttpGet]
        [Route("EditPassword")]
        public ActionResult EditPassword()
        {
            SettingsViewModel settings = new SettingsViewModel();
            settings.USER = userDataAccess.GetUser(null, Session["username"].ToString());
            settings.USER.PASSWORD = null;

            return View(settings);
        }

        [HttpPost]
        public ActionResult UpdateUser(SettingsViewModel model, string Action)
        {
            USER user = new USER();
            GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();

            if (Action == "EditInformation")
            {
                user = userDataAccess.GetUser(model.USER.EMAIL_ADDRESS, null);

                if (userDataAccess.CheckUsername(model.USER.USER_NAME) && user.USER_NAME != model.USER.USER_NAME)
                {
                    ModelState.AddModelError("USER.USER_NAME", "Username already exists.");

                }
                else if (ModelState.IsValidField("USER.MOBILE_NO"))
                {
                    model.USER.PASSWORD = user.PASSWORD;
                    model.USER.SALT = user.SALT;
                    model.USER.PROFILE_PIC = user.PROFILE_PIC;

                    Session["username"] = model.USER.USER_NAME;

                    dataAccessUser.Update(model.USER);

                    return RedirectToAction("Index");
                }
                IEnumerable<SelectListItem> countryListItems;

                countryListItems = countryDataAccess.GetCountryList().Select(i => new SelectListItem()
                {
                    Value = i.ID.ToString(),
                    Text = i.COUNTRY
                });

                ViewBag.CountryList = countryListItems;
            }
            else if (Action == "EditEmail")
            {
                user = userDataAccess.GetUser(null, model.USER.USER_NAME);

                if (userDataAccess.CheckEmail(model.USER.EMAIL_ADDRESS) && user.EMAIL_ADDRESS != model.USER.EMAIL_ADDRESS)
                {
                    ModelState.AddModelError("USER.EMAIL_ADDRESS", "Email Address already exists.");
                }

                if (!passwordManager.IsPasswordMatch(model.USER.PASSWORD, user.SALT, user.PASSWORD))
                {
                    ModelState.AddModelError("USER.PASSWORD", "Password is incorrect.");
                }

                if (ModelState.IsValidField("USER.EMAIL_ADDRESS") && ModelState.IsValidField("USER.PASSWORD"))
                {
                    user.EMAIL_ADDRESS = model.USER.EMAIL_ADDRESS;
                    dataAccessUser.Update(user);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                user = userDataAccess.GetUser(model.USER.EMAIL_ADDRESS, null);

                if (passwordManager.IsPasswordMatch(model.PASSWORD.NewPassword, user.SALT, user.PASSWORD))
                {
                    ModelState.AddModelError("PASSWORD.NewPassword", "Password and New Password is still the same.");
                }

                if (!passwordManager.IsPasswordMatch(model.USER.PASSWORD, user.SALT, user.PASSWORD))
                {
                    ModelState.AddModelError("USER.PASSWORD", "Password is incorrect.");
                }

                if (ModelState.IsValidField("PASSWORD.NewPassword") && ModelState.IsValidField("USER.PASSWORD"))
                {
                    string salt = null;

                    user.PASSWORD = passwordManager.GeneratePasswordHash(model.PASSWORD.NewPassword, out salt);
                    user.SALT = salt;

                    dataAccessUser.Update(user);

                    return RedirectToAction("Index");
                }
            }

            return View(Action, model);
        }

        [HttpGet]
        [Route("Posts/{postID:int:min(1)}")]
        public ActionResult Posts(int postID)
        {
            return View(postDataAccess.GetPost(postID));
        }

        [HttpGet]
        [Route("ViewAllNotifications")]
        public ActionResult ViewAllNotifications()
        {
            return View(notificationDataAccess.GetListOfNotifications((int)Session["user_id"]));
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
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

            int result = pastebookManager.SavePost((int)Session["user_id"], user.ID, content);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAboutMe(string aboutMe, string username)
        {
            int result = pastebookManager.UpdateAboutMe(username, aboutMe);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LikePost(int postID, string status)
        {
            int result = pastebookManager.LikePost(postID, (int)Session["user_id"], status);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommentToPost(string content, int postID)
        {
            int result = pastebookManager.CommentToPost(postID, (int)Session["user_id"], content);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFriend(string username)
        {
            int result = pastebookManager.AddFriend(username, (int)Session["user_id"]);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptRejectRequest(int relationshipID, string status)
        {
            int result = pastebookManager.AcceptRejectRequest(relationshipID, status);

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountOfNotification()
        {
            int count = notificationDataAccess.GetCountOfNotSeenNotifications((int)Session["user_id"]);

            return Json(new { Count = count }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProfileInformation(string username)
        {
            USER user = new USER();
            string source = null;
            string name = null;

            user = userDataAccess.GetUser(null, username);

            if (user.PROFILE_PIC != null)
            {
                var base64 = Convert.ToBase64String(user.PROFILE_PIC);
                source = String.Format("data:image/gif;base64,{0}", base64);
            }
            else
            {
                source = Url.Content("~/Content/Images/default.jpg");
            }
            name = user.FIRST_NAME + ' ' + user.LAST_NAME;

            return Json(new { Source = source, Name = name }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateNotifications()
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();
            GenericDataAccess<NOTIFICATION> dataAcessNotification = new GenericDataAccess<NOTIFICATION>();
            int result = 0;

            listOfNotifications = notificationDataAccess.GetListOfNotSeenNotifications((int)Session["user_id"]);

            foreach (var item in listOfNotifications)
            {
                item.SEEN = "Y";
                result = dataAcessNotification.Update(item);
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}