<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GSSSearchPage.ascx.cs"
    Inherits="Jetstream.Website.layouts.JS.Sublayouts.GSSSearch.GSSSearchPage" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<h1>
    <sc:Text ID="Title" runat="server" Field="Title" />
</h1>
<asp:ListView ID="SearchLV" runat="server" OnItemDataBound="SearchLV_ItemDataBound"
    DataSourceID="SearchDataSource" OnDataBound="SearchLV_DataBound">
    <LayoutTemplate>
        <h3 id="SummaryLit" runat="server">
        </h3>
        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        <div class="paginator">
            <p class="small right">
                <asp:DataPager ID="Pager" runat="server" PagedControlID="SearchLV" PageSize="10"
                    QueryStringField="page">
                    <Fields>
                        <asp:NextPreviousPagerField FirstPageText="First" PreviousPageText="Prev" ShowFirstPageButton="true"
                            ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true"  />
                        <asp:NumericPagerField ButtonType="Link" />
                        <asp:NextPreviousPagerField LastPageText="Last" NextPageText="Next" ShowFirstPageButton="false"
                            ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                    </Fields>
               </asp:DataPager>
            </p>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="mod result flush<%# Container.DataItemIndex == 0 ? " thintop":"" %><%# Container.DataItemIndex == SearchLV.Items.Count - 1 ? " last" : ""%>">
            <h4>
                <asp:HyperLink ID="HL1" runat="server"></asp:HyperLink></h4>
            <asp:HyperLink ID="HL2" runat="server" CssClass="modtxt"></asp:HyperLink>
            <p id="Description" runat="server" class="large top">
            </p>
        </div>
    </ItemTemplate>
    <EmptyDataTemplate>
        <div class="mod intro">
            <p class="large intro">
                <%= Sitecore.Globalization.Translate.Text("We're sorry, but your search for \"{0}\" returned no results.", SearchTerm) %></p>
            <a class="callout-large wssearchagain" href="#">
                <%= Sitecore.Globalization.Translate.Text("Please Search Again") %></a>
        </div>
    </EmptyDataTemplate>
</asp:ListView>
<asp:ObjectDataSource ID="SearchDataSource" runat="server" TypeName="Jetstream.Website.layouts.JS.Sublayouts.GSSSearch.WebsiteSearchDataAdapter"
    SelectMethod="GetResults" EnablePaging="true" SelectCountMethod="GetCount">
    <SelectParameters>
        <asp:QueryStringParameter QueryStringField="s" Name="searchQuery" DefaultValue=""
            Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
