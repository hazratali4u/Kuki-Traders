<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVendorAdd.aspx.cs" Inherits="Forms_frmVendorAdd" Title="SAMS :: Vendor Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style>
        #right #right_data fieldset legend
        {
            width: 120px;
        }
        .ajax__tab_xp .ajax__tab_body
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #525252;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            var val = '<%=Session["Pic"] %>';
            if (val == '') {
            } else {
                var logo = '<%= Session["Pic"]%>';
                $('#pic').attr('src', '../Pics/' + logo).width(175).height(168);
            }
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pic').attr('src', e.target.result).width(175).height(168);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        function ValidateBasicInformation() {
            var str;
            str = document.getElementById('<%=txtName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Vendor Name');
                return false;
            }
            return true;
        }

        function ValidatDealership() {
            var str;

            return true;
        }
        function ValidateTerritory() {
            var str;
            return true;
        }
        function ValidateTown() {
            var str;
            return true;
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="upCustomer" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <strong>
                                <asp:Label Style="font-size: 14px;" runat="server" ID="lblVendorInfo"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:TabContainer ID="TabContainer1" runat="server" Height="405px" Width="100%" ActiveTabIndex="0">
                                <cc1:TabPanel ID="TabPanel1" runat="server">
                                    <HeaderTemplate>
                                        Basic Information
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 48%" valign="top">
                                                    <asp:Panel ID="pnlName" runat="server" GroupingText="Name" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Vendor Name : </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:RadioButtonList ID="rdbVendorType" Width="200px" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="False" Selected="True" Text="Local"></asp:ListItem>
                                                                        <asp:ListItem Value="True" Text="Imported"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <br />
                                                    <br />
                                                    <asp:Panel ID="pnlTax" runat="server" GroupingText="Tax Information" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Income Tax No </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtIncomeTax" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Sales Tax No </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtSalesTax" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display:none;">
                                                                <td style="width: 29%">
                                                                    <strong>Tax Category </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="ddlBusinessType" runat="server" Width="100%">
                                                                        <asp:ListItem Value="0" Text="Distributor"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Whole Saler"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Retailer"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Exemption </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtExemption" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Date </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtTaxDate" runat="server" Width="90%"></asp:TextBox>
                                                                    <asp:ImageButton ID="ibTaxDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                                    </asp:ImageButton>
                                                                    <cc1:CalendarExtender ID="ceTaxDate" runat="server" TargetControlID="txtTaxDate"
                                                                        PopupButtonID="ibTaxDate" Format="dd-MMM-yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <br />
                                                    <br />
                                                    <asp:Panel ID="pnlFreight" runat="server" GroupingText="Account Head" Width="100%"
                                                        Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Creditors</strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="drpCreditors" runat="server" Width="300" onfocus="ddlFocus(this);"
                                                                        onblur="ddlBlur(this);" CssClass="DropList">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Sales Tax Receiveable</strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="drpSaleTaxRble" runat="server" Width="300" onfocus="ddlFocus(this);"
                                                                        onblur="ddlBlur(this);" CssClass="DropList">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <br />
                                                    <strong>
                                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Checked="true" /></strong>
                                                    <br />
                                                    <center>
                                                        <asp:Button ID="btnSaveBasic" runat="server" Font-Size="8pt" OnClick="btnSaveBasic_Click"
                                                            CssClass="Button" Text="Save" Width="85px" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Font-Size="8pt" OnClick="btnCancel_Click"
                                                            CssClass="Button" Text="Cancel" Width="85px" />
                                                    </center>
                                                </td>
                                                <td style="width: 4%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 48%" valign="top">
                                                    <asp:Panel ID="pnlAddress" runat="server" GroupingText="Address" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Office : </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtOfficeAddress" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    <strong>Factory : </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtFactoryAddress" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Registered Office : </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtRegisteredOfficeAddress" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Town Name : </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="ddlTown" runat="server" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <br />
                                                    <br />
                                                    <asp:Panel ID="pnlConcern" runat="server" GroupingText="Concern Person" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Name </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtConcern" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Mobile No </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:TextBox ID="txtConcernMobileNo" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <br />
                                                    <br />
                                                    <asp:Panel ID="pnlOther" runat="server" GroupingText="Other" Width="100%" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <%--Business Deal as--%>
                                                                    <strong>Credit Term</strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="ddlBusinessDeal" runat="server" Width="100%">
                                                                        <asp:ListItem Value="0" Text="45 Days"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="60 Days"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Currency Name </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="100%">
                                                                        <asp:ListItem Value="0" Text="Rs."></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="US $"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="€ EUR"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="ACU $"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 29%">
                                                                    <strong>Vendor Type </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:DropDownList ID="ddlVendorType" runat="server" Width="100%">
                                                                        <asp:ListItem Value="0" Text="Manufacture"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Suppliers"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Others"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" Visible="false">
                                    <HeaderTemplate>
                                        Owner Information
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <br />
                                        <asp:UpdatePanel ID="upOwner" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlOwner" runat="server" GroupingText="Owner Information" Width="100%">
                                                    <br />
                                                    <table width="80%">
                                                        <tr>
                                                            <td style="width: 55%" valign="top">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Owner's Name</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerName" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Father's Name</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtFatherName" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Residential Address</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerAddress" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>CNIC No.</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerCNIC" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>CNIC Expiry</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerCNICExpiry" runat="server" Width="90%"></asp:TextBox>
                                                                            <asp:ImageButton ID="ibExpiry" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                                            </asp:ImageButton>
                                                                            <cc1:CalendarExtender ID="ceExpiry" runat="server" TargetControlID="txtOwnerCNICExpiry"
                                                                                PopupButtonID="ibExpiry" Format="dd-MMM-yyyy">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Address</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerAddress2" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Email Address</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerEmail" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Contact No.</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerContactNo" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Mobile No.</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerMobileNo" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>PTCL</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOwnerPTCL" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                            <br />
                                                                            <asp:Button ID="btnSaveOwner" runat="server" Font-Size="8pt" OnClick="btnSaveOwner_Click"
                                                                                CssClass="Button" Text="Save" Width="85px" />
                                                                            <asp:Button ID="btnCancelOwner" runat="server" Font-Size="8pt" OnClick="btnCancel_Click"
                                                                                CssClass="Button" Text="Cancel" Width="85px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 3%">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 42%" valign="top">
                                                                <label for="ctl00_ctl00_mainCopy_cphPage_TabContainer1_TabPanel3_fuPic" id="lbl"
                                                                    runat="server" style="cursor: pointer;">
                                                                    <img src="../images/noimage.jpg" id="pic" name="pic" width="175" height="168" />
                                                                </label>
                                                                <asp:FileUpload ID="fuPic" onchange="readURL(this);" Style="display: none;" runat="server">
                                                                </asp:FileUpload>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSaveOwner" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3" Visible="false">
                                    <HeaderTemplate>
                                        Opening Balance
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel1" runat="server" GroupingText="Opening Balance" Width="100%">
                                                    <br />
                                                    <table width="80%">
                                                        <tr>
                                                            <td style="width: 55%" valign="top">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Location</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Type</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:RadioButtonList ID="rblOpening" runat="server" Width="100%" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="0" Text="Credit" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Debit"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Date</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOpeningDate" runat="server" Width="90%"></asp:TextBox>
                                                                            <asp:ImageButton ID="ibOpeningDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                                            </asp:ImageButton>
                                                                            <cc1:CalendarExtender ID="ceOpeningDate" runat="server" TargetControlID="txtOpeningDate"
                                                                                PopupButtonID="ibOpeningDate" Format="dd-MMM-yyyy">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Opening Balance</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOpeningBalance" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <strong>Remarks</strong>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                        </td>
                                                                        <td style="width: 65%">
                                                                            <asp:TextBox ID="txtOpeningBalanceRemarks" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                            <br />
                                                                            <asp:Button ID="btnSaveOpeningBalance" runat="server" Font-Size="8pt" OnClick="btnOpeningBalance_Click"
                                                                                CssClass="Button" Text="Save" Width="85px" />
                                                                            <asp:Button ID="btnCancelOpeningBalance" runat="server" Font-Size="8pt" OnClick="btnCancel_Click"
                                                                                CssClass="Button" Text="Cancel" Width="85px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 45%" valign="middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
