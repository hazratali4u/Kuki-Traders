<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVendorOpening.aspx.cs" Inherits="Forms_frmVendorOpening" Title="SAMS :: Supplier Opening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;

            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must Enter Amount');
                return false;
            }
        }
 
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 503px; width: 100px; position: absolute; top: 251px;
            height: 100px">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="22px" />
                    Record Update
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50px" align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>Location</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="89px" Text="Supplier" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px" align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    Amount</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="118px"></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    Remarks</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="300px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                                    Width="90px" Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="-0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Auto"
                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="SteelBlue" HorizontalAlign="Center" OnRowDeleting="GrdOrder_RowDeleting"
                            OnRowEditing="GrdOrder_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Supplier">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Invoice Date">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# Bind("DOCUMENT_DATE") %>' ID="TextBox1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("DOCUMENT_DATE", "{0:dd-MM-yyyy}") %>' ID="Label1"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PURCHASE_MASTER_ID" HeaderText="Invoice No">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Opening Amount" DataFormatString="{0:f2}">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:BoundField>
                                 <asp:CommandField ShowEditButton="True" ShowHeader="True" >
                                           <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
                                         </asp:CommandField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="grdHead">
                            </HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                     <asp:HiddenField runat="server" ID="hfPurchaseID"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
