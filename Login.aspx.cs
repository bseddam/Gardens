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
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void btntesdiq_Click(object sender, EventArgs e)
    {
        if (txtusername.Text.Length < 3)
        {
            Config.MsgBox("İstifadəçi adını daxil edin!", Page);
          
            return;
        }
        if (txtpassword.Text.Length < 3)
        {
            Config.MsgBox("Şifrəni daxil edin!", Page);
            return;
        }
        DataTable dtuser = _db.User(txtusername.Text,
        //Config.Sha1(PassText.Text.ToString()));
        txtpassword.Text);

        string _id = "";
        if (dtuser != null)
        {
            if (dtuser.Rows.Count > 0)
            {
                _id = dtuser.Rows[0]["UserID"].ToParseStr();
            }
        }


        if (_id.Length < 1)
        {
            Config.MsgBox("İstifadəçi adı və ya şifrə yanlışdır!", Page);
          
            //Config.MsgBox("İstifadəçi adı və ya şifrə yanlışdır!", Page);
            return;
        }
        Session["UserID"] = _id;
        Session["UserStatusID"] = dtuser.Rows[0]["UserStatusID"].ToParseStr();

        Config.Rd("/home");


    }
}
