using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gardens : System.Web.UI.Page
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
        txtgardenname.Text = "";
        txtgardenarea.Text = "";
        txtadress.Text = "";
        txtnotes.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtgarden = _db.GetGardens();
        if (dtgarden != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtgarden;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        DataTable dt4 = _db.GetUnitMeasurements();
        ddlunitmeasurement.DataValueField = "UnitMeasurementID";
        ddlunitmeasurement.DataTextField = "UnitMeasurementName";
        ddlunitmeasurement.DataSource = dt4;
        ddlunitmeasurement.DataBind();
        ddlunitmeasurement.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlunitmeasurement.SelectedIndex = 0;
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetGardenById(id: id);
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }
        txtgardenname.Text = dt.Rows[0]["GardenName"].ToParseStr();
        txtgardenarea.Text = dt.Rows[0]["GardenArea"].ToParseStr();
        ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        txtadress.Text = dt.Rows[0]["Address"].ToParseStr();
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteGarden(id: _id);
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
            val = _db.GardensInsert(RegisterTime: cmbregistertime.Text.ToParseStr(),
                GardenName: txtgardenname.Text.ToParseStr(),
                GardenArea: txtgardenarea.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Address: txtadress.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.GardensUpdate(GardenID: btnSave.CommandArgument.ToParseInt(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
    GardenName: txtgardenname.Text.ToParseStr(),
    GardenArea: txtgardenarea.Text.ToParseStr(),
    UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
    Address: txtadress.Text.ToParseStr(),
    Notes: txtnotes.Text.ToParseStr());

            //val = _db.CompanyUpdate(CompanyID: btnSave.CommandArgument.ToParseInt(),
            //   RegisterTime: cmbregistertime.Text.ToParseStr(),
            //   CompanyName: txtcompanyname.Text.ToParseStr(),
            //   CompanyVoen: txtvoen.Text.ToParseStr(),
            //   BankAccount: txtbankaccount.Text.ToParseStr(),
            //   PhoneNumbers: txtphone.Text.ToParseStr(),
            //   Email: txtemail.Text.ToParseStr(),
            //   Adress: txtadress.Text.ToParseStr(),
            //   Notes: txtnotes.Text.ToParseStr());
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