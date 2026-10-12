<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmEmployeeSalary.aspx.cs" Inherits="Forms_frmEmployeeSalary" %>
   
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script language="javascript" type="text/javascript">

  
    function ValidateForm() {
        var str;

        str = document.getElementById('<%=txtBasicSalary.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Basic Salary Amount');
            return false;
        }
     
        return true;
    }
</script>
    <div id="right_data">
        <asp:UpdatePanel ID="pnl_head" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 110px; height: 15px" align="left">
                            <strong>
                                <asp:Label ID="lbldesignationID" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td style="width: 175px; height: 15px">
                            <asp:DropDownList ID="DrpDistributor" runat="server" Width="250px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px; height: 15px">
                            <strong>
                                <asp:Label ID="Label1" runat="server" Width="94px" Text="Employee Name" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td style="width: 175px; height: 15px">
                            <asp:DropDownList ID="DrpEmployee" runat="server" Width="250px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbl_Designation" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnDesignationID" runat="server" Visible="false" />
                             <asp:HiddenField ID="hdnSalaryID" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px; height: 15px">
                            <strong>
                              <asp:Label ID="Label3" runat="server" Width="94px" Text="Basic Salary" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td style="width: 175px; height: 15px">
                         <strong>
                             
                         <asp:TextBox ID="txtBasicSalary" Width="100px" runat="server" CssClass="txtBox"></asp:TextBox>
                        </td>
                        <td style="width: 95px; height: 15px">
                           
                        </td>
                        <td style="width: 175px; height: 15px">
                          
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="pnl_Allowances" runat="server" Height="200px">
                                <fieldset>
                                    <legend align ="left" >Allowances</legend>
                                    <table>
                                        <tr class="tblhead">
                                            <td>
                                                <asp:Label ID="lblAllowanceDesc" runat="server" Text="Allowance Description" ForeColor="White"
                                                    CssClass="lblbox"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Amount" ForeColor="White" CssClass="lblbox"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DrpAllowance" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpAllowance_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAllowanceAmount" runat="server" Text="" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd_Allowance" runat="server" Text="Add" CssClass="Button" Width="80px"
                                                    OnClick="btnAdd_Allowance_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="Grid_Allowances" runat="server" Width="99%" ForeColor="SteelBlue"
                                                    CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                    BackColor="White" OnRowEditing="Grid_Allowances_RowEditing" OnRowDeleting="Grid_Allowances_RowDeleting"
                                                    PageSize="15" 
                                                    onselectedindexchanged="Grid_Allowances_SelectedIndexChanged">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous"></PagerSettings>
                                                    <Columns>
                                                        <asp:BoundField DataField="ALLOWANCE_ID" HeaderText="ALLOWANCE ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ALLOWANCE_DESC" HeaderText="ALLOWANCE DESC">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="120px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ALLOWANCE_AMOUNT" HeaderText="ALLOWANCE AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="60px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20px">
                                                            </ItemStyle>
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    CommandName="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20px">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="pnl_Deductions" runat="server" Height="200px">
                                <fieldset>
                                    <legend align ="left">Deductions</legend>
                                    <table>
                                        <tr class="tblhead">
                                            <td>
                                                <asp:Label ID="lblDeductionDesc" runat="server" Text="Deduction Description" ForeColor="White"
                                                    CssClass="lblbox"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDeductionAmount" runat="server" Text="Amount" ForeColor="White"
                                                    CssClass="lblbox"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DrpDeduction" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDeduction_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDeductionAmount" runat="server" Text="" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd_Deduction" runat="server" Text="Add" CssClass="Button" Width="80px"
                                                    OnClick="btnAdd_Deduction_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="Grid_Deductions" runat="server" Width="99%" ForeColor="SteelBlue"
                                                    CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                    BackColor="White" OnRowEditing="Grid_Deductions_RowEditing" OnRowDeleting="Grid_Deductions_RowDeleting"
                                                    AllowPaging="false">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous"></PagerSettings>
                                                    <Columns>
                                                        <asp:BoundField DataField="DEDUCTION_ID" HeaderText="DEDUCTION ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DEDUCTION_DESC" HeaderText="DEDUCTION DESC">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="120px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DEDUCTION_AMOUNT" HeaderText="DEDUCTION AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="60px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20px">
                                                            </ItemStyle>
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    CommandName="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20px">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" Width="90px"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
