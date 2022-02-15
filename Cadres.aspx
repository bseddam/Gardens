<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cadres.aspx.cs" Inherits="Cadres" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
                                                <dx:GridViewDataColumn Caption="Struktur" FieldName="StructureName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Vəzifəsi" FieldName="PositionName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="İş kartı" FieldName="CardNumber" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>                                 
                                                <dx:GridViewDataColumn Caption="Soyadı" FieldName="Sname" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Adı" FieldName="Name" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Atasının adı" FieldName="FName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cinsi" FieldName="GenderName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                  <dx:GridViewDataColumn Caption="Vəsiqə nomrəsi" FieldName="PassportN" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="FİN" FieldName="PIN" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Ünvan" FieldName="Address" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn Caption="Telefon" FieldName="PhoneNumber" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn Caption="Email" FieldName="Email" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İşə daxil olma tarixi" FieldName="JobEntryDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İşədən çıxma tarixi" FieldName="JobExitDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İş növü" FieldName="WorkStatusName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Şəkil" FieldName="Photo" VisibleIndex="1"> <DataItemTemplate>
                                                    <img src="imgtechnique/<%#Eval("Photo") %>"  Width="50" Height="50"/>                                     </DataItemTemplate>                                                   
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegstrDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("CadreID") %>' Text="Düzəliş" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>



                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("CadreID") %>' Text="Sil" runat="server" />
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
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Struktur</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmStructure" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Vəzifəsi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmPosition" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İş kartı</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmCardNumber" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Soyadı</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtSname" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Adı</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtName" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Atasının adı</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtFname" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Cinsi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmGender" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Vəsiqə nömrəsi</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtPassportN" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                          <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">FİN</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtPIN" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ünvan</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtAddress" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Telefon</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtPhoneNumber" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Email</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtEmail" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşə qəbul olma tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtJobEntryDate" runat="server"></dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşədən çıxma tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtJobExitDate" runat="server"></dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İş növü</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmStatusJobName"
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>                                                   
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtRegstrDate" runat="server"></dx:ASPxDateEdit>
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

