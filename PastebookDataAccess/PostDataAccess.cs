using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PastebookDataAccessLibrary
{
    public class PostDataAccess
    {
        public List<POST> GetUserTimeline(string username)
        {
            var listOfPosts = new List<POST>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    listOfPosts = context.POSTs.Include("LIKEs").Include("COMMENTs").Include("USER").Include("COMMENTs.USER").Include("LIKEs.USER").Where(p => p.USER1.USER_NAME == username).OrderByDescending(d => d.CREATED_DATE).Take(100).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfPosts;
        }

        public List<POST> GetUserNewsFeed(List<USER> listOfUserFriends, int id)
        {
            var listOfPosts = new List<POST>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    var result = context.POSTs.Include("LIKEs").Include("COMMENTs").Include("USER").Include("USER1").Include("COMMENTs.USER").Include("LIKEs.USER").OrderByDescending(d => d.CREATED_DATE).ToList();

                    foreach (var item in result)
                    {
                        if (listOfUserFriends.Any(u => u.ID == item.POSTER_ID || item.PROFILE_OWNER_ID == id))
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

        public int GetProfileOwnerID(int postID)
        {
            POST post = new POST();

            try
            {
                using (var context = new PastebookEntities())
                {
                    post = context.POSTs.FirstOrDefault(p => p.ID == postID);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return post.POSTER_ID;
        }

        public POST GetPost(int postID)
        {
            POST post = new POST();

            try
            {
                using (var context = new PastebookEntities())
                {
                    post = context.POSTs.FirstOrDefault(p => p.ID == postID);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return post;
        }
    }
}
