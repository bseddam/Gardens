using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class OtherExpenses : System.Web.UI.Page
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
        txtamount.Text = "";
        cmbregistertime.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable dt = _db.GetExpenses();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        DataTable dt2x = _db.GetExpenses();
        cmbExpense.ValueField = "OtherExpenseID";
        cmbExpense.TextField = "OtherExpenseName";
        cmbExpense.DataSource = dt2x;
        cmbExpense.DataBind();
        cmbExpense.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbExpense.SelectedIndex = 0;


    }



    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetExpenseByID(id: id);

        cmbExpense.Value = dt.Rows[0]["OtherExpenseID"].ToParseStr();
        txtNote.Text = dt.Rows[0]["Note"].ToParseStr();
        txtamount.Text = dt.Rows[0]["Amount"].ToParseStr();

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
        Types.ProsesType val = _db.DeleteExpense(id: _id);
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
            val = _db.InsertExpense(
                OtherExpenseName: cmbExpense.Text.ToParseStr(),
                Amount: txtamount.Text.ToParseStr(),
                Note: txtNote.Text.ToParseStr(),
                RegisterTime: cmbregistertime.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.UpdateExpense(OtherExpenseID: btnSave.CommandArgument.ToParseInt(),
               OtherExpenseName: cmbExpense.Text.ToParseStr(),
               Amount: txtamount.Text.ToParseStr(),
               Note: txtNote.Text.ToParseStr(),
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