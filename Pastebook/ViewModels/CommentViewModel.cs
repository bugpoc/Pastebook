using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int PosterID { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}