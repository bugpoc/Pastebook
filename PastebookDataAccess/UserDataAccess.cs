using System.Collections.Generic;
using System.Linq;
using PastebookEntityLibrary;

namespace PastebookDataAccessLibrary
{
    public class UserDataAccess
    {
        /// <summary>
        /// This method get the user from the database.
        /// </summary>
        /// <param name="email">Email that is inputted by the user.</param>
        /// <returns>User information.</returns>
        public USER GetUser(string email, string username)
        {
            var selectedUser = new USER();

            using (var context = new PastebookEntities())
            {
                selectedUser = context.USERs.Include("REF_COUNTRY").FirstOrDefault(u => u.EMAIL_ADDRESS == email || u.USER_NAME == username);
            }

            return selectedUser;
        }

        /// <summary>
        /// This method checks the database if the username was already existing.
        /// </summary>
        /// <param name="username">Username that is inputted by the user.</param>
        /// <returns>True if the username exists, false if it is available.</returns>
        public bool CheckUsername(string username)
        {
            bool result = false;

            using (var context = new PastebookEntities())
            {
                result = context.USERs.Any(u => u.USER_NAME == username);
            }

            return result;
        }

        /// <summary>
        /// This method checks the database if the email address was already existing.
        /// </summary>
        /// <param name="email">Email Address that is inputted by the user.</param>
        /// <returns>True if the email address exists, false if it is available.</returns>
        public bool CheckEmail(string email)
        {
            bool result = false;

            using (var context = new PastebookEntities())
            {
                result = context.USERs.Any(u => u.EMAIL_ADDRESS == email);
            }

            return result;
        }

        public List<USER> GetListOfUsers(string name, int id)
        {
            List<USER> listOfUsers = new List<USER>();

            using (var context = new PastebookEntities())
            {
                //http://stackoverflow.com/questions/5676040/search-two-columns-in-linq-to-sql

                listOfUsers = (from user in context.USERs
                               let fullname = user.FIRST_NAME + " " + user.LAST_NAME
                               where fullname == name || user.FIRST_NAME == name || user.LAST_NAME == name
                               select user).ToList();
            }

            return listOfUsers;
        }
    }
}
