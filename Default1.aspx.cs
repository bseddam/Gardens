
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Default1 : System.Web.UI.Page
{
    // ClSsql clSsql = new ClSsql();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }
    protected void btntesdiq_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
        ////exampleInputEmail.Text = Config.Sha1(exampleInputPassword.Text.ToString());
        //try
        //{
        //    ClSsql clSsql = new ClSsql();
        //    SqlCommand cmd = new SqlCommand("Select * from UserAdmin where Login_name=@Login_name and Passvord=@Passvord", clSsql.sqlconn);
        //    cmd.Parameters.AddWithValue("Login_name", exampleInputEmail.Text.ToString());
        //    cmd.Parameters.AddWithValue("Passvord", Config.Sha1(exampleInputPassword.Text.ToString()));
        //    clSsql.sqlconn.Open();
        //    SqlDataAdapter dap = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    dap.Fill(dt);
        //    clSsql.sqlconn.Close();
        //    if (dt.Rows.Count > 0)
        //    {
        //        Session["UsersID1"] = int.Parse(dt.Rows[0]["ID"].ToString());
        //        Session["ElmiMuessiseID1"] = int.Parse(dt.Rows[0]["ElmiMuessiseID"].ToString());
        //        Session["AdminStatus1"] = int.Parse(dt.Rows[0]["AdminStatus"].ToString());
        //        Response.Redirect("Home.aspx");
        //    }
        //}
        //catch (SqlException ex)
        //{

        //    throw new Exception(ex.Message + "(" + ex.Message + ")");
        //}
        //    //catch
        //    //{
        //    //    Response.Redirect("Login.aspx");
        //    //}
    }
}
