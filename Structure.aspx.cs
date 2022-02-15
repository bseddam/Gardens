using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Structure : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtname.Text = "";
        txtsort.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable DTstructure = _db.GetStructure();
        if (DTstructure != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTstructure;
            Grid.DataBind();
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetStructureById(id: id);
        txtname.Text = dt.Rows[0]["StructureName"].ToParseStr();
        txtsort.Text = dt.Rows[0]["StructureSort"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteStructure(id: _id);
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
            val = _db.StructureInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                StructureName: txtname.Text.ToParseStr(),
                StructureSort: float.Parse(txtsort.Text.ToParseStr()));
        }
        else
        {
            val = _db.StructureUpdate(StructureID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                StructureName: txtname.Text.ToParseStr(),
                StructureSort: float.Parse(txtsort.Text.ToParseStr()));
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