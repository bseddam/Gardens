﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OperationStockTransfer.aspx.cs" Inherits="OperationStockTransfer" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <%--<asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-info">Yeni</asp:LinkButton>--%>
    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <%--
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("ProductStockInputOutputID") %>' Text="Sil" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
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
                                    
                                   
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkInsert" OnClick="lnkInsert_Click" CommandArgument='<%#Eval("StockID")+","+ Eval("ProductID")%>' Text="Köçür" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                   


                                </Columns>
                            </dx:ASPxGridView>
                             

                            <br />
<div class="col-12"><b>Kocurulen mallar</b></div>
 <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                            <dx:ASPxGridView ID="Gridtransfer" runat="server"
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
                                    <dx:GridViewDataColumn Caption="Mal adı" FieldName="ProductsName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Anbardan" FieldName="StockFromName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Anbara" FieldName="StockToName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                   
                                    <dx:GridViewDataColumn Caption="Ölçüsü" FieldName="ProductSize" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                     <dx:GridViewDataColumn Caption="Ölçüsü vahidi" FieldName="UnitMeasurementName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                   
                                  
                                    <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegisterTime" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                 <%-- <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("ProductStockTransferID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("ProductStockTransferID") %>' Text="Sil" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>


                                </Columns>
                            </dx:ASPxGridView>







                            









                            <dx:ASPxPopupControl ID="popupEdit"
                                runat="server"
                                ClientInstanceName="popup"
                                AllowDragging="true"
                                AllowResize="true"
                                Modal="true"
                                DragElement="Header"
                                Width="600"
                                HeaderText="Redaktə"
                                PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter"
                                Height="300"
                                ScrollBars="Vertical">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>
                                      
                                                <div class="container">

                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Anbar adı</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbstock" runat="server" Width="100%" Height="30px">
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cmbstock" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ölçü</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtProductSize" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                            </asp:TextBox>
                                                            <asp:CompareValidator ID="cv6" runat="server" ControlToValidate="txtProductSize" ErrorMessage="Format düzgün deyil." Operator="DataTypeCheck" Type="Double" ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red" />
                                                            <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtProductSize" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                   
                                                    <div class="row  mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                        <div class="col-sm-7">

                                                            <dx:ASPxDateEdit ID="cmbregistertime" runat="server" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy" Width="100%" Height="30px">
                                                            </dx:ASPxDateEdit>
                                                            <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="cmbregistertime" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                                                    </div>
                                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="qrup1" CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btntesdiq_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-light" Text="Ləğv et" OnClick="btnCancel_Click" />
                                                </div>

                                           
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>
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

          <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

