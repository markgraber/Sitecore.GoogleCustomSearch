using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System;

namespace GSS
{
    public class GoogleData
    {
        public enum SiteSearchFilter { e, i }
        public enum SafeLevel { off, medium, high }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "kind")]
            public string Kind { get; set; }

            [DataMember(Name = "url")]
            public Url Url { get; set; }

            [DataMember(Name = "queries")]
            public Queries Queries { get; set; }

            [DataMember(Name = "promotions")]
            public List<Promotion> Promotions { get; set; }

            [DataMember(Name = "context")]
            public Context Context { get; set; }

            [DataMember(Name = "searchInformation")]
            public SearchInformation SearchInformation { get; set; }

            [DataMember(Name = "spelling")]
            public Spelling Spelling { get; set; }

            [DataMember(Name = "items")]
            public List<Item> Items { get; set; }
        }

        [DataContract]
        public class Url
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "template")]
            public string Template { get; set; }
        }

        [DataContract]
        public class Queries
        {
            [DataMember(Name = "nextPage")]
            public List<Page> NextPage { get; set; }

            [DataMember(Name = "request")]
            public List<Page> Request { get; set; }
        }

        [DataContract]
        public class Promotion
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "htmlTitle")]
            public string HtmlTitle { get; set; }

            [DataMember(Name = "link")]
            public string Link { get; set; }

            [DataMember(Name = "displayLink")]
            public string DisplayLink { get; set; }

            [DataMember(Name = "bodyLines")]
            public List<BodyLine> BodyLines { get; set; }

            [DataMember(Name = "image")]
            public PromoImage Image { get; set; }
        }

        [DataContract]
        public class BodyLine
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "htmlTitle")]
            public string HtmlTitle { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }

            [DataMember(Name = "link")]
            public string Link { get; set; }
        }

        [DataContract]
        public class PromoImage
        {
            [DataMember(Name = "source")]
            public string Source { get; set; }

            [DataMember(Name = "width")]
            public int Width { get; set; }

            [DataMember(Name = "height")]
            public int Height { get; set; }
        }

        [DataContract]
        public class Page
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "totalResults")]
            public long Request { get; set; }

            [DataMember(Name = "searchTerms")]
            public string SearchTerms { get; set; }

            [DataMember(Name = "count")]
            public int Count { get; set; }

            [DataMember(Name = "startIndex")]
            public int StartIndex { get; set; }

            [DataMember(Name = "startPage")]
            public int StartPage { get; set; }

            [DataMember(Name = "language")]
            public string Language { get; set; }

            [DataMember(Name = "inputEncoding")]
            public string InputEncoding { get; set; }

            [DataMember(Name = "outputEncoding")]
            public string OutputEncoding { get; set; }

            [DataMember(Name = "safe")]
            public string Safe { get; set; }

            [DataMember(Name = "cx")]
            public string CX { get; set; }

            [DataMember(Name = "cref")]
            public string Cref { get; set; }

            [DataMember(Name = "sort")]
            public string Sort { get; set; }

            [DataMember(Name = "filter")]
            public string Filter { get; set; }

            [DataMember(Name = "gl")]
            public string GL { get; set; }

            [DataMember(Name = "cr")]
            public string CR { get; set; }
        }

        [DataContract]
        public class Context
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "facets")]
            public List<List<Facet>> Facets { get; set; }
        }

        [DataContract]
        public class Facet
        {
            [DataMember(Name = "label")]
            public string Label { get; set; }

            [DataMember(Name = "anchor")]
            public string Anchor { get; set; }

            [DataMember(Name = "label_with_op")]
            public string LabelWithOp { get; set; }
        }

        [DataContract]
        public class SearchInformation
        {
            [DataMember(Name = "searchTime")]
            public double searchTime { get; set; }

            [DataMember(Name = "formattedSearchTime")]
            public string formattedSearchTime { get; set; }

            [DataMember(Name = "totalResults")]
            public string totalResults { get; set; }

            [DataMember(Name = "formattedTotalResults")]
            public string formattedTotalResults { get; set; }
        }

        [DataContract]
        public class CseImage
        {
            [DataMember(Name = "src")]
            public string Src { get; set; }
        }

        [DataContract]
        public class CseThumbnail
        {
            [DataMember(Name = "width")]
            public string Width { get; set; }

            [DataMember(Name = "height")]
            public string Height { get; set; }

            [DataMember(Name = "src")]
            public string Src { get; set; }
        }

        [DataContract]
        public class Webpage
        {
            [DataMember(Name = "breadcrumb")]
            public string Breadcrumb { get; set; }
        }

        [DataContract]
        public class Metatag
        {
            [DataMember(Name = "viewport")]
            public string Viewport { get; set; }
        }

        [DataContract]
        public class Breadcrumb
        {
            [DataMember(Name = "url")]
            public string Url { get; set; }

            [DataMember(Name = "title")]
            public string Title { get; set; }
        }

        [DataContract]
        public class Pagemap
        {
            [DataMember(Name = "cse_image")]
            public List<CseImage> CseImage { get; set; }

            [DataMember(Name = "cse_thumbnail")]
            public List<CseThumbnail> CseThumbnail { get; set; }

            [DataMember(Name = "webpage")]
            public List<Webpage> Webpage { get; set; }

            [DataMember(Name = "metatags")]
            public List<Metatag> Metatags { get; set; }

            [DataMember(Name = "breadcrumb")]
            public List<Breadcrumb> Breadcrumb { get; set; }

        }

        [DataContract]
        public class Label
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "displayName")]
            public string DisplayName { get; set; }

            [DataMember(Name = "label_with_op")]
            public string LabelWithOp { get; set; }
        }

        [DataContract]
        public class Spelling
        {
            [DataMember(Name = "correctedQuery")]
            public string CorrectedQuery { get; set; }

            [DataMember(Name = "htmlCorrectedQuery")]
            public string HtmlCorrectedQuery { get; set; }
        }

        [DataContract]
        public class Item
        {
            [DataMember(Name = "kind")]
            public string Kind { get; set; }

            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "htmlTitle")]
            public string HtmlTitle { get; set; }

            [DataMember(Name = "link")]
            public string Link { get; set; }

            [DataMember(Name = "displayLink")]
            public string DisplayLink { get; set; }

            [DataMember(Name = "snippet")]
            public string Snippet { get; set; }

            [DataMember(Name = "htmlSnippet")]
            public string HtmlSnippet { get; set; }

            [DataMember(Name = "cacheId")]
            public string CacheId { get; set; }

            [DataMember(Name = "mime")]
            public string Mime { get; set; }

            [DataMember(Name = "fileFormat")]
            public string FileFormat { get; set; }

            [DataMember(Name = "formattedUrl")]
            public string FormattedUrl { get; set; }

            [DataMember(Name = "htmlFormattedUrl")]
            public string HtmlFormattedUrl { get; set; }

            [DataMember(Name = "pagemap")]
            public Pagemap Pagemap { get; set; }

            [DataMember(Name = "labels")]
            public List<Label> Labels { get; set; }

            [DataMember(Name = "image")]
            public ItemImage Image { get; set; }
        }

        [DataContract]
        public class ItemImage
        {
            [DataMember(Name = "contextLink")]
            public string ContextLink { get; set; }

            [DataMember(Name = "height")]
            public int Height { get; set; }

            [DataMember(Name = "width")]
            public int Width { get; set; }

            [DataMember(Name = "byteSize")]
            public int ByteSize { get; set; }

            [DataMember(Name = "thumbnailLink")]
            public string ThumbnailLink { get; set; }

            [DataMember(Name = "thumbnailHeight")]
            public int ThumbnailHeight { get; set; }

            [DataMember(Name = "thumbnailWidth")]
            public int ThumbnailWidth { get; set; }
        }
    }
}
