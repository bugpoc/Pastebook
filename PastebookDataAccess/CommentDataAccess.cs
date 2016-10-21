using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogicLibrary
{
    public class CommentDataAccess
    {
        public int SaveComment(string content, int postID, int commenterID)
        {
            int result = 0;

            COMMENT comment = new COMMENT()
            {
                CONTENT = content,
                POSTER_ID = commenterID,
                POST_ID = postID,
                DATE_CREATED = DateTime.Now
            };
            try
            {
                using (var context = new PastebookEntities())
                {
                    context.COMMENTs.Add(comment);
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
