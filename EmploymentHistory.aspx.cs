using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmploymentHistory : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {

        _loadGridFromDb();


        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        dtExitDate.Text = "";
        dtEntryDate.Text = "";
        txtEmploymentNumber.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable DTEmploymentHistory = _db.GetEmploymentHistory();
        if (DTEmploymentHistory != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTEmploymentHistory;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        cmStructure.Items.Clear();
        DataTable dt1 = _db.GetStructure();
        cmStructure.ValueField = "StructureID";
        cmStructure.TextField = "StructureName";
        cmStructure.DataSource = dt1;
        cmStructure.DataBind();
        cmStructure.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmStructure.SelectedIndex = 0;
        
        cmPosition.Items.Clear();
        DataTable dt2 = _db.GetPositions();
        cmPosition.ValueField = "PositionID";
        cmPosition.TextField = "PositionName";
        cmPosition.DataSource = dt2;
        cmPosition.DataBind();
        cmPosition.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmPosition.SelectedIndex = 0;
        
        cmCadre.Items.Clear();
        DataTable dt3 = _db.GetCadres();
        cmCadre.ValueField = "CadreID";
        cmCadre.TextField = "NameDDL";
        cmCadre.DataSource = dt3;
        cmCadre.DataBind();
        cmCadre.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCadre.SelectedIndex = 0;

        cmbcardetype.Items.Clear();
        DataTable dt4 = _db.GetCadreType();
        cmbcardetype.ValueField = "CadreTypeID";
        cmbcardetype.TextField = "CadreTypeName";
        cmbcardetype.DataSource = dt4;
        cmbcardetype.DataBind();
        cmbcardetype.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbcardetype.SelectedIndex = 0;

        cmGarden.Items.Clear();
        DataTable dt6 = _db.GetGardens();
        cmGarden.ValueField = "GardenID";
        cmGarden.TextField = "GardenName";
        cmGarden.DataSource = dt6;
        cmGarden.DataBind();
        cmGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmGarden.SelectedIndex = 0;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetEmploymentHistoryById(id: id);
        cmStructure.Value = dt.Rows[0]["StructureID"].ToParseStr();
        cmPosition.Value = dt.Rows[0]["PositionID"].ToParseStr();
        cmGarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmCadre.Value = dt.Rows[0]["CadreID"].ToParseStr();
        cmbcardetype.Value = dt.Rows[0]["CadreTypeID"].ToParseStr();
        txtEmploymentNumber.Text = dt.Rows[0]["EmploymentNumber"].ToParseStr();

        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["EntryDate"].ToParseStr(), out datevalue))
        {
            dtEntryDate.Text = DateTime.Parse(dt.Rows[0]["EntryDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtEntryDate.Text = "";
        }
        DateTime datevalue1;
        if (DateTime.TryParse(dt.Rows[0]["ExitDate"].ToParseStr(), out datevalue1))
        {
            dtExitDate.Text = DateTime.Parse(dt.Rows[0]["ExitDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtExitDate.Text = "";
        }        

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteEmploymentHistory(id: _id);
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
            val = _db.EmploymentHistoryInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                StructureID: cmStructure.Value.ToParseInt(),
                CadreID: cmGarden.Value.ToParseInt(),                
                GardenID: cmGarden.Value.ToParseInt(),
                CadreTypeID: cmbcardetype.Value.ToParseInt(),
                PositionID: cmPosition.Value.ToParseInt(),                
                EmploymentNumber: txtEmploymentNumber.Text.ToParseStr(),
                EntryDate: dtEntryDate.Text.ToParseStr(),
                ExitDate: dtExitDate.Text.ToParseStr()
                );
        }
        else
        {

            val = _db.EmploymentHistoryUpdate(EmploymentHistoryID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                StructureID: cmStructure.Value.ToParseInt(),
                CadreID: cmGarden.Value.ToParseInt(),
                GardenID: cmGarden.Value.ToParseInt(),
                CadreTypeID: cmbcardetype.Value.ToParseInt(),
                PositionID: cmPosition.Value.ToParseInt(),
                EmploymentNumber: txtEmploymentNumber.Text.ToParseStr(),
                EntryDate: dtEntryDate.Text.ToParseStr(),
                ExitDate: dtExitDate.Text.ToParseStr());           
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

    protected void cmGarden_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cmCardNumber.Items.Clear();
        //DataTable dt3 = _db.GetCardsByGardenID(cmGarden.Value.ToParseInt());
        //cmCardNumber.ValueField = "CardID";
        //cmCardNumber.TextField = "CardNumber";
        //cmCardNumber.DataSource = dt3;
        //cmCardNumber.DataBind();
        //cmCardNumber.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        //cmCardNumber.SelectedIndex = 0;
    }
}