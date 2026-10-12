<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseOrderEntry.aspx.cs" Inherits="Forms_frmPurchaseOrderEntry"
    Title="SAMS :: Purchase Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function pageLoad() {
            $("select").searchable();
        }

        function CalcualeAmount() {
            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value;

            document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate);
        }
        function CalculateNetAmount() {

            var grossAmount = document.getElementById('<%=txtAmountFC.ClientID%>').value;
            var gst = document.getElementById('<%=txtgst.ClientID%>').value;
            var netAmount = document.getElementById('<%=txtNetAmount.ClientID%>').value;

            if (gst == "") {
                gst = 0;
            }

            if (netAmount > 0) {

                var gstValue = parseFloat((gst / 100) * parseFloat(grossAmount)).toFixed(2);
                document.getElementById("<%= txtGstValue.ClientID %>").value = gstValue;
                document.getElementById("<%= txtNetAmount.ClientID %>").value = parseFloat(parseFloat(grossAmount) + parseFloat(gstValue)).toFixed(2);


            }
        }
        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            str = document.getElementById('<%=txtRate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Rate');
                return false;
            }

            return true;
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                 <asp:HiddenField ID="hfClosingDate" runat="server"/>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 9%">
                                            <strong>Order For</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 90%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" Width="200px"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <strong>Transaction No</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td >
                                            <strong>Reference #</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <strong>Vendor</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td >
                                            <strong>Currency</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="200px">
                                                <asp:ListItem Value="0" Text="Rs."></asp:ListItem>
                                                <asp:ListItem Value="1" Text="US $"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="€ EUR"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="ACU $"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td >
                                            <strong>Exch Rate</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtExchangeRate" runat="server" Width="195px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtExchangeRate">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <strong>Credit Term Day</strong>
                                        </td>
                                        <td >
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtCreditTerm" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td align="left">
                                            <strong>Quantity Type</strong>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 238px; height: 25px" align="left">
                                            <asp:RadioButtonList ID="RbUnitType" runat="server" RepeatDirection="Horizontal"
                                                Width="225px">
                                                <asp:ListItem Selected="True" Value="0">Quantity In Unit</asp:ListItem>
                                                <asp:ListItem Value="1">Quantity In Ctns</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <%-- <div style="left: 10px; position: absolute; top: 270px">
                                        <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="Silver" 
                                            BorderStyle="Groove" BorderWidth="1px" CssClass="HidePanel" Height="170px" 
                                            Width="250px">
                                            SKU List<br />
                                            <asp:ListBox ID="lstCode" runat="server" Height="151px" 
                                                onkeypress="SelectCode(event)" SelectionMode="Multiple" Width="239px">
                                            </asp:ListBox>
                                        </asp:Panel>
                                    </div>--%>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td class="lblDetail">
                                                <label style="color: White;">
                                                    Product Description</label>
                                            </td>
                                            <td class="lblDetail">
                                                <label style="color: White;">
                                                    Quantity</label>
                                            </td>
                                            <td class="lblDetail">
                                                <label style="color: White;">
                                                    Price</label>
                                            </td>
                                            <td class="lblDetail">
                                                <label style="color: White;">
                                                    Amount</label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="300px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlSkuCde_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="73px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRate" runat="server" Width="70px" onblur="CalcualeAmount();"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtRate">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="76px" Enabled="false">0.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="180px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="100%">
                                                        
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                                                <ItemStyle CssClass="grdDetail" Width="286px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="Rate" DataFormatString="{0:F2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle CssClass="grdDetail" Width="36px" HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" Width="40px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                            <td style="vertical-align: bottom;">
                                                <fieldset style="width: 40%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <strong>Min Stock:</strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblMinStock"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>Max Stock:</strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblMaxStock"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                                <p>
                                                    &nbsp;</p>
                                                <fieldset>
                                                    <legend style="width: 75px;">Calculation</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="Label7" runat="server" Height="16px" Text="Total Qty"></asp:Label></strong>
                                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="88px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="Label1" runat="server" Height="16px" Text="Amount"></asp:Label></strong>
                                                                <asp:TextBox ID="txtAmountFC" runat="server" Width="120px"></asp:TextBox>
                                                            </td>
                                                            <td >
                                                                <strong>
                                                                    <asp:Label ID="Label2" runat="server" Height="16px" Text="[Rs.]:" Visible="false"></asp:Label></strong>
                                                                <asp:TextBox ID="txtAmountRs" runat="server" Width="120px" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none;">
                                                            <td >
                                                                <strong>
                                                                    <asp:Label ID="Label5" runat="server" Height="16px" Visible="false" Text="Sales Tax %"></asp:Label></strong>
                                                                <asp:TextBox ID="txtgst" runat="server" Width="88px" Visible="false" onkeyup="CalculateNetAmount();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="Label6" runat="server" Height="16px"  Visible="false" Text="Sales Tax Value"></asp:Label></strong>
                                                                <asp:TextBox ID="txtGstValue" runat="server" Width="120px" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="Label8" runat="server" Height="16px" Text="Net Amount" Visible="false"></asp:Label></strong>
                                                                <asp:TextBox ID="txtNetAmount" runat="server" Width="120px" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <br />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
