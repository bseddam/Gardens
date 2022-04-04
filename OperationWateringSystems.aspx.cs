using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationWateringSystems : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtNote.Text = "";
        txtWateringSystemSize.Text = "";
        dtRegstrTime.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable DTOperationWateringSystems = _db.GetOperationWateringSystems();
        if (DTOperationWateringSystems != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTOperationWateringSystems;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        cmWateringSystemsName.Items.Clear();
        DataTable dt1 = _db.GetWateringSystems();
        cmWateringSystemsName.ValueField = "WateringSystemID";
        cmWateringSystemsName.TextField = "WateringSystemName";
        cmWateringSystemsName.DataSource = dt1;
        cmWateringSystemsName.DataBind();
        cmWateringSystemsName.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWateringSystemsName.SelectedIndex = 0;

        cmWateringSystemsGarden.Items.Clear();
        DataTable dt2 = _db.GetGardens();
        cmWateringSystemsGarden.ValueField = "GardenID";
        cmWateringSystemsGarden.TextField = "GardenName";
        cmWateringSystemsGarden.DataSource = dt2;
        cmWateringSystemsGarden.DataBind();
        cmWateringSystemsGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWateringSystemsGarden.SelectedIndex = 0;

        cmUnitMeasurement.Items.Clear();
        DataTable dt3 = _db.GetUnitMeasurements();
        cmUnitMeasurement.ValueField = "UnitMeasurementID";
        cmUnitMeasurement.TextField = "UnitMeasurementName";
        cmUnitMeasurement.DataSource = dt3;
        cmUnitMeasurement.DataBind();
        cmUnitMeasurement.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmUnitMeasurement.SelectedIndex = 0;

        cmEntryExitStatus.Items.Clear();
        DataTable dt4 = _db.GetProductOperationTypes();
        cmEntryExitStatus.ValueField = "ProductOperationTypeID";
        cmEntryExitStatus.TextField = "ProductOperationTypeName";
        cmEntryExitStatus.DataSource = dt4;
        cmEntryExitStatus.DataBind();
        cmEntryExitStatus.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmEntryExitStatus.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetOperationWateringSystemsById(id: id);
        cmWateringSystemsGarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmWateringSystemsName.Value = dt.Rows[0]["WateringSystemID"].ToParseStr();
        cmUnitMeasurement.Value = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        txtWateringSystemSize.Text = dt.Rows[0]["WateringSystemSize"].ToParseStr();
        cmEntryExitStatus.Value = dt.Rows[0]["EntryExitStatus"].ToParseStr();
        txtNote.Text = dt.Rows[0]["Notes"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            dtRegstrTime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtRegstrTime.Text = "";
        }        

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteOperationWateringSystems(id: _id);
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

            val = _db.OperationWateringSystemsWorkDoneInsert(UserID: Session["UserID"].ToParseInt(),
                GardenID: cmWateringSystemsGarden.Value.ToParseInt(),
                WateringSystemID: cmWateringSystemsName.Value.ToParseInt(),
                WateringSystemSize: txtWateringSystemSize.Text.ToParseStr(),
                UnitMeasurementID: cmUnitMeasurement.Value.ToParseInt(),
                EntryExitStatus: cmEntryExitStatus.Value.ToParseInt(),
                Notes: txtNote.Text.ToParseStr(),               
                RegisterTime: dtRegstrTime.Text.ToParseStr()
                );            
        }
        else
        {

            val = _db.OperationWateringSystemsWorkDoneUpdate(WateringSystemWorkID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToParseInt(),
                GardenID: cmWateringSystemsGarden.Value.ToParseInt(),
                WateringSystemID: cmWateringSystemsName.Value.ToParseInt(),
                WateringSystemSize: txtWateringSystemSize.Text.ToParseStr(),
                UnitMeasurementID: cmUnitMeasurement.Value.ToParseInt(),
                EntryExitStatus: cmEntryExitStatus.Value.ToParseInt(),
                Notes: txtNote.Text.ToParseStr(),
                RegisterTime: dtRegstrTime.Text.ToParseStr());
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