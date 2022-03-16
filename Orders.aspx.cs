using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Orders : System.Web.UI.Page
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
      
        txtProductSize.Text = "";
        cmbregistertime.Text = "";

    }
    void _loadGridFromDb()
    {
        //DataTable dt = _db.GetProductStockInputOutput();
        //if (dt != null)
        //{
        //    Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
        //    Grid.DataSource = dt;
        //    Grid.DataBind();
        //}
    }
   
    void productcomponentload()
    {
        cmbProducts.Items.Clear();
        DataTable dt6 = _db.GetProductByModelProductId(cmbmodel.Value.ToParseInt(), cmbproducttype.Value.ToParseInt());
        cmbProducts.ValueField = "ProductID";
        cmbProducts.TextField = "ProductsName";
        cmbProducts.DataSource = dt6;
        cmbProducts.DataBind();
        cmbProducts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbProducts.SelectedIndex = 0;
    }
    void modelcomponentload()
    {
        cmbmodel.Items.Clear();
        DataTable dt4x = _db.GetModelsByProductTypeID(cmbproducttype.Value.ToParseInt().ToParseInt());
        cmbmodel.ValueField = "ModelID";
        cmbmodel.TextField = "ModelName";
        cmbmodel.DataSource = dt4x;
        cmbmodel.DataBind();
        cmbmodel.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbmodel.SelectedIndex = 0;
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



    
        cmbproducttype.Items.Clear();
        DataTable dt2 = _db.GetProductTypes();
        cmbproducttype.ValueField = "ProductTypeID";
        cmbproducttype.TextField = "ProductTypeName";
        cmbproducttype.DataSource = dt2;
        cmbproducttype.DataBind();
        cmbproducttype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttype.SelectedIndex = 0;



    




        productcomponentload();
        modelcomponentload();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductStockInputOutputByID(id: id);
        componentsload();
        cmbstock.Value = dt.Rows[0]["StockID"].ToParseStr();
       
        cmbproducttype.Value = dt.Rows[0]["ProductTypeID"].ToParseStr();

       
        

        modelcomponentload();
        cmbmodel.Value = dt.Rows[0]["ModelID"].ToParseStr();
        productcomponentload();

        cmbProducts.Value = dt.Rows[0]["ProductID"].ToParseStr();

        
        txtNote.Text = dt.Rows[0]["Notes"].ToParseStr();
        
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
       // Types.ProsesType val = _db.DeleteProductStockInputOutput(id: _id);
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
            //val = _db.ProductStockInputOutputInsert(
            //    StockID: cmbstock.Value.ToParseInt(),
             
            //    ProductID: cmbProducts.Value.ToParseInt(),
            //    ProductSize: txtProductSize.Text.ToParseStr(),
               
            //    RegisterTime: cmbregistertime.Text.ToParseStr(),
            //    Notes: txtNote.Text.ToParseStr()
            //    );
        }
        else
        {
            //val = _db.ProductStockInputOutputUpdate(ProductStockInputOutputID: btnSave.CommandArgument.ToParseInt(),
            //    StockID: cmbstock.Value.ToParseInt(),
                
            //    ProductID: cmbProducts.Value.ToParseInt(),
            //    ProductSize: txtProductSize.Text.ToParseStr(),
               
            //    RegisterTime: cmbregistertime.Text.ToParseStr(),
            //    Notes: txtNote.Text.ToParseStr()
            //    );
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

   

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        modelcomponentload();
        productcomponentload();
    }

    protected void cmbmodel_SelectedIndexChanged(object sender, EventArgs e)
    {
        productcomponentload();
    }
}