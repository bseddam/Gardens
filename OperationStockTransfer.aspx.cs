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
        txtNotes.Text = "";
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

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        componentsload();
        ClearComponents();
        btnSave.CommandName = "insert";
        btnSave.CommandArgument = id.ToString();
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
            int id = (sender as LinkButton).CommandArgument.ToParseInt();
            DataTable dt = _db.GetProductStockByID(id);

            val = _db.ProductStockInsertTransfer(UserID: Session["UserID"].ToParseInt(),
                ProductID: dt.Rows[0]["ProductID"].ToParseInt(),
                GardenID:cmbgarden.Value.ToParseInt(),                
                ProductSize: txtProductSize.Text.ToParseStr(),
                UnitMeasurementID: dt.Rows[0]["UnitMeasurementID"].ToParseInt(),
                Price: dt.Rows[0]["Price"].ToParseStr(),
                PriceDiscount: dt.Rows[0]["PriceDiscount"].ToParseStr(),
                Amount: dt.Rows[0]["Amount"].ToParseStr(),
                AmountDiscount: dt.Rows[0]["AmountDiscount"].ToParseStr(),               
                RegisterTime: cmbregistertime.Text.ToParseStr(),
                Notes: txtNotes.Text.ToParseStr()
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