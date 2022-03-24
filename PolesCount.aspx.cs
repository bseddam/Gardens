using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PolesCount : System.Web.UI.Page
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
        txtpolecount.Text = "";

        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtline = _db.GetPolesCounts();
        if (dtline != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtline;
            Grid.DataBind();
        }
    }
    void sectorcomponentload()
    {
        ddlsector.ClearSelection();
        DataTable dt6 = _db.GetSectorsByZoneID(ddlzone.SelectedValue.ToParseInt());
        ddlsector.DataValueField = "SectorID";
        ddlsector.DataTextField = "SectorName";
        ddlsector.DataSource = dt6;
        ddlsector.DataBind();
        ddlsector.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlsector.SelectedIndex = 0;
    }
    void linecomponentload()
    {
        ddlline.ClearSelection();
        DataTable dt5 = _db.GetLineBySectorID(ddlsector.SelectedValue.ToParseInt());
        ddlline.DataValueField = "LineID";
        ddlline.DataTextField = "LineName";
        ddlline.DataSource = dt5;
        ddlline.DataBind();
        ddlline.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlline.SelectedIndex = 0;
    }

    void zonacomponentload()
    {
        ddlzone.ClearSelection();
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



        DataTable dt5 = _db.GetGardens();
        ddlgardens.DataValueField = "GardenID";
        ddlgardens.DataTextField = "GardenName";
        ddlgardens.DataSource = dt5;
        ddlgardens.DataBind();
        ddlgardens.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlgardens.SelectedIndex = 0;


      



        DataTable dt3x = _db.GetPoleTypes();
        ddlpoletype.DataValueField = "PoleTypeID";
        ddlpoletype.DataTextField = "PoleTypeName";
        ddlpoletype.DataSource = dt3x;
        ddlpoletype.DataBind();
        ddlpoletype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlpoletype.SelectedIndex = 0;


        zonacomponentload();
        sectorcomponentload();
        linecomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetPolesCountsByID(id: id);
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }

        ddlgardens.SelectedValue = dt.Rows[0]["GardenID"].ToParseStr();
        zonacomponentload();
        ddlzone.SelectedValue = dt.Rows[0]["ZoneID"].ToParseStr();
        sectorcomponentload();
        ddlsector.SelectedValue = dt.Rows[0]["SectorID"].ToParseStr();

        linecomponentload();
        ddlline.SelectedValue = dt.Rows[0]["LineID"].ToParseStr();


     


        ddlpoletype.SelectedValue = dt.Rows[0]["PoleTypeID"].ToParseStr();
        txtpolecount.Text = dt.Rows[0]["PoleCount"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.PoleCountDelete(id: _id);
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
            val = _db.PoleCountInsert(
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineID: ddlline.SelectedValue.ToParseInt(),
                PoleTypeID: ddlpoletype.SelectedValue.ToParseInt(),
                PoleCount: txtpolecount.Text.ToParseInt()
                );
        }
        else
        {
            val = _db.PoleCountUpdate(PoleCountID: btnSave.CommandArgument.ToParseInt(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineID: ddlline.SelectedValue.ToParseInt(),
                PoleTypeID: ddlpoletype.SelectedValue.ToParseInt(),
                PoleCount: txtpolecount.Text.ToParseInt()
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
        ddlline.ClearSelection();
        ddlsector.ClearSelection();
        zonacomponentload();
    }

    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlline.ClearSelection();
        sectorcomponentload();
    }


    protected void ddlsector_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlline.ClearSelection();
        linecomponentload();
    }

   
}