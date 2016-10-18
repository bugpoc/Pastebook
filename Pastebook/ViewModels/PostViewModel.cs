using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Comments = new List<CommentViewModel>();
            Likes = new List<LikeViewModel>();
        }

        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int ProfileOwnerID { get; set; }
        public int PosterID { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<LikeViewModel> Likes { get; set; }
    }
}