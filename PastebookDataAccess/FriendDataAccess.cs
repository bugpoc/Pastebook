using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogicLibrary
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
                    var listOfFriends = context.FRIENDs.Include("USER1").Include("USER").Where(f => f.USER_ID == id || f.FRIEND_ID == id).ToList();
                    foreach (var item in listOfFriends)
                    {
                        if (item.REQUEST == "N")
                        {
                            if (item.USER_ID == id)
                            {
                                listOfFriendsInformation.Add(item.USER);
                            }
                            else
                            {
                                listOfFriendsInformation.Add(item.USER1);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfFriendsInformation;
        }

        public List<USER> GetPendingFriends(int id)
        {
            var listOfFriendsInformation = new List<USER>();
            try
            {
                using (var context = new PastebookEntities())
                {
                    var listOfFriends = context.FRIENDs.Include("USER1").Include("USER").Where(f => f.FRIEND_ID == id && f.REQUEST == "Y").ToList();
                    foreach (var item in listOfFriends)
                    {
                        listOfFriendsInformation.Add(new USER()
                        {
                            ID = item.USER_ID,
                            FIRST_NAME = item.USER1.FIRST_NAME,
                            LAST_NAME = item.USER1.LAST_NAME,
                            USER_NAME = item.USER1.USER_NAME
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfFriendsInformation;
        }

        public List<FRIEND> GetPendingAndApprovedFriends(int id)
        {
            var listOfFriends = new List<FRIEND>();
            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfFriends = context.FRIENDs.Include("USER1").Include("USER").Where(f => f.USER_ID == id || f.FRIEND_ID == id).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfFriends;
        }

        public FRIEND GetFriendID(int friendID, int userID)
        {
            FRIEND getSpecificFriend = new FRIEND();
            try
            {
                using (var context = new PastebookEntities())
                {
                    getSpecificFriend = context.FRIENDs.FirstOrDefault(f => (f.FRIEND_ID == friendID && f.USER_ID == userID) || (f.USER_ID == friendID && f.FRIEND_ID == userID));
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
