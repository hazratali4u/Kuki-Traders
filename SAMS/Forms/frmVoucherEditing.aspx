<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVoucherEditing.aspx.cs"
    Inherits="Forms_frmVoucherEditing" Title="DMS: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style>
        fieldset
        {
            margin: 0px 0px;
            border: 1px solid Silver;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }
        fieldset p
        {
            border: 1px solid #fff;
        }
        fieldset legend
        {
            position: relative;
            top: -1em;
            font-weight: bold;
            display: block;
            text-align: right;
            padding: 0px 0px;
        }
        fieldset legend a
        {
            color: #006699;
            text-decoration: underline;
        }
        fieldset legend a:hover
        {
            color: #3399cc;
        }
        fieldset form div.right
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="../AjaxLibrary/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("select").searchable();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

        }

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtVoucherDate.ClientID %>").value;
            if (str == null || str.length < 9) {
                alert('Must Enter Voucher Date');
                return false;
            }
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }
            else if (parseFloat(Debit) > 0 && parseFloat(Credit) > 0) {
                alert('Enter only Debit or Credit');
                return false;
            }

            return true;
        }
    </script>
</head>
<body style="background: White">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="VoucherScriptManager" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <h2>
                    Voucher Editing</h2>
            </td>
        </tr>
        <tr>
            <td style="width: 70%;" align="center">
                <fieldset style="width: 75%;">
                    <table width="100%">
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label></strong>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 23%">
                                <asp:DropDownList ID="drpDistributor" runat="server" Width="206px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 20%">
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 35%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="Label6" runat="server" Text="Voucher Date"></asp:Label></strong>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 23%">
                                <asp:TextBox ID="txtVoucherDate" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 20%">
                                <strong>
                                    <asp:Label ID="Label1" runat="server" Text="Voucher No"></asp:Label></strong>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox ID="lblVoucherNo" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="Label5" runat="server" Text="Voucher Type"></asp:Label></strong>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 23%">
                                <asp:DropDownList ID="DrpVoucherType" runat="server" Enabled="False" Width="205px">
                                    <asp:ListItem Value="14">Cash Voucher</asp:ListItem>
                                    <asp:ListItem Value="15">Bank Voucher</asp:ListItem>
                                    <asp:ListItem Value="16">Journal Voucher</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 20%">
                                <strong>
                                    <asp:Label ID="lblVouchetSubType" runat="server" Text="Voucher Sub Type"></asp:Label></strong>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="DrpVoucherSubType" runat="server" Enabled="False" Width="205px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <legend></legend>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblPyamentMode" runat="server" CssClass="lblbox" Text="Payment Mode"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DrpPaymentMode" runat="server" Width="206px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="width: 99px">
                                    <strong>
                                        <asp:Label ID="lblBank" runat="server" Text="Bank/Cash Head"></asp:Label>
                                    </strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBank" runat="server" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtChequeNo" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblSlipNo" runat="server" Text="Slip No"></asp:Label></strong>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSlipNo" runat="server" Width="200px"></asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblDueDate" runat="server" CssClass="lblbox" Text="Due Date"></asp:Label>
                                    </strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDueDate" runat="server" onkeyup="BlockEndDateKeyPress()" ReadOnly="true"
                                        Width="200px"></asp:TextBox>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" CssClass="lblbox" Text="Payee's Name"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtpayeesName" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label14" runat="server" CssClass="lblbox" Width="10px"></asp:Label>
                                </td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Narration" Width="85px"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="200px" MaxLength="255"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="z-index: 101; left: 471px; width: 100px; position: absolute; top: 291px;
                            height: 100px">
                            <asp:Panel ID="Panel212" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="39px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:TextBox ID="txtTotalDebit" runat="server" CssClass="HidePanel" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtTotalCredit" runat="server" CssClass="HidePanel" Width="73px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 100px">
            </td>
            <td align="center">
                &nbsp;<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="border-right: #007395 1px solid; border-top: #007395 1px solid;
                            border-left: #007395 1px solid; border-bottom: #007395 1px solid">
                            <tr>
                                <td class="tblhead">
                                    <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Font-Bold="True" Text="Account Description"></asp:Label>
                                </td>
                                <td class="tblhead">
                                    <asp:Label ID="lblskuname" runat="server" ForeColor="White" Font-Bold="True" Text="Remarks"></asp:Label>
                                </td>
                                <td class="tblhead">
                                    <asp:Label ID="Label11" runat="server" ForeColor="White" Font-Bold="True" Text="Debit"></asp:Label>
                                    <td class="tblhead">
                                        <asp:Label ID="Label12" runat="server" ForeColor="White" Font-Bold="True" Text="Credit"></asp:Label>
                                    </td>
                                    <td class="tblhead">
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Width="70px"></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlAccountHead" runat="server" Width="300">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccountDes" runat="server" Width="140px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td style="width: 79px;">
                                    <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="70px"
                                        Font-Size="8pt" Text="Add New" CssClass="Button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6" rowspan="1">
                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CaptionAlign="Left" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing"
                                        ShowFooter="True" ShowHeader="False" Width="100%" Font-Size="X-Small">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <RowStyle ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
                                                <FooterStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name" HeaderText="Account Name">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="230px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                                <FooterStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" HeaderText="Account Description">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="140px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="debit" DataFormatString="{0:F2}" HeaderText="Debit">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="credit" DataFormatString="{0:F2}" HeaderText="Credit">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Principal_id" HeaderText="Principal">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" />
                                        <PagerStyle BackColor="Transparent" />
                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                            TargetControlID="txtCreditAmount" ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                            TargetControlID="txtDebitAmount" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp;<asp:Button ID="btnDone" runat="server" AccessKey="S" Font-Size="8pt" OnClick="btnDone_Click"
                    Text="Save" Width="105px" CssClass="button" />
            </td>
            <td style="width: 93px">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
