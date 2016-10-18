using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogicLibrary
{
    public class PostDataAccess
    {
        public List<POST> GetUserTimeline(int id)
        {
            var listOfPosts = new List<POST>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfPosts = context.POSTs.Include("LIKEs").Include("COMMENTs").Include("USER").Include("COMMENTs.USER").Where(p => p.PROFILE_OWNER_ID == id).OrderByDescending(d => d.CREATED_DATE).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfPosts;
        }

        public List<POST> GetUserNewsFeed(List<USER> listOfUsers, int id)
        {
            var listOfPosts = new List<POST>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    var result = context.POSTs.Include("LIKEs").Include("COMMENTs").Include("USER").Include("COMMENTs.USER").OrderByDescending(d => d.CREATED_DATE).ToList();

                    foreach (var item in result)
                    {
                        if (listOfUsers.Any(u => (u.ID == item.POSTER_ID && u.ID == item.PROFILE_OWNER_ID)) || item.PROFILE_OWNER_ID == id)
                        {
                            listOfPosts.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfPosts;
        }
    }
}
