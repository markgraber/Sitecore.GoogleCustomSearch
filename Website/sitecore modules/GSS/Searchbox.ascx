<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Searchbox.ascx.cs" Inherits="GSS.Searchbox" %>

<asp:Panel id="pnlGSS" runat="server">
    <gcse:searchbox-only resultsUrl="/GSSSearch.aspx" newWindow="false" queryParameterName="s">
</asp:Panel>