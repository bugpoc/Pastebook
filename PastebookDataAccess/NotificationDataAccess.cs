using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PastebookDataAccessLibrary
{
    public class NotificationDataAccess
    {
        public List<NOTIFICATION> GetListOfNotifications(int id, bool isGetList)
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    if (isGetList)
                    {
                        listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id).OrderByDescending(n => n.CREATED_DATE).ToList();
                    }
                    else
                    {
                        listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id && n.SEEN == "N").OrderByDescending(n => n.CREATED_DATE).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfNotifications;
        }
    }
}
