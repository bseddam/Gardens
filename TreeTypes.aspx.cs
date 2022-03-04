﻿using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreeTypes : System.Web.UI.Page
{
    Methods _db = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadGridFromDb();
        if (IsPostBack) return;
    }
    void componentsload()
    {
        DataTable dt2x = _db.GetTrees();
        cmbtrees.ValueField = "TreeID";
        cmbtrees.TextField = "TreeName";
        cmbtrees.DataSource = dt2x;
        cmbtrees.DataBind();
        cmbtrees.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbtrees.SelectedIndex = 0;


        DataTable dt1 = _db.GetCountries();
        cmbcountry.ValueField = "CountryID";
        cmbcountry.TextField = "CountryName";
        cmbcountry.DataSource = dt1;
        cmbcountry.DataBind();
        cmbcountry.Items.Insert(0, new ListEditItem("Seçin", "-1"));
        cmbcountry.SelectedIndex = 0;


    }
    void ClearComponents()
    {
        txttreetypename.Text = "";
        txtcoefficient.Text = "";
        lblPopError.Text = "";
    }
    void _loadGridFromDb()
    {
        DataTable dt = _db.GetTreeTypes();
        if (dt != null)
        {
            Grid.SettingsPager.Summary.Text = "Cari səhifə: {0}, Ümumi səhifələrin sayı: {1}, Tapılmış məlumatların sayı: {2}";
            Grid.DataSource = dt;
            Grid.DataBind();
        }
    }


    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        componentsload();
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetTreeTypesByID(id: id);
        cmbcountry.Value = dt.Rows[0]["CountryID"].ToParseStr();
        cmbtrees.Value = dt.Rows[0]["TreeID"].ToParseStr();
        txtcoefficient.Text = dt.Rows[0]["Coefficient"].ToParseStr();
        txttreetypename.Text = dt.Rows[0]["TreeTypeName"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int _id = (sender as LinkButton).CommandArgument.ToParseInt();
        Types.ProsesType val = _db.DeleteTreeType(id: _id);
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
            val = _db.TreeTypeInsert(
                CountryID: cmbcountry.Value.ToParseInt(),
                TreeID: cmbtrees.Value.ToParseInt(),
                TreeTypeName: txttreetypename.Text.ToParseStr(),
                Coefficient: txtcoefficient.Text.ToParseStr()
                );
        }
        else
        {
            val = _db.TreeTypeUpdate(TreeTypeID: btnSave.CommandArgument.ToParseInt(),
                CountryID: cmbcountry.Value.ToParseInt(),
                TreeID: cmbtrees.Value.ToParseInt(),
                TreeTypeName: txttreetypename.Text.ToParseStr(),
                Coefficient: txtcoefficient.Text.ToParseStr()
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