using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class OperationProductsExit : System.Web.UI.Page
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
        DataTable dt = _db.GetProductStock();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }

        DataTable dt1 = _db.GetProductStockOutput();
        if (dt1 != null)
        {
            Gridtransfer.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Gridtransfer.DataSource = dt1;
            Gridtransfer.DataBind();
        }
    }

   
   
    protected void lnkInsert_Click(object sender, EventArgs e)
    {
        string commandArgs = (sender as LinkButton).CommandArgument.ToString();

        //componentsload();
        ClearComponents();
        btnSave.CommandName = "insert";
        btnSave.CommandArgument = commandArgs.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    //protected void lnkEdit_Click(object sender, EventArgs e)
    //{
    //    int id = (sender as LinkButton).CommandArgument.ToParseInt();
    //    DataTable dt = _db.GetProductStockInputOutputByID(id: id);
    //    componentsload();

    //    txtProductSize.Text = dt.Rows[0]["ProductSize"].ToParseStr();
    //    cmbstock.Value = dt.Rows[0]["StockToID"].ToParseStr();

    //    DateTime datevalue;
    //    if (DateTime.TryParse(dt.Rows[0]["RegisterTime"].ToParseStr(), out datevalue))
    //    {
    //        cmbregistertime.Text = DateTime.Parse(dt.Rows[0]["RegisterTime"].ToParseStr()).ToString("dd.MM.yyyy");
    //    }
    //    else
    //    {
    //        cmbregistertime.Text = "";
    //    }

    //    btnSave.CommandName = "update";
    //    btnSave.CommandArgument = id.ToString();
    //    popupEdit.ShowOnPageLoad = true;
    //}
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteProductOutput(id: _id);
        _loadGridFromDb();
    }

    protected void btntesdiq_Click(object sender, EventArgs e)
    {
        lblPopError.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;

        string[] cma = btnSave.CommandArgument.ToString().Split(new char[] { ',' });
        string StockID = cma[0];
        string ProductID = cma[1];
        if (btnSave.CommandName == "insert")
        {
            val = _db.ProductStockOutputInsert(StockID: StockID.ToParseInt(),
                ProductID: ProductID.ToParseInt(),
                ProductSize: txtProductSize.Text.ToParseStr(),
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                Notes: txtNote.Text.ToParseStr()
                );
        }
        else
        {
            //val = _db.ProductStockUpdateTransfer(ProductStockTransferID: btnSave.CommandArgument.ToParseInt(),
            //    StockFromID: StockFromID.ToParseInt(),
            //    UserID: Session["UserID"].ToParseInt(),
            //    ProductID: ProductID.ToParseInt(),
            //    StockToID: cmbstock.Value.ToParseInt(),
            //    ProductSize: txtProductSize.Text.ToParseStr(),
            //    RegisterTime: cmbregistertime.Text.ToParseStr()
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

  
}