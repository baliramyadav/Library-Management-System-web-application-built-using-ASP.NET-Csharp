using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining
{
    public partial class signout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.Remove("role");
            Session.Remove("fullname");
            Session.Remove("username");
            Session.Remove("status");

            Session.Remove("Adminrole"); Session.Remove("Adminusername"); Session.Remove("Adminfullname");
            //Response.Cache.SetExpires(DateTime.Now);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
            
            Response.Redirect("default.aspx");
        }
    }
}