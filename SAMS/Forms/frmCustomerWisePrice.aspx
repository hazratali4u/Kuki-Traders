<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerWisePrice.aspx.cs" Inherits="Forms_frmCustomerWisePrice"
    Title="SAMS :: Customer Wise Price" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;


            str = document.getElementById('<%=txtUnitPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter unit Price');
                return false;
            }

            return true;
        }


        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <div style="left: 15px; position: absolute; top: 150px; height: 270px;">
                        </div>
                        <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" style="height: 5px">
                                                &nbsp;
                                            </td>
                                            <td align="left" colspan="2" style="height: 5px">
                                                <asp:HiddenField ID="hfSKUID" runat="server" />
                                                <asp:HiddenField ID="hfBatchValue" runat="server" />
                                            </td>
                                            <td align="left" style="height: 5px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="230px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="height: 23px">
                                            &nbsp;</td>
                                            </tr>--%>
                                        <tr>
                                            <td style="width:90px;">
                                                <strong>
                                                    Document No
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpAddEditGroup" runat="server" AutoPostBack="True" Width="230px"
                                                    OnSelectedIndexChanged="drpAddEditGroup_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="height: 23px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    Group Name
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtGroupPrice" CssClass="textbox" class="textbox"
                                                    Width="225px"></asp:TextBox>
                                            </td>
                                             <td align="left" style="height: 23px">
                                                <asp:CheckBox ID="ChbIsActive" runat="server" Text="InActive" Font-Bold="true"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td colspan="3">
                                        </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr class="detailClass">
                                            <td >
                                                <label> SKU Description</label>
                                            </td>
                                            <td >
                                                 <label> Price</label>
                                            
                                            </td>
                                            <td >
                                                 <label> Date Effected</label>
                                         
                                            </td>
                                            <td >
                                               </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                           
                                            <td >
                                                <asp:TextBox ID="txtUnitPrice" runat="server"  Width="90px"></asp:TextBox>
                                            </td>
                                            <td >
                                                <asp:TextBox ID="txtDateEfected" runat="server" CssClass="txtBox" MaxLength="10"
                                                    Enabled="false" onkeyup="BlockStartDateKeyPress()" Width="100px"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="18px" Height="18px"/>
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
                                                <ajaxToolkit:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ibtnStartDate" TargetControlID="txtDateEfected">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td style="height: 16px; width: 100px">
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add"
                                                    Width="90px" AccessKey="A" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <asp:Panel ID="Panel2" runat="server" Height="210px" ScrollBars="Vertical" Width="634px"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                                         AutoGenerateColumns="False" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        Width="100%">
                                                        
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle  CssClass="grdDetail"
                                                                    Width="281px"></ItemStyle>
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="UNIT_PRICE">
                                                                <ItemStyle HorizontalAlign="Right" CssClass="grdDetail"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="DATE_EFFECTED" HeaderText="DATE_EFFECTED" DataFormatString="{0:dd-MMM-yyyy}">
                                                                <ItemStyle HorizontalAlign="Right"  CssClass="grdDetail"
                                                                    Width="90px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" >
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail"  HorizontalAlign="Center" Width="40px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                    <td style="width: 100px">
                        <asp:HiddenField ID="hfBillBookNo" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                           
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="S" ID="btnSaveOrder" OnClick="btnSaveOrder_Click" runat="server"
                                                                    Width="100px" Font-Size="8pt" Text="Save" ToolTip="Save Group" CssClass="Button" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="H" ID="btnCancel" TabIndex="102" runat="server" Width="100px"
                                                                    Font-Size="8pt" Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 1px" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="numTxtTotalSED" runat="server" Font-Bold="False" ForeColor="Black"
                            Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
