using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntityLibrary;
using System.Data.Entity;

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

            try
            {
                using (var context = new PastebookEntities())
                {
                    selectedUser = context.USERs.Include("REF_COUNTRY").FirstOrDefault(u => u.EMAIL_ADDRESS == email || u.USER_NAME == username);
                }
            }
            catch (Exception)
            {

                throw;
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
            try
            {
                using (var context = new PastebookEntities())
                {
                    result = context.USERs.Any(u => u.USER_NAME == username);
                }
            }
            catch (Exception)
            {

                throw;
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
            try
            {
                using (var context = new PastebookEntities())
                {
                    result = context.USERs.Any(u => u.EMAIL_ADDRESS == email);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<USER> GetListOfUsers(string name, int id)
        {
            List<USER> listOfUsers = new List<USER>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfUsers = context.USERs.Where(u => (u.FIRST_NAME.Contains(name) || u.LAST_NAME.Contains(name)) && (u.ID!= id)).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfUsers;
        }
    }
}
