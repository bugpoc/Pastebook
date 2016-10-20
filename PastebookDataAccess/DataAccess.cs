using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public class DataAccess<T> where T : class
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

        public List<T> Retrieve()
        {
            List<T> list = new List<T>();

            try
            {
                using (var context = new PastebookEntities())
                {
                    list = context.Set<T>().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
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
