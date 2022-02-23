using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {

        txtlogin.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetUsers();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }

    void componentsload()
    {
        DataTable dt7 = _db.GetGardens();
        cmbgarden.ValueField = "GardenID";
        cmbgarden.TextField = "GardenName";
        cmbgarden.DataSource = dt7;
        cmbgarden.DataBind();
        cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbgarden.SelectedIndex = 0;


    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetModelByID(id: id);
 

        txtlogin.Text = dt.Rows[0]["ModelName"].ToParseStr();
        cmbgarden.ValueField = dt.Rows[0]["BrandID"].ToParseStr();
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteModel(id: _id);
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
            val = _db.ModelInsert(
                ModelName: txtlogin.Text.ToParseStr(),
                BrandID: cmbgarden.Value.ToParseInt()
                );
        }
        else
        {
            val = _db.ModelUpdate(ModelID: btnSave.CommandArgument.ToParseInt(),
                ModelName: txtlogin.Text.ToParseStr(),
                BrandID: cmbgarden.Value.ToParseInt()
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