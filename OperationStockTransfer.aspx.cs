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
        cmbgarden.Items.Clear();
        DataTable d2t1 = _db.GetGardens();
        cmbgarden.ValueField = "GardenID";
        cmbgarden.TextField = "GardenName";
        cmbgarden.DataSource = d2t1;
        cmbgarden.DataBind();
        cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbgarden.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        string commandArgs = (sender as LinkButton).CommandArgument.ToString();
        

        //int id = (sender as LinkButton).CommandArgument.ToParseInt();
        componentsload();
        ClearComponents();
        btnSave.CommandName = "insert";
        btnSave.CommandArgument = commandArgs.ToString();
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
            string[] cma = btnSave.CommandArgument.ToString().Split(new char[] { ',' });
            string GardenFromID = cma[0];
            string ProductID = cma[1];
            

           

            val = _db.ProductStockInsertTransfer(GardenFromID: GardenFromID.ToParseInt(),
                UserID: Session["UserID"].ToParseInt(),
                ProductID: ProductID.ToParseInt(),
                GardenToID: cmbgarden.Value.ToParseInt(),                
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