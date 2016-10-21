using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntityLibrary;
using System.Data.Entity;

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
                    context.USERs.Add(user);
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

        public int UpdateAboutMe(USER user)
        {
            int result = 0;
            try
            {
                using (var context = new PastebookEntities())
                {
                    context.Entry(user).State = EntityState.Modified;
                    result = context.SaveChanges();
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
