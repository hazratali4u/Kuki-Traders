<%@ Page Title="SAMS :: Sale Order" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleOrder.aspx.cs" Inherits="Forms_frmSaleOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("#<%=ddlDistributor.ClientID%>").searchable();
            $("#<%=ddlCustomer.ClientID%>").searchable();
            $("#<%=ddlContactPerson.ClientID%>").searchable();
            $("#<%=ddlSKus.ClientID%>").searchable();
            
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 10%; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>
                <table cellpadding="2px">
                    <tr>
                        <td style="width: 100px;">
                            <strong>
                                <label>
                                    Location</label>
                            </strong>
                        </td>
                        <td style="width: 265px;">
                            <asp:DropDownList ID="ddlDistributor" runat="server" Width="220px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                        <td rowspan="4" style="width: 37%"></td>
                        <td rowspan="4">
                            <fieldset>
                                <table>
                                    <tr>
                                        <td style="width: 140px; font-size: 16px; text-align: center">
                                            <strong>Ledger Balance</strong>
                                        </td>
                                        <tr>
                                        </tr>
                                        <td style="font-size: 16px; text-align: right">

                                            <asp:Label ID="lblLedgerBalance" runat="server"></asp:Label>
                                            <asp:Label ID="lblOpeningType" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Customer</label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="220px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" TabIndex="3">
                                </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <strong>
                                <asp:Label ID="lblAdress" runat="server" Text="Address"></asp:Label></strong>
                        </td>
                        <td>
                            <strong>
                                <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label></strong>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Contact Person</strong>
                        </td>

                        <td>
                            <asp:DropDownList ID="ddlContactPerson" runat="server" Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                </table>
                <table>
                    
                </table>
                <table>
                    <tr>
                        <td style="width: 100px">
                            <strong>Remarks </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" TabIndex="4" Width="445px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
                <fieldset style="width: 200px;">
                    <table>
                        <tbody>
                            <tr>
                                <td style="height: 16px">
                                    <asp:Label ID="lblskuname" runat="server" Width="357px" Text="Product Name" CssClass="lblDetail"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblquantity" runat="server" Width="86px" Text="Quantity" CssClass="lblDetail"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSKus" runat="server" Width="357px" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="88px" TabIndex="6"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        FilterType="Custom" ValidChars="0123456789" TargetControlID="txtQuantity">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="75px"
                                        Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" TabIndex="7" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:Panel ID="Panel2" runat="server" Width="530px" Height="155px" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" OnRowDeleting="grdOrder_RowDeleting"
                                            OnRowEditing="grdOrder_RowEditing" ShowHeader="False" Width="513px">
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="skuId" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="skuName" HeaderText="SKU Name">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="440px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="price" HeaderText="Price">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="qty" HeaderText="Quantity">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="amount" HeaderText="Amount">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                    <ItemStyle CssClass="grdDetail" Width="40px" HorizontalAlign="Center" />
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grdDetail" Width="45px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <strong>Total Qty </strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalQty" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <strong>Total Amount</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="110px" Font-Size="8pt"
                        TabIndex="8" Text="Save Order" UseSubmitBehavior="False" OnClick="btnSaveOrder_Click"
                        CssClass="Button" />
                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px" Font-Size="8pt"
                        TabIndex="9" Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
