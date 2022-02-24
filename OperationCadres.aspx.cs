using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OperationCadres : System.Web.UI.Page
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
        txtsalary.Text = "";
        txtTreeCount.Text = "";
        cmCadre.Items.Clear();
        cmWork.Items.Clear();
        cmGarden.Items.Clear();
        cmZone.Items.Clear();
        cmSektor.Items.Clear();
        cmLine.Items.Clear();
        cmWeather.Items.Clear();
        cmTreeType.Items.Clear();
        cmTreeAge.Items.Clear();

    }
    void _loadGridFromDb()
    {

        DataTable DTOperationCadre = _db.GetOperationCadre();
        if (DTOperationCadre != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = DTOperationCadre;
            Grid.DataBind();
        }
    }
    void componentsload()
    {
        cmCadre.Items.Clear();
        cmCadre.Items.Clear();
        DataTable dt1 = _db.GetCadres();
        cmCadre.ValueField = "CadreID";
        cmCadre.TextField = "NameDDL";
        cmCadre.DataSource = dt1;
        cmCadre.DataBind();
        cmCadre.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmCadre.SelectedIndex = 0;

        cmWork.Items.Clear();
        DataTable dt2 = _db.GetWorks();
        cmWork.ValueField = "WorkID";
        cmWork.TextField = "WorkName";
        cmWork.DataSource = dt2;
        cmWork.DataBind();
        cmWork.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmWork.SelectedIndex = 0;

        cmGarden.Items.Clear();
        DataTable dt3 = _db.GetGardens();
        cmGarden.ValueField = "GardenID";
        cmGarden.TextField = "GardenName";
        cmGarden.DataSource = dt3;
        cmGarden.DataBind();
        cmGarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmGarden.SelectedIndex = 0;   

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
        cmStatusJobName.Value = dt.Rows[0]["StatusJob"].ToParseStr();
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

        //btnSave.CommandName = "update";
        //btnSave.CommandArgument = id.ToString();
        //popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteOperationCadre(id: _id);
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
        //lblPopError.Text = "";
        //Types.ProsesType val = Types.ProsesType.Error;
        //if (Session["UserID"] != null)
        //{
        //    Session["UserID"] = 1;
        //}

        //if (FileUpload1.HasFile)
        //{
        //    Session["imgpath"] = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_sss") + FileUpload1.FileName;
        //}

        //if (btnSave.CommandName == "insert")
        //{

        //    val = _db.CadresInsert(UserID: Session["UserID"].ToString().ToParseInt(),
        //        StructureID: cmStructure.Value.ToParseInt(),
        //        PositionID: cmPosition.Value.ToParseInt(),
        //        CardID: cmCardNumber.Value.ToParseInt(),
        //        Sname: txtSname.Text.ToParseStr(),
        //        Name: txtName.Text.ToParseStr(),
        //        FName: txtFname.Text.ToParseStr(),
        //        Gender: cmGender.Value.ToParseInt(),
        //        PassportN: txtPassportN.Text.ToParseStr(),
        //        PIN: txtPIN.Text.ToParseStr(),
        //        PhoneNumber: txtPhoneNumber.Text.ToParseStr(),
        //        Photo: Session["imgpath"].ToString(),
        //        Email: txtEmail.Text.ToParseStr(),
        //        Address: txtAddress.Text.ToParseStr(),
        //        JobEntryDate: dtJobEntryDate.Text.ToParseStr(),
        //        JobExitDate: dtJobExitDate.Text.ToParseStr(),
        //        StatusJob: cmStatusJobName.Value.ToParseInt(),
        //        RegstrDate: dtRegstrDate.Text.ToParseStr()
        //        );
        //    if (val == Types.ProsesType.Succes)
        //    {
        //        if (FileUpload1.HasFile)
        //        {
        //            FileUpload1.SaveAs(Server.MapPath("/imgcadres/" + Session["imgpath"].ToString()));
        //        }
        //    }

        //}
        //else
        //{

        //    val = _db.CadresUpdate(CadreID: btnSave.CommandArgument.ToParseInt(),
        //        UserID: Session["UserID"].ToString().ToParseInt(),
        //        StructureID: cmStructure.Value.ToParseInt(),
        //        PositionID: cmPosition.Value.ToParseInt(),
        //        CardID: cmCardNumber.Value.ToParseInt(),
        //        Sname: txtSname.Text.ToParseStr(),
        //        Name: txtName.Text.ToParseStr(),
        //        FName: txtFname.Text.ToParseStr(),
        //        Gender: cmGender.Value.ToParseInt(),
        //        PassportN: txtPassportN.Text.ToParseStr(),
        //        PIN: txtPIN.Text.ToParseStr(),
        //        PhoneNumber: txtPhoneNumber.Text.ToParseStr(),
        //        Photo: Session["imgpath"].ToString(),
        //        Email: txtEmail.Text.ToParseStr(),
        //        Address: txtAddress.Text.ToParseStr(),
        //        JobEntryDate: dtJobEntryDate.Text.ToParseStr(),
        //        JobExitDate: dtJobExitDate.Text.ToParseStr(),
        //        StatusJob: cmStatusJobName.Value.ToParseInt(),
        //        RegstrDate: dtRegstrDate.Text.ToParseStr());
        //    if (val == Types.ProsesType.Succes)
        //    {
        //        if (FileUpload1.HasFile)
        //        {
        //            FileUpload1.SaveAs(Server.MapPath("/imgcadres/" + Session["imgpath"].ToString()));
        //        }
        //    }
        //}

        //if (val == Types.ProsesType.Error)
        //{
        //    lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
        //    return;
        //}

        _loadGridFromDb();
        popupEdit.ShowOnPageLoad = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        popupEdit.ShowOnPageLoad = false;
    }

    protected void cmGarden_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmZone.Items.Clear();
        DataTable dt4 = _db.GetZonesByGardenID(_db.GetGardenById(cmGarden.Value.ToParseInt()).Rows[0]["GardenID"].ToParseInt());
        cmZone.ValueField = "ZoneID";
        cmZone.TextField = "ZoneName";
        cmZone.DataSource = dt4;
        cmZone.DataBind();
        cmZone.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmZone.SelectedIndex = 0;
    }

    protected void cmZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmSektor.Items.Clear();
        DataTable dt5 = _db.GetSectorsByZoneID(_db.GetZoneById(cmZone.Value.ToParseInt()).Rows[0]["ZoneID"].ToParseInt());
        cmSektor.ValueField = "SectorID";
        cmSektor.TextField = "SectorName";
        cmSektor.DataSource = dt5;
        cmSektor.DataBind();
        cmSektor.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmSektor.SelectedIndex = 0;
    }

    protected void cmSektor_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmLine.Items.Clear();
        DataTable dt5 = _db.GetLineBySectorID(_db.GetSectorById(cmSektor.Value.ToParseInt()).Rows[0]["SectorID"].ToParseInt());
        cmLine.ValueField = "LineID";
        cmLine.TextField = "LineName";
        cmLine.DataSource = dt5;
        cmLine.DataBind();
        cmLine.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmLine.SelectedIndex = 0;
    }
}