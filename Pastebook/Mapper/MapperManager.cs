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

        public List<FriendViewModel> ListOfUSERsToListOfFriendsViewModel(List<USER> listOfUser)
        {
            var listOfFriends = new List<FriendViewModel>();
            foreach (var item in listOfUser)
            {
                listOfFriends.Add(new FriendViewModel()
                {
                    ID = item.ID,
                    FullName = item.FIRST_NAME + " " + item.LAST_NAME
                });
            }

            return listOfFriends.OrderBy(l => l.FullName).ToList();
        }

        public ProfileViewModel USERToProfileViewModel(USER user)
        {
            return new ProfileViewModel()
            {
                AboutMe = user.ABOUT_ME,
                Birthday = user.BIRTHDAY,
                Country = user.REF_COUNTRY.COUNTRY,
                DateCreated = user.DATE_CREATED,
                EmailAddress = user.EMAIL_ADDRESS,
                FirstName = user.FIRST_NAME,
                Gender = user.GENDER,
                ID = user.ID,
                LastName = user.LAST_NAME,
                MobileNumber = user.MOBILE_NO,
                Username = user.USER_NAME
            };
        }
    }
}