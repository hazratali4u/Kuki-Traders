<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleReturnAccount.aspx.cs" Inherits="Forms_frmSaleReturnAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
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
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>
                <table cellpadding="2px">
                    <tr>
                        <td style="width: 70px;">
                            <strong>
                                <label>
                                    Location</label>
                            </strong>
                        </td>
                        <td style="width: 265px;">
                            <asp:DropDownList ID="ddlDistributor" runat="server" Width="220px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Customer</label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="220px">
                                </asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hfIsGstReg"/> 
                                <asp:HiddenField runat="server" ID="hfZoneId"/>
                <asp:HiddenField runat="server" ID="hfGst"/>
                <asp:HiddenField runat="server" ID="hfAddGst"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
                <fieldset style="width: 200px;">
                    <table>
                        <tbody>
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:Panel ID="Panel2" runat="server" Width="900px" Height="220px" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="SteelBlue" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                            OnRowDataBound="grdOrder_RowDataBound">
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="skuId" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="skuName" HeaderText="Product Name">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="420px" /> 
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="qty" HeaderText="Return Qty">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NGQty" HeaderText="N.G Qty">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="remarks" HeaderText="Remarks">
                                                    <ItemStyle CssClass="grdDetail" Width="300px" />
                                                    <HeaderStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="returnQty" HeaderText="Inventory Qty">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="80px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <HeaderStyle CssClass="grdHead" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPrice" runat="server" Width="90px" Text='<%# String.Format("{0:0}", Eval("unitPrice")) %>'>
                                                        </asp:TextBox>
                                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            ValidChars=".0123456789" TargetControlID="txtPrice">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" />
                                                    <HeaderStyle CssClass="grdHead" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="techId">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px;">
                            <strong>Remarks</strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRemarks" Width="850px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="100px" Font-Size="8pt"
                                Text="Save" UseSubmitBehavior="False" OnClick="btnSaveOrder_Click" CssClass="Button" />
                            <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveOrder" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
