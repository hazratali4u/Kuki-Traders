<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmWithDrawnEntry.aspx.cs" Inherits="frmWithDrawnEntry" Title="SAMS :: Petty Withdrawn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      
  <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <div style="z-index: 101; left: 537px; width: 100px; position: absolute; top: 250px;
                        height: 83px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>                
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="Label2" runat="server" Text="Transaction No:"></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:DropDownList id="DrpVoucherNo" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherNo_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="lblfromLocation" runat="server" Width="65px" Text="Location:" ></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:DropDownList id="drpDistributor" runat="server" Width="250px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="drpDistributor_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="Label1" runat="server" Text="Withdrawn From:"></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:DropDownList id="DrpAccountHead" runat="server" Width="250px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="Label3" runat="server" Text="Withdrawn Type:"></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:RadioButtonList ID="rbType" runat="server" Width="150px" 
                        RepeatDirection="Horizontal" AutoPostBack="True" 
                        onselectedindexchanged="rbType_SelectedIndexChanged">
                        <asp:ListItem Text="Cheque" Value="35" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="ATM" Value="36"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="lblChequeNo" runat="server" Text="Cheque No:"></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:TextBox ID="txtChequeNo" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="Label6" runat="server" Text="Amount:" ></asp:Label></strong> 
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:TextBox id="txtAmount" runat="server" Width="250px" CssClass="txtBox "></asp:TextBox>
                    <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Custom" TargetControlID="txtAmount" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender> 
                </td>
            </tr>
            <tr>
                <td style="width:10%;" align="right">
                    <strong><asp:Label id="Label9" runat="server" Width="71px" Text="Remarks:" ></asp:Label></strong>
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:10%;" align="right">
                    
                </td>
                <td style="width:1%;"></td>
                <td style="width:81%;">
                    <asp:Button ID="btnSave" runat="server" accessKey="S" CssClass="Button" onclick="btnSave_Click" Text="Save" />
                </td>
            </tr>
            
        </table>
    </ContentTemplate>
</asp:UpdatePanel>                    
                </td>
            </tr>
        </table>        
</div>
</asp:Content>
