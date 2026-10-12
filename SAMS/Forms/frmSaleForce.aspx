<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleForce.aspx.cs" Inherits="Forms_frmSaleForce"
    Title="SAMS :: Employee Information" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   
    <div id="right_data">        
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Literal ID="ltrlNew" runat="server" Text="aa"></asp:Literal>                    
                    <br />
                    <br />
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="420px" BorderWidth="1px" ScrollBars="Auto" BorderStyle="Solid" BorderColor="Silver">
                        <asp:GridView ID="gvEmployee" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="grdHead"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                            OnRowEditing="gvEmployee_RowEditing">
                           
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="User ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Name">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL" HeaderText="Email">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MOBILE" HeaderText="Official No">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" >
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333">
                            </AlternatingRowStyle>
                        </asp:GridView>
                    </asp:Panel>                     
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
