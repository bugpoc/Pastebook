using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.ViewModels;
using PastebookEntityLibrary;

namespace Pastebook.Mapper
{
    public class MapperManager
    {
        public USER RegisterViewModelToUSER(RegisterViewModel model)
        {
            return new USER()
            {
                BIRTHDAY = model.Birthday,
                COUNTRY_ID = model.Country,
                DATE_CREATED = DateTime.Now,
                EMAIL_ADDRESS = model.EmailAddress,
                FIRST_NAME = model.FirstName,
                GENDER = "U",
                LAST_NAME = model.LastName,
                USER_NAME = model.Username,
                PASSWORD = model.Password,
                SALT = model.Salt,
                MOBILE_NO = model.MobileNumber
            };
        }
    }
}