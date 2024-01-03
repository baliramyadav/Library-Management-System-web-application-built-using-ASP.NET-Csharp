using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining.Admin
{
    public partial class AdminHome : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Adminrole"] != null && Session["Adminrole"].ToString() == "Admin")
            {
                if (Session["Adminusername"].ToString() == "" || Session["Adminusername"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("~/signout.aspx");
                }
                else
                {
                    if (!this.IsPostBack)
                    {
                        GetIssueBook();
                        GetTotalBook();
                        Gettotalfine();
                    }
                }
            }
            else
            {
                Response.Redirect("~/signout.aspx");
            }
                
        }
        private void Gettotalfine()
        {
            cmd = new SqlCommand("select sum(fineamount)as tot from BookFineRecord", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();            
            DataTable dt2 = new DataTable();
            dt2 = dbcon.Load_Data(cmd);
            if (dt2.Rows.Count >= 1)
            {
                lblamount.Text = " " + dt2.Rows[0]["tot"].ToString();
            }
            else
            {
                lblamount.Text = "0";
            }
        }

        private void GetTotalBook()
        {
            cmd = new SqlCommand("select count(*)as Totalbook from book_master_tbl", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            DataTable dt2 = new DataTable();
            dt2 = dbcon.Load_Data(cmd);
            if (dt2.Rows.Count >= 1)
            {
                lblTotalbooks.Text = dt2.Rows[0]["Totalbook"].ToString();
            }
            else
            {
                lblTotalbooks.Text = "0";
            }
        }

        private void GetIssueBook()
        {
            cmd = new SqlCommand("select count(*)as Issubook from book_issue_tbl", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@mid", Session["mid"].ToString());
            DataTable dt2 = new DataTable();
            dt2 = dbcon.Load_Data(cmd);
            if (dt2.Rows.Count >= 1)
            {
                lblIssuebook.Text = dt2.Rows[0]["Issubook"].ToString();
            }
            else
            {
                lblIssuebook.Text = "0";
            }
        }
    }
}