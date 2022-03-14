using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WateringSystems : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtnotes.Text = "";
        txtname.Text = "";
        dtRegstrDate.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable DTWateringSystems = _db.GetWateringSystems();
        if (DTWateringSystems != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTWateringSystems;
            Grid.DataBind();
        }
    }

   

    void componentsload()
    {
        cmmodels.Items.Clear();
        DataTable dt2 = _db.GetModels();
        cmmodels.ValueField = "ModelID";
        cmmodels.TextField = "ModelName";
        cmmodels.DataSource = dt2;
        cmmodels.DataBind();
        cmmodels.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmmodels.SelectedIndex = 0;


        
      

        cmbWateringSystemSituationName.Items.Clear();
        DataTable dt4 = _db.GetTechniqueSituations();
        cmbWateringSystemSituationName.ValueField = "TechniqueSituationID";
        cmbWateringSystemSituationName.TextField = "TechniqueSituationName";
        cmbWateringSystemSituationName.DataSource = dt4;
        cmbWateringSystemSituationName.DataBind();
        cmbWateringSystemSituationName.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbWateringSystemSituationName.SelectedIndex = 0;

    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetWateringSystemsById(id: id);
  
 
        cmmodels.Value = dt.Rows[0]["ModelID"].ToParseStr();
        txtname.Text = dt.Rows[0]["WateringSystemName"].ToParseStr();
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();
        cmbWateringSystemSituationName.Value = dt.Rows[0]["TechniqueSituationID"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["Registertime"].ToParseStr(), out datevalue))
        {
            dtRegstrDate.Text = DateTime.Parse(dt.Rows[0]["Registertime"].ToParseStr()).ToString("dd.MM.yyyy");
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
        Types.ProsesType val = _db.DeleteWateringSystems(id: _id);
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
            val = _db.WateringSystemsInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                ModelID: cmmodels.Value.ToParseInt(),
                WateringSystemName: txtname.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr(),
                TechniqueSituationID: cmbWateringSystemSituationName.Value.ToParseInt(),                
                RegisterTime: dtRegstrDate.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.WateringSystemsUpdate(WateringSystemID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                ModelID: cmmodels.Value.ToParseInt(),
                WateringSystemName: txtname.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr(),
                TechniqueSituationID: cmbWateringSystemSituationName.Value.ToParseInt(),
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

   
}