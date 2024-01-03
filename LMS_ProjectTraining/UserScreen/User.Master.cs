using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining.UserScreen
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (Session["role"]!=null && Session["role"].ToString()=="user")
            {
                
                if (!IsPostBack)
                {
                    lblUserName.Text ="Hi," + Session["fullname"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/signout.aspx");
            }
            
        }
    }
}