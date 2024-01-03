using LMS_ProjectTraining.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace LMS_ProjectTraining.UserScreen
{
    public partial class userprofile : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("~/signout.aspx");
            }
            else
            {
                if (!this.IsPostBack)
                {
                    SearchMember();
                    BindGridData();
                }
            }

            
        }
        private void SearchMember()
        {
            cmd = new SqlCommand("sp_getMemberProfileByID", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@member_id", Session["mid"].ToString());
            DataTable dt2 = new DataTable();
            dt2 = dbcon.Load_Data(cmd);
            if (dt2.Rows.Count >= 1)
            {
                txtFullName.Text = dt2.Rows[0]["full_name"].ToString();
                txtDOB.Text = dt2.Rows[0]["dob"].ToString();
                txtContact.Text = dt2.Rows[0]["contact_no"].ToString();
                txtEmail.Text = dt2.Rows[0]["email"].ToString();
                ddlState.SelectedValue = dt2.Rows[0]["state"].ToString();
                txtCity.Text = dt2.Rows[0]["city"].ToString().Trim();
                txtPincode.Text = dt2.Rows[0]["pincode"].ToString();
                txtFullAddress.Text = dt2.Rows[0]["full_address"].ToString();
                txtUserID.Text = dt2.Rows[0]["member_id"].ToString();
                txtOldPassword.Text = dt2.Rows[0]["password"].ToString();
                Session["pwd"] = dt2.Rows[0]["password"].ToString();
                lblStatus.Text = dt2.Rows[0]["account_status"].ToString();
                if (dt2.Rows[0]["account_status"].ToString().Trim() == "active")
                {
                    lblStatus.Attributes.Add("class", "badge badge-pill badge-success");
                }
                else if (dt2.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    lblStatus.Attributes.Add("class", "badge badge-pill badge-warning");
                }
                else if (dt2.Rows[0]["account_status"].ToString().Trim() == "deactive")
                {
                    lblStatus.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    lblStatus.Attributes.Add("class", "badge badge-pill badge-info");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID');</script>");
                //ClearControl();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("Login.aspx");
            }
            else
            {
                if(checkvalidation())
                {
                    UpdateUserProfile();

                }
                else
                {
                    Response.Write("<script>alert('validation error try Again');</script>");
                }
            }
        }

        private void UpdateUserProfile()
        {
            cmd = new SqlCommand("sp_UpdateMember_Profile", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@full_name", txtFullName.Text);
            cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
            cmd.Parameters.AddWithValue("@contact_no", txtContact.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@state", ddlState.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@city", txtCity.Text);
            cmd.Parameters.AddWithValue("@pincode", txtPincode.Text);
            cmd.Parameters.AddWithValue("@full_address", txtFullAddress.Text);
            cmd.Parameters.AddWithValue("@member_id", Session["mid"].ToString());
            if(txtNewPassword.Text!=string.Empty)
            {
                cmd.Parameters.AddWithValue("@password", txtNewPassword.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@password", Session["pwd"].ToString());
            }            
            
            if (dbcon.InsertUpdateData(cmd))
            {                
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Your Profile updated successfully','success')", true);
                Response.Redirect("UserHome.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not inserted ...try again','error')", true);
            }
            dbcon.CloseCon();
        }

        private bool checkvalidation()
        {
             if(txtFullName.Text!=string.Empty && txtEmail.Text!=string.Empty && txtDOB.Text!=string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void BindGridData()
        {
            cmd = new SqlCommand("sp_GetUserIssueBookDetails", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@mid", Session["mid"].ToString());
            GridView1.DataSource = dbcon.Load_Data(cmd);
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}