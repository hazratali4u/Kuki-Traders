<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSaleForceAdd.aspx.cs" Inherits="Forms_frmSaleForceAdd" Title="SAMS :: Employee Information" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
<style>
#right #right_data fieldset legend {
width: 120px;
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
        function ValidateBasicInformation()
		{
		    var str;
			return true;
	    }
	    
	    function ValidatDealership()
		{
			var str;
					
			return true;
	    }
	    function ValidateTerritory()
		{
			var str;
			return true;
	    }
	    function ValidateTown()
		{
			var str;
			return true;
	    }
    </script>
     <div id="right_data"> 
         <asp:UpdatePanel ID="upEmployee" runat="server">
    <ContentTemplate>                                
        <table width="80%">
        <tr>
            <td style="width:55%" valign="top">
                <table width="100%">
                    <tr>
                        <td style="width:30%">
                            <strong>Employee's Name</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Father's Name</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtFatherName" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Residential Address</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeAddress" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>CNIC No.</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeCNIC" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>CNIC Expiry</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeCNICExpiry" runat="server" Width="90%"></asp:TextBox>
                            <asp:ImageButton id="ibExpiry" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            <cc1:CalendarExtender id="ceExpiry" runat="server" TargetControlID="txtEmployeeCNICExpiry" PopupButtonID="ibExpiry" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Address</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeAddress2" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>                    
                    <tr>
                        <td style="width:30%">
                            <strong>Personal No.</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeContactNo" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Official No.</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeMobileNo" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Email Address</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtEmployeeEmail" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Date Of Birth</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtDOB" runat="server" Width="90%"></asp:TextBox>
                            <asp:ImageButton id="ibDOB" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            <cc1:CalendarExtender id="ceDOB" runat="server" TargetControlID="txtDOB" PopupButtonID="ibDOB" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Martial Status</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:DropDownList ID="ddlMartial" runat="server" Width="100%">
                                <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Single"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Married"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Divorced"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Other"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Designation</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:DropDownList ID="ddlDesignation" runat="server" Width="100%">
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Department</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Date Of Joining</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtJoining" runat="server" Width="90%"></asp:TextBox>
                            <asp:ImageButton id="ibJoining" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            <cc1:CalendarExtender id="ceJoining" runat="server" TargetControlID="txtJoining" PopupButtonID="ibJoining" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <strong>Location</strong>
                        </td>
                        <td style="width:5%"></td>
                        <td style="width:65%">
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="100%">
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <br />
                            <asp:Button ID="btnSaveEmployee" runat="server" Font-Size="8pt" OnClick="btnSaveEmployee_Click" CssClass="Button"
                                Text="Save" Width="85px" />
                                <asp:Button ID="btnCancelEmployee" runat="server" Font-Size="8pt" CssClass="Button"
                            Text="Cancel" Width="85px" onclick="btnCancelEmployee_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:45%" valign="middle">
                <label for="ctl00_ctl00_mainCopy_cphPage_fuPic" id="lbl" runat="server" style="cursor: pointer;">
                    <img src="../images/noimage.jpg" id="pic" name="pic"  width="175" height="168" />
                    </label>
                    <asp:FileUpload ID="fuPic" onchange = "readURL(this);" style="display:none;" runat="server" ></asp:FileUpload>
            </td>
        </tr>
    </table>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveEmployee" />
        </Triggers>
    </asp:UpdatePanel>                 
     </div>
</asp:Content>
