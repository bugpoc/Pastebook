using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccessLibrary
{
    public class LikeDataAccess
    {
        public LIKE GetLike(LIKE like)
        {
            LIKE specificLike = new LIKE();

            try
            {
                using (var context = new PastebookEntities())
                {
                    specificLike = context.LIKEs.FirstOrDefault(l => l.LIKED_BY == like.LIKED_BY && l.POST_ID == like.POST_ID);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return specificLike;
        }
    }
}
