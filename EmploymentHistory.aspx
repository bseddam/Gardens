<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmploymentHistory.aspx.cs" Inherits="EmploymentHistory" %>


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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="card">
                    <div class="card-body">                      
                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-dark">Əmək kitabçasına yeni əməliyyat əlavə et</asp:LinkButton>
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
                                                <dx:GridViewDataColumn Caption="Şirkət" FieldName="CompanyName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Bağ" FieldName="GardenName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İşçi" FieldName="NameDDL" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn> 
                                                  <dx:GridViewDataColumn Caption="Müqavilə nömrəsi" FieldName="EmploymentNumber" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn> 
                                                  <dx:GridViewDataColumn Caption="Vəzifəsi" FieldName="PositionName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İşçi növü" FieldName="CadreTypeName" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="İşə daxil olma tarixi" FieldName="EntryDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn Caption="İşdən çıxma tarixi" FieldName="ExitDate" VisibleIndex="1">
                                                    <EditFormSettings VisibleIndex="1" />
                                                </dx:GridViewDataColumn>                                                 
                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("EmploymentHistoryID") %>' Text="Düzəliş" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>



                                                <dx:GridViewDataColumn VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("EmploymentHistoryID") %>' Text="Sil" runat="server" />
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
                                            Height="460" 
                                          
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
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşçi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmCadre" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmCadre" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Bağ</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmGarden" 
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmGarden" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
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
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşçi növü</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxComboBox ID="cmbcardetype"
                                                                    runat="server"
                                                                    Width="100%" Height="30px">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div> 

                                                         <div class="row mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Müqavilə nömrəsi</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtEmploymentNumber" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                                </asp:TextBox>
                                                                 <asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtEmploymentNumber" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtEmploymentNumber" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşə qəbul olma tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtEntryDate" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                        <div class="row  mb-2">
                                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">İşədən çıxma tarixi</label>
                                                            <div class="col-sm-7">
                                                                <dx:ASPxDateEdit ID="dtExitDate" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
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

