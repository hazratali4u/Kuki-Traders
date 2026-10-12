<%@ Page Title="SAMS :: Purchase Vs Sale Activity" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="rptPurchaseVsSaleActivity.aspx.cs" Inherits="Forms_rptPurchaseVsSaleActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }
        function SelectAll() {
            var chkBoxList = document.getElementById('<%= LstCategory.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstCategory.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
        

        function onCalendarShown3() {
            var cal = $find("calendar3");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
            }
        }

        function onCalendarHidden3() {
            var cal = $find("calendar3");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
            }
        }

        function call3(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar3");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


        function onCalendarShown4() {
            var cal = $find("calendar4");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call4);
                    }
                }
            }
        }

        function onCalendarHidden4() {
            var cal = $find("calendar4");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call4);
                    }
                }
            }
        }

        function call4(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar4");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 90px; height: 2px" align="left">
                                                <strong>
                                                    Location</strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Text="Product Source"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSource" runat="server" Width="200px">
                                                  <asp:ListItem Value="-1">All</asp:ListItem>
                                                    <asp:ListItem Value="0">Local Vendors</asp:ListItem>
                                                    <asp:ListItem Value="1">Inhouse Production</asp:ListItem>
                                                    <asp:ListItem Value="2">Imported Vendors</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>Category</strong>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                            <td style="width: 204px; height: 2px" align="left">
                                                <asp:CheckBox ID="ChbAllCategory" onclick="SelectAll()" runat="server" Text="All">
                                                </asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 2px" align="left" colspan="3">
                                                <asp:Panel ID="Panel1" runat="server" Width="302px" Height="140px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove">
                                                    <asp:CheckBoxList ID="LstCategory" onclick="UnCheckSelectAll()" runat="server">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Sort By</strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSortBy" Width="200px" >
                                                    <asp:ListItem Text="Sold Qty" Value="SALED" Selected="True"></asp:ListItem>
                                                   
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Order By</strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlOrderBy" Width="200px" >
                                                    <asp:ListItem Selected="True" Text="Ascending" Value="ASC"></asp:ListItem>
                                                    <asp:ListItem Text="Descending" Value="DESC"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="80px" Height="13px" Text="From Month"></asp:Label></strong>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                &nbsp;<asp:TextBox ID="txtFromMonth" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                    Width="70px" CssClass="txtBox" MaxLength="8" ></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartMonth" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    To Month</strong>
                                            </td>
                                            <td style="width: 1px; height: 25px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                &nbsp;<asp:TextBox ID="txtToMonth" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                    Width="70px" CssClass="txtBox" MaxLength="8"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnEndMonth" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                        <%--<cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                         <cc1:CalendarExtender ID="CEFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="iFromDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>--%>
                                        <cc1:CalendarExtender ID="CEStartMonth" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3"
                                            OnClientHidden="onCalendarHidden3" Format="MMM-yyyy" PopupButtonID="ibtnStartMonth"
                                            TargetControlID="txtFromMonth">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CESEndMonth" runat="server" BehaviorID="calendar4" OnClientShown="onCalendarShown4"
                                            OnClientHidden="onCalendarHidden4" Format="MMM-yyyy" PopupButtonID="ibtnEndMonth"
                                            TargetControlID="txtToMonth">
                                        </cc1:CalendarExtender>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" Width="90" CssClass="Button"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" Width="90" CssClass="Button"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
