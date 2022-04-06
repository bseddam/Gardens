using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationStock : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridInvoiceInput();
        pnlprint.Visible = false;
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtAmount.Text = "";
        txtAmountDiscount.Text = "";
        txtNote.Text = "";
        txtnotesinv.Text = "";
        txtPrice.Text = "";
        txtPriceDiscount.Text = "";
        txtProductSize.Text = "";
        cmbregistertime.Text = "";
        cmbregistertime1.Text = "";

    }
    void _loadGridFromDb(int id)
    {
        DataTable dt = _db.GetProductStockInputByInvoiceID(id);
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }

    void _loadGridInvoiceInput()
    {
        DataTable dt1 = _db.GetInvoiceInput();
        if (dt1 != null)
        {
            GridInvoice.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            GridInvoice.DataSource = dt1;
            GridInvoice.DataBind();
        }
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
        cmbmodel.Items.Insert(1, new ListEditItem("Yoxdur", "0"));
        cmbmodel.SelectedIndex = 0;
    }


    void componentsloadinvoice()
    {



        cmbstock.Items.Clear();
        DataTable d2t1 = _db.GetStocks();
        cmbstock.ValueField = "StockID";
        cmbstock.TextField = "StockName";
        cmbstock.DataSource = d2t1;
        cmbstock.DataBind();
        cmbstock.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbstock.SelectedIndex = 0;

        cmbInvoiceStatus.Items.Clear();
        DataTable dt23 = _db.GetInvoiceStatus();
        cmbInvoiceStatus.ValueField = "InvoiceStatusID";
        cmbInvoiceStatus.TextField = "InvoiceStatusName";
        cmbInvoiceStatus.DataSource = dt23;
        cmbInvoiceStatus.DataBind();
        cmbInvoiceStatus.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbInvoiceStatus.SelectedIndex = 0;



        cmbStockOperationReason.Items.Clear();
        DataTable dt3 = _db.GetStockOperationReasonsByProductOperationTypeID(1);
        cmbStockOperationReason.ValueField = "StockOperationReasonID";
        cmbStockOperationReason.TextField = "ReasonName";
        cmbStockOperationReason.DataSource = dt3;
        cmbStockOperationReason.DataBind();
        cmbStockOperationReason.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbStockOperationReason.SelectedIndex = 0;

    }

        void componentsload()
    {



        





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
    protected void lnkProducts_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        int id = btn.CommandArgument.ToParseInt();
        Session["InvoiceStockID"] = id;
        _loadGridFromDb(id);
        
    }
    protected void lnkEditInvoice_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductStockInputByID(id: id);
        componentsloadinvoice();
        cmbstock.Value = dt.Rows[0]["StockID"].ToParseStr();
        cmbInvoiceStatus.Value = dt.Rows[0]["InvoiceStatusID"].ToParseStr();



        cmbStockOperationReason.Value = dt.Rows[0]["StockOperationReasonID"].ToParseStr();



        txtnotesinv.Text = dt.Rows[0]["Notes"].ToParseStr();





        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
        {
            cmbregistertime1.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            cmbregistertime1.Text = "";
        }




        btnInvoice.CommandName = "update";
        btnInvoice.CommandArgument = id.ToString();
        popupVoice.ShowOnPageLoad = true;
    }
    protected void lnkDeleteInvoice_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteProductStockInput(id: _id);
        _loadGridInvoiceInput();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetProductStockInputOutputByID(id: id);
        componentsload();
        
        cmbproducttype.Value = dt.Rows[0]["ProductTypeID"].ToParseStr();



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
        _loadGridFromDb(Session["InvoiceStockID"].ToParseInt());
    }
    protected void LnkInvoice_Click(object sender, EventArgs e)
    {

        componentsloadinvoice();
        ClearComponents();
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "add":
                btnInvoice.CommandName = "insert";
                popupVoice.ShowOnPageLoad = true;
                break;
        }
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        int id = btn.CommandArgument.ToParseInt();


        pnlprint.Visible = true;
        DataTable dt1 = _db.GetSumProductStockInputByInvoiceID(id);
        if(dt1!=null)
        {
            if(dt1.Rows.Count > 0)
            {
                lblProductSize.Text = dt1.Rows[0]["ProductSize"].ToParseStr();
                lblAmount.Text = dt1.Rows[0]["Amount"].ToParseStr();
                lblAmountDiscount.Text = dt1.Rows[0]["AmountDiscount"].ToParseStr();
            }
            else
            {
                lblProductSize.Text = "";
                lblAmount.Text = "";
                lblAmountDiscount.Text = "";
            }
        }
        else
        {
            lblProductSize.Text = "";
            lblAmount.Text = "";
            lblAmountDiscount.Text = "";
        }
        
        DataTable dt = _db.GetProductStockInputByInvoiceID(id);
        rpprint.DataSource = dt;
        rpprint.DataBind();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "PrintPanel()", false);


        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlprint.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(sender as Control, this.GetType(), "GridPrint", sb.ToString(), false);
        pnlprint.Visible = false;

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
        DataTable dt = _db.GetProductStockInputByID(id: Session["InvoiceStockID"].ToParseInt());
        
        if (btnSave.CommandName == "insert")
        {
            val = _db.ProductStockInputOutputInsert(
                StockID: dt.Rows[0]["StockID"].ToParseInt(),
                InvoiceStockID: Session["InvoiceStockID"].ToParseInt(),
                ProductOperationTypeID: 1,
                StockOperationReasonID: dt.Rows[0]["StockOperationReasonID"].ToParseInt(),
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
                StockID: dt.Rows[0]["StockID"].ToParseInt(),
                InvoiceStockID: Session["InvoiceStockID"].ToParseInt(),
                ProductOperationTypeID: 1,
                StockOperationReasonID: dt.Rows[0]["StockOperationReasonID"].ToParseInt(),
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

        _loadGridFromDb(Session["InvoiceStockID"].ToParseInt());
        popupEdit.ShowOnPageLoad = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        popupEdit.ShowOnPageLoad = false;
    }




    protected void btninvoice_Click(object sender, EventArgs e)
    {
        lblerrorinv.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;




        if (btnInvoice.CommandName == "insert")
        {
            val = _db.ProductStockInputInsert(
                StockID: cmbstock.Value.ToParseInt(),
                ProductOperationTypeID: 1,
                StockOperationReasonID: cmbStockOperationReason.Value.ToParseInt(),
                InvoiceStatusID: cmbInvoiceStatus.Value.ToParseInt(),
                RegisterTime: cmbregistertime1.Text.ToParseStr(),
                Notes: txtnotesinv.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.ProductStockInputUpdate(InvoiceStockID: btnInvoice.CommandArgument.ToParseInt(),
                StockID: cmbstock.Value.ToParseInt(),
                ProductOperationTypeID: 1,
                StockOperationReasonID: cmbStockOperationReason.Value.ToParseInt(),
                InvoiceStatusID: cmbInvoiceStatus.Value.ToParseInt(),
                RegisterTime: cmbregistertime1.Text.ToParseStr(),
                Notes: txtnotesinv.Text.ToParseStr()
                );
        }

        if (val == Types.ProsesType.Error)
        {
            lblerrorinv.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        _loadGridInvoiceInput();
        popupVoice.ShowOnPageLoad = false;
    }
    protected void btninvoiceCancel_Click(object sender, EventArgs e)
    {
        popupVoice.ShowOnPageLoad = false;
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