<%@ Page Title="SAMS :: Vendor Invoice" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmVendorInvoice.aspx.cs" Inherits="Forms_frmVendorInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("select").searchable();

        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;

        }
        function ValidateValueForm() {
            var str;

            str = document.getElementById("<%= drpDistributor.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Plz select Location');
                return false;
            }

            str = document.getElementById("<%= DrpVendor.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Plz select vendor');
                return false;
            }

            str = document.getElementById("<%= txtInvNo.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Plz enter Invoice No');
                return false;
            }

            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }


            return true;
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 750px; width: 100px; position: absolute; top: 10px;
            height: 100px">
            <asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="23px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel runat="server" ID="upMain" RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField runat="server" Value="" ID="hfVMId" />
                <table>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 90px" align="left">
                            <strong>Location</strong>
                        </td>
                        <td style="width: 240px; height: 17px" align="left">
                            <asp:DropDownList ID="drpDistributor" runat="server" Width="240" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 100px;">
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 90px" align="left">
                            <strong>Vendor</strong>
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:DropDownList ID="DrpVendor" runat="server" Width="240px" CssClass="DropList"
                                OnSelectedIndexChanged="DrpVendor_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 100px" align="left">
                            <strong>Invoice Date</strong>
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Width="216px" Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="ibtnInvoiceDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="16px" />
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="ibtnInvoiceDate"
                                TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 100px" align="left">
                            <strong>Invoice No</strong>
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:TextBox ID="txtInvNo" runat="server" Width="235px">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender runat="server" ID="ftbtxtInvoice" FilterType="Numbers"
                                TargetControlID="txtInvNo">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 100px" align="left">
                            <strong>Amount</strong>
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:TextBox ID="txtAmount" runat="server" Width="235px">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" ValidChars=".0987654321"
                                TargetControlID="txtAmount">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 100px;">
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 100px" align="left">
                            <strong>Remarks</strong>
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:TextBox ID="txtRemarks" runat="server" Width="305px">
                            </asp:TextBox>
                        </td>
                        <td style="width: 100px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 26px;">
                        <td style="height: 17px; width: 100px" align="left">
                            <asp:Button AccessKey="S" ID="btnSave" runat="server" Width="100px" Font-Size="8pt"
                                Text="Save" CssClass="Button" OnClick="btnSave_Click" />
                        </td>
                        <td style="width: 201px; height: 17px" align="left">
                            <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                Text="Cancel" CssClass="Button" OnClick="btnCancel_Click" />
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                    width: 650px; border-bottom: silver thin inset">
                                    <tbody>
                                        <tr>
                                            <td style="height: 20px" align="left" colspan="5">
                                                <asp:Panel ID="Panel12" runat="server" Width="774px" Height="200px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GrdVendorMaster" runat="server" Width="100%" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="SteelBlue" HorizontalAlign="Center" BackColor="White"
                                                        AutoGenerateColumns="False" OnRowEditing="GrdVendorMaster_RowEditing" OnRowDeleting="GrdVendorMaster_RowDeleting">
                                                        <Columns>
                                                            <asp:BoundField DataField="VENDER_INVOICE_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRINCIPAL_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="12%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DISTRIBUTOR_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText=" Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="6%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="INVOICE_DATE" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="8%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                                        <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" Width="3%" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        ToolTip="Delete">
                                                                        <img id="imgDelete" alt="" src="~/images/delete.gif" runat="server" />
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="3%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
