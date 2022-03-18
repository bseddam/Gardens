using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class OperationTechniques : System.Web.UI.Page
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
        txtOdometer.Text = "";
        txtTreeCount.Text = "";
        dtRegisterTime.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable DTOperationTechniques = _db.GetOperationTechniqueWorkDone();
        if (DTOperationTechniques != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTOperationTechniques;
            Grid.DataBind();
        }
    }
    void modelcomponentload()
    {
        cmModel.Items.Clear();
        DataTable dt4x = _db.GetModelsByProductTypeID(cmbproducttype.Value.ToParseInt().ToParseInt());
        cmModel.ValueField = "ModelID";
        cmModel.TextField = "ModelName";
        cmModel.DataSource = dt4x;
        cmModel.DataBind();
        cmModel.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmModel.SelectedIndex = 0;
    }
    void componentsload()
    {
        DataTable dt6 = _db.GetProductTypes();
        cmbproducttype.ValueField = "ProductTypeID";
        cmbproducttype.TextField = "ProductTypeName";
        cmbproducttype.DataSource = dt6;
        cmbproducttype.DataBind();
        cmbproducttype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttype.Items.Insert(1, new ListEditItem("Yoxdur", "0"));
        cmbproducttype.SelectedIndex = 0;

       

        cmCompany.Items.Clear();
        DataTable dt2 = _db.GetCompanies();
        cmCompany.ValueField = "CompanyID";
        cmCompany.TextField = "CompanyName";
        cmCompany.DataSource = dt2;
        cmCompany.DataBind();
        cmCompany.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCompany.SelectedIndex = 0;

        cmWork.Items.Clear();
        DataTable dt3 = _db.GetWorks();
        cmWork.ValueField = "WorkID";
        cmWork.TextField = "WorkName";
        cmWork.DataSource = dt3;
        cmWork.DataBind();
        cmWork.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWork.SelectedIndex = 0;

        cmGarden.Items.Clear();
        DataTable dt4 = _db.GetGardens();
        cmGarden.ValueField = "GardenID";
        cmGarden.TextField = "GardenName";
        cmGarden.DataSource = dt4;
        cmGarden.DataBind();
        cmGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmGarden.SelectedIndex = 0;
        modelcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetOperationTechniqueWorkDoneByID(id: id);

        cmbproducttype.Value = dt.Rows[0]["ProductTypeID"].ToParseStr();
        modelcomponentload();
        cmModel.Value = dt.Rows[0]["ModelID"].ToParseStr();
        cmModel_SelectedIndexChanged(null, null);
        cmTechnique.Value = dt.Rows[0]["TechniqueID"].ToParseStr();
        cmCompany.Value = dt.Rows[0]["CompanyID"].ToParseStr();
        cmWork.Value = dt.Rows[0]["WorkID"].ToParseStr();
        txtOdometer.Text = dt.Rows[0]["Odometer"].ToParseStr();
        cmGarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmGarden_SelectedIndexChanged(null, null);
        cmZone.Value = dt.Rows[0]["ZoneID"].ToParseStr();
        cmZone_SelectedIndexChanged(null, null);
        cmSektor.Value = dt.Rows[0]["SectorID"].ToParseStr();
        cmSektor_SelectedIndexChanged(null, null);
        cmLine.Value = dt.Rows[0]["LineID"].ToParseStr();
        txtTreeCount.Text = dt.Rows[0]["TreeCount"].ToParseStr();
        txtNotes.Text = dt.Rows[0]["Notes"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            dtRegisterTime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtRegisterTime.Text = "";
        }
        
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteOperationTechniques(id: _id);
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

            val = _db.OperationTechniqueWorkDoneInsert(UserID: Session["UserID"].ToParseInt(),
                TechniqueID: cmTechnique.Value.ToParseInt(),
                CompanyID: cmCompany.Value.ToParseInt(),
                WorkID: cmWork.Value.ToParseInt(),
                Odometer: txtOdometer.Text.ToParseInt(),
                LineID: cmLine.Value.ToParseInt(),
                TreeCount: txtTreeCount.Text.ToParseInt(),
                Notes: txtNotes.Text.ToParseStr(),
                RegisterTime: dtRegisterTime.Text.ToParseStr()
                );          
        }
        else
        {

            val = _db.OperationTechniqueWorkDoneUpdate(TechniquesWorkDoneID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToParseInt(),
                TechniqueID: cmTechnique.Value.ToParseInt(),
                CompanyID: cmCompany.Value.ToParseInt(),
                WorkID: cmWork.Value.ToParseInt(),
                Odometer: txtOdometer.Text.ToParseInt(),
                LineID: cmLine.Value.ToParseInt(),
                TreeCount: txtTreeCount.Text.ToParseInt(),
                Notes: txtNotes.Text.ToParseStr(),
                RegisterTime: dtRegisterTime.Text.ToParseStr());           
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

   

    protected void cmModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmTechnique.Items.Clear();
        DataTable dt4 = _db.GetTechniqueByModelId(cmModel.Value.ToParseInt());
        cmTechnique.ValueField = "TechniqueID";
        cmTechnique.TextField = "TechniquesName";
        cmTechnique.DataSource = dt4;
        cmTechnique.DataBind();
        cmTechnique.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmTechnique.SelectedIndex = 0;
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
        DataTable dt5 = _db.GetLineBySectorID(_db.GetSectorById(cmSektor.Value.ToParseInt()).Rows[0]["SectorID"].ToParseInt());
        cmLine.ValueField = "LineID";
        cmLine.TextField = "LineName";
        cmLine.DataSource = dt5;
        cmLine.DataBind();
        cmLine.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmLine.SelectedIndex = 0;
    }

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelcomponentload();
    }
}