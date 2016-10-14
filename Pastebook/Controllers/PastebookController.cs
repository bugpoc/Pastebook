using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        // GET: Pastebook
        public ActionResult Index(string username)
        {
            return View();
        }
    }
}