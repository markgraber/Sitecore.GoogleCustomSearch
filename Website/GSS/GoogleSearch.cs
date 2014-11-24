using System;
using System.Configuration;
using System.Linq;
using System.Net;

namespace GSS
{
    public class GoogleSearch
    {
        private Sitecore.Data.Items.Item _gssSiteItem = null;
        private Sitecore.Data.Items.Item GssSiteItem
        {
            get
            {
                if (_gssSiteItem == null)
                {
                    _gssSiteItem = GSS.GoogleUtil.GetSiteItem();
                }

                return _gssSiteItem;
            }
        }
        
        public GoogleSearch() {
            this.Num = 10;
            this.Start = 1;
            this.SiteSearchFilter = GoogleData.SiteSearchFilter.i;
        }

        private string[] _keyArray = null;
        private int _keyIndex = 0;
        private string _key = null;
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    try
                    {
                        _key = GssSiteItem.Fields[Constants.FieldIds.GoogleSearchApiKey].Value;

                        _keyArray = _key.Split(new char[] { ',' });
                        if (_keyArray.Length > 1)
                        {
                            _key = _keyArray[_keyIndex].Trim();
                        }
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("Failed to get Google API Key", ex, this);
                        return null;
                    }
                }

                return _key;
            }
        }

        private string _cx = null;

        /// <summary>
        /// The custom search engine ID to use for this request.
        /// If both cx and cref are specified, the cx value is used.
        /// </summary>
        public string CX
        {
            get
            {
                if (_cx == null)
                {
                    try
                    {
                        _cx = GssSiteItem.Fields[Constants.FieldIds.GoogleSearchIdCx].Value;
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("Failed to get Google Search ID CX", ex, this);
                    }
                }

                return _cx;
            }
        }

        /// <summary>
        /// Number of search results to return.
        /// Valid values are integers between 1 and 10, inclusive.
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// The index of the first result to return.
        /// </summary>
        public int Start { get; set; }
        
        /// <summary>
        /// Specifies all search results should be pages from a given site.
        /// </summary>
        public string SiteSearch { get; set; }

        /// <summary>
        /// Controls whether to include or exclude results from the site named in the siteSearch parameter.
        /// Acceptable values are:
        /// "e": exclude
        /// "i": include
        /// </summary>
        public GoogleData.SiteSearchFilter SiteSearchFilter { get; set; }

        private GoogleData.SafeLevel? _safeLevel = null;

        /// <summary>
        /// Search safety level. 
        /// Acceptable values are:
        /// "high": Enables highest level of SafeSearch filtering.
        /// "medium": Enables moderate SafeSearch filtering.
        /// "off": Disables SafeSearch filtering. (default)
        /// </summary>
        public GoogleData.SafeLevel SafeLevel
        {
            get
            {
                try
                {
                    if (_safeLevel == null)
                    {
                        string itemId = GssSiteItem.Fields[Constants.FieldIds.GoogleSearchSafeLevel].Value;
                        switch (itemId)
                        {
                            case Constants.ItemsIds.SafeLevel.Off:
                                _safeLevel = GoogleData.SafeLevel.off;
                                break;
                            case Constants.ItemsIds.SafeLevel.Medium:
                                _safeLevel = GoogleData.SafeLevel.medium;
                                break;
                            case Constants.ItemsIds.SafeLevel.High:
                                _safeLevel = GoogleData.SafeLevel.high;
                                break;
                        }
                    }

                    return (GoogleData.SafeLevel)_safeLevel;
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Failed to get Google Search SafeLevel", ex, this);
                    return GoogleData.SafeLevel.off;
                }
            }
        }

        public string JsonDataResponse;

        public GoogleData.Response Search(string searchString, string siteSearch, GoogleData.SiteSearchFilter siteSearchFilter, int startIndex) {
            this.SiteSearch = siteSearch;
            this.SiteSearchFilter = siteSearchFilter;
            this.Start = startIndex;

            return Search(searchString);
        }

        public GoogleData.Response Search(string searchString, string siteSearch, int startIndex) {
            this.SiteSearch = siteSearch;
            this.Start = startIndex;

            return Search(searchString);
        }

        public GoogleData.Response Search(string searchString, int startIndex) {
            this.Start = startIndex;

            return Search(searchString);
        }

        public GoogleData.Response Search(string searchString) {
            // Check Parameters
            if (string.IsNullOrWhiteSpace(this.Key)) {
                throw new Exception("Google Search 'Key' cannot be null");
            }
            if (string.IsNullOrWhiteSpace(this.CX)) {
                throw new Exception("Google Search 'CX' cannot be null");
            }
            if (string.IsNullOrWhiteSpace(searchString)) {
                throw new ArgumentNullException("search");
            }
            if (this.Num < 0 || this.Num > 10) {
                throw new ArgumentNullException("Num must be between 1 and 10");
            }
            if (this.Start < 1 || this.Start > 100) {
                throw new ArgumentNullException("Start must be between 1 and 100");
            }

            // Build Query
            string query = string.Empty;
            query += string.Format("q={0}", searchString);
            query += string.Format("&key={0}", this.Key);
            query += string.Format("&cx={0}", this.CX);
            query += string.Format("&safe={0}", this.SafeLevel.ToString());
            query += string.Format("&alt={0}", "json");
            query += string.Format("&num={0}", this.Num);
            query += string.Format("&start={0}", this.Start);
            query += string.Format("&siteSearch={0}", this.SiteSearch);
            query += string.Format("&siteSearchFilter={0}", this.SiteSearchFilter.ToString());

            // Construct URL
            UriBuilder uriBuilder = new UriBuilder()
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "www.googleapis.com",
                Path = "customsearch/v1",
                Query = query
            };

            // Submit Request
            string url = uriBuilder.ToString();
            GoogleData.Response results = null;
            try {
                results = JsonHelper.DownloadAndSerializeJsonData<GoogleData.Response>(url, out JsonDataResponse);
            }
            catch (WebException ex) {
                // Try Another API Key
                if (_keyArray != null && _keyIndex + 1 < _keyArray.Length) {
                    _keyIndex++;
                    _key = _keyArray[_keyIndex];

                    return Search(searchString);
                }
                throw new WebException(ex.Message);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return results;
        }
    }
}
