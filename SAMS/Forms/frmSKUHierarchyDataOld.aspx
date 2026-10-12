<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSKUHierarchyDataOld.aspx.cs" Inherits="frmSKUHierarchyDataOld" Title="SAMS :: SKU Hierarchy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtSTRN.ClientID %>').mask("99-99-9999-999-99");
        }
 
    </script>
<div id="right_data">
         <table width="100%">
             <tr>
                 <td>
                     <cc1:TabContainer ID="TabContainer1" runat="server"  Height="400px"
                         Width="650px" ActiveTabIndex="0">
                         <cc1:TabPanel ID="TabPanel1" runat="server">
                                <HeaderTemplate>
                                 Principal
                             
</HeaderTemplate>
                             <ContentTemplate>
                                 <table width="100%">
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td style="width: 100px">
                                             <asp:UpdatePanel id="UpdatePanel6" runat="server">
                                                 <contenttemplate>
<TABLE width="100%"><TBODY><TR><TD style="WIDTH: 100px"></TD><TD colSpan=2><asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True" __designer:wfdid="w185"></asp:Label><BR /></TD><TD style="WIDTH: 100px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 28px"></TD><TD style="WIDTH: 49px; HEIGHT: 28px">
<strong> <asp:Label id="Label1" runat="server" Width="90px" Text="Principal Code" __designer:wfdid="w186"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:TextBox id="txtPrincipalCode" runat="server" Width="200px" __designer:wfdid="w187" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 49px; HEIGHT: 29px">
<strong><asp:Label id="Label2" runat="server" Width="96px" Text="Principal Name" __designer:wfdid="w188"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtPrincipalName" runat="server" Width="200px" __designer:wfdid="w189" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 49px; HEIGHT: 29px">
<strong><asp:Label id="Label11" runat="server" Width="96px" Text="Address" __designer:wfdid="w190"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtAddress" runat="server" Width="200px" __designer:wfdid="w191" Enabled="False" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 49px; HEIGHT: 29px">
<strong><asp:Label id="Label13" runat="server" Width="96px" Text="NTN" __designer:wfdid="w192"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtNTN" runat="server" Width="200px" __designer:wfdid="w193" Enabled="False" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 49px; HEIGHT: 29px">
<strong><asp:Label id="Label14" runat="server" Width="96px" Text="STRN" __designer:wfdid="w194"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtSTRN" runat="server" Width="200px" __designer:wfdid="w195" Enabled="False" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 37px"></TD><TD style="WIDTH: 49px; HEIGHT: 37px" align=left></TD><TD style="WIDTH: 100px; HEIGHT: 37px"><asp:CheckBox id="ChIsMunalDiscount" runat="server" Width="190px" Text="Is Manual Dicount" __designer:wfdid="w196"></asp:CheckBox> </TD><TD style="WIDTH: 100px; HEIGHT: 37px"></TD></TR><TR><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 49px" align=right>&nbsp;</TD><TD style="WIDTH: 100px">
<asp:Button id="btnSavePrincipal" onclick="btnSavePrincipal_Click" runat="server" Width="85px" Font-Size="8pt" Text="New" CssClass="Button" /> </TD><TD style="WIDTH: 100px"></TD></TR></TBODY></TABLE>
</contenttemplate>
                                             </asp:UpdatePanel>
                                             <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6"><progresstemplate>
<asp:ImageButton id="ImageButton1" runat="server" Width="33px" Height="31px" __designer:wfdid="w199" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton> 
</progresstemplate>
                                             </asp:UpdateProgress></td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="center" colspan="3">
                                             <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" Width="100%">
                                             <asp:UpdatePanel id="UpdatePanel5" runat="server">
                                                 <contenttemplate>
<asp:GridView id="GrdPrincipal" runat="server" Width="90%" ForeColor="SteelBlue" Font-Size="9pt" __designer:wfdid="w202" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdPrincipal_RowEditing" OnRowDeleting="GrdPrincipal_RowDeleting" OnPageIndexChanging="GrdPrincipal_PageIndexChanging">
<PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText="" NextPageText="Next"></PagerSettings>

<Columns>
<asp:BoundField DataField="SKU_HIE_ID" Visible="False" HeaderText="ID">
<ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
<ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
<ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="IS_MANUALDISCOUNT" HeaderText="Manual Discount"></asp:BoundField>
<asp:BoundField DataField="ADDRESS" Visible="False" HeaderText="ADDRESS"></asp:BoundField>
<asp:BoundField DataField="NTN" Visible="False" HeaderText="NTN"></asp:BoundField>
<asp:BoundField DataField="STRN" Visible="False" HeaderText="STRN"></asp:BoundField>
<asp:CommandField HeaderText="Edit" ShowEditButton="True"></asp:CommandField>
<asp:TemplateField HeaderText="Delete">
<ItemTemplate>
    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
      Text="Delete"></asp:LinkButton>                                                         
</ItemTemplate>
</asp:TemplateField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>

<AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
</asp:GridView> 
</contenttemplate>
                                             </asp:UpdatePanel>
                                             </asp:Panel>
                                         </td>
                                     </tr>
                                 </table>
                             
</ContentTemplate>
                         </cc1:TabPanel>
                         <cc1:TabPanel ID="TabPanel2" runat="server">
                             <HeaderTemplate>
                                 Division
                             
</HeaderTemplate>
                             <ContentTemplate>
<table width="100%"><tr><td style="width: 100px;"></td><td align="center"><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
<TABLE width="100%"><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 8px"></TD><TD style="HEIGHT: 8px" align=left colSpan=2>
<asp:Label id="lblErrorMsgDivsion" runat="server" ForeColor="Red" Font-Bold="True" __designer:wfdid="w16"></asp:Label><BR /></TD><TD style="WIDTH: 100px; HEIGHT: 8px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 8px"></TD><TD style="WIDTH: 159px; HEIGHT: 8px"></TD><TD style="WIDTH: 100px; HEIGHT: 8px"></TD><TD style="WIDTH: 100px; HEIGHT: 8px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 23px"></TD><TD style="WIDTH: 159px" align=left>
<strong><asp:Label id="Label3" runat="server" Width="80px" Text="Principal" __designer:wfdid="w17"></asp:Label></strong> &nbsp;</TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:DropDownList id="dddivisonPrincipal" runat="server" Width="200px" __designer:wfdid="w18" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="dddivisonPrincipal_SelectedIndexChanged">
                                             </asp:DropDownList> </TD><TD style="WIDTH: 100px; HEIGHT: 23px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 28px"></TD><TD style="WIDTH: 159px; HEIGHT: 28px" align=left>
                                             <strong><asp:Label id="Label12" runat="server" Width="80px" Text="Division Code" __designer:wfdid="w19"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:TextBox id="txtDivisionCode" runat="server" Width="194px" __designer:wfdid="w20" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 159px; HEIGHT: 29px" align=left>
                                             <strong><asp:Label id="Label21" runat="server" Width="85px" Text="Division Name" __designer:wfdid="w21"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtDivisionName" runat="server" Width="194px" __designer:wfdid="w22" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 37px"></TD><TD style="WIDTH: 159px; HEIGHT: 37px" align=right>&nbsp;</TD><TD style="WIDTH: 100px; HEIGHT: 37px" align=left>
                                             <asp:Button id="btnSaveDivison" onclick="btnSaveDivison_Click" runat="server" Width="85px" Font-Size="8pt" Text="New" CssClass="Button" /> </TD><TD style="WIDTH: 100px; HEIGHT: 37px"></TD></TR></TBODY></TABLE>
</ContentTemplate>
</asp:UpdatePanel> 
    <asp:UpdateProgress id="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate>
<asp:Image id="Image2" runat="server" Width="30px" Height="28px" __designer:wfdid="w25" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading ......... 
</progresstemplate>
    </asp:UpdateProgress></td><td style="width: 100px;"></td></tr><tr><td align="center" colspan="3">
        <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">
            <asp:UpdatePanel id="UpdatePanel1" runat="server"><contenttemplate>
<asp:GridView id="GrdDivision" runat="server" Width="90%" ForeColor="SteelBlue" Font-Size="9pt" __designer:wfdid="w28" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdDivision_RowEditing" OnRowDeleting="GrdDivision_RowDeleting" OnPageIndexChanging="GrdDivision_PageIndexChanging">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<Columns>
<asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
</asp:CommandField>
<asp:TemplateField HeaderText="Delete">
<ItemTemplate>
<asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" Text="Delete"></asp:LinkButton>                                                         
</ItemTemplate>
</asp:TemplateField>
</Columns>

<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
</contenttemplate>
</asp:UpdatePanel> 
        </asp:Panel>
    </td></tr></table><br />
</ContentTemplate>
                         </cc1:TabPanel>
                         <cc1:TabPanel ID="TabPanel3" runat="server">
                             <HeaderTemplate>
                                 Category
                             
</HeaderTemplate>
                             <ContentTemplate>
                                 <table width="100%">
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td align="center" style="width: 100px">
                                             <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                 <ContentTemplate>
<TABLE width="100%"><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 16px"></TD><TD style="HEIGHT: 16px" align=left colSpan=2><BR />
<asp:Label id="lblErrorMsgCategory" runat="server" ForeColor="Red" Font-Bold="True" __designer:wfdid="w114"></asp:Label></TD><TD style="WIDTH: 100px; HEIGHT: 16px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 8px"></TD><TD style="WIDTH: 69px; HEIGHT: 8px"></TD><TD style="WIDTH: 100px; HEIGHT: 8px"></TD><TD style="WIDTH: 100px; HEIGHT: 8px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 21px"></TD><TD style="WIDTH: 69px; HEIGHT: 21px" align=left>
<strong><asp:Label id="Label5" runat="server" Width="80px" Text="Principal" __designer:wfdid="w115"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:DropDownList id="DrpCategoryPrincipal" runat="server" Width="200px" __designer:wfdid="w116" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpCategoryPrincipal_SelectedIndexChanged">
            </asp:DropDownList></TD><TD style="WIDTH: 100px; HEIGHT: 21px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 21px"></TD><TD style="WIDTH: 69px; HEIGHT: 21px" align=left>
            <strong><asp:Label id="Label4" runat="server" Width="80px" Text="Division" __designer:wfdid="w117"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:DropDownList id="ddCategoryDivision" runat="server" Width="200px" __designer:wfdid="w118" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddCategoryDivision_SelectedIndexChanged">
                                             </asp:DropDownList> </TD><TD style="WIDTH: 100px; HEIGHT: 21px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 28px"></TD><TD style="WIDTH: 69px; HEIGHT: 28px" align=left>
                                             <strong><asp:Label id="Label121" runat="server" Width="90px" Text="Category Code" __designer:wfdid="w119"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:TextBox id="txtCategoryCode" runat="server" Width="194px" __designer:wfdid="w120" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px"></TD><TD style="WIDTH: 69px; HEIGHT: 29px" align=left>
                                             <strong><asp:Label id="Label211" runat="server" Width="90px" Text="Category Name" __designer:wfdid="w121"></asp:Label></strong> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtCategoryName" runat="server" Width="194px" __designer:wfdid="w122" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 37px"></TD><TD style="WIDTH: 69px; HEIGHT: 37px" align=right>&nbsp;</TD><TD style="WIDTH: 100px; HEIGHT: 37px" align=left>
                                             <asp:Button id="btnSaveCategory" onclick="btnSaveCategory_Click" runat="server" Width="85px" Font-Size="8pt" Text="New" CssClass="Button" /> </TD><TD style="WIDTH: 100px; HEIGHT: 37px"></TD></TR></TBODY></TABLE>
</ContentTemplate>
                                             </asp:UpdatePanel><asp:UpdateProgress id="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel21"><progresstemplate>
<asp:Image id="Image3" runat="server" Width="30px" Height="28px" __designer:wfdid="w125" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading ......... 
</progresstemplate>
                                             </asp:UpdateProgress>
                                         </td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="center" colspan="3">
                                             <asp:Panel ID="Panel3" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">
                                             <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                                 <contenttemplate>
<asp:GridView id="GrdCategory" runat="server" Width="90%" ForeColor="SteelBlue" Font-Size="9pt" __designer:wfdid="w128" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing" OnRowDeleting="GrdCategory_RowDeleting" OnPageIndexChanging="GrdCategory_PageIndexChanging">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
</asp:CommandField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
                                                 <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                     Text="Delete"></asp:LinkButton>
                                             
</ItemTemplate>
</asp:TemplateField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
</contenttemplate>
                                             </asp:UpdatePanel>
                                             </asp:Panel>
                                         </td>
                                     </tr>
                                 </table>
                             
</ContentTemplate>
                         </cc1:TabPanel>
                         <cc1:TabPanel ID="TabPanel4" runat="server">
                             <HeaderTemplate>
                                 Brand
                             
</HeaderTemplate>
                             <ContentTemplate>
                                 <table width="100%">
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td align="center" style="width: 100px">
                                             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                 <ContentTemplate>
<TABLE width="100%"><TBODY><TR><TD colSpan=2><asp:Label id="lblErrorMsgBrand" runat="server" ForeColor="Red" Font-Bold="True" __designer:wfdid="w148"></asp:Label><BR /></TD><TD style="WIDTH: 100px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="Label6" runat="server" Width="80px" Text="Principal" __designer:wfdid="w149"></asp:Label></strong></TD><TD style="WIDTH: 100px"><asp:DropDownList id="DrpBrandPrincipal" runat="server" Width="200px" __designer:wfdid="w150" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpBrandPrincipal_SelectedIndexChanged">
</asp:DropDownList></TD><TD style="WIDTH: 100px"></TD></TR><TR><TD style="HEIGHT: 21px" align=left>
<strong><asp:Label id="Label10" runat="server" Width="64px" Text="Division" __designer:wfdid="w151"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:DropDownList id="DrpBrandDivision" runat="server" Width="200px" __designer:wfdid="w152" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpBrandDivision_SelectedIndexChanged">
            </asp:DropDownList></TD><TD style="WIDTH: 100px; HEIGHT: 21px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
            <strong><asp:Label id="Label9" runat="server" Width="80px" Text="Category" __designer:wfdid="w153"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:DropDownList id="ddBrandCategory" runat="server" Width="200px" __designer:wfdid="w154" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddBrandCategory_SelectedIndexChanged">
                                                                 </asp:DropDownList></TD><TD style="WIDTH: 100px; HEIGHT: 28px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 28px" align=left>
                                                                 <strong><asp:Label id="Label7" runat="server" Width="90px" Text="Brand Code" __designer:wfdid="w155"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 28px"><asp:TextBox id="txtBrandCode" runat="server" Width="194px" __designer:wfdid="w156" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 28px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 29px" align=left>
                                                                 <strong><asp:Label id="Label8" runat="server" Width="90px" Text="Brand Name" __designer:wfdid="w157"></asp:Label></strong></TD><TD style="WIDTH: 100px; HEIGHT: 29px"><asp:TextBox id="txtBrandName" runat="server" Width="194px" __designer:wfdid="w158" Enabled="False" CssClass="txtBox "></asp:TextBox> </TD><TD style="WIDTH: 100px; HEIGHT: 29px"></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 37px"></TD><TD style="WIDTH: 100px; HEIGHT: 37px" align=left>
                                                                 <asp:Button accessKey="B" id="btnSaveBrand" onclick="btnSaveBrand_Click" runat="server" Width="85px" Font-Size="8pt" Text="New" CssClass="Button" /> </TD><TD style="WIDTH: 100px; HEIGHT: 37px"></TD></TR></TBODY></TABLE>
</ContentTemplate>
                                             </asp:UpdatePanel><asp:UpdateProgress id="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4"><progresstemplate>
<asp:Image id="Image4" runat="server" Width="24px" Height="23px" __designer:wfdid="w161" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading..... 
</progresstemplate>
                                             </asp:UpdateProgress>
                                         </td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="center" colspan="3">
                                             <asp:Panel ID="Panel4" runat="server" Height="150px" ScrollBars="Vertical"
                                                 Width="100%">
                                             <asp:UpdatePanel id="UpdatePanel8" runat="server">
                                                 <contenttemplate>
<asp:GridView id="GrdBrand" runat="server" Width="90%" ForeColor="SteelBlue" Font-Size="9pt" __designer:wfdid="w164" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdBrand_RowEditing" OnRowDeleting="GrdBrand_RowDeleting" OnPageIndexChanging="GrdBrand_PageIndexChanging">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<Columns>
<asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
</asp:CommandField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
                                                 <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                     Text="Delete"></asp:LinkButton>
                                             
</ItemTemplate>
</asp:TemplateField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
</contenttemplate>
                                             </asp:UpdatePanel>
                                             </asp:Panel>
                                         </td>
                                     </tr>
                                 </table>
                             
</ContentTemplate>
                         </cc1:TabPanel>
                     </cc1:TabContainer></td>
             </tr>
         </table>
     </div>
</asp:Content>

