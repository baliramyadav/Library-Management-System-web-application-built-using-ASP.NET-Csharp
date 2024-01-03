using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining.Admin
{
    public partial class AdminSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Adminrole"]!=null && Session["Adminrole"].ToString()== "Admin")
            {

                if (!IsPostBack)
                {
                    lblUserName.Text = "Hi," + Session["Adminusername"].ToString();

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();
                }
            }
            else
            {
                Response.Redirect("~/signout.aspx");
            }
        }
    }
}