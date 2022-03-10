using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Models : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {

        txtmodelname.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dtmodels = _db.GetModels();
        if (dtmodels != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dtmodels;
            Grid.DataBind();
        }
    }

    void componentsload()
    {
        DataTable dt7 = _db.GetBrands();
        ddlbrand.DataValueField = "BrandID";
        ddlbrand.DataTextField = "BrandName";
        ddlbrand.DataSource = dt7;
        ddlbrand.DataBind();
        ddlbrand.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlbrand.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetModelByID(id: id);

        
        txtmodelname.Text = dt.Rows[0]["ModelName"].ToParseStr();
        ddlbrand.SelectedValue = dt.Rows[0]["BrandID"].ToParseStr();
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteModel(id: _id);
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
            val = _db.ModelInsert(
                ModelName: txtmodelname.Text.ToParseStr(),
                BrandID: ddlbrand.SelectedValue.ToParseInt()
                );
        }
        else
        {
            val = _db.ModelUpdate(ModelID: btnSave.CommandArgument.ToParseInt(),
                ModelName: txtmodelname.Text.ToParseStr(),
                BrandID: ddlbrand.SelectedValue.ToParseInt()
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

    protected void btnBrends_Click(object sender, EventArgs e)
    {
        DataTable dt7 = _db.GetProductTypes();
        ddlproducttype.DataValueField = "ProductTypeID";
        ddlproducttype.DataTextField = "ProductTypeName";
        ddlproducttype.DataSource = dt7;
        ddlproducttype.DataBind();
        ddlproducttype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlproducttype.SelectedIndex = 0;
        LinkButton btn = sender as LinkButton;
        switch (btn.CommandArgument)
        {
            case "addBrend":
                btnBrendSave.CommandName = "insert";
                popupBrends.ShowOnPageLoad = true;
                break;
        }
        
    }

    protected void btnBrendSave_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;
        if (btnBrendSave.CommandName == "insert")
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
            Label1.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }
        componentsload();
        popupBrends.ShowOnPageLoad = false;
    }

    protected void btnBrendCancel_Click(object sender, EventArgs e)
    {
        componentsload();
        popupBrends.ShowOnPageLoad = false;
    }
}