﻿using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
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
        txtproductname.Text = "";
        txtprice.Text = "";
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
        DataTable dt6 = _db.GetSectorsByZoneID(ddlbrand.SelectedValue.ToParseInt());
        ddlmodel.DataValueField = "SectorID";
        ddlmodel.DataTextField = "SectorName";
        ddlmodel.DataSource = dt6;
        ddlmodel.DataBind();
        ddlmodel.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlmodel.SelectedIndex = 0;
    }

    void zonacomponentload()
    {
        DataTable dt6 = _db.GetZonesByGardenID(ddl.SelectedValue.ToParseInt());
        ddlbrand.DataValueField = "ZoneID";
        ddlbrand.DataTextField = "ZoneName";
        ddlbrand.DataSource = dt6;
        ddlbrand.DataBind();
        ddlbrand.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlbrand.SelectedIndex = 0;
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
        ddl.DataValueField = "GardenID";
        ddl.DataTextField = "GardenName";
        ddl.DataSource = dt5;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddl.SelectedIndex = 0;


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
        txtproductname.Text = dt.Rows[0]["LineName"].ToParseStr();
        txtprice.Text = dt.Rows[0]["LineArea"].ToParseStr();
        ddl.SelectedValue = dt.Rows[0]["GardenID"].ToParseStr();
        zonacomponentload();
        ddlbrand.SelectedValue = dt.Rows[0]["ZoneID"].ToParseStr();
        sectorcomponentload();
        ddlmodel.SelectedValue = dt.Rows[0]["SectorID"].ToParseStr();
        ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
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
                LineName: txtproductname.Text.ToParseStr(),
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                LineArea: txtprice.Text.ToParseStr(),
                SectorID: ddlmodel.SelectedValue.ToParseInt(),
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
                LineName: txtproductname.Text.ToParseStr(),
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                LineArea: txtprice.Text.ToParseStr(),
                SectorID: ddlmodel.SelectedValue.ToParseInt(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                TreeCount: txttreecount.Text.ToParseInt(),
                Sowingtime: cmbsowingtime.Text.ToParseStr(),
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

    protected void ddlgardens_SelectedIndexChanged(object sender, EventArgs e)
    {
        zonacomponentload();
    }

    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        sectorcomponentload();
    }
}