using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSS
{
    public static class GoogleUtil
    {
        public static Sitecore.Data.Items.Item GetSiteItem()
        {
            var items = Sitecore.Context.Database.GetItem(Constants.ItemsIds.GSS).Children;
            return items.Where(i => i.TemplateID.ToString().Equals(Constants.TemplateIds.GSSSite) &&
                i.Fields[Constants.FieldIds.SiteName].Value.ToLower().Equals(Sitecore.Context.Site.Name.ToLower())).FirstOrDefault();
        }

        /// <summary>
        /// Returns Sitecore Item from Context Database
        /// </summary>
        /// <returns>Sitecore Item from Context Database</returns>
        public static Sitecore.Data.Items.Item GetSitecoreItem(GSS.GoogleData.Item gssItem)
        {
            Sitecore.Data.Items.Item item = null;

            try
            {
                Sitecore.Data.Items.Item gssSiteItem = GSS.GoogleUtil.GetSiteItem();
                string websiteProtocolAndHostname = gssSiteItem.Fields[Constants.FieldIds.WebsiteProtocolAndHostname].Value;

                // Get full url from Google
                string linkUrl = gssItem.Link;

                // Remove each language folder from URL
                Sitecore.Data.Items.Item languagesRootItem = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.LanguageRoot);
                foreach (Sitecore.Data.Items.Item langItem in languagesRootItem.Children)
                {
                    string strIso = langItem.Fields[Sitecore.FieldIDs.LanguageIso].Value;
                    linkUrl = linkUrl.Replace(string.Format("{0}/{1}", websiteProtocolAndHostname, strIso), "");
                }

                // Remove protocol and hostname if not already replaced by language folder.
                // Remove .aspx and replace %20 with space
                linkUrl = linkUrl.Replace(websiteProtocolAndHostname, "").Replace(".aspx", "").Replace("%20", " ");

                // Get the Sitecore Item from the path
                string sitecorePath = Sitecore.Context.Site.StartPath + linkUrl;
                item = Sitecore.Context.Database.GetItem(sitecorePath);
                if (item == null)
                {
                    item = Sitecore.Context.Database.GetItem(sitecorePath.Replace("-", " "));
                }
                if (item == null)
                {
                    item = Sitecore.Context.Database.GetItem(sitecorePath.Replace(" ", "-"));
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GetSitecoreItem failed", ex, new object());
            }

            return item;
        }
    }
}