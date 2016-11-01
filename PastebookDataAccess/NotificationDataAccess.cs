using PastebookEntityLibrary;
using System.Collections.Generic;
using System.Linq;

namespace PastebookDataAccessLibrary
{
    public class NotificationDataAccess
    {
        public List<NOTIFICATION> GetListOfNotifications(int id)
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();
            using (var context = new PastebookEntities())
            {
                listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id).OrderByDescending(n => n.CREATED_DATE).ToList();
            }

            return listOfNotifications;
        }

        public List<NOTIFICATION> GetTopSixListOfNotifications(int id)
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();

            using (var context = new PastebookEntities())
            {
                listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id).OrderByDescending(n => n.CREATED_DATE).Take(6).ToList();
            }

            return listOfNotifications;
        }

        public int GetCountOfNotSeenNotifications(int id)
        {
            int count = 0;

            using (var context = new PastebookEntities())
            {
                count = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id && n.SEEN == "N").ToList().Count;
            }

            return count;
        }

        public List<NOTIFICATION> GetListOfNotSeenNotifications(int id)
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();

            using (var context = new PastebookEntities())
            {
                listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id && n.SEEN == "N").ToList();
            }

            return listOfNotifications;
        }

        public NOTIFICATION GetNotification(LIKE like)
        {
            NOTIFICATION notification = new NOTIFICATION();

            using (var context = new PastebookEntities())
            {
                notification = context.NOTIFICATIONs.FirstOrDefault(n => n.POST_ID == like.POST_ID && n.SENDER_ID == like.LIKED_BY);
            }

            return notification;
        }
    }
}
