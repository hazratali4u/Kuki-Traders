<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDamage_Replacement.aspx.cs" Inherits="Forms_frmDamage_Replacement"
    Title="SAMS :: Damage / Replacement" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("select").searchable();
            var ddlBatch = $('#<%=ddlBatch.ClientID %>');
            $('#<%=ddlBatch.ClientID %> option').remove();
            var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
            for (var i = 0; i < obj.length; i++) {
                var item = obj[i];
                if (item.SKU_ID == document.getElementById("<%=ddlSKuCde.ClientID %>").value) {
                    document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                    var opts = $("<option />");
                    opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                    ddlBatch.append(opts);
                }
            }


            $('#<%=ddlBatch.ClientID %>').change(function () {


                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (item.SKU_ID == $('#<%=ddlBatch.ClientID %> option:selected').val()) {
                        document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                        break;
                    }
                }

                document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
            });

            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (item.SKU_ID == $('#<%=ddlSKuCde.ClientID %> option:selected').val()) {
                        document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;

                        var skuval = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                        skuval = skuval.substring(skuval.indexOf(':') + 1);
                        document.getElementById("<%= txtUnitRate.ClientID %>").value = skuval;
                        var ddlBatch = $('#<%=ddlBatch.ClientID %>');
                        $('#<%=ddlBatch.ClientID %> option').remove();
                        var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                        for (var i = 0; i < obj.length; i++) {
                            var item = obj[i];
                            if (item.SKU_ID == document.getElementById("<%=ddlSKuCde.ClientID %>").value) {
                                var opts = $("<option />");
                                opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                                ddlBatch.append(opts);
                            }
                        }
                        break;
                    }
                }

                document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
          

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
          
        }


        function ValidateForm() {
            var str;

         
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }


            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;

            if (parseInt(Qty) > parseInt(InBatch)) {
                alert('Quantity can not be greater than available Stock.');
                document.getElementById("<%= txtQuantity.ClientID %>").focus();
                return false;
            }

            return true;
        }
       
        function SearchList() {
            var l = document.getElementById('<%= ListCustomer.ClientID %>');
            var tb = document.getElementById('<%= txtOutletCode.ClientID %>');


            if (tb.value == "") {
                ClearSelection(l);
            }
            else {
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                        l.options[i].selected = true;
                        return false;
                    }
                    else {
                        ClearSelection(l);
                    }
                }
            }
        }
        function SearchedCode() {
            var str
            var stroption
            str = document.getElementById("<%= ListCustomer.ClientID %>").value;
            stroption = document.getElementById("<%= txtOutletCode.ClientID %>").value;

            if (str.length > 0) {
                         

            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= ListCustomer.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));

        }
        
        function SelectCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= ListCustomer.ClientID %>").value;
                
            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <div style="left: 120px; position: absolute; top: 190px; z-index:101">
                        <asp:UpdatePanel ID="upCustomer" runat="server">
                            <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                    border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                    <tbody>
                                        <tr>
                                            <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                &nbsp;<strong>Select Customer from List Press Enter</strong>
                                            </td>
                                            <td style="border-bottom: black thin solid" valign="top" align="right">
                                                <asp:Button AccessKey="S" ID="Button4" runat="server" Width="21px" Height="16px"
                                                    Font-Size="8pt" Text="X" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:ListBox ID="ListCustomer" onkeypress="SelectCode(event)" runat="server" Width="95%"
                                    Height="87%"></asp:ListBox>
                            </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                        </div>
                        
                        <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left">
                                              <strong>
                                                    <asp:Label ID="Label5" runat="server" Text="Transaction Type" Width="180px"></asp:Label>
                                                </strong>
                                            </td>
                                                                                        <td valign="top" align="left" colspan="3">
                                               <asp:DropDownList ID="drpTransactionType" runat="server" AutoPostBack="True" 
                                                    Width="231px" 
                                                    onselectedindexchanged="drpTransactionType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="1" Text="Damage" ></asp:ListItem>
                                                    <asp:ListItem  Value="2" Text="Replacement"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Sale Return" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label93" runat="server" Text="Location" Width="180px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td valign="top" align="left" colspan="3">
                                                <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                                    Width="231px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Transaction No" 
                                                    Width="180px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" colspan="3" valign="top">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                    Width="231px">
                                                </asp:DropDownList>
                                                <strong>
                                                    <asp:Label ID="lblBillBook" runat="server" TabIndex="1" Text="Bill Book No:" Visible="false"></asp:Label>
                                                </strong>
                                                <asp:TextBox ID="txtBillBookNo" runat="server" CssClass="uppercase" MaxLength="10"
                                                    Visible="false"></asp:TextBox>
                                                <asp:CheckBox ID="ChbDiscount" runat="server" AutoPostBack="True" Checked="True"
                                                    Text="Promotion" Visible="False" Width="90px" />
                                            </td>
                                        </tr>
                                        <%-- <TR>
    <TD align=left valign="middle">
<strong><asp:Label id="Label11" runat="server" Width="77px" Text="Order Type"></asp:Label></strong></TD>
        <TD align=left colspan="2" valign="top">
            <asp:RadioButtonList ID="RblPayMode" runat="server" Height="1px" 
                RepeatDirection="Horizontal" Width="228px">
                <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                <asp:ListItem Value="215">Credit</asp:ListItem>
                <asp:ListItem Value="216">Advance</asp:ListItem>
            </asp:RadioButtonList>
        </TD>
    <TD align=left colspan="1" valign="top"></TD>
    </TR>--%>
                                        <tr>
                                            <td align="left" style="height: 23px">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Customer" Width="74px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                <asp:TextBox ID="txtOutletCode" runat="server" onkeyup="SearchList()" Width="76px"></asp:TextBox>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                <asp:TextBox ID="txtOutletName" runat="server" onfocus="SearchedCode()" Width="192px"></asp:TextBox>
                                            </td>
                                            <td align="left" style="height: 23px">
                                              <%--  <asp:TextBox ID="txtDiscountType" runat="server" onkeyup="SearchList()" ReadOnly="True"
                                                    Width="180px"></asp:TextBox>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              
                                            </td>
                                            <td >
                                                <asp:HiddenField ID="hfSKUID" runat="server" />
                                                <asp:HiddenField ID="hfBatchNo" runat="server" />
                                                <asp:HiddenField ID="hfBatchValue" runat="server" />
                                            </td>
                                            <td >
                                                
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                           
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="280px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="   SKU Name" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 16px">
                                                <asp:Label ID="Label7" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Batch No" Width="70px" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label3" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Quantity" Width="61px"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Unit Price" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 16px">
                                                <asp:Label ID="Label6" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Exp Date" Width="100%" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label2" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Amount" Width="100%"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label4" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="16px" Text="Add SKU" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                           
                                            <td>
                                                <asp:DropDownList ID ="ddlSKuCde" runat="server" AutoPostBack="true" Width="280px"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBatch" runat="server" Width="70px" 
                                                    onfocus="SearchSKUCode()" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" onfocus="SearchSKUCode()" Width="55px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpDate" runat="server" Width="62px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="62px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    Width="100px" AccessKey="A" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="7">
                                                <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="750px"
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
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="280px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ExpDate" HeaderText="ExpDate">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100px">
                                                               
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="S" ID="btnSaveOrder"  OnClick="btnSaveOrder_Click"
                                                                    runat="server" Width="110px" Font-Size="8pt" Text="Save" ToolTip="Save Damage"
                                                                    CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                               <asp:Button AccessKey="H" ID="btnCancel" TabIndex="102" runat="server" Width="110px"
                                                                    Font-Size="8pt" Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                                           </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 1px" valign="top" align="left">
                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 1px" align="left">
                                                
                                            </td>
                                            <td style="width: 7px" align="right">
                                                
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="numTxtTotalSED" runat="server" Font-Bold="False" ForeColor="Black"
                            Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
