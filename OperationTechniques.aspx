<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OperationTechniques.aspx.cs" Inherits="OperationTechniques" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="card">
                    <div class="card-body">                      
                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-dark">Texnikaların gördüyü işləri əlavə et</asp:LinkButton>
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
                                            <Settings ShowFilterRow="True" GridLines="Both" ShowGroupPanel="True" 
                                                />
                                          
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
                                                <dx:GridViewDataColumn Caption="Texnikanın adı" FieldName="TechniquesName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İş" FieldName="WorkName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Bağ" FieldName="GardenName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>                                 
                                                <dx:GridViewDataColumn Caption="Zona" FieldName="ZoneName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Sektor" FieldName="SectorName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Sıra" FieldName="LineName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Qət etdiyi məsafə" FieldName="Odometer" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Ağac sayı" FieldName="TreeCount" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Qeyd" FieldName="Notes" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegisterTime" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("TechniquesWorkDoneID") %>' Text="Düzəliş" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("TechniquesWorkDoneID") %>' Text="Sil" runat="server" />
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
                                            Height="610" 
                                          
                                            ScrollBars="Vertical">
                                            <ContentCollection>
                                                <dx:PopupControlContentControl>
                                                    <div class="container">
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Marka</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmMarka" 
                                                                    runat="server" AutoPostBack="true"
                                                                    Width="100%" Height="30px" OnSelectedIndexChanged="cmMarka_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmMarka" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Model</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmModel" 
                                                                    runat="server" AutoPostBack="true"
                                                                    Width="100%" Height="30px" OnSelectedIndexChanged="cmModel_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmModel" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>                                                        
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnika</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmTechnique" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmTechnique" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Şirkət</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmCompany" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmCompany" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İş</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmWork" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmWork" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Bağ</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmGarden" 
                                                                    runat="server" AutoPostBack="true"
                                                                    Width="100%" Height="30px" OnSelectedIndexChanged="cmGarden_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmGarden" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Zona</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmZone" 
                                                                    runat="server" AutoPostBack="true"
                                                                    Width="100%" Height="30px" OnSelectedIndexChanged="cmZone_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Sektor</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmSektor" 
                                                                    runat="server" AutoPostBack="true"
                                                                    Width="100%" Height="30px" OnSelectedIndexChanged="cmSektor_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Sıra</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmLine" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qət etdiyi məsafə</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtOdometer" class="form-control mb-0 mt-0" runat="server" placeholder="Sayı daxil edin...">
                                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtOdometer" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>                                                        
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ağac sayı</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtTreeCount" class="form-control mb-0 mt-0" runat="server" placeholder="Sayı daxil edin...">
                                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtTreeCount" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeyd</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtNotes" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin..." TextMode="MultiLine">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtRegisterTime" runat="server" CssClass="form-control" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy" Width="100%" Height="30px"></dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                         <div>
                                            <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                                        </div>
                                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="qrup1"  CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btntesdiq_Click" />
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

