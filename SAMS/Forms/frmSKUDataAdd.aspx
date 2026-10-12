<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUDataAdd.aspx.cs" Inherits="Forms_frmSKUDataAdd" Title="SAMS :: SKU Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }
        function ddlBlur(obj) {
            obj.className = "";
        }
        function pageLoad() {
          
            $("select").searchable();

        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="70%">
                                    <tr>
                                        <td style="width: 40" valign="top">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 15%; height: 26px;">
                                                        <strong>Category </strong>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 84%; height: 26px;" colspan="2">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 26px;">
                                                        <strong>GST On </strong>
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:DropDownList ID="ddlGST" runat="server" Width="200px">
                                                            <asp:ListItem Value="T" Text="Trade Price"></asp:ListItem>
                                                            <asp:ListItem Value="E" Text="Exempted"></asp:ListItem>
                                                            <asp:ListItem Value="R" Text="Retail Price"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 26px;">
                                                        <strong>
                                                            Source Type
                                                        </strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:DropDownList ID="ddlSource" runat="server" Width="200px">
                                                            <asp:ListItem Value="0">Local Vendors</asp:ListItem>
                                                            <asp:ListItem Value="1">Inhouse Production</asp:ListItem>
                                                            <asp:ListItem Value="2">Imported Vendors</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px;">
                                                        <strong>Product Name </strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:TextBox ID="txtskuname" runat="server" Width="192px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px;">
                                                        <strong>Pack Size </strong>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:TextBox ID="txtPackSize" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 26px;">
                                                        <strong>Unit In Case </strong>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:TextBox ID="txtunitincase" runat="server" Width="192px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="unitincase" runat="server" TargetControlID="txtunitincase"
                                                            FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                               
                                                <%--<tr>
                                                    <td style="height: 26px;">
                                                        <strong>Weight </strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="width:16%">
                                                    <asp:TextBox ID="txtSkuweight" runat="server" Width="112px"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 26px;">
                                                        
                                                         <asp:DropDownList ID="ddlUOM" runat="server" Width="80px">
                                                            <asp:ListItem Value="0" Text="Kg"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Grm"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Ltr"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <cc1:FilteredTextBoxExtender ID="fteSkuweight" runat="server" TargetControlID="txtSkuweight"
                                                            FilterType="Custom" ValidChars=".0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>--%>
                                                 
                                                 <tr>
                                                    <td style="height: 26px;">
                                                        <strong>Min. Stock Level </strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:TextBox ID="txtMinimumStock" runat="server" Width="192px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteMinimumStock" runat="server" TargetControlID="txtMinimumStock"
                                                            FilterType="Custom" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 26px;">
                                                        <strong>Max. Stock Level </strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="height: 26px;" colspan="2">
                                                        <asp:TextBox ID="txtMaximumStock" runat="server" Width="192px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteMaximumStock" runat="server" TargetControlID="txtMaximumStock"
                                                            FilterType="Custom" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                               
                                               
                                               
                                              
                                                <tr>
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                    <strong>
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Checked="true" /></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 15%;">
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <br />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="86px" Font-Size="8pt"
                                                Text="Save" CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
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
