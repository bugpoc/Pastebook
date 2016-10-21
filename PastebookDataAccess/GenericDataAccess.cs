using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public class GenericDataAccess<T> where T : class
    {
        public int Create(T items)
        {
            int result = 0;

            try
            {
                using (var context = new PastebookEntities())
                {
                    context.Entry(items).State = EntityState.Added;
                    result = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int Update(T items)
        {
            int result = 0;

            try
            {
                using (var context = new PastebookEntities())
                {
                    context.Entry(items).State = EntityState.Modified;
                    result = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int Delete(T items)
        {
            int result = 0;

            try
            {
                using (var context = new PastebookEntities())
                {
                    context.Entry(items).State = EntityState.Deleted;
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
