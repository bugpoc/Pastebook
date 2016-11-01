using System.Collections.Generic;
using System.Linq;
using PastebookEntityLibrary;

namespace PastebookDataAccessLibrary
{
    public class CountryDataAccess
    {
        public List<REF_COUNTRY> GetCountryList()
        {
            var countryList = new List<REF_COUNTRY>();

            using (var context = new PastebookEntities())
            {
                countryList = context.REF_COUNTRY.ToList();
            }

            return countryList;
        }
    }
}
