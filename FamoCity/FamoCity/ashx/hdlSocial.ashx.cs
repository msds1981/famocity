using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamoLibrary;
namespace FamoCity
{
    /// <summary>
    /// Summary description for hdlSocial
    /// </summary>
    public class hdlSocial : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        FamoPassport pass;
        public void ProcessRequest(HttpContext context)
        {
            pass = (FamoPassport)context.Session["passport"];

            int userid = 0;
            int topid = 0;
            string action = "";
            if (context.Request.QueryString["action"] != null)
            {
                action = context.Request.QueryString["action"].ToString();
            }

            if (context.Request.QueryString["userid"] != null)
            {
                userid = Convert.ToInt32(context.Request.QueryString["userid"]);
            }

            if (context.Request.QueryString["topid"] != null)
            {
                topid = Convert.ToInt32(context.Request.QueryString["topid"]);
            }

            string output = "";
            if (action == "loadpage")
            {
                //تحميل صفحة مستخدم او شركة
                output = LoadPage(userid);
            }
            else if (action == "loadtopic")
            {
                //تحميل مقالة واحدة في نافذة البوب اب
                output = "<div class='tpc-view-photo-left'>" +
                        "<a href='javascript:void(0);' onclick='Display(2);' tpcid='2'><img src='newfiles/images/aa11.jpg' class='tpc-media-img-view' /></a></div>";//LoadTopic(topid);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(output);
        }

        private string LoadPage(int userid)
        {

            return ClassMain.BuildUserPage(pass,FamoBlock.enmObjectCode.Users, userid, ClassMain.PageName.Arena,0);
        }

        //Load topic to the popup in main page
        private string LoadTopic(int topicid)
        {
            return ClassMain.BuildTopicPopup(pass, new FamTopic(pass, topicid));
        }

        //المقالات الجديدة
        private string NewTopics(int lastid)
        {

            return "";
        }

        //المقالات القديمة
        private string OldTopics(int firstid)
        {

            return "";
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