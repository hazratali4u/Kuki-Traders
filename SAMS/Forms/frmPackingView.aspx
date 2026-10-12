<%@ Page Title="SAMS :: Packing List" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPackingView.aspx.cs" Inherits="Forms_frmPackingView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <div style="z-index: 101; left: 85%; width: 100px; position: absolute; top: 2%; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress5" runat="server">
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
                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Literal ID="ltrlNew" runat="server" Text="aa"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                                <table class="tblhead" width="100%" style="margin-bottom: 5px;">
                                    <tbody>
                                        <tr>
                                            <td style="color: White; font-weight: bold;">
                                                <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                            </td>
                                            <td style="width: 170px; height: 22px" align="left">
                                                <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="DistributorName2">All Records</asp:ListItem>
                                                    <asp:ListItem Value="DistributorName">Location</asp:ListItem>
                                                    <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                                    <asp:ListItem Value="TownName">Town</asp:ListItem>
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
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="400px" ScrollBars="Auto"
                            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                            <asp:GridView ID="grdInvoice" runat="server" Width="100%" ForeColor="SteelBlue" HorizontalAlign="Center"
                                BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False" OnRowCommand="grdInvoice_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="packingMasterId" HeaderText="Carton No">
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        <HeaderStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CartonNo" HeaderText="Carton No">
                                        <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="distributorName" HeaderText="Location">
                                        <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name">
                                        <ItemStyle CssClass="grdDetail" Width="24%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="TownName" HeaderText="Town">
                                        <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="transporter" HeaderText="Transporter">
                                        <ItemStyle CssClass="grdDetail" Width="17%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="driverName" HeaderText="Driver Name">
                                        <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                    </asp:BoundField>
                                 
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                <img id="imgEdit" alt="" runat="server" src="~/images/edit.gif" /></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="4%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Remove" ToolTip="Remove"  OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            <img alt="" id="imgRemove" runat="server" src="~/images/delete.gif"/></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="4%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPrint" runat="server" CommandName="Print" CommandArgument="Print"
                                                ToolTip="Print">
                                                <img alt="" id="imgPrint" runat="server" src="~/images/view.gif" /></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="4%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
