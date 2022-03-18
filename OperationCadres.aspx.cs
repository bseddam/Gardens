using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationCadres : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtNotes.Text = "";
        txtsalary.Text = "";
        txtTreeCount.Text = "";
        cmCadre.SelectedIndex = 0;
        cmWork.SelectedIndex = 0;
        cmGarden.SelectedIndex = 0;
        cmZone.SelectedIndex = 0;
        cmSektor.SelectedIndex = 0;
        cmLine.SelectedIndex = 0;
        cmWeather.SelectedIndex = 0;
        cmTreeType.SelectedIndex = 0;
        cmTreeAge.SelectedIndex = 0;
        dtRegstrDate.Text = "";

    }
    void _loadGridFromDb()
    {

        DataTable DTOperationCadre = _db.GetOperationCadre();
        if (DTOperationCadre != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTOperationCadre;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        cmCadre.Items.Clear();
        cmCadre.Items.Clear();
        DataTable dt1 = _db.GetCadres();
        cmCadre.ValueField = "CadreID";
        cmCadre.TextField = "NameDDL";
        cmCadre.DataSource = dt1;
        cmCadre.DataBind();
        cmCadre.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCadre.SelectedIndex = 0;

        cmWork.Items.Clear();
        DataTable dt2 = _db.GetWorks();
        cmWork.ValueField = "WorkID";
        cmWork.TextField = "WorkName";
        cmWork.DataSource = dt2;
        cmWork.DataBind();
        cmWork.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWork.SelectedIndex = 0;

        cmGarden.Items.Clear();
        DataTable dt3 = _db.GetGardens();
        cmGarden.ValueField = "GardenID";
        cmGarden.TextField = "GardenName";
        cmGarden.DataSource = dt3;
        cmGarden.DataBind();
        cmGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmGarden.SelectedIndex = 0;

        cmWeather.Items.Clear();
        DataTable dt4 = _db.GetWeatherCondition();
        cmWeather.ValueField = "WeatherConditionID";
        cmWeather.TextField = "WeatherConditionName";
        cmWeather.DataSource = dt4;
        cmWeather.DataBind();
        cmWeather.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWeather.SelectedIndex = 0;

        cmTreeType.Items.Clear();
        DataTable dt5 = _db.GetTreeTypes();
        cmTreeType.ValueField = "TreeTypeID";
        cmTreeType.TextField = "TreeTypeName";
        cmTreeType.DataSource = dt5;
        cmTreeType.DataBind();
        cmTreeType.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmTreeType.SelectedIndex = 0;

        cmTreeAge.Items.Clear();
        DataTable dt6 = _db.GetTariffTreeAge();
        cmTreeAge.ValueField = "TariffAgeID";
        cmTreeAge.TextField = "TariffAgeName1";
        cmTreeAge.DataSource = dt6;
        cmTreeAge.DataBind();
        cmTreeAge.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmTreeAge.SelectedIndex = 0;


    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetOperationCadreByID(id: id);
        cmCadre.Value = dt.Rows[0]["CadreID"].ToParseStr();
        cmWork.Value = dt.Rows[0]["WorkID"].ToParseStr();
        cmGarden.Value= dt.Rows[0]["GardenID"].ToParseStr();
        cmGarden_SelectedIndexChanged(null,null);
        cmZone.Value = dt.Rows[0]["ZoneID"].ToParseStr();
        cmZone_SelectedIndexChanged(null, null);
        cmSektor.Value = dt.Rows[0]["SectorID"].ToParseStr();    
        cmSektor_SelectedIndexChanged(null, null);
        cmLine.Value = dt.Rows[0]["LinesID"].ToParseStr();
        cmWeather.Value = dt.Rows[0]["WeatherConditionID"].ToParseStr();
        cmTreeType.Value = dt.Rows[0]["TreeTypeID"].ToParseStr();
        cmTreeAge.Value = dt.Rows[0]["TariffAgeID"].ToParseStr();
        txtTreeCount.Text = dt.Rows[0]["TreeCount"].ToParseStr();
        txtsalary.Text = dt.Rows[0]["Salary"].ToParseStr();
        txtNotes.Text = dt.Rows[0]["Notes"].ToParseStr();
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
        Types.ProsesType val = _db.DeleteOperationCadre(id: _id);
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
        if (Session["UserID"] != null)
        {
            Session["UserID"] = 1;
        }

        if (btnSave.CommandName == "insert")
        {
            val = _db.OperationCadreInsert(UserID: Session["UserID"].ToParseInt(),
                CadreID: cmCadre.Value.ToParseInt(),
                WorkID: cmWork.Value.ToParseInt(),
                LinesID: cmLine.Value.ToParseInt(),
                WeatherConditionID: cmWeather.Value.ToParseInt(),
                TreeTypeID: cmTreeType.Value.ToParseInt(),
                TariffAgeID: cmTreeAge.Value.ToParseInt(),
                TreeCount: txtTreeCount.Text.ToParseInt(),
                Salary: txtsalary.Text.ToParseInt(),
                Notes: txtNotes.Text.ToParseStr(),
                RegisterTime: dtRegstrDate.Text.ToParseStr()
                );
        }
        else
        {

            val = _db.OperationCadreUpdate(WorkDoneID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToParseInt(),
                CadreID: cmCadre.Value.ToParseInt(),
                WorkID: cmWork.Value.ToParseInt(),
                LinesID: cmLine.Value.ToParseInt(),
                WeatherConditionID: cmWeather.Value.ToParseInt(),
                TreeTypeID: cmTreeType.Value.ToParseInt(),
                TariffAgeID: cmTreeAge.Value.ToParseInt(),
                TreeCount: txtTreeCount.Text.ToParseInt(),
                Salary: txtsalary.Text.ToParseInt(),
                Notes: txtNotes.Text.ToParseStr(),
                RegisterTime: dtRegstrDate.Text.ToParseStr());
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

    protected void cmGarden_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmZone.Items.Clear();
        DataTable dt4 = _db.GetZonesByGardenID(cmGarden.Value.ToParseInt());
        cmZone.ValueField = "ZoneID";
        cmZone.TextField = "ZoneName";
        cmZone.DataSource = dt4;
        cmZone.DataBind();
        cmZone.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmZone.SelectedIndex = 0;
    }

    protected void cmZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmSektor.Items.Clear();
        DataTable dt5 = _db.GetSectorsByZoneID(cmZone.Value.ToParseInt());
        cmSektor.ValueField = "SectorID";
        cmSektor.TextField = "SectorName";
        cmSektor.DataSource = dt5;
        cmSektor.DataBind();
        cmSektor.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmSektor.SelectedIndex = 0;
    }

    protected void cmSektor_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmLine.Items.Clear();
        DataTable dt5 = _db.GetLineBySectorID(cmSektor.Value.ToParseInt());
        cmLine.ValueField = "LineID";
        cmLine.TextField = "LineName";
        cmLine.DataSource = dt5;
        cmLine.DataBind();
        cmLine.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmLine.SelectedIndex = 0;
    }
}