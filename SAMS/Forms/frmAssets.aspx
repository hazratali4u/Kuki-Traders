<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmAssets.aspx.cs" Inherits="Forms_frmAssets" Title="SAMS :: Assets Opening" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
    <script language="JavaScript" type="text/javascript">
          function pageLoad() {
           $("select").searchable();
       }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

       }
        
  </script>
  <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
 <div id="right_data">
     <div >
         <table width="100%">
             <tr>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>

                         <table width="100%">
                            <tr>
                                <td style="width:15%" align="right">
                                    <strong><asp:Label id="Label4" runat="server" Text="Location:"></asp:Label></strong>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:84%">
                                    <asp:DropDownList id="drpDistributor" runat="server" Width="206px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%" align="right">
                                    <strong><asp:Label ID="lblDueDate" runat="server" Text="Voucher Date:"></asp:Label></strong>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:84%">
                                    <asp:TextBox ID="txtVoucherDate" runat="server" Width="80px" onkeyup="BlockEndDateKeyPress()" ReadOnly="true"></asp:TextBox>
                                         <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                         <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate" TargetControlID="txtVoucherDate">
                                         </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%" align="right">
                                    <strong><asp:Label ID="Label1" runat="server" Text="Narration:"></asp:Label></strong>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:84%">
                                    <asp:TextBox ID="txtNarration" runat="server" Width="206px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width:100%">
                                <asp:Panel id="Panel2" runat="server" Width="740px" Height="300px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
            <asp:GridView ID="gvOpening" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="720px">
            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous" />
            <RowStyle ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                <HeaderStyle CssClass="HidePanel" />
                <ItemStyle CssClass="HidePanel" />
                </asp:BoundField>
                <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Account Description">
                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                    <asp:TextBox ID="txtRemarks" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>                 
                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="150px"/>                                                                                            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemTemplate>
                    <asp:TextBox ID="txtDebit" runat="server" Width="100%"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebit" FilterType="Custom" ValidChars=".0123456789"> 
                        </cc1:FilteredTextBoxExtender>
                    </ItemTemplate>                 
                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="80px"/>                                                                                            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                    <asp:TextBox ID="txtCredit" runat="server" Width="100%"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCredit" FilterType="Custom" ValidChars=".0123456789"> 
                        </cc1:FilteredTextBoxExtender>
                    </ItemTemplate>                 
                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="80px"/>                                                                                            
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" />
            <PagerStyle BackColor="Transparent" />
            <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
            VerticalAlign="Middle" />
            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
            </asp:GridView>
        </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                <DIV style="Z-INDEX: 101; LEFT: 706px; WIDTH: 100px; POSITION: absolute; TOP: 364px; HEIGHT: 100px">
                                <asp:Panel id="Panel21" runat="server">
                                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                         <ProgressTemplate>
                                             <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                 Width="39px" />
                                             Wait Update
                                         </ProgressTemplate>
                                     </asp:UpdateProgress>
                                 </asp:Panel> 
                                 </DIV>
                                </td>
                            </tr>
                         </table> 
                                                                                 
</ContentTemplate>
                     </asp:UpdatePanel>
                     </td>
             </tr>
         </table>
     </div>
     <div >         
         <table width="50%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save" CssClass="Button" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click" 
                        Text="Cancel" CssClass="Button" />                        
                </td>
            </tr>
         </table>
       </div>
<br />
  </div>
</asp:Content>

