using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogicLibrary
{
    public class LikeDataAccess
    {
        public int SaveLike(int postID, int likerID)
        {
            int result = 0;

            LIKE like = new LIKE()
            {
                POST_ID = postID,
                LIKED_BY = likerID
            };

            try
            {
                using (var context = new PastebookEntities())
                {
                    context.LIKEs.Add(like);
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
