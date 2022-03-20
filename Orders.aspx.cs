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
        DTInvoice.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetOrderInvoice();
        if (dt != null)
        {
            GridOrderInvoice.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            GridOrderInvoice.DataSource = dt;
            GridOrderInvoice.DataBind();
        }
    }

    void _loadGridFromDbProduct(int id)
    {
        DataTable dt = _db.GetOrderProductByIDInvoice(id);
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
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

        cmbStatus.Items.Clear();
        DataTable dt2 = _db.GetInvoiceStatus();
        cmbStatus.ValueField = "InvoiceStatusID";
        cmbStatus.TextField = "InvoiceStatusName";
        cmbStatus.DataSource = dt2;
        cmbStatus.DataBind();
        cmbStatus.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbStatus.SelectedIndex = 0;
    }

    void componentsloadProduct()
    {
        cmbproducttype.Items.Clear();
        DataTable d2t1 = _db.GetProductTypes();
        cmbproducttype.ValueField = "ProductTypeID";
        cmbproducttype.TextField = "ProductTypeName";
        cmbproducttype.DataSource = d2t1;
        cmbproducttype.DataBind();
        cmbproducttype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbproducttype.SelectedIndex = 0;

        cmbmodel.Items.Clear();
        DataTable dt2 = _db.GetModels();
        cmbmodel.ValueField = "ModelID";
        cmbmodel.TextField = "ModelName";
        cmbmodel.DataSource = dt2;
        cmbmodel.DataBind();
        cmbmodel.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbmodel.SelectedIndex = 0;
    }

    void ClearComponentsProduct()
    {
        txtProductSize.Text = "";
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetOrderInvoiceByID(id: id);
        componentsload();
        cmbstock.Value = dt.Rows[0]["StockID"].ToParseStr();
        cmbStatus.Value = dt.Rows[0]["InvoiceStatusID"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["InvoiceDate"].ToParseStr(), out datevalue))
        {
            DTInvoice.Text = DateTime.Parse(dt.Rows[0]["InvoiceDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            DTInvoice.Text = "";
        }

        btnInvoiceSave.CommandName = "update";
        btnInvoiceSave.CommandArgument = id.ToString();
        PopupOrderInvoice.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
         Types.ProsesType val = _db.DeleteOrderInvoice(id: _id);
        _loadGridFromDb();
    }

    protected void btnNewInvoice_Click(object sender, EventArgs e)
    {
        componentsload();
        ClearComponents();
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "add":
                btnInvoiceSave.CommandName = "insert";
                PopupOrderInvoice.ShowOnPageLoad = true;
                break;
        }
    }

    protected void btnInvoiceCancel_Click(object sender, EventArgs e)
    {
        PopupOrderInvoice.ShowOnPageLoad = false;
    }

    protected void btnInvoiceSave_Click(object sender, EventArgs e)
    {
        lblErrorInvoice.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;


        if (btnInvoiceSave.CommandName == "insert")
        {
            val = _db.OrderInvoiceInsert(
                StockID: cmbstock.Value.ToParseInt(),
                InvoiceDate: DTInvoice.Text.ToParseStr(),
                InvoiceStatusID: cmbStatus.Value.ToParseInt()
                );
        }
        else
        {
            val = _db.OrderInvoiceUpdate(OrderInvoiceID:btnInvoiceSave.CommandArgument.ToParseInt(),
                StockID: cmbstock.Value.ToParseInt(),
                InvoiceDate: DTInvoice.Text.ToParseStr(),
                InvoiceStatusID: cmbStatus.Value.ToParseInt()
                );            
        }

        if (val == Types.ProsesType.Error)
        {
            lblErrorInvoice.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        _loadGridFromDb();
        PopupOrderInvoice.ShowOnPageLoad = false;
    }

    protected void lnkProducts_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        int id = btn.CommandArgument.ToParseInt();
        Session["idInvoice"] = id;
        _loadGridFromDbProduct(id);
    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        componentsloadProduct();
        ClearComponentsProduct();
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "add":
                btnProductSave.CommandName = "insert";
                popupEditProduct.ShowOnPageLoad = true;
                break;
        }
    }

    protected void lnkEditProduct_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetOrderProductByID(id: id);
        componentsloadProduct();        
        cmbproducttype.Value = dt.Rows[0]["ProductTypeID"].ToParseStr();
        cmbmodel.Value = dt.Rows[0]["ModelID"].ToParseStr();
        cmbmodel_SelectedIndexChanged(null,null);
        cmbProducts.Value = dt.Rows[0]["ProductID"].ToParseStr();
        txtProductSize.Text = dt.Rows[0]["ProductSize"].ToParseStr();

        btnProductSave.CommandName = "update";
        btnProductSave.CommandArgument = id.ToString();
        popupEditProduct.ShowOnPageLoad = true;
    }

    protected void lnkDeleteProduct_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteOrderProduct(id: _id);
        _loadGridFromDbProduct(Session["idInvoice"].ToParseInt());
    }

    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        lblPopError.Text = "ffff";
        Types.ProsesType val = Types.ProsesType.Error;


        if (btnProductSave.CommandName == "insert")
        {
            val = _db.OrderProductInsert(
                ProductID: cmbProducts.Value.ToParseInt(),
                OrderInvoiceID: Session["idInvoice"].ToParseInt(),
                ProductSize: txtProductSize.Text.ToParseStr()                
                );
        }
        else
        {
            val = _db.OrderProductUpdate(OrderProductID: btnProductSave.CommandArgument.ToParseInt(),
                ProductID: cmbProducts.Value.ToParseInt(),
                OrderInvoiceID: Session["idInvoice"].ToParseInt(),
                ProductSize: txtProductSize.Text.ToParseStr()
                );
        }

        if (val == Types.ProsesType.Error)
        {
            lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        _loadGridFromDbProduct(Session["idInvoice"].ToParseInt());
        popupEditProduct.ShowOnPageLoad = false;
    }

    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        popupEditProduct.ShowOnPageLoad = false;
    }

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProducts.Items.Clear();
        DataTable dt3 = _db.GetProductByModelProductId(cmbmodel.Value.ToParseInt(), cmbproducttype.Value.ToParseInt());
        cmbProducts.ValueField = "ProductID";
        cmbProducts.TextField = "ddlname";
        cmbProducts.DataSource = dt3;
        cmbProducts.DataBind();
        cmbProducts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbProducts.SelectedIndex = 0;
    }

    protected void cmbmodel_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbProducts.Items.Clear();
        DataTable dt3 = _db.GetProductByModelProductId(cmbmodel.Value.ToParseInt(), cmbproducttype.Value.ToParseInt());
        cmbProducts.ValueField = "ProductID";
        cmbProducts.TextField = "ddlname";
        cmbProducts.DataSource = dt3;
        cmbProducts.DataBind();
        cmbProducts.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbProducts.SelectedIndex = 0;
    }
}