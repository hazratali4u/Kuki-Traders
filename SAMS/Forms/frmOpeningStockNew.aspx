<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOpeningStockNew.aspx.cs" Inherits="Forms_frmOpeningStockNew" Title="SAMS :: Stock Adjustment" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">

          function pageLoad() {

              var ddlBatch = $('#<%=ddlBatch.ClientID %>');
              $('#<%=ddlBatch.ClientID %> option').remove();
              var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
              for (var i = 0; i < obj.length; i++) {
                  var item = obj[i];
                  if (item.SKU_ID == document.getElementById("<%=hfSKUID.ClientID %>").value) {

                      var opts = $("<option />");
                      opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                      ddlBatch.append(opts);
                  }
              }


              $('#<%=ddlBatch.ClientID %>').change(function () {
                  var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
                  for (var i = 0; i < obj.length; i++) {
                      var item = obj[i];
                      if (item.SKU_ID == $('#<%=ddlBatch.ClientID %> option:selected').val() && item.BATCH_NO == $('#<%=ddlBatch.ClientID %> option:selected').text()) {
                          document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                          break;
                      }
                  }

                  document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
              });
          }

 function ConfirmDelete()
 {
	    if( confirm("Do you want to Cancel this record?")==true)
		return true;
		
	    else
	    {
	    return false;
	    }
 }
    
 function ValidateForm()
 {
		var str;
    
		str  = document.getElementById('<%=txtskuName.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Select SKU Name');
			return false;
		}
		str  = document.getElementById('<%=txtQuantity.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Enter Quantity');
			return false;
		}
		
        return true;
 }
 function SearchList()
 {
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuCode.ClientID %>');

            if(tb.value == "")
            {
                ClearSelection(l);
            }
            else
            {
                for (var i=0; i < l.options.length; i++)
                {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase()))
                    {
                        l.options[i].selected = true;
                        return false;
                    }
                    else
                    {
                        ClearSelection(l);
                    }
                }
           }
 }
 function SearchedCode() {
     var stroption = document.getElementById("<%= txtskuCode.ClientID %>").value;
     var l = document.getElementById('<%= lstCode.ClientID %>');
     var str;
     var str2;
     for (var i = 0; i < l.options.length; i++) {
         if (l.options[i].selected) {
             str = l.options[i].value;
             str2 = l.options[i].text;
             ClearSelection(l);
             break;
         }
         else {
             str = "";
             str2 = "";
         }
     }

     if (str.length > 0) {

         document.getElementById("<%= txtskuCode.ClientID %>").value = str2.substring(0, str2.indexOf('-'));
         document.getElementById("<%= txtskuName.ClientID %>").value = str2.substring(str2.indexOf('-') + 1, str2.indexOf(':'));
         document.getElementById("<%=hfSKUID.ClientID %>").value = str;
         document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
     }
     else if (stroption.length == 0) {
         document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
         document.getElementById("<%= lstCode.ClientID %>").focus();
     }

     ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));
    
 }
 function SelectCode(e)
 {
      if(e.keyCode == 13)
      {
          var str = document.getElementById("<%=lstCode.ClientID %>");
          var str2 = str.options[str.selectedIndex].text;
          document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;

          document.getElementById("<%= txtskuCode.ClientID %>").value = str2.substring(0, str2.indexOf('-'));
          document.getElementById("<%= txtskuName.ClientID %>").value = str2.substring(str2.indexOf('-') + 1, str2.indexOf(':'));
          document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";


          var ddlBatch = $('#<%=ddlBatch.ClientID %>');
          $('#<%=ddlBatch.ClientID %> option').remove();
          var obj = jQuery.parseJSON($("#<%= hfBatchNo.ClientID %>").val());
          for (var i = 0; i < obj.length; i++) {
              var item = obj[i];
              if (item.SKU_ID == document.getElementById("<%=hfSKUID.ClientID %>").value) {

                  var opts = $("<option />");
                  opts = $("<option />").html(item.BATCH_NO).attr("value", item.SKU_ID);
                  ddlBatch.append(opts);
              }
          }

          for (var i = 0; i < obj.length; i++) {
              var item = obj[i];
              if (item.SKU_ID == $('#<%=ddlBatch.ClientID %> option:selected').val() && item.BATCH_NO == $('#<%=ddlBatch.ClientID %> option:selected').text()) {
                  document.getElementById("<%= txtExpDate.ClientID %>").value = item.EXPIRY_DATE;
                  break;
              }
          }

          document.getElementById("<%= hfBatchValue.ClientID%>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
          document.getElementById("<%= ddlBatch.ClientID %>").focus();
        
      }
 }
 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
  <div id="right_data">
    <div >
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<table>
<tbody><tr><td align=left colSpan=2>
<asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> </td>
<td align=left colSpan=1>&nbsp; &nbsp; &nbsp; &nbsp; </td>
<td style="WIDTH: 316px" vAlign=middle align=center colSpan=1 rowSpan=6>
<strong><asp:Label id="Label4" runat="server" Width="239px" Height="8px"></asp:Label></strong> &nbsp; &nbsp;&nbsp; 
<asp:Panel id="Panel1" runat="server" Width="250px" Height="170px" CssClass="HidePanel" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="Silver">
            <strong><asp:Label ID="Label3" runat="server" CssClass="lblbox" Width="170px">Select from SKU List</asp:Label></strong><br />
        <asp:ListBox ID="lstCode" runat="server" Height="154px" onkeyup="SelectCode(event)"
                SelectionMode="Multiple" Width="245px"></asp:ListBox></asp:Panel> 
                </td>
                </tr><tr><td style="HEIGHT: 25px" align=left>
                <strong><asp:Label id="Label2" runat="server" Width="98px" Height="14px" Text="Transaction Type" CssClass="lblbox"></asp:Label></strong></td><td style="HEIGHT: 25px">
                <asp:DropDownList id="DrpDocumentType" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                <asp:ListItem Value="7">Opening Stock</asp:ListItem>
    <asp:ListItem Value="8">Short</asp:ListItem>
    <asp:ListItem Value="9">Execes</asp:ListItem>
</asp:DropDownList></td><td style="HEIGHT: 25px">
<strong><asp:Label id="Label5" runat="server" Width="50px" CssClass="lblbox"></asp:Label></strong></td></tr><tr><td style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lblDocumentNo" runat="server" Width="94px" Text="Document No" CssClass="lblbox"></asp:Label></strong></td><td style="HEIGHT: 25px"><asp:DropDownList id="drpDocumentNo" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"></asp:DropDownList></td><td style="HEIGHT: 25px"></td></tr><tr><td style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lbltoLocation" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td><td style="HEIGHT: 25px"><asp:DropDownList id="drpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"></asp:DropDownList></td><td style="HEIGHT: 25px"></td></tr><tr><td style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></td><td style="HEIGHT: 25px"><asp:DropDownList id="drpDistributor" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"></asp:DropDownList></td><td style="HEIGHT: 25px"></td></tr><tr><td style="HEIGHT: 25px" align=left>
<strong><asp:Label id="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong></td><td style="HEIGHT: 25px"><asp:TextBox id="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox></td>
<td style="HEIGHT: 25px">
<asp:HiddenField ID="hfSKUID" runat="server"/>
    <asp:HiddenField ID="hfBatchNo" runat="server"/>
    <asp:HiddenField ID="hfBatchValue" runat="server"/>
</td></tr></tbody>
</table>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
           </div>
    <div >
    <table width="100%">
        <tr>
            <td >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<table>
<tbody>
<tr>
<td>
<asp:Label id="lblskuCode" runat="server" Width="87px" Height="16px" ForeColor="White" Font-Bold="True" Text="  SKU Code"  BackColor="#006699"></asp:Label></td>
<td style="WIDTH: 100px"><asp:Label id="lblskuname" runat="server" Width="206px" Height="16px" ForeColor="White" Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
<td><asp:Label id="lblBatchNo" runat="server" Width="82px" Height="16px" ForeColor="White" Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label></td>
<td><asp:Label id="lblquantity" runat="server" Width="76px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
<td><asp:Label id="lblFreeSKU" runat="server" Width="100px" Height="16px" ForeColor="White" Font-Bold="True" Text="Exp Date" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label></td>
<td style="WIDTH: 100px"><asp:Label id="Label41" runat="server" Width="92px" Height="16px" ForeColor="White" Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
</tr>
<tr>
<td><asp:TextBox id="txtskuCode" onkeyup="SearchList()" runat="server" Width="82px" CssClass="txtBox"></asp:TextBox></td>
<td style="WIDTH: 100px"><asp:TextBox id="txtskuName" runat="server" Width="200px" Font-Bold="True" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
<TD><asp:DropDownList ID="ddlBatch" runat="server" Width="56px" onfocus="SearchedCode()"></asp:DropDownList></TD>
<td><asp:TextBox id="txtQuantity" onfocus="SearchedCode()" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox></td>
<td>
    <asp:TextBox id="txtExpDate" runat="server" Width="75px" Enabled="false"></asp:TextBox>
</td>
<td style="WIDTH: 100px"><asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Width="95px" Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" /> </td>
</tr>
<tr>
<td align=left colSpan=6>
<asp:Panel id="Panel2" runat="server" Width="665px" Height="140px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
<asp:GridView id="GrdPurchase" runat="server" Width="645px" ForeColor="SteelBlue" BorderColor="White" BackColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" HorizontalAlign="Center" AutoGenerateColumns="False">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous" />
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                        Width="85px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                        Width="205px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BATCH_NO" HeaderText="BatchNo">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                        Width="75px" />
                                                </asp:BoundField>                                                                                                
                                                <asp:BoundField DataField="TIME_STAMP" HeaderText="Exp Date"  DataFormatString="{0:dd-MMM-yyyy}">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                        Width="75px" />
                                                </asp:BoundField>                                                
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" />
                                            <PagerStyle BackColor="Transparent" />
                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView> </asp:Panel> </td></tr></tbody></table>
                                        <asp:Button accessKey="S" id="btnSaveDocument" onclick="btnSaveDocument_Click" runat="server" Width="119px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False" CssClass="Button" /> 
                                        <asp:Button accessKey="C" id="btnCancel" onclick="btnCancel_Click" runat="server" Width="120px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button" /> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
        &nbsp;
    </div>
    </div>
   
</asp:Content>

