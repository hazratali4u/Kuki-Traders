<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerRelization.aspx.cs" Inherits="Forms_frmCustomerRelization"
    Title="SAMS :: Bank Transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }
        function pageLoad() {
            $('#<%=GrdOrder.ClientID %>').tablesorter(
	     {
	         headers: {
	             3: {
	                 sorter: false
	             },
	             6: {
	                 sorter: false
	             },
	             10: {
	                 sorter: false
	             },
	             11: {
	                 sorter: false
	             },
	             12: {
	                 sorter: false
	             },
	             13: {
	                 sorter: false
	             }
	         }
	     }
	     );
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 300px; width: 100px; position: absolute; top: 245px;
            height: 100px">
            <asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="23px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 400px;">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 201px" align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="58px" Text="Town"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Customer" Width="55px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Account Type" Width="99px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="width: 201px">
                                                <asp:DropDownList ID="DrpAccountType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpAccountType_SelectedIndexChanged"
                                                    Width="200px">
                                                    <asp:ListItem Value="21">Cash </asp:ListItem>
                                                    <asp:ListItem Value="33">Online Transfer </asp:ListItem>
                                                    <%-- <asp:ListItem Value="34">Cheque Received</asp:ListItem>--%>
                                                    <%-- <asp:ListItem Value="35">Cash Sales</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="97px" Text="Bank/Cash" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px">
                                                <asp:DropDownList ID="DrpAccountDetail" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpAccountDetail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 201px">
                                            </td>
                                            <td valign="middle" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblSlipNo" runat="server" Width="94px" Text="Slip No" Visible="False"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="lblChequeNo" runat="server" Width="94px" Text="Cheque No" Visible="False"></asp:Label></strong>
                                                <strong>
                                                    <asp:Label ID="lblTransferDate" runat="server" Width="94px" Text="Transfer Date"
                                                        Visible="False"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" Visible="False"></asp:TextBox>
                                                <asp:TextBox ID="txtTransferDate" runat="server" Width="194px" Visible="False" ></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:ImageButton ID="imgTransferDate" runat="server" Visible="false" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                                <ajaxToolkit:CalendarExtender ID="ceExpDate" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="imgTransferDate" TargetControlID="txtTransferDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="73px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="199px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                                    Text="Cancel" CssClass="Button" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td>
                                
                                <asp:Panel ID="pnlInvoices" runat="server" Height="200px" ScrollBars="Vertical" BorderColor="Silver"
                                    BorderStyle="Groove" BorderWidth="1px" Width="348px">
                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="SteelBlue" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        Width="100%" DataKeyNames="SALE_INVOICE_ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="INV No">
                                                <ItemStyle CssClass="grdDetail" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="INV Date">
                                                <ItemStyle CssClass="grdDetail" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MANUAL_DOCUMENT_NO" HeaderText="Type">
                                                <ItemStyle CssClass="grdDetail" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                           
                        </tr>
                    </table>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    
                                                 <asp:HiddenField ID="hfZoneId" runat="server"></asp:HiddenField>
                </div>
                <table width="72%">
                    <tr align="right">
                        <td style="width: 30%">
                        </td>
                        <td style="width: 40%; text-align: right; font-size: medium; color: steelBlue;">
                            <strong>Today's Received Payment: &nbsp;
                                <asp:Label runat="server" ID="lblAmount" Text="0"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DrpRoute" EventName="SelectedIndexChanged">
                </asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                        width: 650px; border-bottom: silver thin inset">
                        <tbody>
                            <tr>
                                <td style="height: 20px" align="left" colspan="5">
                                    <asp:Panel ID="Panel12" runat="server" Width="750px" Height="195px" ScrollBars="Vertical">
                                        <table style="background-color: silver" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 21px" align="left">
                                                        <strong>
                                                            <asp:Label ID="Label110" runat="server" Width="154px" Text="Select Searching Type"></asp:Label></strong>
                                                    </td>
                                                    <td style="width: 170px; height: 21px" align="left">
                                                        <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                            <asp:ListItem Value="CHEQUE_NO">All Records</asp:ListItem>
                                                            <asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
                                                            <asp:ListItem Value="ACCOUNT_NAME">Bank Name</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 224px; height: 21px" align="left">
                                                        <asp:TextBox ID="txtSearch" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                    </td>
                                                    <td style="width: 250px; height: 21px" align="left">
                                                        <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                            OnClick="btnFilter_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="GrdOrder" runat="server" Width="733px" ForeColor="SteelBlue" CssClass="gridRow2"
                                            BorderColor="SteelBlue" OnRowDeleting="GrdOrder_RowDeleting" HorizontalAlign="Center"
                                            BackColor="White" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Voucher_no" HeaderText="Voucher_no">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel" ></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Document_no" HeaderText="Document_no">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                    <ItemStyle CssClass="grdDetail" Width="19%" ></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Bank Name">
                                                    <ItemStyle CssClass="grdDetail" Width="19%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="region" HeaderText="Region">
                                                    <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="branchCode" HeaderText="Branch Code">
                                                    <ItemStyle CssClass="grdDetail" Width="9%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Balance" DataFormatString="{0:F2}" HeaderText="Amount">
                                                    <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grdDetail" Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                            </HeaderStyle>
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView>
                                      
                                        <asp:HiddenField ID="hfVoucherNo" Value="" runat="server"></asp:HiddenField>
                                         
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="70%">
                        <tr style="text-align: right;">
                            <td>
                                <strong>Total Amount</strong>
                                <asp:TextBox ID="txttotalAmount" runat="server" Width="130px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
