<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="SAMS :: SKU Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">

    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Literal ID="ltrlNew" runat="server" Text="aa"></asp:Literal>
                               
                                <table class="tblhead" width="100%" style="margin-bottom:5px;margin-top:10px;">
                                    <tr>
                                        <td style="color: White; font-weight: bold;">
                                            <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                        </td>
                                        <td style="width: 170px; height: 22px" align="left">
                                            <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                <asp:ListItem Value="packsize">All Records</asp:ListItem>
                                                <asp:ListItem Value="TECHNOLOGY">Category</asp:ListItem>
                                                <asp:ListItem Value="SKU_NAME">Product Name</asp:ListItem>
                                                <asp:ListItem Value="Source">Source Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 224px; height: 22px" align="left">
                                            <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                        <td style="width: 250px; height: 22px" align="left">
                                            <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                OnClick="btnFilter_Click"></asp:Button>
                                        </td>
                                        <td style="width: 250px; height: 22px">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="400px" ScrollBars="Auto"
                                    BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                    <asp:GridView ID="grdSKUData" runat="server" Width="99.9%" ForeColor="SteelBlue"
                                        HorizontalAlign="Center" BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False"
                                        OnRowEditing="grdSKUData_RowEditing" OnRowDeleting="grdSKUData_RowDeleting">
                                        <Columns>
                                           
                                            <asp:BoundField DataField="TECHNOLOGY_ID" HeaderText="Category_Id">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                           
                                           
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                                <ItemStyle CssClass="grdDetail" Width="40%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PACKSIZE" HeaderText="Pack Size">
                                                <ItemStyle CssClass="grdDetail" Width="14%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="MAX_STOCK" HeaderText="Max. Stock">
                                                <ItemStyle CssClass="grdDetail" Width="14%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MIN_STOCK" HeaderText="Min. Stock">
                                                <ItemStyle CssClass="grdDetail" Width="14%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                            </asp:BoundField>
                                          
                                            <asp:BoundField DataField="UNITS_IN_CASE" HeaderText="UIC">
                                                <ItemStyle CssClass="grdDetail" Width="14%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                            </asp:BoundField>
                                           
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                        <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="5%"  />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        Text="Inactive"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left">
                                                </ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                            </asp:TemplateField>
                                           
                                         
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                </asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
