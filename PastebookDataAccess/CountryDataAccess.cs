using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntityLibrary;

namespace PastebookDataAccessLibrary
{
    public class CountryDataAccess
    {
        public List<REF_COUNTRY> GetCountryList()
        {
            var countryList = new List<REF_COUNTRY>();
            try
            {
                using (var context = new PastebookEntities())
                {
                    countryList = context.REF_COUNTRY.ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return countryList;
        }
    }
}
