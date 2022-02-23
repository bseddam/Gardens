using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EntryExit : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtcard.Focus();


        //_loadGridFromDb();
        if (IsPostBack) return;


    }
   
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetEntryExits();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }


   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteEntryExit(id: _id);
        _loadGridFromDb();
    }
  
    protected void Timer1_Tick(object sender, EventArgs e)
    { 
     
        if (String.IsNullOrEmpty(txtcard.Text) == false)
        {

            DataTable dtcard = _db.GetCadreByCardnumber(txtcard.Text);
            if (dtcard == null || dtcard.Rows.Count < 1)
            {
                lblPopError.Text = "Məlumat tapılmadı.";
                txtcard.Text = "";
                lblfullname.Text = "";
                return;
            }

            int cardid = dtcard.Rows[0]["CadreID"].ToParseInt();
            lblfullname.Text = dtcard.Rows[0]["fullname"].ToParseStr();


            Types.ProsesType val = Types.ProsesType.Error;
            val = _db.EntryExitInsert(
                    CadreID: cardid,
                    EntryExitStatus: 1
                    );

           
            if (val == Types.ProsesType.Error)
            {
                lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
                lblfullname.Text = "";
                return;
            }

            lblPopError.Text = "";
            txtcard.Text = "";
            //_loadGridFromDb();

        }
       
    }

    protected void btntesdiqle_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtcard.Text) == false)
        {

            DataTable dtcard = _db.GetCadreByCardnumber(txtcard.Text);
            if (dtcard == null || dtcard.Rows.Count < 1)
            {
                lblPopError.Text = "Məlumat tapılmadı.";
                txtcard.Text = "";
                lblfullname.Text = "";
                return;
            }

            int cardid = dtcard.Rows[0]["CadreID"].ToParseInt();
            lblfullname.Text = dtcard.Rows[0]["fullname"].ToParseStr();


            Types.ProsesType val = Types.ProsesType.Error;
            val = _db.EntryExitInsert(
                    CadreID: cardid,
                    EntryExitStatus: 1
                    );


            if (val == Types.ProsesType.Error)
            {
                lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
                lblfullname.Text = "";
                return;
            }

            lblPopError.Text = "";
            txtcard.Text = "";
            //_loadGridFromDb();

        }
    }
}