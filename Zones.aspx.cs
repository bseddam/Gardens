using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Zones : System.Web.UI.Page
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
        txtzonename.Text = "";
        txtzonearea.Text = "";
        txtnotes.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtzones = _db.GetZones();
        if (dtzones != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtzones;
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


        DataTable dt5 = _db.GetGardens();
        ddlgardens.DataValueField = "GardenID";
        ddlgardens.DataTextField = "GardenName";
        ddlgardens.DataSource = dt5;
        ddlgardens.DataBind();
        ddlgardens.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlgardens.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetZoneById(id: id);
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }
        txtzonename.Text = dt.Rows[0]["ZoneName"].ToParseStr();
        txtzonearea.Text = dt.Rows[0]["ZoneArea"].ToParseStr();
        ddlgardens.SelectedValue = dt.Rows[0]["GardenID"].ToParseStr();
        if (dt.Rows[0]["UnitMeasurementID"].ToParseStr() != "")
        {
            ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        }
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteZone(id: _id);
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
            val = _db.ZoneInsert(RegisterTime: cmbregistertime.Text.ToParseStr(),
                GardenID: ddlgardens.SelectedValue.ToParseInt(),
                ZoneName: txtzonename.Text.ToParseStr(),
                ZoneArea: txtzonearea.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.ZoneUpdate(ZoneID: btnSave.CommandArgument.ToParseInt(),
               RegisterTime: cmbregistertime.Text.ToParseStr(),
                GardenID: ddlgardens.SelectedValue.ToParseInt(),
                ZoneName: txtzonename.Text.ToParseStr(),
                ZoneArea: txtzonearea.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Notes: txtnotes.Text.ToParseStr()
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