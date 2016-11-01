using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccessLibrary
{
    public class FriendDataAccess
    {
        public List<USER> GetListOfFriends(int id)
        {
            var listOfFriendsInformation = new List<USER>();
            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfFriendsInformation = context.FRIENDs.Include("USER1").Include("USER").Where(f => f.USER_ID == id && f.REQUEST == "N").Select(f => f.USER).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfFriendsInformation;
        }

        public List<int> GetListOfFriendsID(int id)
        {
            List<int> listOfFriendsID = new List<int>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfFriendsID = context.FRIENDs.Where(f => f.USER_ID == id && f.REQUEST == "N").Select(f => f.FRIEND_ID).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfFriendsID;
        }

        public List<FRIEND> GetPendingFriends(int id)
        {
            var listOfFriendsInformation = new List<FRIEND>();
            try
            {
                using (var context = new PastebookEntities())
                {
                     listOfFriendsInformation = context.FRIENDs.Include("USER1").Where(f => f.FRIEND_ID == id && f.REQUEST == "Y").ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfFriendsInformation;
        }

        public FRIEND GetPendingAndApprovedFriends(int userID, int friendID)
        {
            var friend = new FRIEND();
            try
            {
                using (var context = new PastebookEntities())
                {
                    friend = context.FRIENDs.Include("USER1").Include("USER").FirstOrDefault(f => (f.USER_ID == userID && f.FRIEND_ID == friendID) || (f.USER_ID == friendID && f.FRIEND_ID == userID));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return friend;
        }

        public FRIEND GetFriendRelationship(int id)
        {
            FRIEND getSpecificFriend = new FRIEND();

            try
            {
                using (var context = new PastebookEntities())
                {
                    getSpecificFriend = context.FRIENDs.FirstOrDefault(f => f.ID == id);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return getSpecificFriend;
        }
    }
}
