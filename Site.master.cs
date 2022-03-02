using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        string surl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

        if (!Page.IsPostBack)
        {
            Session["UserID"] = 1;
            if (Session["UserID"] == null)
            {
                Config.Rd("/login");
            }

            if (!controlLink(surl) && surl != "home")
            {
                Config.Rd("/home");
            }
           // Response.Write("'"+surl+"'");
        }

    }
    protected bool controlLink(string urls)
    {
        List<string> s = new List<string>();
        DataTable dt = _db.GetSiteMap();
        foreach (DataRow row in dt.Rows)
        {
            string aa = row["MenuASAX"].ToString();
            s.Add(aa);
        }

        bool b = true;
        foreach (string urls1 in s)
        {
            if (urls == urls1)
            {
                b = false;
                break;
            }
        }

        return b;
    }
}
