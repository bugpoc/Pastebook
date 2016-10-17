using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntityLibrary;

namespace PastebookBusinessLogicLibrary
{
    public class UserDataAccess
    {
        /// <summary>
        /// This method save the user information to the database.
        /// </summary>
        /// <param name="user">User Information Model.</param>
        /// <returns>1 if the insert into the database is success, 0 if the insert fails.</returns>
        public int SaveUser(USER user)
        {
            int result = 0;

            try
            {
                using (var context = new PastebookEntities())
                {
                    context.USERs.Add((user));
                    result = context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// This method get the user from the database.
        /// </summary>
        /// <param name="email">Email that is inputted by the user.</param>
        /// <returns>User information.</returns>
        public USER GetUser(string email)
        {
            var selectedUser = new USER();

            try
            {
                using (var context = new PastebookEntities())
                {
                    selectedUser = context.USERs.FirstOrDefault(u => u.EMAIL_ADDRESS == email);
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
    }
}
