<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPromotionStep3.aspx.cs" Inherits="Forms_frmPromotionStep3" Title="SAMS :: Promotion Wizard Step 2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <h2>
                        Promotion Wizard Step 2</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 16px" align="left">
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 100px; height: 22px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16px; height: 20px">
                                        </td>
                                        <td style="height: 20px" colspan="2">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="96px" Text="Location Type" CssClass="lblbox"></asp:Label></strong>
                                            <asp:CheckBox ID="ChbAllLocationType" runat="server" Text="All Type Location" AutoPostBack="True"
                                                OnCheckedChanged="ChbAllLocationType_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td style="width: 30px; height: 20px">
                                        </td>
                                        <td style="height: 20px" colspan="2">
                                            &nbsp; <strong>
                                                <asp:Label ID="Label5" runat="server" Width="96px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            <asp:CheckBox ID="chkSelectAllDistributors" runat="server" Text="All Location" AutoPostBack="True"
                                                OnCheckedChanged="chkSelectAllDistributors_CheckedChanged"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1">
                                        </td>
                                        <td style="height: 126px" colspan="2">
                                            <asp:Panel ID="Panel1" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ChbDistributorType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChbDistributorType_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 30px">
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="Panel6" runat="server" Width="300px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="chklDistributors" runat="server" CssClass="lblbox">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 16px" colspan="1">
                                        </td>
                                        <td style="height: 16px" colspan="2">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="96px" Text="Channel Type" CssClass="lblbox"></asp:Label></strong>
                                            <asp:CheckBox ID="chkSelectAllCustomerType" runat="server" Text="All Channel Type"
                                                CssClass="lblbox" AutoPostBack="True" OnCheckedChanged="chkSelectAllCustomerType_CheckedChanged">
                                            </asp:CheckBox>
                                        </td>
                                        <td style="width: 30px; height: 16px">
                                        </td>
                                        <td style="height: 16px" colspan="2">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="96px" Text="Volume Class" CssClass="lblbox"></asp:Label></strong>
                                            <asp:CheckBox ID="ChbAllVolumeClass" runat="server" Text="All Volume Class" CssClass="lblbox"
                                                AutoPostBack="True" OnCheckedChanged="ChbAllVolumeClass_CheckedChanged"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1">
                                        </td>
                                        <td style="height: 126px" colspan="2">
                                            <asp:Panel ID="Panel5" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="chklCustomerType" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 30px">
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="Panel8" runat="server" Width="300px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ChbVolumClass" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td style="width: 16px">
                                        </td>
                                        <td style="width: 170px">
                                            <asp:RadioButton ID="rBtnBasketPromotion" runat="server" Width="121px" Font-Size="8pt"
                                                Text="Basket Promotion" AutoPostBack="True" Visible="False"></asp:RadioButton><br />
                                            <asp:RadioButton ID="rBtnSlabPromotion" runat="server" Width="114px" Font-Size="8pt"
                                                Text="Slab Promotion" AutoPostBack="True" Checked="True"></asp:RadioButton>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="90px"
                                    Text="Cancel" ValidationGroup="vg" CausesValidation="False" CssClass="Button" />
                                &nbsp;
                                <asp:Button ID="btnBack" OnClick="btnBack_Click" runat="server" Width="90" Text="Back"
                                    ValidationGroup="vg" CssClass="Button" />
                                <asp:Button ID="btnNext" OnClick="btnNext_Click" runat="server" Width="90" Text="Next"
                                    ValidationGroup="vg" CssClass="Button" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
