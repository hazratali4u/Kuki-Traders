<%@ Page Title="Customer(s) Information" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmCustomerView.aspx.cs" Inherits="Forms_frmCustomerView" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   <script language="JavaScript" type="text/javascript">
       function SearchRecord() {
           var str;
           str = document.getElementById('<%=txtSearch.ClientID%>').value;
           if (str == null || str.length == 0) {
               alert('Must enter Key Word for Searching');
               return false;
           }
           return true;
       }
   </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <asp:Literal ID="ltrlNew" runat="server" Text="aa"></asp:Literal>
                            </td>
                            <td style="width: 80%">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class="tblhead">
                        <tbody>
                            <tr>
                                <td style="height: 21px; width: 13%" align="left">
                                    <asp:Label ID="Label10" runat="server" Text="Select Searching Type" Font-Bold="true"
                                        ForeColor="White"></asp:Label>
                                </td>
                                
                                <td style="height: 21px; width: 15%" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px">
                                        <asp:ListItem Value="All">All Records</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NO">Mobile No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 21px; width: 15%" align="left">
                                    <asp:TextBox ID="txtSearch" runat="server" Width="180px"></asp:TextBox>
                                </td>
                                <td style="height: 21px; width: 8%" align="left">
                                    <asp:Button ID="btnSearch" runat="server" Width="90px" Font-Size="9pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                                <td style="width: 35%;">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="400px" ScrollBars="Auto"
                        BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                        <asp:GridView ID="gvCustomer" runat="server" Width="100%" ForeColor="#006699" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="SteelBlue" OnRowEditing="gvCustomer_RowEditing"
                            OnRowDeleting="gvCustomer_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                    <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="TOWN" HeaderText="Town">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NO" HeaderText="Contact Number">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" HeaderText="Address">
                                    <ItemStyle CssClass="grdDetail" Width="23%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle  Width="4%"></HeaderStyle>
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
                            <HeaderStyle CssClass="grdHead"/>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
