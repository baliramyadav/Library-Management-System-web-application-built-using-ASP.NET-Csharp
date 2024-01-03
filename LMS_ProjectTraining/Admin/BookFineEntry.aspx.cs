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
    public partial class BookFineEntry : System.Web.UI.Page
    {
        SqlCommand cmd;
        DBConnect dbcon = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (Request.QueryString["mid"] != null && Request.QueryString["mid"] != string.Empty)
                {
                    GetMemName(Request.QueryString["mid"]);
                }
                if (Request.QueryString["day"] != null && Request.QueryString["day"] != string.Empty)
                {
                    Calculatebookfine(Request.QueryString["day"]);
                }
                lblredirectMsg.Visible = false;
            }
        }
        private void GetMemName(string mmid)
        {
            cmd = new SqlCommand("sp_getMember_ByID", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", mmid);
            DataTable dtt = dbcon.Load_Data(cmd);
            if (dtt.Rows.Count >= 1)
            {
                lblMembername.Text = dtt.Rows[0]["full_name"].ToString();
                txtFullName.Text= dtt.Rows[0]["full_name"].ToString();
                txtEmail.Text= dtt.Rows[0]["email"].ToString();
                txtaddress.Text= dtt.Rows[0]["full_address"].ToString();
                txtCity.Text = dtt.Rows[0]["city"].ToString();
                txtstate.Text = dtt.Rows[0]["state"].ToString();
                txtzip.Text = dtt.Rows[0]["pincode"].ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','wrong member ID ...try again','error')", true);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            A1.Visible = false;
            A2.Visible = true;
            btnNext.Visible = false;
        }
        private void Calculatebookfine(string d)
        {
            int days = Convert.ToInt32(d);
            double fine;
            if (days <= 0)
            {
                fine = 0.0;
            }
            else if (days >=1 && days <= 5)
            {
                fine = days  * 0.5F;
            }
            else if (days > 5 && days <= 10)
            {
                fine = 5 * 0.5F + (days - 5) * 1;
            }
            else if (days > 10 && days <= 30)
            {                
                fine = 5 * 0.5F + (days-10) * 1.5F;
            }
            else
            {                
                fine = 5 * 0.5F + 25 * 1.5F + (days-30) * 2;                
            }
            lblfine.Text = "" + fine;
            txtAmount.Text = fine.ToString();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                InsertBookFine();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! Validation issue ...try again','error')", true);
            }
        }

        private void InsertBookFine()
        {
            cmd = new SqlCommand("sp_InsertFineDetials", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", Request.QueryString["bid"]);
            cmd.Parameters.AddWithValue("@member_id", Request.QueryString["mid"]);
            cmd.Parameters.AddWithValue("@member_fullname", lblMembername.Text.Trim());
            cmd.Parameters.AddWithValue("@FineAmount", txtAmount.Text.Trim());           
            
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@full_address", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
            cmd.Parameters.AddWithValue("@state", txtstate.Text.Trim());
            cmd.Parameters.AddWithValue("@pincode", txtzip.Text.Trim());
            cmd.Parameters.AddWithValue("@paymentoption", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@nameoncard", txtNameOnCard.Text.Trim());
            cmd.Parameters.AddWithValue("@cardnumber", txtcardNumber.Text.Trim());
            cmd.Parameters.AddWithValue("@expmonth", txtExpmonth.Text.Trim());
            cmd.Parameters.AddWithValue("@expyear", txtexpyear.Text.Trim());
            cmd.Parameters.AddWithValue("@cvv", txtcvv.Text.Trim());
            if (dbcon.InsertUpdateData(cmd))
            {
                ReturnBook();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Books Fine Paid and Book Return Successfully','success')", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not Updated ...try again','error')", true);
            }
        }
        private void ReturnBook()
        {
            cmd = new SqlCommand("sp_returnBook_Updatestock", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", Request.QueryString["bid"]);
            cmd.Parameters.AddWithValue("@member_id", Request.QueryString["mid"]);
            if (dbcon.InsertUpdateData(cmd))
            {
                A2.Visible = false;
                lblredirectMsg.Visible = true;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Books Return Successfully','success')", true);
                                
                Response.AddHeader("REFRESH", "5;URL=AdminHome.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not Updated ...try again','error')", true);
            }
        }
    }
}