using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS_ProjectTraining.Admin
{
    public partial class bookIssueReturn : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGridData();                
            }
        }
        private void BindGridData()
        {
            cmd = new SqlCommand("sp_GetIssueBook", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@StatementType", "Select");
            GridView1.DataSource = dbcon.Load_Data(cmd);
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                GetMemName();
                GetBookName();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Validation error plz enter MemberID or Book ID ...try again','error')", true);
            }
        }

        private void GetBookName()
        {
            cmd = new SqlCommand("sp_Insert_Up_Del_BookInventory", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", txtBookID.Text.Trim());
            cmd.Parameters.AddWithValue("@StatementType", "SelectByID");            

            DataTable dtt = dbcon.Load_Data(cmd);
            if (dtt.Rows.Count >= 1)
            {
                txtBookName.Text = dtt.Rows[0]["book_name"].ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','wrong Book ID ...try again','error')", true);
            }
        }

        private void GetMemName()
        {
            cmd = new SqlCommand("sp_getMember_ByID", dbcon.GetCon());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID",txtMemID.Text.Trim());
            DataTable dtt=dbcon.Load_Data(cmd);
            if(dtt.Rows.Count>=1)
            {
                txtMemName.Text = dtt.Rows[0]["full_name"].ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','wrong member ID ...try again','error')", true);
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if(IsBookExist() && IsMemberExist())
            {
                if (IsIssueEntryExist())
                {
                    Response.Write("<script>alert('This Member already has this book');</script>");
                }
                else
                {
                    issueBook();
                    BindGridData();
                }
            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or Member ID');</script>");
            }
        }

        private void issueBook()
        {
            cmd = new SqlCommand("sp_InsertBookIssue", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", txtBookID.Text.Trim());
            cmd.Parameters.AddWithValue("@member_id", txtMemID.Text.Trim());
            cmd.Parameters.AddWithValue("@member_name", txtMemName.Text.Trim());
            cmd.Parameters.AddWithValue("@book_name", txtBookName.Text.Trim());
            cmd.Parameters.AddWithValue("@issue_date", txtIssueDate.Text.Trim());
            cmd.Parameters.AddWithValue("@due_date", txtDueDate.Text.Trim());
            if (dbcon.InsertUpdateData(cmd))
            {
                updateBookStock();                
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not Updated ...try again','error')", true);
            }
        }

        private void updateBookStock()
        {
            cmd = new SqlCommand("sp_UpdateIssueBookStock", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", txtBookID.Text.Trim());            
            if (dbcon.InsertUpdateData(cmd))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Books Issue Successfully','success')", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! current stock not Updated ...try again','error')", true);
            }
        }

        private bool IsIssueEntryExist()
        {
            cmd = new SqlCommand("sp_checkIssueexistOrnot", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@bid", txtBookID.Text.Trim());
            cmd.Parameters.AddWithValue("@mid", txtMemID.Text.Trim());
            DataTable dtt = dbcon.Load_Data(cmd);
            if (dtt.Rows.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsBookExist()
        {
            cmd = new SqlCommand("sp_CheckBookStockExist", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id",txtBookID.Text.Trim());
            DataTable dtt = dbcon.Load_Data(cmd);
            if(dtt.Rows.Count>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsMemberExist()
        {
            cmd = new SqlCommand("sp_getMember_ByID", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", txtMemID.Text.Trim());
            DataTable dtt = dbcon.Load_Data(cmd);
            if (dtt.Rows.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (IsBookExist() && IsMemberExist())
            {
                if (IsIssueEntryExist())
                {
                    
                    if(CheckFine())
                    {
                        ReturnBook();
                        BindGridData();
                    }
                    else
                    {
                        //open fine page where user can paid fine
                        Response.Redirect("BookFineEntry.aspx?bid="+txtBookID.Text+ "&mid="+txtMemID.Text+ "&day="+ Session["day"].ToString());

                    }
                    
                }
                else
                {
                    Response.Write("<script>alert('This Entry does not exist');</script>");                    
                }
            }
            else
            {
                Response.Write("<script>alert('Wrong Book ID or Member ID');</script>");
            }
        }

        private void ReturnBook()
        {
            cmd = new SqlCommand("sp_returnBook_Updatestock", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id", txtBookID.Text.Trim());
            cmd.Parameters.AddWithValue("@member_id", txtMemID.Text.Trim());
            if (dbcon.InsertUpdateData(cmd))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Books Return Successfully','success')", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not Updated ...try again','error')", true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType == DataControlRowType.DataRow)
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
        private bool CheckFine()
        {
            int days;
            cmd = new SqlCommand("sp_GetNumOfDay", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@book_id",txtBookID.Text.Trim());
            cmd.Parameters.AddWithValue("@member_id",txtMemID.Text.Trim());
            DataTable dtt = dbcon.Load_Data(cmd);
            if(dtt.Rows.Count>=1)
            {
                days =Convert.ToInt32( dtt.Rows[0]["number_of_day"].ToString());
                Session["day"] = days;
                if(days<=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}