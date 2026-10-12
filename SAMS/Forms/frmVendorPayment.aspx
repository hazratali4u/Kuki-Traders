<%@ Page Title="SAMS :: Supplier Payment" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmVendorPayment.aspx.cs" Inherits="Forms_frmVendorPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="upVendorPayment" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 50%;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblPrincipal" runat="server" Text="Supplier"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:DropDownList ID="ddlPrincipal" runat="server" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPrincipal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 50%;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="200px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                <asp:ListItem Value="18" Text="Cheque"></asp:ListItem>
                                <asp:ListItem Value="19" Text="Cash"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 50%;">
                            <asp:DropDownList ID="ddlBank" runat="server" Width="202px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblCheque" runat="server" Text="Cheque No"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:TextBox ID="txtCheque" runat="server" Width="198px"></asp:TextBox>
                        </td>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblSlip" runat="server" Text="Slip No"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 50%;">
                            <asp:TextBox ID="txtSlip" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:TextBox ID="txtChequeDate" runat="server" Width="180px" onkeyup="BlockEndDateKeyPress()"
                                Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="16px" />
                            <ajax:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                TargetControlID="txtChequeDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 50%;">
                            <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount"
                                FilterType="Custom" ValidChars="-.0123456789">
                            </ajax:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="Label1" runat="server" Text="Cheque Status"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 22%">
                            <asp:DropDownList ID="drpChequeStatus" Width="200px" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpChequeStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Due" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 50%;">
                        </td>
                    </tr>
                    <%-- <tr>
                        <td style="width:2%;">&nbsp;</td>
                        <td style="width:10%;">--%>
                    <strong>
                        <asp:Label ID="lblIncomeTax" runat="server" Text="Income Tax" Visible="False"></asp:Label>
                    </strong>
                    <%-- </td>
                        <td style="width:22%">--%>
                    <asp:TextBox ID="txtIncomeTax" runat="server" Width="198px" Visible="False"></asp:TextBox>
                    <%-- </td>
                        <td style="width:10%;">--%>
                    <strong>
                        <asp:Label ID="lblDiscount" runat="server" Text="Discount" Visible="False"></asp:Label>
                    </strong>
                    <%--  </td>
                        <td style="width:50%;">--%>
                    <asp:TextBox ID="txtDiscount" runat="server" Width="200px" Visible="False"></asp:TextBox>
                    <%-- </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblNarration" runat="server" Text="Narration"></asp:Label>
                            </strong>
                        </td>
                        <td colspan="3" style="width: 88%">
                            <asp:TextBox ID="txtNarration" runat="server" Width="563px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 22%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Click"
                                Width="103px" />
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 50%;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 98%;" colspan="4">
                            <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Auto"
                                BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid">
                                <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="SteelBlue" HorizontalAlign="Center" OnRowDeleting="GrdOrder_RowDeleting"
                                    OnRowEditing="GrdOrder_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SLIP_NO" HeaderText="Slip No">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHQUE_DATE" HeaderText="Cheque Date" DataFormatString="{0:dd-MMM-yyyy}">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f2}">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Discount" HeaderText="Discount">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IncomeTax" HeaderText="IncomeTax">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vendor_ledger_Id" HeaderText="vendor_ledger_Id">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PAYMENT_MODE" HeaderText="PAYMENT_MODE">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="time_stamp"  >
                                              <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHEQUE_STATUS" HeaderText="CHEQUE_STATUS">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowEditButton="True" ShowHeader="True">
                                            <HeaderStyle CssClass="grdHead" />
                                            <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                        </asp:CommandField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="grdHead">
                                    </HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField runat="server" ID="hfVId" />
                  <asp:HiddenField runat="server" ID="hfLId" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
