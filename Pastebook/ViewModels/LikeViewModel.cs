using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class LikeViewModel
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int LikedBY { get; set; }
        public string FullName { get; set; }
    }
}