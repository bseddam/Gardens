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
        cmbregistertime.Text = "";
        txtproductname.Text = "";
        txtprice.Text = "";
        txtpricediscount.Text = "";
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
        DataTable dt6 = _db.GetModelByID(ddlbrand.SelectedValue.ToParseInt());
        ddlmodel.DataValueField = "ModelID";
        ddlmodel.DataTextField = "ModelName";
        ddlmodel.DataSource = dt6;
        ddlmodel.DataBind();
        ddlmodel.Items.Insert(0, new ListItem("Seçin", "-1"));
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

        DataTable dt6 = _db.GetBrands();
        ddlbrand.DataValueField = "BrandID";
        ddlbrand.DataTextField = "BrandName";
        ddlbrand.DataSource = dt6;
        ddlbrand.DataBind();
        ddlbrand.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlbrand.SelectedIndex = 0;


        DataTable dt7 = _db.GetProductTypes();
        ddlproducttype.DataValueField = "ProductTypeID";
        ddlproducttype.DataTextField = "ProductTypeName";
        ddlproducttype.DataSource = dt7;
        ddlproducttype.DataBind();
        ddlproducttype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlproducttype.SelectedIndex = 0;




        modelcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductById(id: id);
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime.Text = "";
        }
        txtproductname.Text = dt.Rows[0]["ProductsName"].ToParseStr();
        ddlproducttype.SelectedValue = dt.Rows[0]["ProductTypeID"].ToParseStr();
        ddlbrand.SelectedValue = dt.Rows[0]["BrandID"].ToParseStr();
        modelcomponentload();
        ddlmodel.SelectedValue = dt.Rows[0]["ModelID"].ToParseStr();
        txtcode.Text = dt.Rows[0]["Code"].ToParseStr();
        ddlunitmeasurement.SelectedValue = dt.Rows[0]["UnitMeasurementID"].ToParseStr();
        txtprice.Text = dt.Rows[0]["Price"].ToParseStr();
        txtpricediscount.Text = dt.Rows[0]["PriceDiscount"].ToParseStr();
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
            val = _db.ProductInsert(RegisterTime: cmbregistertime.Text.ToParseStr(),
                ProductsName: txtproductname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt(),
                BrandID: ddlbrand.SelectedValue.ToParseInt(),
                ModelID: ddlmodel.SelectedValue.ToParseInt(),
                Code: txtcode.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Price: txtprice.Text.ToParseStr(),
                PriceDiscount: txtpricediscount.Text.ToParseStr(),
                Notes: txtnotes.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.ProductUpdate(ProductID: btnSave.CommandArgument.ToParseInt(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                ProductsName: txtproductname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt(),
                BrandID: ddlbrand.SelectedValue.ToParseInt(),
                ModelID: ddlmodel.SelectedValue.ToParseInt(),
                Code: txtcode.Text.ToParseStr(),
                UnitMeasurementID: ddlunitmeasurement.SelectedValue.ToParseInt(),
                Price: txtprice.Text.ToParseStr(),
                PriceDiscount: txtpricediscount.Text.ToParseStr(),
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

   


   

    protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelcomponentload();
    }
}