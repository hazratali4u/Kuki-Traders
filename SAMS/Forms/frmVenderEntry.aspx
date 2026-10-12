<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVenderEntry.aspx.cs" Inherits="Forms_frmVenderEntry" Title="SAMS :: Vender/Invoice Step 2"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
            var ddlBatch = $('#<%=ddlBatch.ClientID %>');
            //            $('#<%=ddlBatch.ClientID %> option').remove();
            //            var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
            //            for (var i = 0; i < obj.length; i++) {
            //                var item = obj[i];
            //                if (item.SKU_ID == document.getElementById("<%=ddlSKuCde.ClientID %>").value) {
            //                    document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
            //                    document.getElementById("<%= lblInStore.ClientID %>").innerHTML = item.CLOSING_STOCK;
            //                    document.getElementById("<%= lblInBatch.ClientID %>").innerHTML = item.UNITS_IN_STOCK;
            //                    var opts = $("<option />");
            //                    opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
            //                    ddlBatch.append(opts);
            //                }
            //            }

            $('#<%=ddlBatch.ClientID %>').change(function () {
                document.getElementById("<%= lblInStore.ClientID %>").innerHTML = '0';
                document.getElementById("<%= lblInBatch.ClientID %>").innerHTML = '0';
                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());

                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (item.SKU_ID == $('#<%=ddlBatch.ClientID %> option:selected').val() && item.BATCH_NO == $('#<%=ddlBatch.ClientID %> option:selected').text()) {
                        document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                        var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                        unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                        document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;

                        break;
                    }
                }
                //  document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
                //                document.getElementById("<%= drpDistributor.ClientID %>").focus();
            });
            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                var ddlBatch = $('#<%=ddlBatch.ClientID %>');
                var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
                $('#<%=ddlBatch.ClientID %> option').remove();

                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    document.getElementById("<%= lblInStore.ClientID %>").innerHTML = item.CLOSING_STOCK;
                    document.getElementById("<%= lblInBatch.ClientID %>").innerHTML = item.UNITS_IN_STOCK;
                    if (item.SKU_ID == $('#<%=ddlSKuCde.ClientID %> option:selected').val()) {
                        var opts = $("<option />");
                        opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                        ddlBatch.append(opts);
                        break;
                    }
                }
                document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();

            });
            //            document.getElementById("<%= drpDistributor.ClientID %>").focus();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtCustomerInvoiceNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Customer Invoice No');
                return false;
            }
            str = document.getElementById('<%=txtBillBookNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Principal Invoice No');
                return false;
            }

            str = document.getElementById('<%=txtTransporter.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Transporter');
                return false;
            }
            str = document.getElementById('<%=txtTruckNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Truck No');
                return false;
            }
            str = document.getElementById('<%=txtTruckSize.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Truck Size');
                return false;
            }
            str = document.getElementById('<%=txtDriverCellNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Driver Cell No');
                return false;
            }
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }


            //            var InBatch = document.getElementById('<%=lblInBatch.ClientID%>').innerHTML;
            //            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            //            var PrvQty = document.getElementById("<%=hfPrevQty.ClientID %>").value;
            //            if (parseInt(Qty) > parseInt(InBatch) + parseInt(PrvQty)) {
            //                alert('Quantity can not be greater than available Stock.');
            //                document.getElementById("<%= txtQuantity.ClientID %>").focus();
            //                return false;
            //}
            //  document.getElementById("<%= ddlSKuCde.ClientID %>").focus();
            return true;
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <span class="heading">Purchase/Invoice</span>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <%--<div style="left: 15px; position: absolute; top: 275px; height: 270px;">
                            <asp:UpdatePanel ID="upSKUDetail" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                        BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                            border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                            <tbody>
                                                <tr>
                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                        &nbsp;<strong>Select SKU from List Press Enter</strong>
                                                    </td>
                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                        <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                                            Font-Size="8pt" Height="16px" Text="X" Width="21px" CssClass="Button" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:ListBox ID="lstCode" onkeypress="SelectSkuCode(event)" runat="server" Width="95%"
                                            Height="87%" SelectionMode="Multiple"></asp:ListBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                                <div style="z-index: 101; left: 759px; width: 100px; position: absolute; top: 300px;
                                    height: 100px">
                                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                    Width="31px" />
                                                Wait Update
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </asp:Panel>
                                </div>
                                <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>--%>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left">
                                                &nbsp;
                                            </td>
                                            <td valign="top" align="left" colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="231px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="1">
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblBillBook" runat="server" Text="Principal Invoice No:" Visible="true"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBillBookNo" runat="server" CssClass="uppercase" MaxLength="10"
                                                    Visible="true" TabIndex="5"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label93" runat="server" Text="Principal" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:DropDownList ID="ddlStore" runat="server" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                                    Width="231px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblTransporter" runat="server" Text="Transporter" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTransporter" runat="server" CssClass="uppercase" Visible="true"
                                                    TabIndex="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="104px" Height="16px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" colspan="1" valign="top">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                    Width="231px" TabIndex="2">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChbDiscount" runat="server" AutoPostBack="True" Checked="True"
                                                    Text="Promotion" Visible="False" Width="90px" />
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblTruckNo" runat="server" Text="Truck No" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTruckNo" runat="server" CssClass="uppercase" Visible="true" TabIndex="7"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="77px" Text="Order Type" Visible="false"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text="Customer" Width="74px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RblPayMode" runat="server" Height="1px" RepeatDirection="Horizontal"
                                                    Width="228px" TabIndex="2" Visible="false">
                                                    <asp:ListItem Value="214">Cash</asp:ListItem>
                                                    <asp:ListItem Value="215">Credit</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="216">Advance</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="231px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" TabIndex="3">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="height: 23px; width: 50px;">
                                                <asp:TextBox ID="txtDiscountType" runat="server" ReadOnly="True" Width="90px" TabIndex="4"
                                                    Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblTruckSize" runat="server" Text="Truck Size" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTruckSize" runat="server" CssClass="uppercase" Visible="true"
                                                    TabIndex="8"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 23px">
                                                <strong>
                                                    <asp:Label ID="Label12" runat="server" Text="Customer Invoice No:" Width="137px"
                                                        Height="16px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                <asp:TextBox ID="txtCustomerInvoiceNo" runat="server" CssClass="uppercase" Visible="true"
                                                    TabIndex="4" Width="227px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblDriverCell" runat="server" Text="Driver Cell No" Width="155px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDriverCellNo" runat="server" CssClass="uppercase" Visible="true"
                                                    TabIndex="9"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel5" runat="server">
                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                                    <table width="80%">
                                        <tr>
                                            <td style="width: 20%">
                                                <strong></strong>
                                                <asp:Label ID="lblInStore" runat="server" Text="0" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hfPrevQty" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 20%">
                                                <strong></strong>
                                                <asp:Label ID="lblInBatch" runat="server" Text="0" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlBatch" runat="server" TabIndex="11" Visible="false">
                                                </asp:DropDownList>
                                                <asp:Label ID="Label7" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Batch No" Enabled="False" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 40%">
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskuname" runat="server" Width="535px" CssClass="lblDetail" Text="SKU Name" ></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label3" runat="server" CssClass="lblDetail"
                                                     Text="Quantity" Width="100%"></asp:Label>
                                                <asp:Label ID="lblquantity" runat="server" Width="100%" CssClass="lblDetail"
                                                    Text="Unit Price"  Visible="false"></asp:Label>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblDetail"
                                                     Text="Exp Date" Width="100%" Enabled="False" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblDetail"
                                                    Text="Amount" Width="100%" Visible="false"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" AutoPostBack="true" Width="530px"
                                                    OnSelectedIndexChanged="ddlSKuCde_SelectedIndexChanged" TabIndex="10">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" onblur="SearchSKUCode()" Width="75px"
                                                    TabIndex="12"></asp:TextBox>
                                                <asp:TextBox ID="txtUnitRate" runat="server" Width="75px" Enabled="false" TabIndex="13"
                                                    Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtExpDate" runat="server" Width="62px" Enabled="false" TabIndex="14"
                                                    Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="100px" Enabled="false" Visible="false"
                                                    TabIndex="15"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    Width="100px" AccessKey="A" CssClass="Button" TabIndex="16" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="7">
                                                <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="725px"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" CssClass="grdDetail"
                                                                    Width="532px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" CssClass="grdDetail"
                                                                    Width="83px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}"
                                                                Visible="false">
                                                                <ItemStyle HorizontalAlign="Right" CssClass="grdDetail"
                                                                    Width="75px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ExpDate" HeaderText="ExpDate">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}"
                                                                Visible="false">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CLAIM_PER" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="35px"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                                </asp:Panel>
                            </td>
                            <td style="width: 100px">
                                <asp:HiddenField ID="hfBillBookNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Button ID="btnCalculate" runat="server" AccessKey="C" CssClass="Button" Enabled="False"
                                                    Font-Size="8pt" OnClick="btnCalculate_Click" TabIndex="100" Text="Calculate"
                                                    Width="100px" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button ID="btnSaveOrder" runat="server" AccessKey="S" CssClass="Button" Enabled="False"
                                                    Font-Size="8pt" OnClick="btnSaveOrder_Click" TabIndex="101" Text="Save Order"
                                                    Width="110px" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button ID="btnCancel" runat="server" AccessKey="H" CssClass="Button" Font-Size="8pt"
                                                    OnClick="btnCancel_Click" TabIndex="102" Text="Home" Width="110px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" colspan="2" rowspan="7">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU" Visible="false"></asp:Label></strong><asp:Panel
                                                        ID="Panel4" runat="server" Width="350px" Height="90px" Visible="false" BorderColor="Silver"
                                                        BorderStyle="Groove" BorderWidth="1px">
                                                        <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                            BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
                                                            </RowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                                    <ItemStyle Width="80px" CssClass="grdDetail">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                    <ItemStyle Width="200px" CssClass="grdDetail">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                    <ItemStyle Width="50px" CssClass="grdDetail">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                            </td>
                                            <td style="height: 20px; width: 93px;" align="left">
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo1" runat="server" Width="94px" Text="Gross Sale" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px">
                                                <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 93px">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation1" runat="server" Width="94px" Text="Replacement" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px">
                                                <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px; width: 93px;" align="left">
                                            </td>
                                            <td style="height: 18px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label51" runat="server" Width="110px" Text="Discount" Visible="false"></asp:Label></strong>
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="116px" Text="Claimable  Discount" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 18px">
                                                <asp:TextBox ID="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="numtxtUnClaimabledist" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True" Visible="false" TabIndex="23"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label94" runat="server" Text="Unloading Charges" Width="116px" Visible="false"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numtxtUnLoadingCharges" runat="server" CssClass="txtBoxnNum" Font-Bold="True"
                                                    ForeColor="Black" ReadOnly="True" Visible="false" Width="150px" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label81" runat="server" Width="93px" Text="GST Amount" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="93px" Text="TST Amount" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label91" runat="server" Width="105px" Text="Net Amount" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    CssClass="txtBoxnNum" ReadOnly="True" TabIndex="-1" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <table>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 93px" valign="top" align="left">
                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 1px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="105px" Text="Cash Received" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px" align="right">
                                                <asp:TextBox ID="txtCashReceived" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True" CssClass="txtBoxnNum" Enabled="False" Visible="false" TabIndex="-1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:HiddenField ID="hfSKUID" runat="server" />
                                <asp:HiddenField ID="hfBatchNo" runat="server" />
                                <asp:HiddenField ID="hfBatchValue" runat="server" />
                                <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                                &nbsp;&nbsp;
                                <asp:TextBox ID="numTxtTotalSED" runat="server" Font-Bold="False" ForeColor="Black"
                                    Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
