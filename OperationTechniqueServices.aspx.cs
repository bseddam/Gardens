using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationTechniqueServices : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtAmount.Text = "";
        txtNote.Text = "";
        txtodometer.Text = "";
        txtPrice.Text = "";
        txtProductSize.Text = "";
        txtServicePrice.Text = "";
        cmbregistertime.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable dt = _db.GetTechniquesServices();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }
    void componentsload()
    {

        DataTable dt21x = _db.GetProductTypes();
        cmbproducttypetex.ValueField = "ProductTypeID";
        cmbproducttypetex.TextField = "ProductTypeName";
        cmbproducttypetex.DataSource = dt21x;
        cmbproducttypetex.DataBind();
        cmbproducttypetex.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttypetex.SelectedIndex = 0;


        DataTable dt2x = _db.GetProductTypes();
        cmbproducttype.ValueField = "ProductTypeID";
        cmbproducttype.TextField = "ProductTypeName";
        cmbproducttype.DataSource = dt2x;
        cmbproducttype.DataBind();
        cmbproducttype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttype.SelectedIndex = 0;


        DataTable dt1 = _db.GetGardens();
        cmbGarden.ValueField = "GardenID";
        cmbGarden.TextField = "GardenName";
        cmbGarden.DataSource = dt1;
        cmbGarden.DataBind();
        cmbGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbGarden.SelectedIndex = 0;

        DataTable dt3 = _db.GetUnitMeasurements();
        cmbUnitMeasurement.ValueField = "UnitMeasurementID";
        cmbUnitMeasurement.TextField = "UnitMeasurementName";
        cmbUnitMeasurement.DataSource = dt3;
        cmbUnitMeasurement.DataBind();
        cmbUnitMeasurement.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbUnitMeasurement.SelectedIndex = 0;


        DataTable dt2 = _db.GetWorkByWorkTypeId(5);
        cmbWork.ValueField = "WorkID";
        cmbWork.TextField = "WorkName";
        cmbWork.DataSource = dt2;
        cmbWork.DataBind();
        cmbWork.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbWork.SelectedIndex = 0;

       


    

        modeltechniquecomponentload();
        techniquecomponentload();

        modelsparepartscomponentload();
        sparepartscomponentload();
    }
    void modeltechniquecomponentload()
    {
        cmbmodel.Items.Clear();
        DataTable dt4x = _db.GetModelsByProductTypeID(cmbproducttypetex.Value.ToParseInt().ToParseInt());
        cmbmodel.ValueField = "ModelID";
        cmbmodel.TextField = "ModelName";
        cmbmodel.DataSource = dt4x;
        cmbmodel.DataBind();
        cmbmodel.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbmodel.SelectedIndex = 0;
    }
   
    void modelsparepartscomponentload()
    {
        DataTable dt6 = _db.GetModelsByProductTypeID(cmbproducttype.Value.ToParseInt().ToParseInt());
        cmbmodelspareparts.ValueField = "ModelID";
        cmbmodelspareparts.TextField = "ModelName";
        cmbmodelspareparts.DataSource = dt6;
        cmbmodelspareparts.DataBind();
        cmbmodelspareparts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbmodelspareparts.SelectedIndex = 0;
    }
    void techniquecomponentload()
    {
        DataTable dt6 = _db.GetTechniquesByModelId(cmbmodel.Value.ToParseInt());
        cmTechnique.ValueField = "TechniqueID";
        cmTechnique.TextField = "TechniquesName";
        cmTechnique.DataSource = dt6;
        cmTechnique.DataBind();
        cmTechnique.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmTechnique.SelectedIndex = 0;
    }
    void sparepartscomponentload()
    {
        DataTable dt6 = _db.GetProductByModelProductId(cmbmodelspareparts.Value.ToParseInt(),cmbproducttype.Value.ToParseInt());
        cmbspareparts.ValueField = "ProductID";
        cmbspareparts.TextField = "ProductsName";
        cmbspareparts.DataSource = dt6;
        cmbspareparts.DataBind();
        cmbspareparts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbspareparts.SelectedIndex = 0;
    }


    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetTechniquesServiceById(id: id);

        cmbGarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmbWork.Value = dt.Rows[0]["WorkID"].ToParseStr();

        cmbproducttypetex.Value = dt.Rows[0]["ProductTypeID1"].ToParseStr();
        cmbproducttype.Value = dt.Rows[0]["ProductTypeID2"].ToParseStr();
        cmbUnitMeasurement.Value = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        txtProductSize.Text= dt.Rows[0]["ProductSize"].ToParseStr();
        txtPrice.Text = dt.Rows[0]["Price"].ToParseStr();
        txtAmount.Text = dt.Rows[0]["Amount"].ToParseStr();
        txtServicePrice.Text = dt.Rows[0]["ServicePrice"].ToParseStr();
        txtNote.Text = dt.Rows[0]["Note"].ToParseStr();
        txtodometer.Text = dt.Rows[0]["Odometer"].ToParseStr();
        modeltechniquecomponentload();
        cmbmodel.Value = dt.Rows[0]["modeltexid"].ToParseStr();
        techniquecomponentload();
        cmTechnique.Value = dt.Rows[0]["TechniqueID"].ToParseStr();

      


        modelsparepartscomponentload();
        cmbmodelspareparts.Value = dt.Rows[0]["modelprodid"].ToParseStr();

        sparepartscomponentload();
        cmbspareparts.Value = dt.Rows[0]["ProductID"].ToParseStr();

        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
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

     
        if (btnSave.CommandName == "insert")
        {
            val = _db.InsertTechniquesService(
                TechniqueID: cmTechnique.Value.ToParseInt(),
                GardenID: cmbGarden.Value.ToParseInt(),
                WorkID: cmbWork.Value.ToParseInt(),
                ProductID: cmbspareparts.Value.ToParseInt(),
                Price: txtPrice.Text.ToParseStr(),
                ProductSize: txtProductSize.Text.ToParseInt(),
                UnitMeasurementID: cmbUnitMeasurement.Value.ToParseInt(),
                Amount: txtAmount.Text.ToParseStr(),
                ServicePrice: txtServicePrice.Text.ToParseStr(),
                Odometer: txtodometer.Text.ToParseInt(),
                Note: txtNote.Text.ToParseStr(),
                RegisterTime: cmbregistertime.Text.ToParseStr()
                );
        }
        else
        {

            val = _db.UpdateTechniquesService(TechniqueServiceID:btnSave.CommandArgument.ToParseInt(),
      TechniqueID: cmTechnique.Value.ToParseInt(),
     GardenID: cmbGarden.Value.ToParseInt(),
     WorkID: cmbWork.Value.ToParseInt(),
     ProductID: cmbspareparts.Value.ToParseInt(),
     Price: txtPrice.Text.ToParseStr(),
     ProductSize: txtProductSize.Text.ToParseInt(),
     UnitMeasurementID: cmbUnitMeasurement.Value.ToParseInt(),
     Amount: txtAmount.Text.ToParseStr(),
     ServicePrice: txtServicePrice.Text.ToParseStr(),
     Odometer: txtodometer.Text.ToParseInt(),
      Note: txtNote.Text.ToParseStr(),
     RegisterTime: cmbregistertime.Text.ToParseStr()
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

    

    protected void cmbmodel_SelectedIndexChanged(object sender, EventArgs e)
    {
        techniquecomponentload();
    }

   

    protected void cmbmodelspareparts_SelectedIndexChanged(object sender, EventArgs e)
    {
        sparepartscomponentload();
    }



    protected void cmbproducttypetex_SelectedIndexChanged(object sender, EventArgs e)
    {
        modeltechniquecomponentload();
        techniquecomponentload();
    }

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelsparepartscomponentload();
    }
}