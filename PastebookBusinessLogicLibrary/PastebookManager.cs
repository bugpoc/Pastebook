using System;
using PastebookDataAccessLibrary;
using PastebookEntityLibrary;

namespace PastebookBusinessLogicLibrary
{
    public class PastebookManager
    {
        private UserDataAccess userDataAccess = new UserDataAccess();
        private PostDataAccess postDataAccess = new PostDataAccess();
        private LikeDataAccess likeDataAccess = new LikeDataAccess();
        private NotificationDataAccess notificationDataAccess = new NotificationDataAccess();
        private FriendDataAccess friendDataAccess = new FriendDataAccess();

        public USER GetDetailedUserInfomration(string username)
        {
            var user = new USER();

            user = userDataAccess.GetUser(null, username);

            if (user.GENDER == "M")
            {
                user.GENDER = "Male";
            }
            else if (user.GENDER == "F")
            {
                user.GENDER = "Female";
            }
            else
            {
                user.GENDER = "Unspecified";
            }

            return user;
        }

        public int SavePost(int posterID, int profileOwnerID, string content)
        {
            GenericDataAccess<POST> dataAccessPost = new GenericDataAccess<POST>();
            int result = 0;

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrWhiteSpace(content) && content.Length <= 1000)
            {
                var post = new POST()
                {
                    CONTENT = content.Trim(),
                    POSTER_ID = posterID,
                    PROFILE_OWNER_ID = profileOwnerID,
                    CREATED_DATE = DateTime.Now
                };

                result = dataAccessPost.Create(post);
            }

            return result;
        }

        public int UpdateAboutMe(string username, string aboutMe)
        {
            var user = new USER();
            GenericDataAccess<USER> dataAccessUser = new GenericDataAccess<USER>();
            int result = 0;

            if (!string.IsNullOrEmpty(aboutMe) && !string.IsNullOrWhiteSpace(aboutMe) && aboutMe.Length <= 2000)
            {
                user = userDataAccess.GetUser(null, username);
                user.ABOUT_ME = aboutMe.Trim();

                result = dataAccessUser.Update(user);
            }

            return result;
        }

        public int LikePost(int postID, int userID, string status)
        {

            GenericDataAccess<LIKE> dataAccessLike = new GenericDataAccess<LIKE>();
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();
            int result;
            int posterID = postDataAccess.GetProfileOwnerID(postID);

            var like = new LIKE()
            {
                LIKED_BY = userID,
                POST_ID = postID
            };

            if (status == "like")
            {
                result = dataAccessLike.Create(like);

                if (result == 1 && userID != posterID)
                {
                    NOTIFICATION notification = new NOTIFICATION()
                    {
                        NOTIF_TYPE = "L",
                        POST_ID = postID,
                        RECEIVER_ID = posterID,
                        SENDER_ID = userID,
                        CREATED_DATE = DateTime.Now,
                        SEEN = "N"
                    };

                    dataAccessNotification.Create(notification);
                }
            }
            else
            {
                result = dataAccessLike.Delete(likeDataAccess.GetLike(like));

                if (result == 1 && userID != posterID)
                {
                    dataAccessNotification.Delete(notificationDataAccess.GetNotification(like));
                }
            }

            return result;
        }

        public int CommentToPost(int postID, int userID, string content)
        {
            GenericDataAccess<COMMENT> dataAccessComment = new GenericDataAccess<COMMENT>();
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();
            int posterID = postDataAccess.GetProfileOwnerID(postID);
            int result = 0;

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrWhiteSpace(content) && content.Length <= 1000)
            {
                var comment = new COMMENT()
                {
                    POSTER_ID = userID,
                    POST_ID = postID,
                    DATE_CREATED = DateTime.Now,
                    CONTENT = content.Trim()
                };

                result = dataAccessComment.Create(comment);

                if (result == 1 && userID != posterID)
                {
                    var notification = new NOTIFICATION()
                    {
                        NOTIF_TYPE = "C",
                        POST_ID = postID,
                        COMMENT_ID = comment.ID,
                        RECEIVER_ID = posterID,
                        SENDER_ID = userID,
                        CREATED_DATE = DateTime.Now,
                        SEEN = "N"
                    };

                    dataAccessNotification.Create(notification);
                }
            }

            return result;
        }

        public int AddFriend(string username, int userID)
        {
            var user = new USER();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            GenericDataAccess<NOTIFICATION> dataAccessNotification = new GenericDataAccess<NOTIFICATION>();
            int result;

            user = userDataAccess.GetUser(null, username);

            var friend = new FRIEND()
            {
                USER_ID = userID,
                FRIEND_ID = user.ID,
                REQUEST = "Y",
                CREATED_DATE = DateTime.Now,
                BLOCKED = "N"
            };

            result = dataAccessFriend.Create(friend);

            if (result == 1)
            {
                var notification = new NOTIFICATION()
                {
                    NOTIF_TYPE = "F",
                    RECEIVER_ID = user.ID,
                    SENDER_ID = userID,
                    CREATED_DATE = DateTime.Now,
                    SEEN = "N"
                };

                dataAccessNotification.Create(notification);
            }

            return result;
        }

        public int AcceptRejectRequest(int relationshipID, string status)
        {
            var friend = new FRIEND();
            GenericDataAccess<FRIEND> dataAccessFriend = new GenericDataAccess<FRIEND>();
            int result;

            friend = friendDataAccess.GetFriendRelationship(relationshipID);

            if (status == "Confirm")
            {
                friend.REQUEST = "N";

                var friend1 = new FRIEND()
                {
                    USER_ID = friend.FRIEND_ID,
                    FRIEND_ID = friend.USER_ID,
                    REQUEST = "N",
                    CREATED_DATE = DateTime.Now,
                    BLOCKED = "N"
                };

                result = dataAccessFriend.Update(friend);

                dataAccessFriend.Create(friend1);
            }
            else
            {
                result = dataAccessFriend.Delete(friend);
            }

            return result;
        }

    }
}
