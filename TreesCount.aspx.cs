using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreesCount : System.Web.UI.Page
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
        txttreecount.Text = "";
       
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtline = _db.GetTreesCounts();
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
    void treetypecomponentload()
    {
        DataTable dt2x = _db.GetTreeTypeByTreeCountryID(ddltree.SelectedValue.ToParseInt(),
            ddlcountry.SelectedValue.ToParseInt());
        ddltreetype.DataValueField = "TreeTypeID";
        ddltreetype.DataTextField = "TreeTypeName";
        ddltreetype.DataSource = dt2x;
        ddltreetype.DataBind();
        ddltreetype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddltreetype.SelectedIndex = 0;
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


        DataTable dt1 = _db.GetCountries();
        ddlcountry.DataValueField = "CountryID";
        ddlcountry.DataTextField = "CountryName";
        ddlcountry.DataSource = dt1;
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlcountry.SelectedIndex = 0;

        DataTable dt2x = _db.GetTrees();
        ddltree.DataValueField = "TreeID";
        ddltree.DataTextField = "TreeName";
        ddltree.DataSource = dt2x;
        ddltree.DataBind();
        ddltree.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddltree.SelectedIndex = 0;



        DataTable dt3x = _db.GetTreeSitiuations();
        ddltreesitiuation.DataValueField = "TreesSitiuationID";
        ddltreesitiuation.DataTextField = "TreesSitiuationName";
        ddltreesitiuation.DataSource = dt3x;
        ddltreesitiuation.DataBind();
        ddltreesitiuation.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddltreesitiuation.SelectedIndex = 0;


        zonacomponentload();
        sectorcomponentload();
        linecomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetTreesCountsByID(id: id);
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


        ddlcountry.SelectedValue = dt.Rows[0]["CountryID"].ToParseStr();

        ddltree.SelectedValue = dt.Rows[0]["TreeID"].ToParseStr();

        treetypecomponentload();
        ddltreetype.SelectedValue = dt.Rows[0]["TreeTypeID"].ToParseStr();

        
        ddltreesitiuation.SelectedValue = dt.Rows[0]["TreesSitiuationID"].ToParseStr();
        txttreecount.Text = dt.Rows[0]["TreeCount"].ToParseStr();

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
            val = _db.TreeCountInsert(
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineID: ddlline.SelectedValue.ToParseInt(),
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                TreeCount: txttreecount.Text.ToParseInt(),
                TreeSitiuation: ddltreesitiuation.SelectedValue.ToParseInt()
                );
        }
        else
        {
            val = _db.TreeCountUpdate(TreeCountID: btnSave.CommandArgument.ToParseInt(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                LineID: ddlline.SelectedValue.ToParseInt(),
                TreeTypeID: ddltreetype.SelectedValue.ToParseInt(),
                TreeCount: txttreecount.Text.ToParseInt(),
                TreeSitiuation: ddltreesitiuation.SelectedValue.ToParseInt()
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

    protected void ddltree_ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        treetypecomponentload();
    }
}