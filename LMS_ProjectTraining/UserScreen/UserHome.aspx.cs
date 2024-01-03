using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining.UserScreen
{
    public partial class UserHome : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("Login.aspx");
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

        private void Gettotalfine()
        {
            cmd = new SqlCommand("select sum(fineamount)as tot from BookFineRecord where member_id=@mid", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@mid", Session["mid"].ToString());
            DataTable dt2 = new DataTable();
            dt2 = dbcon.Load_Data(cmd);
            if (dt2.Rows.Count >= 1)
            {
                lblamount.Text =" "+ dt2.Rows[0]["tot"].ToString();
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
            cmd = new SqlCommand("select count(*)as Issubook from book_issue_tbl where member_id=@mid", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@mid", Session["mid"].ToString());
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