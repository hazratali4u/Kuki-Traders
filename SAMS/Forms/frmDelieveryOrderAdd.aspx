<%@ Page Title="SAMS :: Delivery Order" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmDelieveryOrderAdd.aspx.cs" Inherits="Forms_frmDelieveryOrderAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 60%; width: 100px; position: absolute; top: 1%; height: 80px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait....
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfIsGstReg" />
                <asp:HiddenField runat="server" ID="hfOrderQty" />
                <asp:HiddenField runat="server" ID="hfGst" />
                <asp:HiddenField runat="server" ID="hfInvoiceDiscount" />
                <table cellpadding="2px">
                    <tr class="tblhead">
                        <td class="tblhead" style="width: 400px;">
                            <strong>
                                <asp:Label runat="server" ID="lblCustomer" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblDate" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblDONo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblOrderNo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 65px;">
                            <strong>
                                <label>
                                    Location</label>
                            </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistributor" runat="server" Width="231px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <label>
                                    Customer</label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="231px">
                                </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfStandardDiscount" Value="0" />
                
                <asp:HiddenField runat="server" ID="hfExtraDiscount" Value="0" />

                <asp:HiddenField runat="server" ID="hfGstAmount" Value="0" />
                <table width="100%">
                    <tr valign="top">
                        <td style="width: 65%">
                            <fieldset style="width: 200px;">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="645px" Height="280px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                        OnRowDataBound="grdOrder_RowDataBound">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="skuId" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="skuName" HeaderText="Product Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="440px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="qty" HeaderText="Order Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Prev Dispatch Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="75px" Text='<%# Eval("RcdQty")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQty" runat="server" Width="75px" Text='<%# Eval("CurrentRcdQty")%>'
                                                                        OnTextChanged="txtCurrentRecQty_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CurrentRcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="standardDiscount" HeaderText="Statndard Discounted" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="discountedPrice" HeaderText="Discounted Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="netAmount" HeaderText="netAmount" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 205px;">
                                            </td>
                                            <td>
                                                <strong style="font-size: 14px;">Total Amount</strong>
                                                <asp:TextBox ID="txtTotalQty" runat="server" Font-Size="14px" Width="100px" Enabled="false"
                                                    Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtTotalAmount" runat="server" Font-Size="14px" Width="100px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 100%">
                                <%-- <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <table>
                                             <tr>
                                                    <td style="font-size: 16px;">
                                                        <strong>Business Limit:</strong>
                                                    </td>
                                                    <td style="font-size: 16px;">
                                                        <asp:Label ID="lblSaleLimit" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <table>
                                                <tr>
                                                    <td style="width: 200px; font-size: 16px;">
                                                        <strong>Ledger Balance:</strong>
                                                    </td>
                                                    <td style="font-size: 16px;">
                                                        <asp:Label ID="lblLedgerBalance" runat="server"></asp:Label>&nbsp;
                                                        <asp:Label ID="lblOpeningType" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <strong>Driver Name </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDriverName" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <strong>Cell No </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCellNo" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <strong>Truck No </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTruckNo" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <strong>Builty No </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBuilty" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <strong>Payment Mode</strong>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:RadioButtonList ID="rdbInvoiceType" runat="server" Width="130px" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Credit" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Cash"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <strong>Discount Type</strong>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbDiscountType" runat="server" Width="130px" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="%" ></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Value" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <strong>Invoice Discount</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFreight" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Remarks </strong>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Columns="40" Rows="3"
                                            Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:CheckBox ID="chkLimit" Text="Skip Limit" Font-Size="14px" runat="server" /></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="100px" Font-Size="8pt"
                        Text="Save" UseSubmitBehavior="False" OnClick="btnSaveOrder_Click" CssClass="Button" />
                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                </td>
                <%--   <td>
                    
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td valign="top" align="left" colspan="2" rowspan="7">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU"></asp:Label></strong><asp:Panel
                                            ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                            BorderWidth="1px">
                                            <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
                                                </RowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                        <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                        </ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                        <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                        </ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                            </asp:GridView>
                                        </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
                    
                </td>
                --%>
            </tr>
        </table>
    </div>
</asp:Content>
