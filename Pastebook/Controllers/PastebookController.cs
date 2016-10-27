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

    public class PastebookController : Controller
    {
        private FriendDataAccess friendDataAccess = new FriendDataAccess();
        private UserDataAccess userDataAccess = new UserDataAccess();
        private PostDataAccess postDataAccess = new PostDataAccess();
        private LikeDataAccess likeDataAccess = new LikeDataAccess();
        private NotificationDataAccess notificationDataAccess = new NotificationDataAccess();
        private CountryDataAccess countryDataAccess = new CountryDataAccess();
        private PasswordManager passwordManager = new PasswordManager();

        [HttpGet]
        [Route("Pastebook.com")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Pastebook.com/{username}")]
        public ActionResult UserProfile(string username)
        {
            return View(userDataAccess.GetUser(null, username));
        }

        [HttpGet]
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

        [HttpGet]
        [Route("Pastebook.com/Friends")]
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
            var listOfUserFriends = friendDataAccess.GetListOfFriends((int)Session["user_id"]);

            return PartialView("NewsFeedPartialView", postDataAccess.GetUserNewsFeed(listOfUserFriends, (int)Session["user_id"]));
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
            else
            {
                return PartialView("AddFriendPartialView", friendDataAccess.GetPendingAndApprovedFriends((int)Session["user_id"], user.ID));
            }
        }

        [HttpGet]
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
            if (file.ContentLength > 0)
            {
                user.PROFILE_PIC = new byte[file.ContentLength];
                file.InputStream.Read(user.PROFILE_PIC, 0, file.ContentLength);
                GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();
                dataAccessUser.Update(user);
            }
            return RedirectToAction("UserProfile", "Pastebook", new { username = username });
        }

        [HttpGet]
        public ActionResult NotificationPartial()
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();
            GenericDataAccess<NOTIFICATION> dataAcessNotification = new GenericDataAccess<NOTIFICATION>();
            int result = 0;

            listOfNotifications = notificationDataAccess.GetListOfNotifications((int)Session["user_id"], true);

            foreach (var item in listOfNotifications)
            {
                item.SEEN = "Y";
                result = dataAcessNotification.Update(item);
            }
            return PartialView("NotificationPartialView", listOfNotifications);
        }

        [HttpGet]
        [Route("Pastebook.com/SearchUser/{name}")]
        public ActionResult SearchUser(string name)
        {
            return View(userDataAccess.GetListOfUsers(name, (int)Session["user_id"]));
        }

        [HttpGet]
        [Route("Pastebook.com/EditInformation")]
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
        [Route("Pastebook.com/EditEmail")]
        public ActionResult EditEmail()
        {
            SettingsViewModel settings = new SettingsViewModel();
            settings.USER = userDataAccess.GetUser(null, Session["username"].ToString());
            settings.USER.PASSWORD = null;

            return View(settings);
        }

        [HttpGet]
        [Route("Pastebook.com/EditPassword")]
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

            if(Action=="EditInformation")
            {
                user = userDataAccess.GetUser(model.USER.EMAIL_ADDRESS, null);

                if (userDataAccess.CheckUsername(model.USER.USER_NAME) && user.USER_NAME != model.USER.USER_NAME)
                {
                    ModelState.AddModelError("USER.USER_NAME", "Username already exists.");

                }
                else if(ModelState.IsValidField("USER.MOBILE_NO"))
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
            else if(Action=="EditEmail")
            {
                user = userDataAccess.GetUser(null, model.USER.USER_NAME);

                if (userDataAccess.CheckEmail(model.USER.EMAIL_ADDRESS) && user.EMAIL_ADDRESS != model.USER.EMAIL_ADDRESS)
                {
                    ModelState.AddModelError("USER.EMAIL_ADDRESS", "Email Address already exists.");
                }

                if(!passwordManager.IsPasswordMatch(model.USER.PASSWORD, user.SALT, user.PASSWORD))
                {
                    ModelState.AddModelError("USER.PASSWORD", "Password is incorrect.");
                }

                if(ModelState.IsValidField("USER.EMAIL_ADDRESS") && ModelState.IsValidField("USER.PASSWORD"))
                {
                    user.EMAIL_ADDRESS = model.USER.EMAIL_ADDRESS;
                    dataAccessUser.Update(user);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                user = userDataAccess.GetUser(model.USER.EMAIL_ADDRESS, null);

                if(passwordManager.IsPasswordMatch(model.PASSWORD.NewPassword, user.SALT, user.PASSWORD))
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
        [Route("Pastebook.com/Posts/{id:int}")]
        public ActionResult Posts(int? id)
        {
            return View();
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
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();
            int posterID = postDataAccess.GetProfileOwnerID(postID);

            LIKE like = new LIKE()
            {
                LIKED_BY = (int)Session["user_id"],
                POST_ID = postID
            };

            if (status == "like")
            {
                result = dataAccessLike.Create(like);
                if (result == 1 && (int)Session["user_id"] != posterID)
                {
                    NOTIFICATION notification = new NOTIFICATION()
                    {
                        NOTIF_TYPE = "L",
                        POST_ID = postID,
                        RECEIVER_ID = posterID,
                        SENDER_ID = (int)Session["user_id"],
                        CREATED_DATE = DateTime.Now,
                        SEEN = "N"
                    };

                    //add catcher
                    dataAccessNotification.Create(notification);
                }
            }
            else
            {
                result = dataAccessLike.Delete(likeDataAccess.GetLike(like));
                if (result == 1 && (int)Session["user_id"] != posterID)
                {
                    //remove notif
                }
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommentToPost(string content, int postID)
        {
            GenericDataAccess<COMMENT> dataAccessComment = new GenericDataAccess<COMMENT>();
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();
            int posterID = postDataAccess.GetProfileOwnerID(postID);

            COMMENT comment = new COMMENT();

            comment.POSTER_ID = (int)Session["user_id"];
            comment.POST_ID = postID;
            comment.DATE_CREATED = DateTime.Now;
            comment.CONTENT = content;

            int result = dataAccessComment.Create(comment);

            if (result == 1)
            {
                NOTIFICATION notification = new NOTIFICATION()
                {
                    NOTIF_TYPE = "C",
                    POST_ID = postID,
                    COMMENT_ID = comment.ID,
                    RECEIVER_ID = posterID,
                    SENDER_ID = (int)Session["user_id"],
                    CREATED_DATE = DateTime.Now,
                    SEEN = "N"
                };

                dataAccessNotification.Create(notification);
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFriend(string username)
        {
            USER user = new USER();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();

            user = userDataAccess.GetUser(null, username);

            int result = dataAccessFriend.Create(new FRIEND()
            {
                USER_ID = (int)Session["user_id"],
                FRIEND_ID = user.ID,
                REQUEST = "Y",
                CREATED_DATE = DateTime.Now,
                BLOCKED = "N"
            });

            if (result == 1)
            {
                NOTIFICATION notification = new NOTIFICATION()
                {
                    NOTIF_TYPE = "F",
                    RECEIVER_ID = user.ID,
                    SENDER_ID = (int)Session["user_id"],
                    CREATED_DATE = DateTime.Now,
                    SEEN = "N"
                };

                dataAccessNotification.Create(notification);
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptRejectRequest(int relationshipID, string status)
        {
            FRIEND friend = new FRIEND();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            int result = 0;

            friend = friendDataAccess.GetFriendRelationship(relationshipID);

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

        public JsonResult GetCountOfNotification()
        {
            int count = notificationDataAccess.GetListOfNotifications((int)Session["user_id"], false).Count;

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
                //source = "/Content/Images/default.jpg";
                source = Url.Content("~/Content/Images/default.jpg");
            }
            name = user.FIRST_NAME + ' ' + user.LAST_NAME;

            return Json(new { Source = source, Name = name }, JsonRequestBehavior.AllowGet);
        }
    }
}