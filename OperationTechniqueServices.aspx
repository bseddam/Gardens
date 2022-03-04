<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OperationTechniqueServices.aspx.cs" Inherits="OperationTechniqueServices" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-info">Yeni</asp:LinkButton>
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
                            <Settings ShowFilterRow="True" GridLines="Both" ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterBlankItems="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" />

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
                                    <dx:GridViewDataColumn Caption="Servis növü" FieldName="WorkName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Bağ" FieldName="GardenName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                  
                                    <dx:GridViewDataColumn Caption="Marka texnika" FieldName="brandtex" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Model texnika" FieldName="modeltex" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                     <dx:GridViewDataColumn Caption="Texnikanın adı" FieldName="TechniquesName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Marka mal" FieldName="modelbrand" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Model mal" FieldName="modelprod" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>



                                    <dx:GridViewDataColumn Caption="Malın adı" FieldName="ProductsName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                

                                    <dx:GridViewDataColumn Caption="Ölçü vahidi" FieldName="UnitMeasurementID" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Qiyməti" FieldName="Price" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Məbləğ" FieldName="Amount" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Servis qiyməti" FieldName="ServicePrice" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Yürüş" FieldName="Odometer" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Qeyd" FieldName="Note" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegisterTime" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("TechniqueServiceID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("TechniqueServiceID") %>' Text="Sil" runat="server" />
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
                                Height="600"
                                ScrollBars="Vertical">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>
                                        <div class="container">
                                            <%--<div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Servis növü</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmServicesTypeName"
                                                        runat="server"
                                                        Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                </div>
                                            </div>--%>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Bağ</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbGarden"
                                                        runat="server"
                                                        Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cmbGarden" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                           
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Xidmətin adı</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbWork"
                                                        runat="server"
                                                        Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmbWork" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                           
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın markası</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbbrand" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbbrand_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbbrand" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın modeli</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbmodel" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbmodel_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbmodel" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın adı</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmTechnique"
                                                        runat="server"
                                                        Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="cmTechnique" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                             
                                                </div>
                                            </div>





                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın kateqoriyası</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbproducttype" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbproducttype_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmbproducttype" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                             <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın markası</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbbrandspareparts" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbbrandspareparts_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbbrandspareparts" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın modeli</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbmodelspareparts" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbmodelspareparts_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbmodelspareparts" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                           
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın adı</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbspareparts" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbspareparts" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                  
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ölçü vahidi</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbUnitMeasurement" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbUnitMeasurement" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qiyməti</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtPrice" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Format düzgün deyil." Operator="DataTypeCheck" Type="Double" ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red" />
                                                    <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtPrice" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>

                                            
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Məbləğ</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtAmount" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtAmount" ErrorMessage="Format düzgün deyil." Operator="DataTypeCheck" Type="Double" ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red" />
                                                    <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtAmount" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                           
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Xidmət haqqı</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtServicePrice" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                    </asp:TextBox>
                                                     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtServicePrice" ErrorMessage="Format düzgün deyil." Operator="DataTypeCheck" Type="Double" ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red" />
                                                    <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtServicePrice" ID="RequiredFieldValidator13" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Cari yürüş(km)</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtodometer" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeyd</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtNote" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin..." TextMode="MultiLine">
                                                    </asp:TextBox>
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

           </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

