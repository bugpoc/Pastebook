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
                        if (item.USER_ID == id)
                        {
                            listOfFriendsInformation.Add(new USER()
                            {
                                ID = item.FRIEND_ID,
                                FIRST_NAME = item.USER.FIRST_NAME,
                                LAST_NAME = item.USER.LAST_NAME
                            });
                        }
                        else
                        {
                            listOfFriendsInformation.Add(new USER()
                            {
                                ID = item.USER_ID,
                                FIRST_NAME = item.USER1.FIRST_NAME,
                                LAST_NAME = item.USER1.LAST_NAME
                            });
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
    }
}
