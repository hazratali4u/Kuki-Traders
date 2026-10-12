<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptGeoHierarchy.aspx.cs" Inherits="Forms_RptGeoHierarchy" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }
       
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                           
                                <table>
                                    <tbody>
                                      <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            Zone</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="drpZone" runat="server" Width="250px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpZone_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                           <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            Region</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="drpRegion" runat="server" Width="250px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            District</strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="drpDistrict" runat="server" Width="250px" CssClass="DropList"
                                            >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                    </tbody>
                                </table>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" Width="90" CssClass="Button"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" Width="90" CssClass="Button"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>



