<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Orders" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <asp:LinkButton ID="btnNewInvoice" runat="server" CommandArgument="add" OnClick="btnNewInvoice_Click" CssClass="btn btn-dark">Yeni qaimə əlavə et</asp:LinkButton>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                            <dx:ASPxGridView ID="GridOrderInvoice" runat="server"
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
                                    <dx:GridViewDataColumn Caption="Anbar" FieldName="StockName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                                                       
                                    <dx:GridViewDataColumn Caption="Sifarişin statusu" FieldName="InvoiceStatusName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Tarix" FieldName="InvoiceDate" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkProducts" OnClick="lnkProducts_Click" CommandArgument='<%#Eval("OrderInvoiceID") %>' Text="Sifariş malları" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("OrderInvoiceID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("OrderInvoiceID") %>' Text="Sil" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>


                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxPopupControl ID="PopupOrderInvoice"
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
                                Height="250"
                                ScrollBars="Vertical">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>
                                      
                                                <div class="container">

                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Anbar</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbstock" runat="server" Width="100%" Height="30px" >
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbstock" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qaimənin statusu</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbStatus" runat="server" Width="100%" Height="30px">
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbStatus" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                

                                                    <div class="row  mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                        <div class="col-sm-7">

                                                            <dx:ASPxDateEdit ID="DTInvoice" runat="server" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy" Width="100%" Height="30px">
                                                            </dx:ASPxDateEdit>
                                                            <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="DTInvoice" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label Text="" ForeColor="Red" ID="lblErrorInvoice" runat="server" />
                                                    </div>
                                                    <asp:Button ID="btnInvoiceSave" runat="server" ValidationGroup="qrup1" CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btnInvoiceSave_Click" />
                                                    <asp:Button ID="btnInvoiceCancel" runat="server" CssClass="btn btn-light" Text="Ləğv et" OnClick="btnInvoiceCancel_Click" />
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
    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <asp:LinkButton ID="btnAddProduct" runat="server" CommandArgument="add" OnClick="btnAddProduct_Click" CssClass="btn btn-dark">Yeni mal əlavə et</asp:LinkButton>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
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
                                    <dx:GridViewDataColumn Caption="Malın kateqoriyası" FieldName="ProductTypeName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Model" FieldName="ModelName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Malın adı" FieldName="ProductsName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Ölçü vahidi" FieldName="UnitMeasurementName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Ölçüsü" FieldName="ProductSize" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                  
<%--                                    <dx:GridViewDataColumn Caption="Qeyd" FieldName="Notes" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>--%>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEditProduct" OnClick="lnkEditProduct_Click" CommandArgument='<%#Eval("OrderProductID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteProduct" OnClick="lnkDeleteProduct_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("OrderProductID") %>' Text="Sil" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>


                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxPopupControl ID="popupEditProduct"
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
                                ScrollBars="Vertical"  EnableHierarchyRecreation="false">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>


                                        <div class="container">

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                               
                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın kateqoriyası</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbproducttype" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbproducttype_SelectedIndexChanged">
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmbproducttype" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                 
                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Modeli</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbmodel" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbmodel_SelectedIndexChanged">
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbmodel" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                       
                                                            </div>
                                                    </div>


                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın adı</label>
                                                        <div class="col-sm-7">
                                                            <dx:ASPxComboBox ID="cmbProducts" runat="server" Width="100%" Height="30px">
                                                            </dx:ASPxComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbProducts" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ölçü</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtProductSize" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                            </asp:TextBox>
                                                            <asp:CompareValidator ID="cv6" runat="server" ControlToValidate="txtProductSize" ErrorMessage="Format düzgün deyil." Operator="DataTypeCheck" Type="Double" ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red" />
                                                            <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup2" ControlToValidate="txtProductSize" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                   <%-- <div class="row mb-2">
                                                        <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeyd</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtNote" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin..." TextMode="MultiLine">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>--%>

                                                    <div>
                                                        <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                                                    </div>
                                                    <asp:Button ID="btnProductSave" runat="server" ValidationGroup="qrup2" CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btnProductSave_Click" />
                                                    <asp:Button ID="btnProductCancel" runat="server" CssClass="btn btn-light" Text="Ləğv et" OnClick="btnProductCancel_Click" />
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

    
</asp:Content>



