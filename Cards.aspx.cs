using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cards : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        
        if (IsPostBack) return;
    }

    void ClearComponents()
    {
        txtcardbarkod.Text = "";
        txtcardnumber.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable DTCards = _db.GetCards();
        if (DTCards != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTCards;
            Grid.DataBind();
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetCardsByID(id: id);
        txtcardbarkod.Text = dt.Rows[0]["CardBarcode"].ToParseStr();
        txtcardnumber.Text = dt.Rows[0]["CardNumber"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteCards(id: _id);
        _loadGridFromDb();
    }
    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
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
            val = _db.CardsInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                CardNumber: txtcardnumber.Text.ToParseStr(),
                CardBarcode: txtcardbarkod.Text.ToParseStr());
        }
        else
        {
            val = _db.CardsUpdate(CardID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                CardNumber: txtcardnumber.Text.ToParseStr(),
                CardBarcode: txtcardbarkod.Text.ToParseStr());
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