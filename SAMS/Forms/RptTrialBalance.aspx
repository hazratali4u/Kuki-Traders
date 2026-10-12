<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptTrialBalance.aspx.cs" Inherits="Forms_RptTrialBalance" Title="SAMS :: Trial Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:CheckBox ID="ChbSelectAll" runat="server" Height="27px" Text="All Accounts" Font-Bold="true"
                                            Width="100px" AutoPostBack="True" Checked="True" OnCheckedChanged="ChbSelectAll_CheckedChanged" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    <strong>Voucher Type</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:RadioButtonList ID="rdbPostType" runat="server" Height="27px"  RepeatDirection="Horizontal" Width="200px">
                                            <asp:ListItem Text="Posted" Value="1" Selected="True"></asp:ListItem>
                                             <asp:ListItem Text="UnPosted" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>Location</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="223px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>By Column</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <asp:DropDownList ID="drpTrialLevel" runat="server" Width="223px">
                                            <asp:ListItem Selected="True" Text="Two Columns" Value="Two"></asp:ListItem>
                                            <asp:ListItem Text="Six Columns" Value="Six"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>Level</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <asp:DropDownList ID="DrpLevel" runat="server" Width="222px" CssClass="DropList">
                                            <asp:ListItem Value="4">Level4</asp:ListItem>
                                            <asp:ListItem Value="3">Level3</asp:ListItem>
                                            <asp:ListItem Value="2">Level2</asp:ListItem>
                                            <asp:ListItem Value="1">Level1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>Main Account</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:DropDownList ID="DrpMainAccount" runat="server" Width="222px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="From Account Head"
                                                Width="120px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:DropDownList ID="ddlFromAccount" runat="server" Width="222px" Enabled="false">
                                            <asp:ListItem Text="0000000000" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>To Account Head</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="width: 224px; height: 25px">
                                        <asp:DropDownList ID="ddlToAccount" runat="server" Width="222px" Enabled="false">
                                            <asp:ListItem Text="9999999999" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>From Date</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                            Width="150px"></asp:TextBox>&nbsp;<asp:ImageButton ID="ibtnStartDate" runat="server"
                                                ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>To Date</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                            Width="150px"></asp:TextBox>&nbsp;<asp:ImageButton ID="ibnEndDate" runat="server"
                                                ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px; width: 224px;">
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                            TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   &nbsp; &nbsp;&nbsp;<asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
