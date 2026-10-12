<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSKUGroups.aspx.cs" Inherits="frmSKUGroups"
    MasterPageFile="~/Forms/PageMaster.master" Title="SAMS :: SKU Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table style="width: 350px">
                                <tbody>
                                    <tr>
                                        <td colspan="4">
                                         <asp:Label ID="lblErrormsg" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 32px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="72px" Text="Group Name" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtGroupName" runat="server" Width="194px" CssClass="txtBox "></asp:TextBox>
                                          <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Width="136px"
                                                ControlToValidate="txtGroupName" Display="Dynamic" ErrorMessage="Enter Group Name"
                                                ValidationGroup="vg"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>                                                                      
                                    <tr>
                                        <td style="width: 47px; height: 23px" align="left">
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Width="52px" Text="Category"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddCatagory" runat="server" Width="200px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddCatagory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td style="width: 47px; height: 23px" align="left">                                            
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                  <tr>
                                    <td></td>
                                  </tr>
                                  
                                    <tr>
                                        <td align="center" colspan="5">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="117px" Text="Product Collection" CssClass="lblbox"></asp:Label></strong>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 115px" colspan="5">
                                       
                                            <table>
                                            
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 107px" rowspan="4">
                                                            <asp:ListBox ID="lstUnAssignSKU" runat="server" Width="300px" Height="150px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button ID="btnAddAll" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                                OnClick="btnAddAll_Click" CssClass="Button" />
                                                        </td>
                                                        <td style="width: 102px" rowspan="4">
                                                            <asp:ListBox ID="lstAssignSKU" runat="server" Width="300px" Height="150px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnAdd" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAdd_Click"
                                                                CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnRemove" runat="server" Width="30px" Font-Size="8pt" Text="<" OnClick="btnRemove_Click"
                                                                CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnRemoveAll" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                                OnClick="btnRemoveAll_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="1">
                                                            <asp:CheckBox ID="chIsActive" runat="server" Visible="False" Text="Is Active" CssClass="lblbox">
                                                            </asp:CheckBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="62px" Font-Size="8pt"
                                                                Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                        </td>
                                                        <td rowspan="1">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table>
                                            
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Width="675px" Height="200px" ScrollBars="Vertical"
                                BackColor="#E0E0E0">
                                <asp:GridView ID="SKUGroup_Grid" runat="server" Width="658px" Height="1px" ForeColor="SteelBlue"
                                    CssClass="gridRow2" BackColor="White" OnPageIndexChanging="SKUGroup_Grid_PageIndexChanging"
                                    AutoGenerateColumns="False" BorderColor="White" HorizontalAlign="Center" OnRowEditing="SKUGroup_Grid_RowEditing">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <Columns>
                                        <asp:BoundField DataField="SKU_GROUP_ID" HeaderText="Group Id">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GROUP_NAME" HeaderText="Group Name">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:ListBox ID="listbox1" runat="server" Width="350px" CssClass="DropList" AutoPostBack="True">
                                                </asp:ListBox>
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
