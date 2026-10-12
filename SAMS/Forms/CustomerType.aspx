<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerType.aspx.cs" Inherits="Forms_CustomerType" Title="Customer Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style type="text/css" >
        .cmp input
        {
            width: 90%;
        }
        .cmp select
        {
            width: 90%;
        }
        .list input
        {
            width: 20%;
        }
        .list
        {
            width: 90%;
        }
    </style>
    <div id="right_data" >
        <table width="100%">
            <tr>
                <td style="width: 100px">
                    <cc1:TabContainer ID="TabContainer1" runat="server" Height="375px" Width="850px"
                        ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Designation
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px; ">
                                            </td>
                                            <td style="width: 100px">
                                            </td>
                                            <td style="width: 100px; ">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                            </td>
                                            <td style="width: 100px">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel3" runat="server">
                                                            <fieldset>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                            <td style="width: 49px">
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="lblErrorMsg" runat="server" AccessKey="C" Font-Bold="True" 
                                                                                    ForeColor="Red"></asp:Label>
                                                                                <br />
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 28px">
                                                                            </td>
                                                                            <td style="width: 49px; height: 28px">
                                                                                <strong>
                                                                                Code
                                                                                </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtChannelCode" runat="server" CssClass="txtBox " 
                                                                                    Enabled="False" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 28px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px">
                                                                            </td>
                                                                            <td style="width: 49px; height: 29px">
                                                                                <strong>
                                                                                Name
                                                                                </strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                                <asp:TextBox ID="txtChannelName" runat="server" CssClass="txtBox " 
                                                                                    Enabled="False" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px">
                                                                            </td>
                                                                            <td align="right">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnSaveChannelType" runat="server" CssClass="Button" 
                                                                                    Font-Size="8pt" OnClick="btnSaveChannelType_Click" Text="New" Width="85px" />
                                                                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Font-Size="8pt" 
                                                                                    OnClick="btnCancel_Click" Text="Cancel" Width="85px" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </fieldset>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 100px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="Panel1" runat="server" Height="220px" ScrollBars="Vertical" 
                                                    Width="100%">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grdChannelData" runat="server" AutoGenerateColumns="False" 
                                                                
                                                                HorizontalAlign="Center" OnPageIndexChanging="grdChannelData_PageIndexChanging" 
                                                                OnRowDeleting="grdChannelData_RowDeleting" 
                                                                OnRowEditing="grdChannelData_RowEditing" Width="100%">
                                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" 
                                                                    NextPageText="Next" PreviousPageText="Previous" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="REF_ID" HeaderText="Id">
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        CssClass="HidePanel" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SLASH_CODE" HeaderText="Code">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SLASH_DESC" HeaderText="Name">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:CommandField  ShowEditButton="True">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        Width="40px" HorizontalAlign="Center"/>
                                                                    </asp:CommandField>
                                                                    <asp:TemplateField >
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                            Width="50px" HorizontalAlign="Center"/>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" 
                                                                                OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" 
                                                                                Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="tblhead" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" Visible="true">
                            <HeaderTemplate>
                                Department
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px; ">
                                            </td>
                                            <td align="center" style="width: 100px">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnl_dept" runat="server">
                                                            <fieldset>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px">
                                                                            </td>
                                                                            <td align="left" colspan="2" style="height: 8px">
                                                                                <strong>
                                                                                <asp:Label ID="lblErrorMsgDivsion" runat="server" Font-Bold="True" 
                                                                                    ForeColor="Red"></asp:Label>
                                                                                </strong>
                                                                                <br />
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 49px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 28px">
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>
                                                                                Code
                                                                                </strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtbustypeCode" runat="server" CssClass="txtBox " 
                                                                                    Enabled="False" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 28px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px">
                                                                            </td>
                                                                            <td align="left">
                                                                                <strong>
                                                                                Name
                                                                                </strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                                <asp:TextBox ID="txtbustypeName" runat="server" CssClass="txtBox " 
                                                                                    Enabled="False" Width="194px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px">
                                                                            </td>
                                                                            <td align="right">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSaveBusType" runat="server" CssClass="Button" 
                                                                                    Font-Size="8pt" OnClick="btnSaveBusType_Click" Text="New" Width="85px" />
                                                                                <asp:Button ID="btncancelDestype" runat="server" CssClass="Button" 
                                                                                    Font-Size="8pt" OnClick="btncancelDestype_Click" Text="Cancel" Width="85px" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </fieldset>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 100px; ">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Height="215px" ScrollBars="Vertical" 
                                                    Width="100%">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="GrdBusType" runat="server" AutoGenerateColumns="False" 
                                                                
                                                                HorizontalAlign="Center" OnPageIndexChanging="GrdBusType_PageIndexChanging" 
                                                                OnRowDeleting="GrdBusType_RowDeleting" OnRowEditing="GrdBusType_RowEditing" 
                                                                Width="100%">
                                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" 
                                                                    NextPageText="Next" PreviousPageText="Previous" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="REF_ID" HeaderText="Id">
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        CssClass="HidePanel" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SLASH_CODE" HeaderText="Code">
                                                                    <HeaderStyle BorderColor="Silver" BorderWidth="1px" />
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SLASH_DESC" HeaderText="Name">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                        HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:CommandField  ShowEditButton="True">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"/>
                                                                    </asp:CommandField>
                                                                    <asp:TemplateField >
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                                            Width="50px" HorizontalAlign="Center"/>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" 
                                                                                OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" 
                                                                                Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="tblhead" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                &nbsp;
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
