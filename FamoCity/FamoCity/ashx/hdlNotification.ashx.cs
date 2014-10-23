using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamoLibrary;

namespace FamoCity.ashx
{
    /// <summary>
    /// Summary description for hdlNotification
    /// </summary>
    public class hdlNotification : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}