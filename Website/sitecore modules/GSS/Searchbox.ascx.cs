using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Sitecore.Web.UI.WebControls;

namespace GSS
{
    public partial class Searchbox : System.Web.UI.UserControl
    {
        private SublayoutParameterHelper _subHelper = null;
        private SublayoutParameterHelper SubHelper
        {
            get
            {
                if (_subHelper == null)
                {
                    _subHelper = new SublayoutParameterHelper(this, false);
                }

                return _subHelper;
            }
        }

        private string CX
        {
            get
            {
                GoogleSearch gs = new GoogleSearch();
                return gs.CX;
            }
        }

        private string Background
        {
            get
            {
                return SubHelper.GetParameter("Background");
            }
        }

        private string CssClass
        {
            get
            {
                return SubHelper.GetParameter("CssClass");
            }
        }

        private void RenderHeadControls()
        {
            HtmlLink linkGssCss = (HtmlLink)Page.Header.FindControl("linkGssCss");
            if (linkGssCss == null)
            {
                HtmlLink link = new HtmlLink();
                link.ID = "linkGssCss";
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("href", "/sitecore modules/GSS/GSS.css?" + Guid.NewGuid().ToString());

                // Add to page head tag
                Page.Header.Controls.Add(link);
            }
        }

        private void RenderBodyControls()
        {
            pnlGSS.CssClass = "gss";
            
            string strScript = @"(function () { 
                    var cx = '" + CX + @"'; 
                    var gcse = document.createElement('script'); 
                    gcse.type = 'text/javascript'; 
                    gcse.async = true; 
                    gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') + '//www.google.com/cse/cse.js?cx=' + cx; 
                    var s = document.getElementsByTagName('script')[0]; 
                    s.parentNode.insertBefore(gcse, s); 
                })(); 
                $(window).load(function () { 
                    setTimeout(function () { 
                        var background = '" + Background + @"'; 
                        if (background != '') { 
                            $('#" + pnlGSS.ClientID + @" input.gsc-input').css('background', background); 
                        } 
                        var cssClass = '" + CssClass + @"'; 
                        if (cssClass != '') { 
                            $('#" + pnlGSS.ClientID + @" input.gsc-input').addClass(cssClass); 
                        } 
                        $('#" + pnlGSS.ClientID + @" input.gsc-input').css('display', 'block'); 
                    }, 100); 
                });";

            HtmlGenericControl scriptTag = new HtmlGenericControl("script");
            scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.InnerHtml = strScript;
            pnlGSS.Controls.AddAt(0, scriptTag);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RenderHeadControls();
            RenderBodyControls();

            //HtmlGenericControl scriptGss = (HtmlGenericControl)Page.Header.FindControl("scriptGss");
            //if (scriptGss == null)
            //{
            //    HtmlGenericControl script = new HtmlGenericControl("script");
            //    script.ID = "scriptGss";
            //    script.Attributes.Add("type", "text/javascript");
            //    script.Attributes.Add("src", "/sitecore modules/GSS/GSS.js?" + Guid.NewGuid().ToString());

            //    // Add to bottom of page body
            //    Page.Header.Controls.Add(script);
            //}
        }
    }
}