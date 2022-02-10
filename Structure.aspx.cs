using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Structure : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        cmbregistertime.Text = "";
        txtcompanyname.Text = "";
        txtvoen.Text = "";
        txtbankaccount.Text = "";
        txtphone.Text = "";
        txtemail.Text = "";
        txtadress.Text = "";
        txtnotes.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtstructure = _db.GetCompanies();
        if (dtstructure != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtstructure;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetCompanyById(id: id);

        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }
        txtcompanyname.Text = dt.Rows[0]["CompanyName"].ToParseStr();
        txtvoen.Text = dt.Rows[0]["CompanyVoen"].ToParseStr();
        txtbankaccount.Text = dt.Rows[0]["BankAccount"].ToParseStr();
        txtphone.Text = dt.Rows[0]["PhoneNumbers"].ToParseStr();
        txtemail.Text = dt.Rows[0]["Email"].ToParseStr();
        txtadress.Text = dt.Rows[0]["Adress"].ToParseStr();
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteCompany(id: _id);
        _loadGridFromDb();
    }
    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
        componentsload();
        ClearComponents();
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "add":
                btnSave.CommandName = "insert";
                popupEdit.ShowOnPageLoad = true;
                break;
        }
    }
    protected void btntesdiq_Click(object sender, EventArgs e)
    {
        lblPopError.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;
        if (btnSave.CommandName == "insert")
        {

            val = _db.CompaniesInsert(RegisterTime: cmbregistertime.Text.ToParseStr(),
                CompanyName: txtcompanyname.Text.ToParseStr(),
                CompanyVoen: txtvoen.Text.ToParseStr(),
                BankAccount: txtbankaccount.Text.ToParseStr(),
                PhoneNumbers: txtphone.Text.ToParseStr(),
                Email: txtemail.Text.ToParseStr(),
                Adress: txtadress.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.CompanyUpdate(CompanyID: btnSave.CommandArgument.ToParseInt(),
               RegisterTime: cmbregistertime.Text.ToParseStr(),
               CompanyName: txtcompanyname.Text.ToParseStr(),
               CompanyVoen: txtvoen.Text.ToParseStr(),
               BankAccount: txtbankaccount.Text.ToParseStr(),
               PhoneNumbers: txtphone.Text.ToParseStr(),
               Email: txtemail.Text.ToParseStr(),
               Adress: txtadress.Text.ToParseStr(),
               Notes: txtnotes.Text.ToParseStr());
        }

        if (val == Types.ProsesType.Error)
        {
            lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        _loadGridFromDb();
        popupEdit.ShowOnPageLoad = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        popupEdit.ShowOnPageLoad = false;
    }
}