using DevExpress.Web;
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
       
        txtproductname.Text = "";
        txtnotes.Text = "";
        lblPopError.Text = "";
        txtcode.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtproduct = _db.GetProducts();
        if (dtproduct != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtproduct;
            Grid.DataBind();
        }
    }
    void modelcomponentload()
    {
        DataTable dt6 = _db.GetModelsByProductTypeID(ddlproducttype.SelectedValue.ToParseInt());
        ddlmodel.DataValueField = "ModelID";
        ddlmodel.DataTextField = "ModelName";
        ddlmodel.DataSource = dt6;
        ddlmodel.DataBind();
        ddlmodel.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlmodel.Items.Insert(1, new ListItem("Yoxdur", "0"));
        ddlmodel.SelectedIndex = 0;
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

        DataTable dt7 = _db.GetProductTypes();
        ddlproducttype.DataValueField = "ProductTypeID";
        ddlproducttype.DataTextField = "ProductTypeName";
        ddlproducttype.DataSource = dt7;
        ddlproducttype.DataBind();
        ddlproducttype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlproducttype.SelectedIndex = 0;

       






        //modelcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductById(id: id);

        txtproductname.Text = dt.Rows[0]["ProductsName"].ToParseStr();
        ddlproducttype.SelectedValue = dt.Rows[0]["ProductTypeID"].ToParseStr();
   
       
        modelcomponentload();

        if (dt.Rows[0]["ModelID"].ToParseStr() != "")
        {
            ddlmodel.SelectedValue = dt.Rows[0]["ModelID"].ToParseStr();
        }
        

        
        txtcode.Text = dt.Rows[0]["Code"].ToParseStr();
        if (dt.Rows[0]["UnitMeasurementID"].ToParseStr() != "")
        {
            ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        }
  
        txtnotes.Text = dt.Rows[0]["Notes"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteProduct(id: _id);
        _loadGridFromDb();
    }
    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
        componentsload();
        modelcomponentload();
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
            val = _db.ProductInsert(
                ProductsName: txtproductname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt(),
             
                ModelID: ddlmodel.SelectedValue.ToParseInt(),
                Code: txtcode.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.ProductUpdate(ProductID: btnSave.CommandArgument.ToParseInt(),
                ProductsName: txtproductname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt(),
               
                ModelID: ddlmodel.SelectedValue.ToParseInt(),
                Code: txtcode.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
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


    protected void ddlproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelcomponentload();
    }
    protected void lnkbtnproducttype_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "addproducttype":
                btnProductTypeSave.CommandName = "insert";
                PopupProductType.ShowOnPageLoad = true;
                break;
        }        
    }
    protected void btnProductTypeCancel_Click(object sender, EventArgs e)
    {
        PopupProductType.ShowOnPageLoad = false;
    }

    protected void btnProductTypeSave_Click(object sender, EventArgs e)
    {
        lblPopErrorProducttype.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;
        if (btnProductTypeSave.CommandName == "insert")
        {
            val = _db.ProductTypeInsert(
                ProductTypeName: txtproducttypename.Text.ToParseStr()
                ) ;
        }
        

        if (val == Types.ProsesType.Error)
        {
            lblPopErrorProducttype.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        componentsload();
        PopupProductType.ShowOnPageLoad = false;
    }

    protected void lnkbtnModels_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "addModels":
                btnModelsSave.CommandName = "insert";
                PopupModels.ShowOnPageLoad = true;
                break;
        }
    }

    protected void btnModelsCancel_Click(object sender, EventArgs e)
    {
        PopupModels.ShowOnPageLoad = false;
    }

    protected void btnModelsSave_Click(object sender, EventArgs e)
    {
        ErrorModel.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;
        if (btnModelsSave.CommandName == "insert")
        {
            val = _db.ModelInsert(
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt(),
                ModelName: txtmodelname.Text.ToParseStr()

                );
        }
        

        if (val == Types.ProsesType.Error)
        {
            ErrorModel.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        modelcomponentload();
        PopupModels.ShowOnPageLoad = false;
    }
}