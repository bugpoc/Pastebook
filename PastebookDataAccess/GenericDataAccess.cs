using PastebookEntityLibrary;
using System.Data.Entity;

namespace PastebookDataAccessLibrary
{
    public class GenericDataAccess<T> where T : class
    {
        public int Create(T items)
        {
            int result = 0;

            using (var context = new PastebookEntities())
            {
                context.Entry(items).State = EntityState.Added;
                result = context.SaveChanges();
            }

            return result;
        }

        public int Update(T items)
        {
            int result = 0;

            using (var context = new PastebookEntities())
            {
                context.Entry(items).State = EntityState.Modified;
                result = context.SaveChanges();
            }

            return result;
        }

        public int Delete(T items)
        {
            int result = 0;

            using (var context = new PastebookEntities())
            {
                context.Entry(items).State = EntityState.Deleted;
                result = context.SaveChanges();
            }

            return result;
        }
    }
}
