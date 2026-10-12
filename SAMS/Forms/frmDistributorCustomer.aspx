<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer"
    Title="SAMS :: New Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        #right #right_data fieldset legend {
            width: 120px;
        }

        .ajax__tab_xp .ajax__tab_body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #525252;
        }
    </style>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCustomerName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Customer Name');
                return false;
            }
        }

        //        function validateEmail() {
        //            var email;
        //            email = document.getElementById('<%=txtEmailAddress.ClientID%>').value;

        //            if (email == null || email.length == 0) {

        //                if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(document.getElementById('<%=txtEmailAddress.ClientID%>').value)) {
        //                    return (true)
        //                }
        //                else {
        //                    alert("You have entered an invalid email address!")
        //                    return (false)
        //                }
        //            }
        //            return true;
        //        }


        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;
            return true;
        }
        function onlyNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;

            return true;
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="80%" ActiveTabIndex="0">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            Basic Information
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <strong>Location</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 45px"></td>
                                                        <td style="width: 105px" align="left">
                                                            <strong>City </strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpTown" runat="server" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px" align="left">
                                                            <strong>Channel</strong>
                                                        </td>
                                                        <td style="width: 175px">
                                                            <asp:DropDownList ID="DrpChannelType" runat="server" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 45px"></td>
                                                        <td style="width: 105px" align="left">
                                                            <strong>Address </strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress" runat="server" Width="205px" CssClass="txtBox " MaxLength="250"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px" align="left">
                                                            <strong>Name</strong>
                                                        </td>
                                                        <td style="width: 175px; height: 20px">
                                                            <asp:TextBox ID="txtCustomerName" runat="server" Width="205px" CssClass="txtBox"
                                                                MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1px; height: 20px" valign="top"></td>
                                                        <td>
                                                            <strong>Phone No</strong>
                                                        </td>
                                                        <td style="width: 219px; height: 20px">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="205px" CssClass="txtBox" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px" align="left">
                                                            <strong>Fax No</strong>
                                                        </td>
                                                        <td style="width: 175px; height: 20px">
                                                            <asp:TextBox ID="txtFAX" runat="server" Width="205px" CssClass="txtBox" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1px; height: 20px" valign="top"></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblNTN" runat="server" Text="NTN #" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 219px; height: 20px">
                                                            <asp:TextBox ID="txtNTN" runat="server" Width="205px" CssClass="txtBox" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px">
                                                            <strong>S.TaxNo</strong>
                                                        </td>
                                                        <td style="width: 219px">
                                                            <asp:TextBox ID="txtGstNo" runat="server" Width="205px" CssClass="txtBox" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <strong>S.Tax % </strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTaxRate" runat="server" Width="205px" CssClass="txtBox " onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px" align="left">
                                                            <strong>Credit Limit</strong>
                                                        </td>
                                                        <td style="width: 175px; height: 20px">
                                                            <asp:TextBox ID="txtCreditLimit" runat="server" Width="205px" CssClass="txtBox "></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbeCreditLimit" ValidChars="-.0123456789" runat="server"
                                                                TargetControlID="txtCreditLimit">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td></td>
                                                        <td align="left">
                                                            <strong>Allowed Days</strong>
                                                        </td>
                                                        <td style="width: 219px; height: 20px">
                                                            <asp:TextBox ID="txtAllowedDays" runat="server" Width="205px" CssClass="txtBox "
                                                                onkeypress="return onlyNumbers(this,event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 115px; height: 20px">
                                                            <%-- <strong>Discount Allowed %</strong>--%>
                                                            <strong>Email</strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDiscount" runat="server" Width="205px" CssClass="txtBox" Visible="false"
                                                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="205px" CssClass="txtBox"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td align="left">
                                                            <strong>I.Tax Deduction</strong>
                                                        </td>
                                                        <td style="width: 219px; height: 20px">
                                                            <asp:TextBox ID="txtITaxDeduction" runat="server" Width="205px" CssClass="txtBox "
                                                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 115px; height: 20px">
                                                            <strong>WH.Tax Deduction</strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWHTaxDeduction" runat="server" Width="205px" CssClass="txtBox "
                                                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td align="left">
                                                            <strong>Promotion Class</strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpVolumeClass" runat="server" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Price Group</strong>
                                                        </td>
                                                        
                                                        <td>
                                                            <asp:DropDownList ID="drpPriceGroup" runat="server" Width="205px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td align="left"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td align="left" colspan="4">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Font-Bold="true" Text="IsActive"
                                                                Width="93px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 105px"></td>
                                                        <td style="width: 175px">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                                                                Text="Save" ValidationGroup="vg" Width="80px" />
                                                            &nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnCancel_Click"
                                                                Text="Cancel" Width="80px" />
                                                        </td>
                                                        <td style="width: 1px"></td>
                                                        <td></td>
                                                        <td style="width: 219px"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div style="z-index: 101; left: 740px; width: 100px; position: absolute; top: 30px; height: 100px">
                                                &nbsp;<asp:Panel ID="Panel21" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="23px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tbContact" runat="server" Visible="false">
                        <HeaderTemplate>
                            Contact
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 97px; height: 20px">
                                        <strong>Designation</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpDesignation" runat="server" Width="205px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Contact Person</strong>
                                    </td>
                                    <td style="width: 219px; height: 20px">
                                        <asp:TextBox ID="txtContactPerson" runat="server" Width="205px" CssClass="txtBox"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Contact No</strong>
                                    </td>
                                    <td style="width: 219px; height: 20px">
                                        <asp:TextBox ID="txtContactNo" runat="server" Width="205px" CssClass="txtBox" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px" align="left">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Width="100px" Text="Email Address" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="width: 175px; height: 20px">
                                        <asp:TextBox ID="txtContactEmail" runat="server" Width="205px" CssClass="txtBox"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActiveContact" runat="server" Checked="True" Font-Bold="true"
                                            Text="IsActive" Width="93px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveContact" runat="server" CssClass="Button" Font-Size="8pt"
                                            OnClick="btnSaveContact_Click" Text="Save" ValidationGroup="vg" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%-- <table class="tblhead">
                                            <tbody>
                                                <tr>
                                                    <td style="color: White; font-weight: bold;">
                                                        <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                                    </td>
                                                    <td style="width: 170px; height: 21px" align="left">
                                                        <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                            <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                                            <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 224px; height: 21px" align="left">
                                                        <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                    </td>
                                                    <td style="height: 21px" align="left" width="450">
                                                        <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                            OnClick="btnSearch_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>--%>
                                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="170px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid">
                                            <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue" OnRowEditing="Grid_users_RowEditing">
                                                <RowStyle ForeColor="Black"></RowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="CONTACT_ID">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DESIGNATION_ID">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DESIGNATION" HeaderText="Designation">
                                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CONTACT_NO" HeaderText="Contact Number">
                                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EMAIL" HeaderText="Email">
                                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                                        <ItemStyle CssClass="grdDetail"></ItemStyle>
                                                        <HeaderStyle CssClass="grdHead" Width="4%"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                                <img id="imgEdit" src="../images/edit.gif" runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="3%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#006699"
                                                    Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
