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
                GENDER = model.Gender,
                LAST_NAME = model.LastName,
                USER_NAME = model.Username,
                PASSWORD = model.Password,
                SALT = model.Salt,
                MOBILE_NO = model.MobileNumber
            };
        }

        public List<FriendViewModel> ListOfUSERsToListOfFriendViewModel(List<USER> listOfUsers)
        {
            var listOfFriends = new List<FriendViewModel>();
            foreach (var item in listOfUsers)
            {
                listOfFriends.Add(new FriendViewModel()
                {
                    ID = item.ID,
                    FullName = item.FIRST_NAME + " " + item.LAST_NAME,
                    Username = item.USER_NAME
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

        public List<PostViewModel> ListOfPOSTsToListOfPostViewModel(List<POST> listOfPosts)
        {
            var listOfPostsViewModel = new List<PostViewModel>();

            foreach (var item in listOfPosts)
            {
                var listOfComments = new List<CommentViewModel>();
                var listOfLikes = new List<LikeViewModel>();

                foreach (var element in item.COMMENTs)
                {
                    listOfComments.Add(new CommentViewModel()
                    {
                        ID = element.ID,
                        FullName = element.USER.FIRST_NAME + " " + element.USER.LAST_NAME,
                        Content = element.CONTENT,
                        DateCreated = element.DATE_CREATED,
                        PosterID = element.POSTER_ID,
                        PostID = element.POST_ID
                    });
                }

                foreach (var element in item.LIKEs)
                {
                    listOfLikes.Add(new LikeViewModel()
                    {
                        ID = element.ID,
                        FullName = element.USER.FIRST_NAME + " " + element.USER.LAST_NAME,
                        LikedBY = element.LIKED_BY,
                        PostID = element.POST_ID
                    });
                }

                listOfPostsViewModel.Add(new PostViewModel()
                {
                    ID = item.ID,
                    FullName = item.USER.FIRST_NAME + " " + item.USER.LAST_NAME,
                    Content = item.CONTENT,
                    CreatedDate = item.CREATED_DATE,
                    PosterID = item.POSTER_ID,
                    ProfileOwnerID = item.PROFILE_OWNER_ID,
                    Comments = listOfComments,
                    Likes = listOfLikes
                });
            }

            return listOfPostsViewModel;
        }
    }
}