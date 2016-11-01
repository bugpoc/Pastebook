using PastebookDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class NotificationController : Controller
    {
        private NotificationDataAccess notificationDataAccess = new NotificationDataAccess();
        
        public ActionResult NotificationPartial()
        {
            return PartialView("NotificationPartialView", notificationDataAccess.GetTopSixListOfNotifications((int)Session["user_id"]));
        }
    }
}