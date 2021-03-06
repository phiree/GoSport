using System;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Web.UI.WebControls;
[assembly: CLSCompliant(true)]

namespace PB.Library
{
    public class WebResourceControl : Literal
    {

        private string recName;

        public string RecName
        {
            get { return recName; }
            set { recName = value; }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Text = WebResourceManager.GetString(recName);
        }
    }

    public sealed class WebResourceManager
    {
        static HttpContext Context {
            get {
                if (HttpContext.Current == null)
                {
                    return HttpContextBuilder.CreateHttpContext();
                }
                else {
                    return HttpContext.Current;
                }
            }
        }
        private WebResourceManager()
        {
            
        }
        enum ResourceManagerType
        {
            String,
            ErrorMessage,
            Template
        }

        public static string GetString(string name)
        {

            Hashtable resources = null;
            //HttpContext context = HttpContext.Current;
            //string userLanguage = cSContext.User.Profile.Language;

            string language = ConfigurationManager.AppSettings["Language"];

            if (string.IsNullOrEmpty(language))
                language = "zh-CN";
                resources = GetResource(ResourceManagerType.String, language, "Resources.xml");

            string text = resources[name] as string;

            //try the standard file if we passed a file that didnt have the key we were looking for
           
            if (text == null)
            {

                            text = string.Empty;
            }
            return text;
        }

        #region GetMessage
       //deleted by yuanfei
        #endregion

        private static Hashtable GetResource(ResourceManagerType resourceType, string userLanguage, string fileName)
        {


          
            string cacheKey = resourceType.ToString()  + userLanguage + fileName;

            // Ensure the user has a language set
            //

            // Attempt to get the resources from the Cache

            Hashtable resources = Context.Cache.Get(cacheKey) as Hashtable;

            if (resources == null)
            {
                resources = new Hashtable();

                // First load the English resouce, changed from loading the default language
                // since the userLanguage is set to the defaultLanguage if the userLanguage
                // is unassigned. We load the english language always just to ensure we have
                // a resource loaded just incase the userLanguage doesn't have a translated
                // string for this English resource.
                //
                    resources = LoadResource(resourceType, resources, userLanguage, cacheKey, fileName);

            }

            return resources;
        }

        private static string GetCurrentVPath()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_POST"] + (HttpContext.Current.Request.ApplicationPath.EndsWith("/") ? HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/");
        }

        private static Hashtable LoadResource(ResourceManagerType resourceType, Hashtable target, string language, string cacheKey, string fileName)
        {
            string filePath =Environment.CurrentDirectory +"\\Languages\\" + language + "\\" + fileName;

            //			switch (resourceType) {
            //				case ResourceManagerType.ErrorMessage:
            //					filePath = string.Format(filePath, "Messages.xml");
            //					break;
            //
            //				default:
            //					filePath = string.Format(filePath, "Resources.xml");
            //					break;
            //			}

            CacheDependency dp;
            XmlDocument d = new XmlDocument();

            try
            {
                dp = new CacheDependency(filePath);
                d.Load(filePath);
            }
            catch
            {
                return target;
            }

            foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    switch (resourceType)
                    {
                        case ResourceManagerType.ErrorMessage:
                           
                            break;

                        case ResourceManagerType.String:
                            if (target[n.Attributes["name"].Value] == null)
                                target.Add(n.Attributes["name"].Value, n.InnerText);
                            else
                                target[n.Attributes["name"].Value] = n.InnerText;
                            break;

                        case ResourceManagerType.Template:
                           
                            break;
                    }
                }
            }

            // Create a new cache dependency and set it to never expire
            // unless the underlying file changes
            //
            // 7/26/2004 Terry Denham
            // We should only keep the default language cached forever, not every language.
            //DateTime cacheUntil;
          
               Context.Cache.Insert(cacheKey, target, dp);
           

            return target;

        }

    }
}
