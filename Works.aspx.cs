using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Works : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
      
        txtworkname.Text = "";

        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetWorks();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        DataTable dt5 = _db.GetWorkTypes();
        ddlworktype.DataValueField = "WorkTypeID";
        ddlworktype.DataTextField = "WorkTypeName";
        ddlworktype.DataSource = dt5;
        ddlworktype.DataBind();
        ddlworktype.Items.Insert(0, new ListItem("Seçin", "-1"));
        ddlworktype.SelectedIndex = 0;


       
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetWorkById(id: id);

        txtworkname.Text = dt.Rows[0]["WorkName"].ToParseStr();
        ddlworktype.SelectedValue = dt.Rows[0]["WorkTypeID"].ToParseStr();
        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteWork(id: _id);
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
            val = _db.WorkInsert(
                WorkTypeID: ddlworktype.SelectedValue.ToParseInt(),
                WorkName: txtworkname.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.WorkUpdate(WorkID: btnSave.CommandArgument.ToParseInt(),
                WorkTypeID: ddlworktype.SelectedValue.ToParseInt(),
                WorkName: txtworkname.Text.ToParseStr()
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