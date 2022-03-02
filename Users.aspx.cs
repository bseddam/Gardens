using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void ClearComponents()
    {
        txtlogin.Text = "";
        txtpassword.Text = "";
        txtlogin.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetUsers();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }

    void componentsload()
    {
        DataTable dt7 = _db.GetGardens();
        cmbgarden.ValueField = "GardenID";
        cmbgarden.TextField = "GardenName";
        cmbgarden.DataSource = dt7;
        cmbgarden.DataBind();
        cmbgarden.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbgarden.SelectedIndex = 0;


        cmbcadres.Items.Clear();
        DataTable dt1 = _db.GetCadres();
        cmbcadres.ValueField = "CadreID";
        cmbcadres.TextField = "NameDDL";
        cmbcadres.DataSource = dt1;
        cmbcadres.DataBind();
        cmbcadres.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbcadres.SelectedIndex = 0;




        cmbstatus.Items.Clear();
        DataTable dt2 = _db.GetUserStatus();
        cmbstatus.ValueField = "UserStatusID";
        cmbstatus.TextField = "UserStatusName";
        cmbstatus.DataSource = dt2;
        cmbstatus.DataBind();
        cmbstatus.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbstatus.SelectedIndex = 0;

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetUserByid(userid: id);
        cmbgarden.Value = dt.Rows[0]["GardenID"].ToParseStr();
        cmbcadres.Value = dt.Rows[0]["CadreID"].ToParseStr();

        txtlogin.Text = dt.Rows[0]["Login"].ToParseStr();
        txtpassword.Text = dt.Rows[0]["Password"].ToParseStr();

        cmbstatus.Value = dt.Rows[0]["UserStatusID"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteUser(id: _id);
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
            val = _db.UserInsert(
                GardenID:cmbgarden.Value.ToParseInt(),
                CadreID: cmbcadres.Value.ToParseInt(),
                Login: txtlogin.Text.ToParseStr(),
                Password: txtpassword.Text.ToParseStr(),
                UserStatusID: cmbstatus.Value.ToParseInt()
                );
        }
        else
        {
            val = _db.UserUpdate(id: btnSave.CommandArgument.ToParseInt(),
                GardenID: cmbgarden.Value.ToParseInt(),
                CadreID: cmbcadres.Value.ToParseInt(),
                Login: txtlogin.Text.ToParseStr(),
                Password: txtpassword.Text.ToParseStr(),
                UserStatusID: cmbstatus.Value.ToParseInt()
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

    protected void lnkPermission_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetUsersPermissions();
        chlist.DataValueField = "SiteMapID";
        chlist.DataTextField = "name";
        chlist.DataSource = dt;
        chlist.DataBind();
        btnPermissionsSave.CommandArgument = id.ToString();
        popupPermission.ShowOnPageLoad = true;
    }

    protected void btnPermissionsSave_Click(object sender, EventArgs e)
    {
        lblPopError.Text = "";
        Types.ProsesType val = Types.ProsesType.Error;        
        string IDS = "";
        foreach (ListItem item in chlist.Items)
        {
            if (item.Selected)
            {
                IDS += item.Value+","; 
            }
        }
        int id = btnPermissionsSave.CommandArgument.ToParseInt();

       IDS=IDS.Substring(0, IDS.Length - 1);

        string[] PermissionID = IDS.Split(',');

        val = _db.UserInsertPermissions(id,PermissionID);

        if (val == Types.ProsesType.Error)
        {
            lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

     //   _loadGridFromDb();
        popupPermission.ShowOnPageLoad = false;
    }

    protected void btnPermissionsCancel_Click(object sender, EventArgs e)
    {
        popupPermission.ShowOnPageLoad = false;
    }
}