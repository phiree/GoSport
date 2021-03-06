﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Reflection;
namespace PB.Library
{
   public class HttpContextBuilder
    {
       public static HttpContext CreateHttpContext()
       {
           return CreateHttpContext("index.html", "http://bing.com", null);
       }
        private static HttpContext CreateHttpContext(string fileName, string url, string queryString)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var hres = new HttpResponse(sw);
            var hreq = new HttpRequest(fileName, url, queryString);
            var httpc = new HttpContext(hreq, hres);
            return httpc;
        }

        private static object RunInstanceMethod(object source, string method, object[] objParams)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var type = source.GetType();
            var m = type.GetMethod(method, flags);
            if (m == null)
            {
                throw new ArgumentException(string.Format("There is no method '{0}' for type '{1}'.", method, type));
            }

            var objRet = m.Invoke(source, objParams);
            return objRet;
        }

        public static void SetPrivateInstanceFieldValue(object source, string memberName, object value)
        {
            var field = source.GetType().GetField(memberName, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                throw new ArgumentException(string.Format("Could not find the private instance field '{0}'", memberName));
            }

            field.SetValue(source, value);
        }
    }
}
