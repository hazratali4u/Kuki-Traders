<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVendor.aspx.cs" Inherits="Forms_frmVendor" Title="SAMS :: Vendor Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        th
        {
            font-weight: bold;
            font-size: 13px;
        }
    </style>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <asp:Literal ID="ltrlNew" runat="server" Text="aa"></asp:Literal>
                            </td>
                            <td style="width: 40%">
                            </td>
                            <td style="font-size: 14px; width: 20%">
                                <strong>No. of Vendors:
                                    <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label></strong>
                            </td>
                        </tr>
                    </table>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                        width: 100%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px; width: 22%" align="left">
                                    <asp:Label ID="Label10" runat="server" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="height: 21px; width: 22%" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="100%">
                                        <asp:ListItem Value="All">All Records</asp:ListItem>
                                        <asp:ListItem Value="VENDOR_NAME">Vendor Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NO">Mobile No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 21px; width: 22%" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td style="height: 21px; width: 12%" align="left">
                                    <asp:Button ID="btnSearch" runat="server" Width="100%" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="450px" ScrollBars="Auto"
                        BorderStyle="Solid" BorderColor="Silver">
                        <asp:GridView ID="gvVendor" runat="server" Width="100%" ForeColor="SteelBlue" HorizontalAlign="Center"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue" OnRowEditing="gvVendor_RowEditing"
                            OnRowDeleting="gvVendor_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="VENDOR_ID" HeaderText="Vendor Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VENDOR_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NO" HeaderText="Mobile No">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="3%" Height="14px" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            Text="Inactive"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                ForeColor="White" />
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
