using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Technique : System.Web.UI.Page
{
    Methods _db = new Methods();
   
    protected void Page_Load(object sender, EventArgs e)
    {

        _loadGridFromDb();

        
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtBirka.Text = "";
        txtmotor.Text = "";
        txtname.Text = "";
        txtpassport.Text = "";
        txtProductionYear.Text = "";
        txtRegisterNumber.Text = "";
        txtSerieNumber.Text = "";
    }
    void _loadGridFromDb()
    {
        
        DataTable DTTechnique = _db.GetTechnique();
        if (DTTechnique != null)
        {

            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTTechnique;
            Grid.DataBind();
        }
    }
    void modelsload() 
    {
        DataTable dt2 = _db.GetModelByID(cmBrand.Value.ToParseInt());
        cmmodels.ValueField = "ModelID";
        cmmodels.TextField = "ModelName";
        cmmodels.DataSource = dt2;
        cmmodels.DataBind();
        cmmodels.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmmodels.SelectedIndex = 0;
    }
    void componentsload()
    {
        DataTable dt1 = _db.GetBrands();
        cmBrand.ValueField = "BrandID";
        cmBrand.TextField = "BrandName";
        cmBrand.DataSource = dt1;
        cmBrand.DataBind();
        cmBrand.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmBrand.SelectedIndex = 0;

        modelsload();

        DataTable dt3 = _db.GetCompanies();
        cmCompany.ValueField = "CompanyID";
        cmCompany.TextField = "CompanyName";
        cmCompany.DataSource = dt3;
        cmCompany.DataBind();
        cmCompany.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCompany.SelectedIndex = 0;

        DataTable dt4 = _db.GetTechniqueSituations();
        cmbTechniqueSituationName.ValueField = "TechniqueSituationID";
        cmbTechniqueSituationName.TextField = "TechniqueSituationName";
        cmbTechniqueSituationName.DataSource = dt4;
        cmbTechniqueSituationName.DataBind();
        cmbTechniqueSituationName.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbTechniqueSituationName.SelectedIndex = 0;

        cmbGPS.Items.Clear();
        cmbGPS.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbGPS.Items.Insert(1, new ListEditItem("Hə", "0"));
        cmbGPS.Items.Insert(2, new ListEditItem("Yox", "1"));
        cmbGPS.SelectedIndex = 0;


    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetTechniqueById(id: id);
        cmBrand.Value = dt.Rows[0]["BrandID"].ToParseStr();
        modelsload();
        cmmodels.Value = dt.Rows[0]["ModelID"].ToParseStr();
        txtRegisterNumber.Text = dt.Rows[0]["RegisterNumber"].ToParseStr();
        txtSerieNumber.Text = dt.Rows[0]["SerieNumber"].ToParseStr();
        txtmotor.Text = dt.Rows[0]["Motor"].ToParseStr();
        cmCompany.Value = dt.Rows[0]["CompanyID"].ToParseStr();
        cmbTechniqueSituationName.Value = dt.Rows[0]["TechniqueSituationID"].ToParseStr();
        cmbGPS.Value = dt.Rows[0]["GPS"].ToParseStr();
        txtlogin.Text = dt.Rows[0]["GPSLogin"].ToParseStr();
        txtpass.Text = dt.Rows[0]["GPSPassword"].ToParseStr();
        txtProductionYear.Text = dt.Rows[0]["ProductionYear"].ToParseStr();
        txtBirka.Text = dt.Rows[0]["Birka"].ToParseStr();
        txtname.Text = dt.Rows[0]["TechniquesName"].ToParseStr();
        txtpassport.Text = dt.Rows[0]["Passport"].ToParseStr();
        Session["imgpath"] = dt.Rows[0]["Photo"].ToParseStr();
        imgUser.ImageUrl= @"imgtechnique\"+dt.Rows[0]["Photo"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["BoughtDate"].ToParseStr(), out datevalue))
        {
            dtBoughtDate.Text = DateTime.Parse(dt.Rows[0]["BoughtDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtBoughtDate.Text = "";
        }

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteTechnique(id: _id);
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

        if (FileUpload1.HasFile)
        {
            Session["imgpath"] = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_sss") + FileUpload1.FileName;
        } 

        if (btnSave.CommandName == "insert")
        {

            val = _db.TechniqueInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                BrandID: cmBrand.Value.ToParseInt(),
                ModelID: cmmodels.Value.ToParseInt(),
                RegisterNumber: txtRegisterNumber.Text.ToParseStr(),
                SerieNumber: txtSerieNumber.Text.ToParseStr(),
                Motor: txtmotor.Text.ToParseInt(),
                CompanyID: cmCompany.Value.ToParseInt(),
                TechniqueSituationID: cmbTechniqueSituationName.Value.ToParseInt(),
                GPS: cmbGPS.Value.ToParseInt(),
                GPSLogin: txtlogin.Text.ToParseStr(),
                GPSPassword: txtpass.Text.ToParseStr(),
                ProductionYear: txtProductionYear.Text.ToParseInt(),
                Photourl: Session["imgpath"].ToString(),
                Birka: txtBirka.Text.ToParseStr(),
                TechniquesName: txtname.Text.ToParseStr(),
                Passport: txtpassport.Text.ToParseStr(),
                BoughtDate: dtBoughtDate.Text.ToParseStr()                
                );
            if (val == Types.ProsesType.Succes)
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("/imgtechnique/" + Session["imgpath"].ToString()));
                }
            }

        }
        else
        {

            val = _db.TechniqueUpdate(TechniqueID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                BrandID: cmBrand.Value.ToParseInt(),
                ModelID: cmmodels.Value.ToParseInt(),
                RegisterNumber: txtRegisterNumber.Text.ToParseStr(),
                SerieNumber: txtSerieNumber.Text.ToParseStr(),
                Motor: txtmotor.Text.ToParseInt(),
                CompanyID: cmCompany.Value.ToParseInt(),
                TechniqueSituationID: cmbTechniqueSituationName.Value.ToParseInt(),
                GPS: cmbGPS.Value.ToParseInt(),
                GPSLogin: txtlogin.Text.ToParseStr(),
                GPSPassword: txtpass.Text.ToParseStr(),
                ProductionYear: txtProductionYear.Text.ToParseInt(),
                Photourl: Session["imgpath"].ToString(),
                Birka: txtBirka.Text.ToParseStr(),
                TechniquesName: txtname.Text.ToParseStr(),
                Passport: txtpassport.Text.ToParseStr(),
                BoughtDate: dtBoughtDate.Text.ToParseStr());
            if (val == Types.ProsesType.Succes)
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("/imgtechnique/" + Session["imgpath"].ToString()));
                }
            }
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


    protected void cmBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelsload();
    }
}