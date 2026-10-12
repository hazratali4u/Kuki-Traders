<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="SAMS :: SKU Price" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();

            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=ddskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;
            }
            str = document.getElementById('<%=txtTradePrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Trade Price');
                return false;
            }
            return true;
        }
        function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }
    </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width:50%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                Category
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddskuCategory" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddskuCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                Product Name
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddskuName" runat="server" Width="250px"
                                                OnSelectedIndexChanged="ddskuName_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td style="width:20%">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="101px" Text="Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddskuDivision" runat="server" Width="250px" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Width="101px" Text="Nature"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddskuBrand" runat="server" Width="250px"  Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="103px" Text="Model"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:DropDownList ID="ddskuPrincipal" runat="server" Width="250px"  Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                   --%>                                                                                         
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                Trade Price
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:TextBox ID="txtTradePrice" runat="server" Width="246px" MaxLength="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%">
                                            <strong>
                                                Date Effected
                                            </strong>
                                        </td>
                                        <td style="width:80%">
                                            <asp:TextBox ID="txtDateEffected" runat="server" Width="220px" MaxLength="8" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton id="ibDateEffected" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                                    <cc1:CalendarExtender id="ceDateEffected" runat="server" TargetControlID="txtDateEffected" PopupButtonID="ibDateEffected" Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td style="width:20%">
                                         
                                        </td>
                                        <td style="width:80%">
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                                Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" __designer:wfdid="w393"
                                                TargetControlID="txtTradePrice" ValidChars=".0123456789" FilterType="Custom">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:5%"></td>
                            <td style="width:45%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%; border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" align="left" 
                                    rowspan="10">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="200">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="140px" ForeColor="White" Font-Bold="True"
                                                            Text="Location Name"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" Checked="true"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="300">
                                                    <asp:Panel ID="Panel1" runat="server" Width="300px" Height="150px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="174px" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                    <td style="width:40%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px;
                        height: 100px">
                        &nbsp;
                        <asp:Panel ID="Panel21" runat="server" >
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="26px" 
                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="85%" Height="220px" ScrollBars="Auto"
                     BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%"
                            BorderColor="SteelBlue"
                            AutoGenerateColumns="False" HorizontalAlign="Center">
                            
                            <Columns>
                                <asp:BoundField DataField="distributor_name" HeaderText="Location">
                                    <ItemStyle CssClass="grdDetail" />
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" Width="20%"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                    <ItemStyle CssClass="grdDetail" Width="55%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>                                
                                <asp:BoundField DataField="TRADE_PRICE" HeaderText="Trade Price" DataFormatString="{0:F2}">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE_EFFECTED" HeaderText="Date Effected">
                                    <ItemStyle CssClass="grdDetail" Width="15%">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="grdHead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
