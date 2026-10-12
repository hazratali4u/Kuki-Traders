<%@ Page Title="SAMS :: Purchase Entry" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPurchase.aspx.cs" Inherits="Forms_frmPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }


        function CalculateAmount() {
            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value;

            document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate);
        }

        function CalculateNetAmount() {

            var Discount = document.getElementById('<%=txtTotalDiscount.ClientID%>').value;
            var Amount = document.getElementById('<%=txtGrossAmount.ClientID%>').value;

            if (Discount == "") {
                Discount = 0;
            }

            if (Amount == "") {
                Amount = 0;
            }
            else {
                document.getElementById("<%= txtNetAmount.ClientID %>").value = (Amount - Discount);
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
            str = document.getElementById('<%=txtInvoiceNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Invoice No');
                return false;
            }

            return true;
        }
        function onlyNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;

            return true;
        }
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;
            return true;
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
                                            <strong>Purchase For</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="true" Width="200px"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9%">
                                            <strong>Transaction No</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td style="width: 9%">
                                            <strong>Reference #</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9%">
                                            <strong>Vendor</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9%">
                                            <strong>Invoice No</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" Width="195px"
                                             onkeypress="return onlyNumbers(this,event);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>Remarks</strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
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
                                <asp:HiddenField runat="server" ID="hfRowNo" />
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
                                                    Rate</label>
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
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="73px" onkeypress="return onlyNumbers(this,event);"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRate" runat="server" Width="70px" onblur="CalculateAmount();"
                                                    onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </td>
                                            
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="76px" Enabled="false">0.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Width="665px" Height="180px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="100%">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
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
                                                <fieldset>
                                                    <legend style="width: 75px;">Calculation</legend>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 50%; height:26px;">
                                                                <strong>Total Qty</strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="120px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height:26px;">
                                                                <strong>
                                                                    <asp:Label ID="Label1" runat="server" Height="16px" Text="Gross Amount"></asp:Label></strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGrossAmount" runat="server" Width="120px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height:26px;">
                                                                <strong>Discount</strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalDiscount" runat="server" Width="120px" onkeyup="CalculateNetAmount();"
                                                                  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height:26px;">
                                                                <strong>
                                                                    <asp:Label ID="Label8" runat="server" Height="16px" Text="Net Amount"></asp:Label></strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNetAmount" runat="server" Width="120px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                              
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
