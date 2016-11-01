using PastebookEntityLibrary;
using System.Linq;

namespace PastebookDataAccessLibrary
{
    public class LikeDataAccess
    {
        public LIKE GetLike(LIKE like)
        {
            LIKE specificLike = new LIKE();

            using (var context = new PastebookEntities())
            {
                specificLike = context.LIKEs.FirstOrDefault(l => l.LIKED_BY == like.LIKED_BY && l.POST_ID == like.POST_ID);
            }

            return specificLike;
        }
    }
}
