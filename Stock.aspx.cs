using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stock : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {

        txtstock.Text = "";

    }
    void _loadGridFromDb()
    {

        DataTable dt = _db.GetStocks();
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
        DataTable dt3 = _db.GetGardens();
        cmbgarden.ValueField = "GardenID";
        cmbgarden.TextField = "GardenName";
        cmbgarden.DataSource = dt3;
        cmbgarden.DataBind();
        cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbgarden.Items.Insert(1, new ListEditItem("Ümumi", "0"));
        cmbgarden.SelectedIndex = 0;



     

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetStockById(id: id);

        cmbgarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        
        txtstock.Text = dt.Rows[0]["StockName"].ToParseStr();
       

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteStock(id: _id);
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
            val = _db.StockInsert(
                GardenID: cmbgarden.Value.ToParseInt(),
                StockName: txtstock.Text.ToParseStr()
                );
        }
        else
        {

            val = _db.StockUpdate(StockID: btnSave.CommandArgument.ToParseInt(),
                GardenID: cmbgarden.Value.ToParseInt(),
                StockName: txtstock.Text.ToParseStr()
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