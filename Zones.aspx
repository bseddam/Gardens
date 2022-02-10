<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Zones.aspx.cs" Inherits="Zones" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- ======= Default Section ======= -->
    <section id="about" class="about section-bg">
        <div class="container">

            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click" CssClass="btn btn-dark">Yeni</asp:LinkButton>

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
                                <dx:GridViewDataColumn Caption="Qeydiyyat tarixi" FieldName="RegisterTime" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Zona adı" FieldName="ZonaName" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Bağ adı" FieldName="GardenName" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn Caption="Ümumi sahəsi" FieldName="ZonaArea" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="Ölçü vahidi" FieldName="UnitMeasurementName" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>


                               
                                <dx:GridViewDataColumn Caption="Qeyd" FieldName="Notes" VisibleIndex="1">
                                    <EditFormSettings VisibleIndex="1" />
                                </dx:GridViewDataColumn>








                                <dx:GridViewDataColumn VisibleIndex="1">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("ZoneID") %>' Text="Düzəliş" runat="server" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>



                                <dx:GridViewDataColumn VisibleIndex="1">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("ZoneID") %>' Text="Sil" runat="server" />
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
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeydiyyata alınma tarixi</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxDateEdit ID="cmbregistertime" runat="server" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy" Width="100%" Height="30px">
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>


                                        <div class="row mb-2">
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Zona adı</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtzonename" runat="server" class="form-control mb-0 mt-0" Height="50px" placeholder="Mətni daxil edin..."></asp:TextBox>
                                                <asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtzonename" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtzonename" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        
                                        <div class="row mb-2">
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Bağ</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlgardens" class="form-control" runat="server"></asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlgardens" ErrorMessage="Mütləq seçilməlidir." InitialValue="-1" Text="Mütləq seçilməlidir." ValidationGroup="qrup1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <%--asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtmovzuadi" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>--%>
                                                <%--<asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtmovzuadi" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>


                                        <div class="row mb-2">
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Sahəsi</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtzonearea" Height="50px" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin...">
                                                </asp:TextBox>
                                                <%--asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtmovzuadi" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>--%>
                                                <%--<asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtmovzuadi" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="row mb-2">
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Ölçü vahidi</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlunitmeasurement" class="form-control" runat="server"></asp:DropDownList>
                                                <%--asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtmovzuadi" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>--%>
                                                <%--<asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtmovzuadi" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="row mb-2">
                                            <label for="exampleInputUsername3" class="col-sm-5 col-form-label">Qeyd</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtnotes" Height="50px" class="form-control mb-0 mt-0" runat="server" placeholder="Mətni daxil edin..." TextMode="MultiLine">
                                                </asp:TextBox>
                                                <%--<asp:RegularExpressionValidator ValidationGroup="qrup1" Display="Dynamic" ControlToValidate="txtdissetantadi" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{3,500}$" runat="server" ForeColor="Red" ErrorMessage="Mətn 3 simvoldan cox olmalıdır."></asp:RegularExpressionValidator>--%>
                                                <%--<asp:RequiredFieldValidator CssClass="requiredstyle" ValidationGroup="qrup1" ControlToValidate="txtdissetantadi" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Mütləq doldurulmalıdır." ForeColor="Red"></asp:RequiredFieldValidator>--%>
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
    </section>
    <!-- End Default -->
</asp:Content>


