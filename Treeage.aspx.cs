using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Treeage : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtFirstAge.Text = "";
        txtLastAge.Text = "";
        txtcoefficient.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetTariffTreeAge();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }


    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetTariffTreeAgeByID(id: id);
        txtFirstAge.Text = dt.Rows[0]["FirstAge"].ToParseStr();
        txtLastAge.Text = dt.Rows[0]["LastAge"].ToParseStr();
        txtcoefficient.Text = dt.Rows[0]["Coefficient"].ToParseStr();
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteTariffTreeAge(id: _id);
        _loadGridFromDb();
    }
    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
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
            val = _db.TariffTreeAgeInsert(
                FirstAge: txtFirstAge.Text.ToParseInt(),
                LastAge: txtLastAge.Text.ToParseInt(),
                Coefficient: txtcoefficient.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.TariffTreeAgeUpdate(TariffAgeID: btnSave.CommandArgument.ToParseInt(),
                FirstAge: txtFirstAge.Text.ToParseInt(),
                LastAge: txtLastAge.Text.ToParseInt(),
                Coefficient: txtcoefficient.Text.ToParseStr()
                );
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