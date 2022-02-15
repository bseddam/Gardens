using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadres : System.Web.UI.Page
{
    Methods _db = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {

        _loadGridFromDb();


        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtAddress.Text = "";
        txtEmail.Text = "";
        txtFname.Text = "";
        txtName.Text = "";
        txtPassportN.Text = "";
        txtPhoneNumber.Text = "";
        txtPIN.Text = "";
        txtSname.Text = "";
    }
    void _loadGridFromDb()
    {

        DataTable DTCadres = _db.GetCadres();
        if (DTCadres != null)
        {

            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTCadres;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        DataTable dt1 = _db.GetStructure();
        cmStructure.ValueField = "StructureID";
        cmStructure.TextField = "StructureName";
        cmStructure.DataSource = dt1;
        cmStructure.DataBind();
        cmStructure.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmStructure.SelectedIndex = 0;

        DataTable dt2 = _db.GetPositions();
        cmPosition.ValueField = "PositionID";
        cmPosition.TextField = "PositionName";
        cmPosition.DataSource = dt2;
        cmPosition.DataBind();
        cmPosition.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmPosition.SelectedIndex = 0;

        DataTable dt3 = _db.GetCards();
        cmCardNumber.ValueField = "CardID";
        cmCardNumber.TextField = "CardNumber";
        cmCardNumber.DataSource = dt3;
        cmCardNumber.DataBind();
        cmCardNumber.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCardNumber.SelectedIndex = 0;

        DataTable dt4 = _db.GetWorkStatus();
        cmStatusJobName.ValueField = "WorkStatusID";
        cmStatusJobName.TextField = "WorkStatusName";
        cmStatusJobName.DataSource = dt4;
        cmStatusJobName.DataBind();
        cmStatusJobName.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmStatusJobName.SelectedIndex = 0;

        DataTable dt5 = _db.GetGenders();
        cmGender.ValueField = "GenderID";
        cmGender.TextField = "GenderName";
        cmGender.DataSource = dt5;
        cmGender.DataBind();
        cmGender.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmGender.SelectedIndex = 0;


    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetCadresById(id: id);
        cmStructure.Value = dt.Rows[0]["StructureID"].ToParseStr();
        cmPosition.Value = dt.Rows[0]["PositionID"].ToParseStr();
        cmCardNumber.Value = dt.Rows[0]["CardID"].ToParseStr();
        txtSname.Text = dt.Rows[0]["Sname"].ToParseStr();
        txtName.Text = dt.Rows[0]["Name"].ToParseStr();
        txtFname.Text = dt.Rows[0]["FName"].ToParseStr();
        cmGender.Value = dt.Rows[0]["Gender"].ToParseStr();
        txtPassportN.Text = dt.Rows[0]["PassportN"].ToParseStr();
        txtPIN.Text = dt.Rows[0]["PIN"].ToParseStr();
        txtAddress.Text = dt.Rows[0]["Address"].ToParseStr();
        txtPhoneNumber.Text = dt.Rows[0]["PhoneNumber"].ToParseStr();
        txtEmail.Text = dt.Rows[0]["Email"].ToParseStr();
        DateTime datevalue;
        if (DateTime.TryParse(dt.Rows[0]["JobEntryDate"].ToParseStr(), out datevalue))
        {
            dtJobEntryDate.Text = DateTime.Parse(dt.Rows[0]["JobEntryDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtJobEntryDate.Text = "";
        }
        DateTime datevalue1;
        if (DateTime.TryParse(dt.Rows[0]["JobExitDate"].ToParseStr(), out datevalue1))
        {
            dtJobExitDate.Text = DateTime.Parse(dt.Rows[0]["JobExitDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtJobExitDate.Text = "";
        }
        cmStatusJobName.Value = dt.Rows[0]["WorkStatusName"].ToParseStr();
        Session["imgpath"] = dt.Rows[0]["Photo"].ToParseStr();
        imgUser.ImageUrl = @"imgCadres\" + dt.Rows[0]["Photo"].ToParseStr();
        DateTime datevalue3;
        if (DateTime.TryParse(dt.Rows[0]["RegstrDate"].ToParseStr(), out datevalue3))
        {
            dtRegstrDate.Text = DateTime.Parse(dt.Rows[0]["RegstrDate"].ToParseStr()).ToString("dd.MM.yyyy");
        }
        else
        {
            dtRegstrDate.Text = "";
        }

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteCadres(id: _id);
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
        if (Session["UserID"] != null)
        {
            Session["UserID"] = 1;
        }

        if (FileUpload1.HasFile)
        {
            Session["imgpath"] = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_sss") + FileUpload1.FileName;
        }

        if (btnSave.CommandName == "insert")
        {

            val = _db.CadresInsert(UserID: Session["UserID"].ToString().ToParseInt(),
                StructureID: cmStructure.Value.ToParseInt(),
                PositionID: cmPosition.Value.ToParseInt(),
                CardID: cmCardNumber.Value.ToParseInt(),
                Sname: txtSname.Text.ToParseStr(),
                Name: txtName.Text.ToParseStr(),
                FName: txtFname.Text.ToParseStr(),
                Gender: cmGender.Value.ToParseInt(),
                PassportN: txtPassportN.Text.ToParseStr(),
                PIN: txtPIN.Text.ToParseStr(),
                PhoneNumber: txtPhoneNumber.Text.ToParseStr(),
                Photo: Session["imgpath"].ToString(),
                Email: txtEmail.Text.ToParseStr(),
                Address: txtAddress.Text.ToParseStr(),
                JobEntryDate: dtJobEntryDate.Text.ToParseStr(),
                JobExitDate: dtJobExitDate.Text.ToParseStr(),
                WorkStatusID: cmStatusJobName.Value.ToParseInt(),
                RegstrDate: dtRegstrDate.Text.ToParseStr()
                );
            if (val == Types.ProsesType.Succes)
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("/imgcadres/" + Session["imgpath"].ToString()));
                }
            }

        }
        else
        {

            val = _db.CadresUpdate(CadreID: btnSave.CommandArgument.ToParseInt(),
                UserID: Session["UserID"].ToString().ToParseInt(),
                StructureID: cmStructure.Value.ToParseInt(),
                PositionID: cmPosition.Value.ToParseInt(),
                CardID: cmCardNumber.Value.ToParseInt(),
                Sname: txtSname.Text.ToParseStr(),
                Name: txtName.Text.ToParseStr(),
                FName: txtFname.Text.ToParseStr(),
                Gender: cmGender.Value.ToParseInt(),
                PassportN: txtPassportN.Text.ToParseStr(),
                PIN: txtPIN.Text.ToParseStr(),
                PhoneNumber: txtPhoneNumber.Text.ToParseStr(),
                Photo: Session["imgpath"].ToString(),
                Email: txtEmail.Text.ToParseStr(),
                Address: txtAddress.Text.ToParseStr(),
                JobEntryDate: dtJobEntryDate.Text.ToParseStr(),
                JobExitDate: dtJobExitDate.Text.ToParseStr(),
                WorkStatusID: cmStatusJobName.Value.ToParseInt(),
                RegstrDate: dtRegstrDate.Text.ToParseStr());
            if (val == Types.ProsesType.Succes)
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("/imgcadres/" + Session["imgpath"].ToString()));
                }
            }
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