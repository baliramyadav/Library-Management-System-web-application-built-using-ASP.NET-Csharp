using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace LMS_ProjectTraining
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError(); Server.ClearError();
            if(ex.InnerException != null)
            {
                Response.Redirect("~/ErrorPage.aspx?ErrorMessage=" + ex.InnerException.Message);
            }
            else
            {
                Response.Redirect("~/ErrorPage.aspx?ErrorMessage=" + ex.Message);
            }
        }
    }
}