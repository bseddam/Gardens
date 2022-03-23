using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GardenInformation : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        dtRegstrDate.Text = "";
        txtadress.Text = "";
        txtarea.Text = "";
        txtregisternum.Text = "";
        txtregistrynum.Text = "";
        txtXCoordinate.Text = "";
        txtYCoordinate.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable dt = _db.GetGardenInformations();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        cmbgarden.Items.Clear();
        DataTable dt3 = _db.GetGardens();
        cmbgarden.ValueField = "GardenID";
        cmbgarden.TextField = "GardenName";
        cmbgarden.DataSource = dt3;
        cmbgarden.DataBind();
        cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbgarden.SelectedIndex = 0;



        cmbcompany.Items.Clear();
        DataTable dt1 = _db.GetCompanies();
        cmbcompany.ValueField = "CompanyID";
        cmbcompany.TextField = "CompanyName";
        cmbcompany.DataSource = dt1;
        cmbcompany.DataBind();
        cmbcompany.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbcompany.SelectedIndex = 0;

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetGardenInformationByID(id: id);

        cmbgarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmbcompany.Value = dt.Rows[0]["CompanyID"].ToParseStr();
        txtadress.Text = dt.Rows[0]["Adress"].ToParseStr();
        txtarea.Text = dt.Rows[0]["Area"].ToParseStr();
        txtregistrynum.Text = dt.Rows[0]["RegistryNumber"].ToParseStr();
        txtregisternum.Text = dt.Rows[0]["RegisterNumber"].ToParseStr();
        txtXCoordinate.Text = dt.Rows[0]["XCoordinate"].ToParseStr();
        txtYCoordinate.Text = dt.Rows[0]["YCoordinate"].ToParseStr();

        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            dtRegstrDate.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtRegstrDate.Text = "";
        }

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteGardenInformation(id: _id);
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
            val = _db.GardenInformationInsert(
                
                GardenID: cmbgarden.Value.ToParseInt(),
                CompanyID: cmbcompany.Value.ToParseInt(),
                Adress:txtadress.Text.ToParseStr(),
                Area: txtarea.Text.ToParseStr(),
                RegisterNumber:txtregisternum.Text.ToParseStr(),
                RegistryNumber: txtregistrynum.Text.ToParseStr(),
                RegisterTime: dtRegstrDate.Text.ToParseStr(),
                XCoordinate: txtXCoordinate.Text.ToParseStr(),
                YCoordinate: txtYCoordinate.Text.ToParseStr()
                );
        }
        else
        {

            val = _db.GardenInformationUpdate(GardenInformationID: btnSave.CommandArgument.ToParseInt(),
                GardenID: cmbgarden.Value.ToParseInt(),
                CompanyID: cmbcompany.Value.ToParseInt(),
                Adress: txtadress.Text.ToParseStr(),
                Area: txtarea.Text.ToParseStr(),
                RegisterNumber: txtregisternum.Text.ToParseStr(),
                RegistryNumber: txtregistrynum.Text.ToParseStr(),
                RegisterTime: dtRegstrDate.Text.ToParseStr(),
                XCoordinate: txtXCoordinate.Text.ToParseStr(),
                YCoordinate: txtYCoordinate.Text.ToParseStr()
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