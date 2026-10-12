<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSaleReturnViewAccount.aspx.cs" Inherits="Forms_frmSaleReturnViewAccount" %>


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
                                                     <asp:ListItem Value="townName">Town</asp:ListItem>
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
                                <fieldset>
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="400px" ScrollBars="Vertical">
                                        <asp:GridView ID="grdSaleOrder" runat="server" Width="100%" ForeColor="SteelBlue"
                                            HorizontalAlign="Center" BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False"
                                            OnRowCommand="grdSaleOrder_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="SALES_RETURN_ID" HeaderText="Return No">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                                    <ItemStyle CssClass="grdDetail" Width="14%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name">
                                                    <ItemStyle CssClass="grdDetail" Width="30%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                   <asp:BoundField DataField="townName" HeaderText="Town">
                                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="qaDate" HeaderText="Q.A Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                </asp:BoundField>
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edt" ToolTip="Edit">
                                                            <img id="Img1" runat="server" alt="" src="~/images/edit.gif"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="4%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="4%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAccount" runat="server" CommandName="Account" OnClientClick="javascript:return confirm('Are you sure you want to Close this Document?');return false;"
                                                            Text="Close" ToolTip="Close" />
                                                    </ItemTemplate>
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
