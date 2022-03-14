using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationStock : System.Web.UI.Page
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
        txtAmountDiscount.Text = "";
        txtNote.Text = "";
        txtPrice.Text = "";
        txtPriceDiscount.Text = "";
        txtProductSize.Text = "";
        cmbregistertime.Text = "";
       
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetProductStockInputOutput();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }
    void Reasoncomponentload()
    {
        cmbStockOperationReason.Items.Clear();
        DataTable dt3 = _db.GetStockOperationReasonsByProductOperationTypeID
            (cmbProductOperationType.Value.ToParseInt());
        cmbStockOperationReason.ValueField = "StockOperationReasonID";
        cmbStockOperationReason.TextField = "ReasonName";
        cmbStockOperationReason.DataSource = dt3;
        cmbStockOperationReason.DataBind();
        cmbStockOperationReason.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbStockOperationReason.SelectedIndex = 0;

    }


    void brandcomponentload()
    {
        DataTable dt6 = _db.GetBrandsByProductTypeID(cmbproducttype.Value.ToParseInt());
        cmbbrand.ValueField = "BrandID";
        cmbbrand.TextField = "BrandName";
        cmbbrand.DataSource = dt6;
        cmbbrand.DataBind();
        cmbbrand.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbbrand.Items.Insert(1, new ListEditItem("Yoxdur", "0"));
        cmbbrand.SelectedIndex = 0;
    }

    void modelcomponentload()
    {
        DataTable dt6 = _db.GetModelsByBrandID(cmbbrand.Value.ToParseInt());
        cmbmodel.ValueField = "ModelID";
        cmbmodel.TextField = "ModelName";
        cmbmodel.DataSource = dt6;
        cmbmodel.DataBind();
        cmbmodel.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbmodel.Items.Insert(1, new ListEditItem("Yoxdur", "0"));
        cmbmodel.SelectedIndex = 0;
    }

    void productcomponentload()
    {
        cmbProducts.Items.Clear();
        DataTable dt6 = _db.GetProductByModelProductId(cmbmodel.Value.ToParseInt(),cmbproducttype.Value.ToParseInt());
        cmbProducts.ValueField = "ProductID";
        cmbProducts.TextField = "ProductsName";
        cmbProducts.DataSource = dt6;
        cmbProducts.DataBind();
        cmbProducts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbProducts.SelectedIndex = 0;
    }


    void componentsload()
    {

        cmbstock.Items.Clear();
        DataTable d2t1 = _db.GetStocks();
        cmbstock.ValueField = "StockID";
        cmbstock.TextField = "StockName";
        cmbstock.DataSource = d2t1;
        cmbstock.DataBind();
        cmbstock.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbstock.SelectedIndex = 0;



        cmbProductOperationType.Items.Clear();
        DataTable dt1 = _db.GetProductOperationTypes();
        cmbProductOperationType.ValueField = "ProductOperationTypeID";
        cmbProductOperationType.TextField = "ProductOperationTypeName";
        cmbProductOperationType.DataSource = dt1;
        cmbProductOperationType.DataBind();
        cmbProductOperationType.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbProductOperationType.SelectedIndex = 0;



        cmbproducttype.Items.Clear();
        DataTable dt2 = _db.GetProductTypes();
        cmbproducttype.ValueField = "ProductTypeID";
        cmbproducttype.TextField = "ProductTypeName";
        cmbproducttype.DataSource = dt2;
        cmbproducttype.DataBind();
        cmbproducttype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttype.SelectedIndex = 0;



        cmbUnitMeasurement.Items.Clear();
        DataTable dt3 = _db.GetUnitMeasurements();
        cmbUnitMeasurement.ValueField = "UnitMeasurementID";
        cmbUnitMeasurement.TextField = "UnitMeasurementName";
        cmbUnitMeasurement.DataSource = dt3;
        cmbUnitMeasurement.DataBind();
        cmbUnitMeasurement.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbUnitMeasurement.SelectedIndex = 0;




        Reasoncomponentload();
        brandcomponentload();
        modelcomponentload();
        productcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductStockInputOutputByID(id: id);
        componentsload();
        cmbstock.Value = dt.Rows[0]["StockID"].ToParseStr();
        cmbProductOperationType.Value = dt.Rows[0]["ProductOperationTypeID"].ToParseStr();
        cmbproducttype.Value = dt.Rows[0]["ProductTypeID"].ToParseStr();

        if (dt.Rows[0]["UnitMeasurementID"].ToParseStr() != "")
        {
            cmbUnitMeasurement.Value = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        }
        Reasoncomponentload();
        cmbStockOperationReason.Value = dt.Rows[0]["StockOperationReasonID"].ToParseStr();
        brandcomponentload();
        
        cmbbrand.Value = dt.Rows[0]["BrandID"].ToParseStr();
        modelcomponentload();
       
        cmbmodel.Value = dt.Rows[0]["ModelID"].ToParseStr();
        productcomponentload();
       
        cmbProducts.Value = dt.Rows[0]["ProductID"].ToParseStr();

        txtAmount.Text = dt.Rows[0]["Amount"].ToParseStr();
        txtAmountDiscount.Text = dt.Rows[0]["AmountDiscount"].ToParseStr();
        txtNote.Text = dt.Rows[0]["Notes"].ToParseStr();
        txtPrice.Text = dt.Rows[0]["Price"].ToParseStr();
        txtPriceDiscount.Text = dt.Rows[0]["PriceDiscount"].ToParseStr();
        txtProductSize.Text = dt.Rows[0]["ProductSize"].ToParseStr();




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
        Types.ProsesType val = _db.DeleteProductStockInputOutput(id: _id);
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
            val = _db.ProductStockInputOutputInsert(
                StockID: cmbstock.Value.ToParseInt(),
                ProductOperationTypeID: cmbProductOperationType.Value.ToParseInt(),
                StockOperationReasonID: cmbStockOperationReason.Value.ToParseInt(),
                ProductID: cmbProducts.Value.ToParseInt(),
                ProductSize: txtProductSize.Text.ToParseStr(),
                Price: txtPrice.Text.ToParseStr(),
                PriceDiscount: txtPriceDiscount.Text.ToParseStr(),
                Amount: txtAmount.Text.ToParseStr(),
                AmountDiscount: txtAmountDiscount.Text.ToParseStr(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                Notes: txtNote.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.ProductStockInputOutputUpdate(ProductStockInputOutputID: btnSave.CommandArgument.ToParseInt(),
                StockID: cmbstock.Value.ToParseInt(),
                ProductOperationTypeID: cmbProductOperationType.Value.ToParseInt(),
                StockOperationReasonID: cmbStockOperationReason.Value.ToParseInt(),
                ProductID: cmbProducts.Value.ToParseInt(),
                ProductSize: txtProductSize.Text.ToParseStr(),
                Price: txtPrice.Text.ToParseStr(),
                PriceDiscount: txtPriceDiscount.Text.ToParseStr(),
                Amount: txtAmount.Text.ToParseStr(),
                AmountDiscount: txtAmountDiscount.Text.ToParseStr(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                Notes: txtNote.Text.ToParseStr()
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

    protected void cmbProductOperationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reasoncomponentload();
    }

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        brandcomponentload();
        modelcomponentload();
        productcomponentload();
    }

    protected void cmbbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelcomponentload();
        productcomponentload();
    }

    protected void cmbmodel_SelectedIndexChanged(object sender, EventArgs e)
    {
        productcomponentload();
    }
}