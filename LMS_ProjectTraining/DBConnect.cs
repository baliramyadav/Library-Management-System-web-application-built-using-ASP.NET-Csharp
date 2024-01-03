using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;

namespace LMS_ProjectTraining
{
    public class DBConnect
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        public SqlConnection GetCon()
        {
            return con;
        }

        public void OpenCon()
        {
            if(con.State==ConnectionState.Closed)
                con.Open();
        }
        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
        public DataTable Load_Data(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                dt.Dispose();
                da.Dispose();
                CloseCon();
            }
        }
        public Boolean InsertUpdateData(SqlCommand cmd)
        {
            bool recordSaved;            
            try
            {
                OpenCon();
                cmd.ExecuteNonQuery();
                recordSaved = true;
            }
            catch
            {
                //throw;
                recordSaved = false;
            }
            finally
            {
                CloseCon();
            }
            return recordSaved;

        }
    }
}