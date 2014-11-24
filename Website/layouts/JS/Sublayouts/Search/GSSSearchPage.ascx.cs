namespace Jetstream.Website.layouts.JS.Sublayouts.GSSSearch
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Web.UI.HtmlControls;
  using System.Web.UI.WebControls;
  using Jetstream.Business.Data;
  using Jetstream.Search;
  using Jetstream.Search.SearchParams;
  using Lucene.Net.Documents;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Globalization;
  using Sitecore.Links;
  using Sitecore.Search;
  using Sitecore.Web;

  public partial class GSSSearchPage : Jetstream.Website.layouts.JS.Controls.BaseSublayout
  {

    private string searchTerm;

    public string SearchTerm
    {
      get
      {
        return this.searchTerm ?? (this.searchTerm = WebUtil.GetQueryString("s"));
      }
    }

    
    private void Page_Load(object sender, EventArgs e)
    {
      // Put user code to initialize the page here
    }

    protected void SearchLV_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
      if (e.Item.ItemType == ListViewItemType.DataItem)
      {
        var hl1 = e.Item.FindControl("HL1") as HyperLink;
        var hl2 = e.Item.FindControl("HL2") as HyperLink;
        var descriptionP = e.Item.FindControl("Description") as HtmlGenericControl;


        var hit = e.Item.DataItem as GSS.GoogleData.Item;
        if (hit != null)
        {

          var item = GSS.GoogleUtil.GetSitecoreItem(hit);
          if (item != null)
          {
            if (hl1 != null)
            {
              hl1.NavigateUrl = LinkManager.GetItemUrl(item);
              hl1.Text = hit.HtmlTitle;
              if (hl2 != null)
              {
                hl2.NavigateUrl = hl1.NavigateUrl;
                hl2.Text = hl1.NavigateUrl;
              }
            }





            descriptionP.InnerHtml = hit.HtmlSnippet;
          }
        }
      }
    }

    protected string GetField(Document document, string fieldName)
    {
      if (document != null)
      {
        var field = document.GetField(fieldName);
        if (field != null)
        {
          return field.StringValue();
        }
      }
      return string.Empty;
    }

    protected void SearchLV_DataBound(object sender, EventArgs e)
    {
      var lv = sender as ListView;
      var pager = WebUtil.FindControl(lv, "Pager") as DataPager;
      var h3 = WebUtil.FindControl(lv, "SummaryLit") as HtmlGenericControl;
      if (h3 != null && pager != null)
      {
        int min = pager.StartRowIndex + 1;
        int max = pager.StartRowIndex + lv.Items.Count;
        int total = pager.TotalRowCount;

        h3.InnerText = Translate.Text("{0}-{1} of {2} results for search term '{3}'", min, max, total, SearchTerm);

        int pages = (pager.TotalRowCount / pager.PageSize) + 1;
        int page = (pager.StartRowIndex / pager.PageSize) + 1;

        if (pager.Fields.Count == 3)
        {
          var prevFirst = pager.Fields[0] as NextPreviousPagerField;
          var nextLast = pager.Fields[2] as NextPreviousPagerField;

          if (prevFirst != null && nextLast != null)
          {
            prevFirst.ShowFirstPageButton = page != 1;
            prevFirst.ShowPreviousPageButton = page != 1;

            prevFirst.PreviousPageText = Translate.Text("Prev");
            prevFirst.FirstPageText = Translate.Text("First");

            nextLast.ShowLastPageButton = page < pages;
            nextLast.ShowNextPageButton = page < pages;

            nextLast.NextPageText = Translate.Text("Next");
            nextLast.LastPageText = Translate.Text("Last");
          }
        }
      }
    }
  }

  [DataObject(true)]
  public class WebsiteSearchDataAdapter
  {
    private GSS.GoogleSearch searcher = null;

    public GSS.GoogleSearch Searcher
    {
      get
      {
        if (searcher == null)
        {
          searcher = new GSS.GoogleSearch();
        }

        return searcher;
      }
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public IEnumerable<GSS.GoogleData.Item> GetResults(string searchQuery, int maximumRows, int startRowIndex)
    {
      var searchParams = new WebsiteSearchParams {
        SearchTerm = searchQuery,
        Language = Context.Language
      };

      return Searcher.Search(searchParams.SearchTerm).Items;
    }

    public int GetCount(string searchQuery)
    {
      var searchParams = new WebsiteSearchParams {
        SearchTerm = searchQuery,
        Language = Context.Language
      };
      return Searcher.Search(searchParams.SearchTerm).Items.Count;
    }
  }
}