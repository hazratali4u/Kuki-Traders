<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDemangedClaim.aspx.cs" Inherits="Forms_frmDemangedClaim" Title="SAMS :: Customer Claim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateValueForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }
        }

        function pageLoad() {
            $("select").searchable();
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div style="z-index: 101; left: 487px; width: 100px; position: absolute; top: 250px;
                            height: 100px">
                            <asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                            <asp:HiddenField ID="hfZoneId" Value="0" runat="server"/>

                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    TargetControlID="txtAmount" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddNew">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Claim Type" Width="65px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RbdClaimType" runat="server" RepeatDirection="Horizontal"
                                                    Width="215px" AutoPostBack="True" OnSelectedIndexChanged="RbdClaimType_SelectedIndexChanged">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="lblClaimMode" runat="server" Text="Claim Mode"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="ddlClaimMode" runat="server" Width="250px" AutoPostBack="false"
                                                    OnSelectedIndexChanged="ddlClaimMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">Value Wise</asp:ListItem>
                                                    <asp:ListItem Value="1">Product Wise</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Location"
                                                        Width="65px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Text="Town" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="ddlRoute" runat="server" Width="250px" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Customer" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="250px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Visible="False"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumentNo_SelectedIndexChanged"
                                                    Visible="False" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Account Head" Width="82px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpAccountHead" runat="server" Width="250px" CssClass="DropList"
                                                    AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Remarks" Width="71px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="243px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <strong>
                                                    <asp:Label ID="lblAmount" runat="server" Width="63px" Text="Amount"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="107px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                            </td>
                                            <td align="left" colspan="1">
                                                <asp:Button ID="btnAddNew" runat="server" AccessKey="S" Font-Size="8pt" OnClick="btnAddNew_Click"
                                                    CssClass="Button" Text="Save" Width="95px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <strong>
                            <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="pnlValue" runat="server" Height="200px" BorderColor="Silver" BorderStyle="Solid"
                                                    BorderWidth="1px" ScrollBars="Vertical" Width="750px">
                                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing" Width="100%">
                                                       
                                                        <Columns>
                                                            <asp:BoundField DataField="LEDGER_ID" HeaderText="LEDGER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal" Visible="false">
                                                                <ItemStyle CssClass="grdDetail" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                                <ItemStyle CssClass="grdDetail" Width="15%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Account Head">
                                                                <ItemStyle CssClass="grdDetail"  Width="15%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No">
                                                                <ItemStyle CssClass="grdDetail" Width="5%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Balance" DataFormatString="{0:F2}" HeaderText="Amount">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="8%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                                <ItemStyle CssClass="grdDetail" Width="15%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Route" HeaderText="Route">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
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
                                                                       ToolTip="Delete" >
                                                                        <img id="imgDelete" alt="" src="~/images/delete.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="3%"/>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
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
