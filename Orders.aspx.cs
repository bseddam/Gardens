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
        _loadGridFromDbProduct(id);
    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {

    }

    protected void lnkEditProduct_Click(object sender, EventArgs e)
    {

    }

    protected void lnkDeleteProduct_Click(object sender, EventArgs e)
    {

    }

    protected void btnProductSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnProductCancel_Click(object sender, EventArgs e)
    {

    }

    protected void cmbproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbmodel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}