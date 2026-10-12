<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleOrderView.aspx.cs" Inherits="Forms_frmSaleOrderView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <div style="z-index: 101; left: 85%; width: 100px; position: absolute; top: 2%; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                        </asp:ImageButton>&nbsp; Loading....
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="tblhead" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="color: White; font-weight: bold;">
                                                <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                            </td>
                                            <td style="width: 170px; height: 22px" align="left">
                                                <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="CUSTOMER_NAME2">All Records</asp:ListItem>
                                                    <asp:ListItem Value="DISTRIBUTOR_NAME">Location</asp:ListItem>
                                                    <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 224px; height: 22px" align="left">
                                                <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 250px; height: 22px" align="left">
                                                <asp:Button ID="btnFilter" runat="server" Width="85px" Text="Filter" OnClick="btnFilter_Click">
                                                </asp:Button>
                                            </td>
                                            <td style="width: 250px;">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <p>
                                    &nbsp;</p>
                                <fieldset>
                                    <legend style="width: 120px;"><strong>Sale Order</strong></legend>
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="170px" ScrollBars="Vertical"
                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                        <asp:GridView ID="grdSaleOrder" runat="server" Width="100%" ForeColor="SteelBlue"
                                            HorizontalAlign="Center" BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False"
                                            OnRowEditing="grdSaleOrder_RowEditing" OnRowDeleting="grdSaleOrder_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Order No">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                                    <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name">
                                                    <ItemStyle CssClass="grdDetail" Width="30%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="SO Date">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Order Amount" DataFormatString="{0:f2}">
                                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="D.O"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="5%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </fieldset>
                                <p>
                                    &nbsp;</p>
                                <fieldset>
                                    <legend style="width: 120px;"><strong>Delivery Order</strong></legend>
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="170px" ScrollBars="Vertical"
                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                        <asp:GridView ID="grdInvoice" runat="server" Width="100%" ForeColor="SteelBlue" HorizontalAlign="Center"
                                            BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False" OnRowEditing="grdInvoice_RowEditing"
                                            OnRowDeleting="grdInvoice_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Order No">
                                                    <ItemStyle CssClass="HidePanel" />
                                                    <HeaderStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="DO No">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                                    <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name">
                                                    <ItemStyle CssClass="grdDetail" Width="30%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DO Date">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTAL_NET_AMOUNT" HeaderText="Amount" DataFormatString="{0:f2}">
                                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="5%" Height="14px" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="HidePanel" HorizontalAlign="Left"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
