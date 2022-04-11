<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProductStocks.aspx.cs" Inherits="OperationStockTransfer" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <div class="col-12"><b>Anbardakı mallar</b></div>

                            <dx:ASPxGridViewExporter ID="gridExporter" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                            <dx:ASPxGridView ID="Grid" runat="server"
                                ClientInstanceName="grid"
                                AutoGenerateColumns="False"
                                Width="100%"
                                SettingsBehavior-ConfirmDelete="true"
                                SettingsBehavior-EnableCustomizationWindow="true"
                                KeyFieldName="id" Theme="Office2010Blue" CssClass="griddizayn" SettingsText-GroupPanel="Qruplaşdırmaq istədiyiniz sütun başlıqlarını buraya sürüşdürün">
                                <Settings ShowFilterRow="True" GridLines="Both" ShowGroupPanel="True" />

                                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                <SettingsText CommandNew="Yeni" PopupEditFormCaption="Form" CommandDelete="Sil"
                                    CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                                    CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />



                                <SettingsDataSecurity AllowDelete="True" AllowEdit="True" AllowInsert="True" />



                                <SettingsPager Summary-Visible="True" PageSizeItemSettings-Visible="False">
                                    <PageSizeItemSettings Visible="False"></PageSizeItemSettings>

                                </SettingsPager>


                                <Columns>

                                    <dx:GridViewDataColumn Caption="Sıra nömrəsi" FieldName="sn" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Anbar adı" FieldName="StockName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>


                                    <dx:GridViewDataColumn Caption="Model adı" FieldName="ModelName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Malın adı" FieldName="ProductsName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Malın kateqoriyası" FieldName="ProductTypeName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Ölçü vahidi" FieldName="UnitMeasurementName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Ölçüsü" FieldName="productsizesum" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>




                                </Columns>
                            </dx:ASPxGridView>


                            <br />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .griddizayn {
            font-size: 0.875rem;
            color: rgb(28, 39, 60);
            font-family: Roboto, sans-serif;
            font-weight: initial;
            -webkit-font-smoothing: antialiased;
        }
    </style>


</asp:Content>

