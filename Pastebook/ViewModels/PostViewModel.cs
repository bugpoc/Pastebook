using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int ProfileOwnerID { get; set; }
        public int PosterID { get; set; }
    }
}