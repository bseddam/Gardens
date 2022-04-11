<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OperationStockTransfer.aspx.cs" Inherits="OperationStockTransfer" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="card">
            <div class="card-body">
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="add" OnClick="LnkInvoice_Click" CssClass="btn btn-dark">Yeni qaimə əlavə et</asp:LinkButton>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                            <dx:ASPxGridView ID="GridInvoice" runat="server"
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
                                    <dx:GridViewDataColumn Caption="Anbar adı" FieldName="StockFromName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Anbara" FieldName="StockToName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Qaimənin statusu" FieldName="InvoiceStatusName" VisibleIndex="1">
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
                                            <asp:LinkButton ID="lnkProducts" OnClick="lnkProducts_Click" CommandArgument='<%#Eval("InvoiceStockTransferID") %>' Text="Mallar" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkPrint" OnClick="lnkPrint_Click" CommandArgument='<%#Eval("InvoiceStockTransferID") %>' Text="Çap et" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" OnClick="lnkEditInvoice_Click" CommandArgument='<%#Eval("InvoiceStockTransferID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDeleteInvoice_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("InvoiceStockTransferID") %>' Text="Sil" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxPopupControl ID="popupVoice"
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
                                Height="400"
                                ScrollBars="Vertical">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>
                                        <div class="container">
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Anbardan</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbfromstock" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbfromstock" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Anbara</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbtostock" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbtostock" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                          
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qaimənin statusu</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbInvoiceStatus" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbInvoiceStatus" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeyd</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtnotesinv" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin..." TextMode="MultiLine">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row  mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyat tarixi</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxDateEdit ID="cmbregistertime1" runat="server" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy" Width="100%" Height="30px">
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup2" ControlToValidate="cmbregistertime1" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div>
                                                <asp:Label Text="" ForeColor="Red" ID="lblerrorinv" runat="server" />
                                            </div>
                                            <asp:Button ID="btnInvoice" runat="server" ValidationGroup="qrup2" CssClass="btn btn-success mr-2" Text="Yadda saxla" OnClick="btninvoice_Click" />
                                            <asp:Button ID="btnInvoiceCancel" runat="server" CssClass="btn btn-light" Text="Ləğv et" OnClick="btninvoiceCancel_Click" />
                                        </div>


                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>


                        </div>
                    </div>
                </div>





                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">

                             <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-dark">Yeni mal əlavə et</asp:LinkButton>

                           
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
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
                                   

                                    <dx:GridViewDataColumn Caption="Model adı" FieldName="ModelName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Malın adı" FieldName="ProductsName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Malın kateqoriyası" FieldName="ProductTypeName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Ölçüsü" FieldName="ProductSize" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Ölçü vahidi" FieldName="UnitMeasurementName" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <%--<dx:GridViewDataColumn Caption="Qiyməti" FieldName="Price" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Endirimli qiyməti" FieldName="PriceDiscount" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Məbləğ" FieldName="Amount" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Endirimli məbləğ" FieldName="AmountDiscount" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Qeyd" FieldName="Notes" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>--%>

                                    <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegisterTime" VisibleIndex="1">
                                        <EditFormSettings VisibleIndex="1" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn VisibleIndex="1">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("ProductStockTransferID") %>' Text="Düzəliş" runat="server" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>

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
                                Height="500"
                                ScrollBars="Vertical">
                                <ContentCollection>
                                    <dx:PopupControlContentControl>
                                        <div class="container">
                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın kateqoriyası</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbproducttype" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbproducttype_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmbproducttype" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Modeli</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbmodel" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="cmbmodel_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbmodel" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>


                                            <div class="row mb-2">
                                                <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Malın adı</label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxComboBox ID="cmbProducts" runat="server" Width="100%" Height="30px">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbProducts" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
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

     <asp:Panel ID="pnlprint" Visible="false" runat="server">
        <table style="border-collapse: collapse; width: 100%;">
            <tr>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Sıra №</td>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Anbardan</td>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Anbara</td>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Mal kateqoriyası</td>
                <td style="border: 1px solid black; text-align: center; width: 20%; white-space: normal; font-weight: bold">Model</td>
                <td style="border: 1px solid black; text-align: center; width: 20%; white-space: normal; font-weight: bold">Məhsulun Adı</td>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Ölçü vahidi</td>
                <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal; font-weight: bold">Miqdarı</td>
               
            </tr>
            <asp:Repeater ID="rpprint" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Container.ItemIndex+1 %></td>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Eval("StockFromName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Eval("StockToName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Eval("ProductTypeName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 20%; white-space: normal;"><%#Eval("ModelName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 20%; white-space: normal;"><%#Eval("ProductsName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Eval("UnitMeasurementName")%></td>
                        <td style="border: 1px solid black; text-align: center; width: 10%; white-space: normal;"><%#Eval("ProductSize")%></td>
                    
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            
             <tr style="height:150px;">
                <td colspan="4">
                    Tehvil verdi:____________________
                </td>
                <td colspan="4">
                    Tehvil aldı:____________________
                </td>
            </tr>
            <tr style="height:100px;">
                <td colspan="8">
                    Anbardar:____________________
                </td>
                
            </tr>
            <tr style="height:100px;">
                
                <td colspan="8">
                    Nəzarət:____________________
                </td>
            </tr>
        </table>
    </asp:Panel>



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

