using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook
{
    public class CustomAuthetication : AuthorizeAttribute
    {
        //https://vivekcek.wordpress.com/2013/06/29/custom-form-authentication-in-mvc-4-with-custom-authorize-attribute-and-session-variable/

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Now check the session:
            var myvar = httpContext.Session["user_id"];
            if (myvar == null)
            {
                // the session has expired
                return false;
            }

            return true;
        }
    }
}