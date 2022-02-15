using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lines : System.Web.UI.Page
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
        txtlinename.Text = "";
        txtlinearea.Text = "";
        txtnotes.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtline = _db.GetLines();
        if (dtline != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtline;
            Grid.DataBind();
        }
    }
    void sectorcomponentload()
    {
        DataTable dt6 = _db.GetSectorsByZoneID(ddlzone.SelectedValue.ToParseInt());
        ddlsector.DataValueField = "SectorID";
        ddlsector.DataTextField = "SectorName";
        ddlsector.DataSource = dt6;
        ddlsector.DataBind();
        ddlsector.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlsector.SelectedIndex = 0;
    }

    void zonacomponentload()
    {
        DataTable dt6 = _db.GetZonesByGardenID(ddlgardens.SelectedValue.ToParseInt());
        ddlzone.DataValueField = "ZoneID";
        ddlzone.DataTextField = "ZoneName";
        ddlzone.DataSource = dt6;
        ddlzone.DataBind();
        ddlzone.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlzone.SelectedIndex = 0;
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


        DataTable dt1 = _db.GetTreeTypes();
        ddltreetype.DataValueField = "TreeTypeID";
        ddltreetype.DataTextField = "TreeTypeName";
        ddltreetype.DataSource = dt1;
        ddltreetype.DataBind();
        ddltreetype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddltreetype.SelectedIndex = 0;


        

        zonacomponentload();
        sectorcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetLineById(id: id);
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }
        txtlinename.Text = dt.Rows[0]["LineName"].ToParseStr();
        txtlinearea.Text = dt.Rows[0]["LineArea"].ToParseStr();
        ddlgardens.SelectedValue = dt.Rows[0]["GardenID"].ToParseStr();
        zonacomponentload();
        ddlzone.SelectedValue = dt.Rows[0]["ZoneID"].ToParseStr();
        sectorcomponentload();
        ddlsector.SelectedValue = dt.Rows[0]["SectorID"].ToParseStr();
        if (dt.Rows[0]["UnitMeasurementID"].ToParseStr() != "")
        {
            ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        }
        ddltreetype.SelectedValue = dt.Rows[0]["TreeTypeID"].ToParseStr();
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteLine(id: _id);
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
            val = _db.LineInsert(RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineName: txtlinename.Text.ToParseStr(), 
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                LineArea: txtlinearea.Text.ToParseStr(),
                SectorID: ddlsector.SelectedValue.ToParseInt(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                TreeCount: txttreecount.Text.ToParseInt(),
                Sowingtime: cmbsowingtime.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.LineUpdate(LineID: btnSave.CommandArgument.ToParseInt(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineName: txtlinename.Text.ToParseStr(),
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                LineArea: txtlinearea.Text.ToParseStr(),
                SectorID: ddlsector.SelectedValue.ToParseInt(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                TreeCount: txttreecount.Text.ToParseInt(),
                Sowingtime: cmbsowingtime.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr()
                ) ;
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

    protected void ddlgardens_SelectedIndexChanged(object sender, EventArgs e)
    {
        zonacomponentload();
    }

    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        sectorcomponentload();
    }
}