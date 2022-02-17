using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Brands : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtbrandname.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtbrands = _db.GetBrands();
        if (dtbrands != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtbrands;
            Grid.DataBind();
        }
    }
   
    void componentsload()
    {
        DataTable dt7 = _db.GetProductTypes();
        ddlproducttype.DataValueField = "ProductTypeID";
        ddlproducttype.DataTextField = "ProductTypeName";
        ddlproducttype.DataSource = dt7;
        ddlproducttype.DataBind();
        ddlproducttype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlproducttype.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetBrandsByID(id: id);

        txtbrandname.Text = dt.Rows[0]["BrandName"].ToParseStr();
        ddlproducttype.SelectedValue = dt.Rows[0]["ProductTypeID"].ToParseStr();
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteProduct(id: _id);
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
            val = _db.BrandInsert(
                BrandName: txtbrandname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt()
                );
        }
        else
        {
            val = _db.BrandUpdate(BrandID: btnSave.CommandArgument.ToParseInt(),
                BrandName: txtbrandname.Text.ToParseStr(),
                ProductTypeID: ddlproducttype.SelectedValue.ToParseInt()
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