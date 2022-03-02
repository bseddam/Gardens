<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Technique.aspx.cs" Inherits="Technique" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
          var openFile = function(file) {
    var input = file.target;

    var reader = new FileReader();
    reader.onload = function(){
      var dataURL = reader.result;
      var output = document.getElementById('ContentPlaceHolder1_popupEdit_imgUser');
      output.src = dataURL;
    };
    reader.readAsDataURL(input.files[0]);
  };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="content-wrapper">
                <div class="card">
                    <div class="card-body">                      
                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-dark">Yeni texnika əlavə et</asp:LinkButton>
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
                                                <dx:GridViewDataColumn Caption="Sistem istifadəçisi" FieldName="UsingUsers" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Texnikanın adi" FieldName="TechniquesName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Markası" FieldName="BrandName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Modeli" FieldName="ModelName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>                                 
                                                <dx:GridViewDataColumn Caption="Şirkət" FieldName="CompanyName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Motor" FieldName="Motor" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="İnvertar kodu" FieldName="RegisterNumber" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Məsul şəxs" FieldName="SerieNumber" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Passport" FieldName="Passport" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Buraxılış ili" FieldName="ProductionYear" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Birka" FieldName="Birka" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn Caption="Ümumi vəziyyəti" FieldName="TechniqueSituationName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn Caption="GPS" FieldName="GPS" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="GPS istifadəçi adı" FieldName="GPSLogin" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="GPS Şifrə" FieldName="GPSPassword" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Şəkil" FieldName="Photo" VisibleIndex="1"> <DataItemTemplate>
                                                    <img src="imgtechnique/<%#Eval("Photo") %>"  Width="50" Height="50"/>                                     </DataItemTemplate>                                                   
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="BoughtDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("TechniqueID") %>' Text="Düzəliş" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>



                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("TechniqueID") %>' Text="Sil" runat="server" />
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
                                            Height="650" 
                                          
                                            ScrollBars="Vertical">
                                            <ContentCollection>
                                                <dx:PopupControlContentControl>
                                                    <div class="container">
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın markası</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmBrand" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmBrand_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın modeli</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmmodels" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Texnikanın adı</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtname" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Bağ</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmbgarden" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Şirkət</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmCompany" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Motor</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtmotor" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                          <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İnvertar kodu</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtRegisterNumber" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Məsul şəxs</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtSerieNumber" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Passport</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtpassport" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Buraxılış ili</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtProductionYear" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                         <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Birka</label>
                                                            <div class="col-sm-7">
                                                                    <asp:TextBox ID="txtBirka" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ümumi vəziyyəti</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmbTechniqueSituationName"
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">GPS</label>
                                                            <div class="col-sm-7">
                                                                 <dx:ASPxComboBox ID="cmbGPS"
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">GPS istifadəçi adı</label>
                                                            <div class="col-sm-7">
                                                                 <asp:TextBox ID="txtlogin" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">GPS şifrə</label>
                                                            <div class="col-sm-7">
                                                                 <asp:TextBox ID="txtpass" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtBoughtDate" runat="server"></dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                          
                                                         <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Şəkil</label>
                                                            <div class="col-sm-7">
                                                               
                                 <asp:Image ID="imgUser" runat="server" width="150" height="150"/><br />

                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="hidden" onchange='openFile(event)' />
    
                                                            </div>
                                                        </div>
                                                         <div>
                                                            <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                                                        </div>
                                                        <asp:Button ID="btnSave" runat="server"  CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btntesdiq_Click" />
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

 <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

