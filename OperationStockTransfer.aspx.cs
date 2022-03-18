using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class OperationStockTransfer : System.Web.UI.Page
{
    
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
      
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

        //cmbgarden.Items.Clear();
        //DataTable d2t1 = _db.GetGardens();
        //cmbgarden.ValueField = "GardenID";
        //cmbgarden.TextField = "GardenName";
        //cmbgarden.DataSource = d2t1;
        //cmbgarden.DataBind();
        //cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        //cmbgarden.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        string commandArgs = (sender as LinkButton).CommandArgument.ToParseStr();
        

        //int id = (sender as LinkButton).CommandArgument.ToParseInt();
        componentsload();
        ClearComponents();
        btnSave.CommandName = "insert";
        btnSave.CommandArgument = commandArgs.ToParseStr();
        popupEdit.ShowOnPageLoad = true;
    }
    //protected void lnkDelete_Click(object sender, EventArgs e)
    //{
    //    int _id = (sender as LinkButton).CommandArgument.ToParseInt();
    //    Types.ProsesType val = _db.DeleteProductStock(id: _id);
    //    _loadGridFromDb();
    //}
   
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
            string[] cma = btnSave.CommandArgument.ToParseStr().Split(new char[] { ',' });
            string StockFromID = cma[0];
            string ProductID = cma[1];
            

           

            val = _db.ProductStockInsertTransfer(StockFromID: StockFromID.ToParseInt(),
                UserID: Session["UserID"].ToParseInt(),
                ProductID: ProductID.ToParseInt(),
                StockToID: cmbstock.Value.ToParseInt(),                
                ProductSize: txtProductSize.Text.ToParseStr(),   
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
}