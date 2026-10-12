<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOpeningStock.aspx.cs" Inherits="Forms_frmOpeningStock" Title="SAMS :: Stock Adjustment"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">


        function pageLoad() {
            $("select").searchable();
        }
        function ConfirmDelete() {
            if (confirm("Do you want to Cancel this record?") == true)
                return true;

            else {
                return false;
            }
        }

        function ValidateForm() {
            var str;


            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
        }


        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 10%;
                    height: 100px">
                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="31px" />
                                Wait Update
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td align="left" colspan="1">
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                            </td>
                                            <td style="width: 316px" valign="middle" align="center" colspan="1" rowspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="100px" Height="14px" Text="Transaction Type"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Value="7">Opening Stock</asp:ListItem>
                                                    <asp:ListItem Value="8">Short</asp:ListItem>
                                                    <asp:ListItem Value="9">Excess</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="50px" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="94px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                    TabIndex="3">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox" TabIndex="4"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:Label ID="lblskuname" runat="server" Width="287px"
                                                     Text="Product Name" CssClass="lblDetail" ></asp:Label>
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="76px" 
                                                     Text="Quantity" CssClass="lblDetail"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Width="76px" 
                                                     Text="Pur. Price" CssClass="lblDetail" ></asp:Label>
                                            </td>
                                            <asp:Label ID="lblFreeSKU" runat="server" Width="100px" 
                                                Visible="false"  Text="Exp Date" CssClass="lblDetail" 
                                                ></asp:Label>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label4" runat="server" Width="95px" 
                                                     Text="Amount" CssClass="lblDetail" ></asp:Label>
                                            </td>
                                            <td>
                                           </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="287px" TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                         
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="75px" CssClass="txtBox " TabIndex="7"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789" TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPurchasePrice" runat="server" Width="75px" CssClass="txtBox "
                                                    TabIndex="8"></asp:TextBox>
                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Custom" ValidChars=".0123456789" TargetControlID="txtPurchasePrice">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <%--<td>--%>
                                            <asp:TextBox ID="txtExpDate" runat="server" Width="100px" Visible="false" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibExpDate" runat="server" Visible="false" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <ajaxToolkit:CalendarExtender ID="ceExpDate" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ibExpDate" TargetControlID="txtExpDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--</td>--%>
                                            <td>
                                                  <asp:TextBox ID="txtAmount" runat="server" Width="95px" ReadOnly="true"></asp:TextBox>
                                           
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" TabIndex="9" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="661px" Height="200px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="100%" ForeColor="SteelBlue"
                                                        BorderColor="White" BackColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False"
                                                        OnRowEditing="GrdPurchase_RowEditing">
                                                        
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="250px" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="65px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="65px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TIME_STAMP" HeaderText="Exp Date" DataFormatString="{0:dd-MMM-yyyy}"
                                                                Visible="false">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                             <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="35px" HorizontalAlign="Center"/>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"
                                                                    Width="40px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" OnClick="btnSaveDocument_Click" runat="server"
                                    Width="110px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False"
                                    CssClass="Button" TabIndex="10" />
                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                    Width="110px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button"
                                    TabIndex="11" />
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
