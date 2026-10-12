<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVoucherView.aspx.cs" Inherits="Forms_frmVoucherView" Title="SAMS :: Voucher View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="lbltoLocation" runat="server" Width="67px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Voucher Type" Width="104px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpVoucherType" runat="server" Width="200px" CssClass="DropList"
                                        AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Voucher Sub Type" Width="104px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpVoucherSubType" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px" align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                        Width="177px"></asp:TextBox>
                                         <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
                                </td>
                                <td align="left" style="height: 25px;">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                        Width="177px"></asp:TextBox>
                                          <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
                                </td>
                                <td align="left" style="height: 25px">
                                  
                                </td>
                            </tr>
                            <tr>
                                   
                                    <td align="left">
                                    <strong>Voucher Type</strong>
                                    </td>
                                   
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:RadioButtonList ID="rdbPostType" runat="server" Height="27px"  RepeatDirection="Horizontal" Width="200px">
                                            <asp:ListItem Text="Posted" Value="true" Selected="True"></asp:ListItem>
                                             <asp:ListItem Text="UnPosted" Value="false"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                     <td align="left">
                                    </td>
                                </tr>
                            <tr>
                                <td align="left" style="width: 100px; height: 12px" valign="top">
                                </td>
                                <td align="left" style="width: 100px; height: 12px">
                                    <asp:Button ID="btnView" runat="server" CssClass="Button" OnClick="btnView_Click"
                                        Text="View" Width="90" />
                                </td>
                                <td align="left" style="height: 25px">
                                </td>
                            </tr>
                        </table>
                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                            TargetControlID="txtStartDate">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                            TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                            width: 650px; border-bottom: silver thin inset; height: 251px;">
                            <tbody>
                                <tr>
                                    <td align="left" colspan="5" style="height: 21px">
                                        <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto" Width="720px">
                                            <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="SteelBlue" ForeColor="SteelBlue" OnRowEditing="GrdLedger_RowEditing"
                                                Width="100%">
                                                
                                                <Columns >
                                                
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No">
                                                        <ItemStyle CssClass="grdDetail" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date">
                                                        <ItemStyle CssClass="grdDetail" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                        <ItemStyle CssClass="grdDetail" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="Voucher Amount" DataFormatString="{0:N}">
                                                        <ItemStyle CssClass="grdDetail" Width="13%" HorizontalAlign="Right"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle CssClass="grdDetail" Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_TYPE_ID" HeaderText="VOUCHER_TYPE_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_DATE" HeaderText="VOUCHER_DATE">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:CommandField  ShowEditButton="True" EditText="Print">
                                                        <ItemStyle CssClass="grdDetail" Width="5%" HorizontalAlign="Center"/>
                                                    </asp:CommandField>
                                                </Columns>
                                                <HeaderStyle CssClass="grdHead" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
