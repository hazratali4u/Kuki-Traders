<%@ Page Language="C#" MasterPageFile="~/Forms/AppMaster.master" AutoEventWireup="true" CodeFile="frmTest.aspx.cs" Inherits="Forms_frmTest" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainCopy" Runat="Server">

    <div class="container" style="background-color: white">
    
        <h2>
            &nbsp; Test
        </h2>
        <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>
        <ajaxtoolkit:autocompleteextender ID="aceProduct" runat="server" 
                TargetControlID="txtTest" ServicePath="wsAccountHead.asmx"      
                MinimumPrefixLength="1" CompletionInterval="500" CompletionSetCount="10" 
                CompletionListCssClass="autocomplete_completionListElement"
                  CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true"
                  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                FirstRowSelected="true"
                ServiceMethod="GetAccountHead"></ajaxtoolkit:autocompleteextender>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>     
    
    </asp:Content>

