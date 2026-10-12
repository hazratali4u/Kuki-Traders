<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPromotionStep1.aspx.cs" Inherits="Forms_frmPromotionStep1" Title="SAMS :: Promotion Wizard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must select Promotion Start Date');
                return false;
            }
            str = document.getElementById('<%=txttoDate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must select Promotino End Date');
                return false;
            }

            return true;
        }
        function BlockFromDateKeyPress() {
            document.getElementById('<%=txtFromdate.ClientID%>').value = '';
            alert('Click Clender Button for Select Date');
        }
        function BlocktoDateKeyPress() {
            document.getElementById('<%=txttoDate.ClientID%>').value = '';
            alert('Click Clender Button for Select Date');
        }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 201px">
                                </td>
                                <td style="width: 100px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Width="61px" Visible="False">Scheme</asp:Label></strong>
                                </td>
                                <td align="left" style="width: 201px">
                                    <asp:DropDownList ID="drpScheme" runat="server" Width="200px" CssClass="DropList"
                                        Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 20px">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Width="90px" Height="13px" Text="From Date"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px; width: 201px;">
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" runat="server" Width="192px"
                                        onkeyup="BlockFromDateKeyPress()" CssClass="txtBox"></asp:TextBox>
                                </td>
                                <td style="height: 20px" align="left">
                                    <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        CausesValidation="False"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Width="90px" Height="13px" Text="To Date"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 201px">
                                    <asp:TextBox Style="text-align: justify" ID="txttoDate" runat="server" Width="192px"
                                        onkeyup="BlocktoDateKeyPress()" CssClass="txtBox"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="btnToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        CausesValidation="False"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 201px">
                                </td>
                                <td style="width: 100px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                </td>
                                <td align="left" style="width: 201px">
                                    <asp:CheckBox ID="ChbActive" Font-Bold="true" runat="server" Text="Is Active"></asp:CheckBox>
                                </td>
                                <td style="width: 100px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 14px">
                                </td>
                                <td style="width: 201px; height: 14px">
                                </td>
                                <td style="width: 100px; height: 14px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    &nbsp;
                                    <asp:Button ID="btnPromotion" OnClick="btnPromotion_Click" runat="server" Width="125px"
                                        Font-Size="8pt" Text="Get Promotion" CssClass="Button" />
                                    <asp:Button ID="btnNew" runat="server" Width="125px" Font-Size="8pt" Text="New Promotion"
                                        OnClick="btnNew_Click" CssClass="Button" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromdate"
                        PopupButtonID="ImgBntFromCalc" EnableViewState="False" Format="dd-MMM-yyyy">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttoDate"
                        PopupButtonID="btnToDate" EnableViewState="False" Format="dd-MMM-yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="tblhead" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="color: White; font-weight: bold;">
                                            <asp:Label ID="Label10" runat="server" Width="94px" Text="Searching Type"></asp:Label>
                                        </td>
                                        <td style="width: 170px; height: 21px" align="left">
                                            <asp:DropDownList ID="ddSearchType" runat="server" Width="154px" CssClass="DropList">
                                                <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                                <asp:ListItem Value="SCHEME_DESC">Scheme</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_ID">Promotion Id</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_CODE">Promotion Code</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_DESCRIPTION">Description</asp:ListItem>
                                                <asp:ListItem>Principal</asp:ListItem>
                                                <asp:ListItem Value="Promotion_Class_Code">Promotion Class Code</asp:ListItem>
                                                <asp:ListItem Value="Promotion_Class_Name">Promotion Class Name</asp:ListItem>
                                                <asp:ListItem Value="GROUP_NAME">GROUP NAME</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 224px; height: 21px" align="left">
                                            <asp:TextBox ID="txtSeach" runat="server" Width="180px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                        <td style="height: 21px" align="left" width="250">
                                            <asp:Button ID="btnFilter" OnClick="btnFilter_Click" runat="server" Width="85px"
                                                Text="Filter"></asp:Button>
                                        </td>
                                        <td style="width: 250px;">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Panel ID="Panel2" runat="server" Width="100%" Height="280px" ScrollBars="Vertical"
                                BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%" ForeColor="SteelBlue"
                                    CssClass="gridRow2" BackColor="White" BorderColor="White" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowEditing="Grid_pricedetails_RowEditing" OnRowDeleting="Grid_pricedetails_RowDeleting">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <Columns>
                                        <asp:BoundField DataField="SCHEME_ID" HeaderText="Scheme Id">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass="grdDetail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROMOTION_ID" HeaderText="Promotion Id">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCHEME_DESC" HeaderText="Scheme">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROMOTION_CODE" HeaderText="Promotion Name">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROMOTION_DESCRIPTION" HeaderText="Description">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="START_DATE" HeaderText="Start Date">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="END_DATE" HeaderText="End Date">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Principal" HeaderText="Principal">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                    <img id="Img1" src="../images/edit.gif" runat="server" alt="" /></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    CommandName="Delete" ToolTip="Delete"><img src="../images/delete.gif" runat="server" alt=""/></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
