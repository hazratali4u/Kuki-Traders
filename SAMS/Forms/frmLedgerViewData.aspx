<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmLedgerViewData.aspx.cs" Inherits="Forms_frmLedgerViewData" Title="SAMS :: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.GrdLedger.Rows.Count %>');
            //Get total no. of checked Select Product CheckBoxes in side the GridView.
            Counter = 0;
        }
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.GrdLedger.ClientID %>');
            var TargetChildControl = "ChbSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)

                    if (!Inputs[n].disabled) {
                        Inputs[n].checked = CheckBox.checked;
                    }

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;
            //Change state of the header CheckBox.        
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
    </script>
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
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Voucher Sub Type" Width="104px"></asp:Label></strong>
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
                                        <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="User Name" Width="104px"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:DropDownList ID="DrpUser" runat="server" CssClass="DropList" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px" align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                        Width="190px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 25px;">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                        Width="191px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
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
                        <table width="300">
                            <tr>
                                <td style="width: 100px; height: 21px;">
                                </td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Button ID="btnView" runat="server" Font-Size="8pt" OnClick="btnView_Click" Text="View"
                                        CssClass="Button" Width="107px" />
                                </td>
                                <td style="width: 100px; height: 21px;">
                                </td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnDelete" runat="server" Font-Size="8pt" Text="Cancel Voucher" CssClass="Button"
                                        Width="108px" OnClick="btnDelete_Click" />
                                </td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnPrintVoucher" runat="server" Font-Size="8pt" Text="Print Voucher"
                                        CssClass="Button" Width="108px" OnClick="btnPrintVoucher_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                            width: 650px; border-bottom: silver thin inset; height: 251px;">
                            <tbody>
                                <tr>
                                    <td align="left" colspan="5" style="height: 21px">
                                        <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="750px">
                                            <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="SteelBlue" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                OnRowEditing="GrdLedger_RowEditing" OnRowDeleting="GrdLedger_RowDeleting" OnRowCreated="GrdLedger_RowCreated">
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Voucher">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChbSelect" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="ChbSelectHead" runat="server" onclick="javascript:HeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="4%"/>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No">
                                                        <ItemStyle CssClass="grdDetail" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date">
                                                        <ItemStyle CssClass="grdDetail" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                        <ItemStyle CssClass="grdDetail" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="Voucher Amount" DataFormatString="{0:N}">
                                                        <ItemStyle CssClass="grdDetail" Width="15%" HorizontalAlign="Right"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle CssClass="grdDetail" Width="25%" />
                                                        <HeaderStyle HorizontalAlign="Left" />
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
                                                        <ItemStyle CssClass="grdDetail" Width="5%"  HorizontalAlign="Center"/>
                                                    </asp:CommandField>
                                                    <asp:TemplateField >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;"
                                                                Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grdDetail" Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="grdHead" BorderColor="SteelBlue" HorizontalAlign="Center" VerticalAlign="Middle" />
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
