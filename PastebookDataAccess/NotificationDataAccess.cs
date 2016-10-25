using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccessLibrary
{
    public class NotificationDataAccess
    {
        public List<NOTIFICATION> GetListOfNotifications(int id)
        {
            List<NOTIFICATION> listOfNotifications = new List<NOTIFICATION>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfNotifications = context.NOTIFICATIONs.Include("USER").Include("USER1").Where(n => n.RECEIVER_ID == id).OrderByDescending(n => n.CREATED_DATE).ToList();
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
