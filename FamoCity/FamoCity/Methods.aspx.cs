using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using FamoLibrary;
using System.Data;
using System.Text;
using jQueryUploadTest;
using System.IO;
using mUtilities;
using System.Text.RegularExpressions;
using System.Diagnostics;
using FamoCity.PersonalService;
namespace FamoCity
{
    #region Structures
    public struct User
    {
        public int ID;
        public string Name;
        public string Email;
        public string PhotoPath;
        public string Country;
        public string Link;

    }
    public struct MessageData
    {
        public int ID;
        public int MsdID;
        public int Sender;
        public string SenderName;
        public string SenderPhotoPath;
        public string SenderUrl;
        public string Message;
        public string Subject;
        public int Reciver;
        public string ReciverName;
        public string ReciverPhotoPath;
        public string ReciverUrl;
        public string Time;
    }
    #endregion


    public partial class Methods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/login");
        }

        #region Public Methods


        [WebMethod]
        public static bool PassportIsValid()
        {
            FamoPassport Passport;
            if (HttpContext.Current.Session["passport"] != null)
            {
                Passport = (FamoPassport)HttpContext.Current.Session["passport"];
                if (!Passport.Logged)
                {
                    HttpContext.Current.Session["target"] = "/Shop/edit/Status";
                    return false;
                }
            }
            else
            {
                HttpContext.Current.Session["target"] = "/Shop/edit/Status";
                return false;
            }

            if (Passport.UserType != FamoBlock.enmUserType.Shop)
                return false;
            return true;
        }

        [WebMethod]
        public static string getEncryptedParameter(string parameter)
        {
            //return "done";
            return ClassMain.EncryptParameter(parameter);
        }

        [WebMethod]
        public static int SetSession(string username, string password)
        {
            //return "done";
            HttpContext.Current.Session["Email"] = username;
            HttpContext.Current.Session["Pass"] = ClassMain.EncryptParameter(password);
            return 1;
        }

        [WebMethod]
        public static bool SaveUserFirstName(string firstname, string lastname)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamUser u = new FamUser(pass, pass.UserID);
            u.FirstName = firstname;
            u.LastName = lastname;

            return u.Update();
        }

/*        [WebMethod]
        public static bool SaveUser(string User)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamUser fu = new FamUser(pass, pass.UserID);
            fu.UserName = User;
            return fu.Update();
        }
        */
        [WebMethod]
        public static bool SaveAddress(string address, int country, string city, int nationality, string bx)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamContactProfile fcp = new FamContactProfile(pass, pass.UserID);
            fcp.Address = address;
            fcp.Country.ID = country;
            fcp.City = city;
            fcp.Nationality.ID = nationality;
            fcp.PoBox = bx;
            return fcp.Update();
        }

        [WebMethod]
        public static bool SaveLang(int lang)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamUser fu = new FamUser(pass, pass.UserID);
            fu.LanguageID = lang;
            return fu.Update();
        }

        [WebMethod]
        public static bool SaveState(string State)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamContactProfile fcp = new FamContactProfile(pass, pass.UserID);
            fcp.State = State;
            return fcp.Update();
        }

        /*       [WebMethod]
               public static bool SaveBithDate(string day, string month, string year)
               {
                   FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
                   if (pass == null) return false;
                   if (pass.UserType != FamoBlock.enmUserType.User) return false;
                   FamUser fu = new FamUser(pass, pass.UserID);
                   fu.BirthDate = day + "/" + month + "/" + year;
                   return fu.Update();
               }
               */
        #endregion

        #region Sociality Methods
        [WebMethod]
        public static int LikeTopic(int objid)
        {
            //اضافة اعجاب على موضوع
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamVote v = new FamVote(pass);
            int like = 0;
            like=v.VoteUp(FamoBlock.enmObjectCode.Topics, objid);
            if (like > 0)
            {

                SaveNotification((int)FamoBlock.enmObjectCode.Votes, like,(int) FamoBlock.enmObjectCode.Topics, objid);
            }
            return like;
        }

        [WebMethod]
        public static int UnLikeTopic(int objid)
        {
            //اضافة عدم اعجاب على موضوع
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamVote v = new FamVote(pass);
            return v.VoteDown(FamoBlock.enmObjectCode.Topics, objid);

        }

        [WebMethod]
        public static string[] NoCommentTopic(int topid)
        {
            //منع/تفعيل التعليقات على المقالات
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            FamTopic v = new FamTopic(pass, topid);
            v.Nocomment = !v.Nocomment;
            string[] output = new string[] { "0", "" };

            string nc = "";
            //if worng update 
            if (v.Update()) nc = Convert.ToInt16(v.Nocomment).ToString();
            else nc = Convert.ToInt16(!v.Nocomment).ToString();

            output[0] = nc;
            output[1] = ClassMain.BuildInputCommentTags(pass, new FamTopic(pass, topid));
            return output;
        }

        //اضافة منتج الى المفضلة
        [WebMethod]
        public static int FavoriteProduct(int objid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FameFavorite fv = new FameFavorite(pass);
            fv.ObjectCode = FamoBlock.enmObjectCode.Products;
            fv.ObjectId = objid;
            fv.User.ID = pass.UserID;
            return fv.Save();
        }

        //الغاء منتج من المفضلة
        [WebMethod]
        public static int UnFavorite(int id)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FameFavorite fv = new FameFavorite(pass, id);
            return Convert.ToInt32(fv.Delete());
        }


        [WebMethod]
        public static int SaveComment(int objid, string desc)
        {
            //اضافة تعليق جديد
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamComment fc = new FamComment(pass);
            fc.ObjectCode = FamoBlock.enmObjectCode.Topics;
            fc.ObjectId = objid;
            fc.User.ID = pass.UserID;
            fc.Status = FamoBlock.enmBlockStatus.Block;
            fc.Description = desc;
            int commentid = fc.Save();
            if (commentid > 0)
                SaveNotification((int)FamoBlock.enmObjectCode.Comments, commentid, (int)FamoBlock.enmObjectCode.Topics, objid);

            return commentid;
        }

        //عدد الاعجاب لموضوع معين
        [WebMethod]
        public static int GetLikesCountOfTopic(int objid)
        {
            //Get Number Like
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamVote fm = new FamVote(pass);
            return fm.GetVoteUpCount((int)FamoBlock.enmObjectCode.Topics, objid);
        }

        //عدد عدم الاعجاب لموضوع معين
        [WebMethod]
        public static int GetUnLikesCountOfTopic(int objid)
        {
            //Get number Unlike
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamVote fm = new FamVote(pass);
            return fm.GetVoteDownCount((int)FamoBlock.enmObjectCode.Topics, objid);
        }

        //عرض اسماء الغير معجبين
        [WebMethod]
        public static string GetUserUnLikesCount(int objcode, int objid)
        {
            //Prev  Users Unlike
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";
            FamVote fm = new FamVote(pass);
            return fm.GetUserOfVoteDown(objcode, objid);
        }

        //عرض اسماء المعجبين بموضوع
        [WebMethod]
        public static string NamesUsersOfLikes(int objcode, int objid)
        {
            //Prev  Users like
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";
            FamVote fm = new FamVote(pass);
            return fm.GetUserOfVoteUp(objcode, objid);
        }

        [WebMethod]
        public static int getNumbersOfComments(int objcode, int objid)
        {
            //عدد التعليقات لموضوع
            //Get number Users Comments
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;

            FamComment fc = new FamComment(pass);
            return fc.GetNumbersOfComments(objcode, objid);

        }

        [WebMethod]
        public static int getCountCommentsOfTopic(int topid)
        {
            //عدد التعليقات لموضوع
            //Get number Users Comments
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;

            FamComment fc = new FamComment(pass);
            return fc.GetNumbersOfComments((int)FamoBlock.enmObjectCode.Topics, topid);

        }
        [WebMethod]
        public static string SeeMoreComments(int topid, int lastid)
        {
            //عرض المزيد من التعليقات على مقالة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";

            FamComment fc = new FamComment(pass);
            StringBuilder output = new StringBuilder();
            List<FamComment> lst = fc.GetLastCommentsList((int)FamoBlock.enmObjectCode.Topics, topid, lastid);
            int i = 1;
            foreach (FamComment comm in lst)
            {
                output.Append(ClassMain.BuildOneComment(pass, comm));
                if (i == 10) output.Append(ClassMain.BuildSeeMoreComments(comm));
                i++;
            }
            return output.ToString();
        }

        [WebMethod]
        public static string LoadNewestComments(int topid, int firstid)
        {
            //عرض المزيد من التعليقات على مقالة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";

            FamComment fc = new FamComment(pass);
            StringBuilder output = new StringBuilder();
            List<FamComment> lst = fc.GetNewestCommentsList((int)FamoBlock.enmObjectCode.Topics, topid, firstid);
            foreach (FamComment comm in lst)
            {
                output.Append(ClassMain.BuildOneComment(pass, comm));
            }
            return output.ToString();
        }

        [WebMethod]
        public static string[] LoadNewestTopicsOfMyWall(int firstid)
        {
            //خاص بصفحتي الخاصة
            //عرض المقالات الحديثة يتم استدعاء الدالة كل 5 ثواني
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;

            FamTopic top = new FamTopic(pass);
            string[] output = new string[] { "0", "" };
            List<FamTopic> lst;
            lst = top.GetNewestTopicsList(pass.UserID, firstid);//عرض مقالات صفحتي الحائط

            output[0] = lst[0].ID.ToString();//new first id

            //build tags
            foreach (FamTopic t in lst)
            {
                output[1] += ClassMain.BuildTopicTags(pass, t, pass.UserID);
            }
            return output;
        }

        [WebMethod]
        public static string LoadNewestTopicsOfUser(int userid, int firstid)
        {
            //خاص بصفحة الشخص الآخر
            //عرض المقالات الحديثة يتم استدعاءه عند الضغط على زر اظهار المشاركات الجديدة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";

            FamTopic top = new FamTopic(pass);
            StringBuilder output = new StringBuilder();
            List<FamTopic> lst;
            lst = top.GetMyNewestTopicsList(userid, firstid);//عرض مقالات شخص معين 
            foreach (FamTopic t in lst)
            {
                output.Append(ClassMain.BuildTopicTags(pass, t, userid));
            }
            return output.ToString();
        }

        [WebMethod]
        public static string[] LoadMoreTopics(int lastid)
        {
            //see more topics
            //ãÔÇåÏÉ ÇáãÒíÏ ãä ÇáãÞÇáÇÊ
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;

            FamTopic top = new FamTopic(pass);
            string[] output = new string[] { "0", "", "0" };
            List<FamTopic> lst;
            lst = top.GetOldTopicsList(pass.UserID, lastid);

            output[0] = lst[lst.Count - 1].ID.ToString();//new last id
            output[2] = lst.Count.ToString(); //count of topics

            //build tags
            //output[1] = "<div class='right_panel'>";
            foreach (FamTopic t in lst)
            {
                output[1] += ClassMain.BuildTopicTags(pass, t, pass.UserID);
            }
            //output[1] += "</div>";
            return output;
        }


        [WebMethod]
        public static string[] GetCountOfNewTopics(int firstid)
        {
            //عرض عدد المقالات الحديثة يتم استدعاء الدالة كل 5 ثواني
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;

            string[] output = new string[] { "0", "" };
            FamTopic top = new FamTopic(pass);
            //count of new topics
            int count = top.GetCountOfNewTopics(pass.UserID, firstid);

            output[0] = count.ToString();
            output[1] = ClassMain.BuildCountOfNewPosts(pass, pass.UserID, firstid, count);
            return output;
        }

        //اعادة اسماء المعلقين على موضوع
        [WebMethod]
        public static string NamesUsersOfComments(int objcode, int objid)
        {

            //Prev  Users Comments
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;

            FamComment fc = new FamComment(pass);
            return fc.GetUserOfComment(objcode, objid);

        }

        //تتبع المنتج 
        [WebMethod]
        public static int FollowShop(int Shopid, int Userid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamFollow ffw = new FamFollow(pass);
            DataTable dt = ffw.GetShopFollow(Userid, Shopid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["folw_id"] != null)
                {
                    ffw = new FamFollow(pass, Convert.ToInt32(dt.Rows[0]["folw_id"]));
                    if (ffw.Status == FamoBlock.enmFollowStatus.Follow)
                        return 0;
                    else
                    {
                        ffw.Status = FamoBlock.enmFollowStatus.Follow;
                        return Convert.ToInt32(ffw.Update());
                    }
                }
                else
                    return 0;
            }
            else
            {

                ffw.Shop.ID = Shopid;
                ffw.User.ID = Userid;
                ffw.Status = FamoBlock.enmFollowStatus.Follow;
                return ffw.Save();
            }
        }

        //عدم تتبع المنتج 
        [WebMethod]
        public static int UnFollowShop(int Shopid, int Userid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamFollow ffw = new FamFollow(pass);
            DataTable dt = ffw.GetShopFollow(Userid, Shopid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["folw_id"] != null)
                {
                    ffw = new FamFollow(pass, Convert.ToInt32(dt.Rows[0]["folw_id"]));
                    if (ffw.Status == FamoBlock.enmFollowStatus.Cancle)
                        return 0;
                    else
                    {
                        ffw.Status = FamoBlock.enmFollowStatus.Cancle;
                        return Convert.ToInt32(ffw.Update());
                    }
                }
                else
                    return 0;
            }
            else
            {

                ffw.Shop.ID = Shopid;
                ffw.User.ID = Userid;
                ffw.Status = FamoBlock.enmFollowStatus.Cancle;
                return ffw.Save();
            }
        }

        //Get number Follow
        [WebMethod]
        public static int GetNumbersFollow(int Shopid, int status)
        {

            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamFollow ffw = new FamFollow(pass);
            return ffw.GetCountFollowers(Shopid, status);
        }


        [WebMethod]
        public static int FriendshipNextAction(int linkid, int nextaction, int user)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            int res = 0;
            StringBuilder output = new StringBuilder();
            try
            {
                if (nextaction == (int)FamoBlock.enmLinkeStatus.Request)
                    res = LinkFriend(pass.UserID, user);//linkid instead the userid
                else if (nextaction == (int)FamoBlock.enmLinkeStatus.Cancel_Request)
                    res = CancelRequest(linkid);
                else if (nextaction == (int)FamoBlock.enmLinkeStatus.Allow)
                    res = AcceptFrindShip(linkid);
                else if (nextaction == (int)FamoBlock.enmLinkeStatus.Unlink)
                    res = UnLinkFriend(linkid);
                else if (nextaction == (int)FamoBlock.enmLinkeStatus.Not_Allow)
                    res = NotAllowRequest(linkid);

                /*if (res > 0) {
                    output.Append(ClassMain.BuildFriendshipLinks(pass, new FamUser(pass,user), ""));
                }*/
            }
            catch (Exception ex) { }
            return res;
        }

        //اضافة صديق في حالة عدم اضافته من قبل
        [WebMethod]
        public static int LinkFriend(int userid, int touserid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            DataTable dt = fl.GetLinks(userid, touserid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["link_id"] != null)
                {
                    fl = new FamLink(pass, Convert.ToInt32(dt.Rows[0]["link_id"]));
                    if (fl.Status == FamoBlock.enmLinkeStatus.Request)
                        return 0;
                    else
                    {
                        fl.Status = FamoBlock.enmLinkeStatus.Request;
                        return Convert.ToInt32(fl.Update());
                    }
                }
                else
                    return 0;
            }
            else
            {
                fl.UserSource.ID = userid;
                fl.UserTarget.ID = touserid;
                fl.Status = FamoBlock.enmLinkeStatus.Request;
                FamoPersonalService pers = new FamoPersonalService();
                pers.IncreaseNotificationCounter(pass.Email, pass.Password, touserid, 3);
                //ClassMain.IncreaseNotificationCounter(pass, touserid, ClassMain.NotificationEnum.Notification_Friend);
                return fl.Save();
            }
        }

        // قبول صديق 
        [WebMethod]
        public static int AcceptFrindShip(int linkid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            DataTable dt = fl.GetLink(linkid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["link_id"] != null)
                {
                    //اذا كانت الحالة طلب 
                    if ((FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]) == FamoBlock.enmLinkeStatus.Request)
                    {
                        fl = new FamLink(pass, Convert.ToInt32(dt.Rows[0]["link_id"]));
                        if (fl.Status == FamoBlock.enmLinkeStatus.Allow)
                            return 0;
                        else
                        {
                            fl.Status = FamoBlock.enmLinkeStatus.Allow;
                            FamoPersonalService pers = new FamoPersonalService();
                            pers.IncreaseNotificationCounter(pass.Email, pass.Password, fl.UserSource.ID, 3);
                           // ClassMain.IncreaseNotificationCounter(pass, fl.UserSource.ID, ClassMain.NotificationEnum.Notification_Friend);
                            return Convert.ToInt32(fl.Update());
                        }
                    }
                }
                else
                    return 0;
            }
            return 0;
        }

        // رفض قبول طلب الصداقة
        [WebMethod]
        public static int NotAllowRequest(int linkid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            DataTable dt = fl.GetLink(linkid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["link_id"] != null)
                {
                    //اذا كانت الحالة طلب 
                    if ((FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]) == FamoBlock.enmLinkeStatus.Request)
                    {
                        fl = new FamLink(pass, Convert.ToInt32(dt.Rows[0]["link_id"]));
                        if (fl.Status == FamoBlock.enmLinkeStatus.Not_Allow)
                            return 0;
                        else
                        {
                            fl.Status = FamoBlock.enmLinkeStatus.Not_Allow;
                            return Convert.ToInt32(fl.Update());
                        }
                    }
                }
                else
                    return 0;
            }
            return 0;
        }

        //رفض طلب الصداقة عند المرسل
        [WebMethod]
        public static int CancelRequest(int linkid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            DataTable dt = fl.GetLink(linkid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["link_id"] != null)
                {
                    //اذا كانت الحالة طلب 
                    if ((FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]) == FamoBlock.enmLinkeStatus.Request)
                    {
                        fl = new FamLink(pass, Convert.ToInt32(dt.Rows[0]["link_id"]));
                        if (fl.Status == FamoBlock.enmLinkeStatus.Cancel_Request)
                            return 0;
                        else
                        {
                            fl.Status = FamoBlock.enmLinkeStatus.Cancel_Request;
                            return Convert.ToInt32(fl.Update());
                        }
                    }
                }
                else
                    return 0;
            }
            return 0;
        }

        //الغاء طلب الصداقة
        [WebMethod]
        public static int UnLinkFriend(int linkid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            DataTable dt = fl.GetLink(linkid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["link_id"] != null)
                {
                    //اذا كانت الحالة طلب 
                    if ((FamoBlock.enmLinkeStatus)Convert.ToInt32(dt.Rows[0]["status"]) == FamoBlock.enmLinkeStatus.Allow)
                    {
                        fl = new FamLink(pass, Convert.ToInt32(dt.Rows[0]["link_id"]));
                        if (fl.Status == FamoBlock.enmLinkeStatus.Unlink)
                            return 0;
                        else
                        {
                            fl.Status = FamoBlock.enmLinkeStatus.Unlink;
                            return Convert.ToInt32(fl.Update());
                        }
                    }
                }
                else
                    return 0;
            }
            return 0;
        }

        // حالة الصديق
        [WebMethod]
        public static int GetFriendShip(int userid, int frindid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            return Convert.ToInt32(fl.GetAction(userid, frindid));
        }
        public static int GetCountOfRequest(int userid)
        {
            //request of friendship طلبات الصداقة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
            FamLink fl = new FamLink(pass);
            return fl.GetCountOfRequestedMe(userid, (FamoBlock.enmLinkeStatus.Request));
            // return fl.GetCountOfRequest(userid, (FamoBlock.enmLinkeStatus.Request));
        }

        [WebMethod]
        public static string[] SaveTopic(string description, string url)
        {
            //اضافة مقالة جديد
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", "" };

            FamTopic ft = new FamTopic(pass);
            List<FilesStatus> c = (List<FilesStatus>)HttpContext.Current.Session["filesarry"];

            string path1 = "", path2 = "";
            int countfiles = 0;
            if (c != null)
                countfiles = c.Count;

            //read files uploaded from fileuploader jquery via session
            if (countfiles > 0)
            {
                foreach (FilesStatus f in c)
                {
                    path1 = f.FullName;
                    path2 = HttpContext.Current.Server.MapPath("~") + "files_upload\\" + Path.GetFileName(f.FullName);
                    File.Move(path1, path2);
                }
            }

            if (countfiles <= 0 && description == "") return null;

            HttpContext.Current.Session["filesarry"] = null;    //clear session

            //save topic
            ft.ObjectCode = FamoBlock.enmObjectCode.Users;
            ft.ObjectId = pass.UserID;
            ft.User.ID = pass.UserID;
            ft.Description = description;
            ft.Link = url == "" ? "" : url;
            int topid = ft.Save();
            FamOption op = new FamOption(pass);
            FamAlbum album1 = new FamAlbum(pass);
            DataTable dt = new DataTable();
            //dt = album1.GetUserAlbum(pass.UserID);
            int ablbumid = 0;

            if (op.GetValue(FamoBlock.Const_Option_Usr_TopicAlbum + "_" + pass.UserID) != "" || op.GetValue(FamoBlock.Const_Option_Usr_TopicAlbum + "_" + pass.UserID) != null)
                ablbumid = Convert.ToInt32(op.GetValue(FamoBlock.Const_Option_Usr_TopicAlbum + "_" + pass.UserID));

            FamFile fl;
            if (countfiles > 0 && topid > 0)
            {
                foreach (FilesStatus f in c)
                {
                    fl = new FamFile(pass);
                    fl.Name = Path.GetFileName(f.FullName);
                    fl.Path = ClassMain.FolderPath + fl.Name;
                    fl.ObjectCode = FamoBlock.enmObjectCode.Topics;
                    fl.ObjectId = topid;
                    fl.UserID = pass.UserID;
                    if (ablbumid == 0)
                    {
                        if (pass.Language_ID == 1)
                            album1.Name = "MyTopic";
                        else
                            album1.Name = "صفحتي";
                        album1.User.ID = pass.UserID;
                        ablbumid = album1.Save();
                        op.SetValue(FamoBlock.Const_Option_Usr_TopicAlbum + "_" + pass.UserID, ablbumid.ToString());
                    }
                    fl.AlbumID = ablbumid;
                    fl.FileType = FamoBlock.enmFileType.Photo;
                    fl.Save();
                }
            }

            output[0] = topid.ToString();
            //اعادة هيكل المقاله لإظهارها في الصفحه
            output[1] = ClassMain.BuildTopicTags(pass, new FamTopic(pass, topid), pass.UserID);

            return output;
        }

        [WebMethod]
        public static bool DeleteTopic(int topid)
        {
            //عرض مقالة 
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            return new FamTopic(pass, topid).Delete();
        }

        [WebMethod]
        public static string GetTopic(int topid)
        {
            //عرض مقالة 
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";
            return ClassMain.BuildTopicPopup(pass, new FamTopic(pass, topid));
        }

        [WebMethod]
        public static string GetShops(int activ, int lastid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "";
            FamShop fsho = new FamShop(pass);
            List<FamShop> lstshop = new List<FamShop>();

            if (activ > 0)
                lstshop = fsho.getShopsFollowingByActivitylist(pass.UserID, activ, lastid);
            else if (activ == 0)
                lstshop = fsho.getShopsFollowinglist(pass.UserID, lastid);




            NopService.NopService client = new NopService.NopService();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //if (isFollowed)
          //  {
                ds = client.GetVendorsFollowingSeeMore(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true, lastid,true);

                dt = ds.Tables[0];
          //  }
            //else
            //{

            //}
            StringBuilder output = new StringBuilder();

            foreach (DataRow fshp in dt.Rows)
                output.Append(ClassMain.BuildShopBox(pass, fshp, activ));
            //foreach (FamShop sho in lstshop)
            //{
            //    output.Append(ClassMain.BuildShopBox(pass, sho, activ));
            //}
            return output.ToString();

        }

        #endregion

        #region User Pages
        /*[WebMethod]
        public static string LoadPage(int objcode, int objid, int page, int tab)
        {
            //تحميل الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";

            return ClassMain.BuildUserPage(pass, (FamoBlock.enmObjectCode)objcode, objid, (ClassMain.PageName)page, tab);
        }*/

        // Hisham Change LoadPage Function
        [WebMethod]
        public static string[] LoadPage(int userid, int page, int tab)
        {
            //LoadWholePage()
            //تحميل الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;


            string[] output = new string[] { "0", ClassMain.PageName.Arena.ToString(), "" };

            //return username if avaliable or userid
            FamUser u = new FamUser(pass, userid);
            output[0] = !string.IsNullOrEmpty(u.UserName) ? u.UserName : userid.ToString();

            output[2] = ClassMain.BuildUserPage(pass, userid, (ClassMain.PageName)page, tab);
            return output;// ClassMain.BuildUserPage(pass, userid, (ClassMain.PageName)page, tab);
        }

        [WebMethod]
        public static string[] LoadTopicsArea(int userid)
        {
            //HttpContext.p
            //int userid = Convert.ToInt32(HttpContext.Current.Request.QueryString["{userid}"]);
            //string y = "";
            //try
            //{
            //    //string x = HttpContext.Current.Request.RequestContext.RouteData.Values["userid"].ToString();
            //     y = HttpContext.Current.Request.RequestContext.RouteData.Values["userid"] as string;
            //}
            //catch (Exception ex) { }
            //userid=Convert.ToInt32();
            //تحميل محتوى صفحة المقالات فقط بدون باقي الصفحة القائمة الرئيسية ورأس الصفحة
            //userid = Convert.ToInt32(y);
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", ClassMain.PageName.Arena.ToString(), "" };
            output[0] = pass.UserID.ToString();

            output[2] = ClassMain.BuildTopicsArea(pass, new FamUser(pass, userid));
            return output;
        }

        [WebMethod]
        public static string[] LoadPhotosArea(int userid)
        {
            //تحميل محتوى صفحة ألبومات الصور فقط بدون باقي الصفحة القائمة الرئيسية ورأس الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", ClassMain.PageName.Photos.ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[2] = ClassMain.BuildAlbumsArea(pass, new FamUser(pass, userid));
            return output;
        }

        [WebMethod]
        public static string[] LoadFriendsArea(int userid)
        {
            //تحميل صفحة الأصدقاء
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", ClassMain.PageName.Friends.ToString(), Convert.ToInt32(ClassMain.FriendArea.MyFriends).ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[3] = ClassMain.BuildFriendArea(pass, new FamUser(pass,  userid), ClassMain.FriendArea.MyFriends);
            return output;
        }

        [WebMethod]
        public static string[] LoadShopArea(int userid)
        {
            //تحميل صفحة الأصدقاء
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", ClassMain.PageName.Shops.ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[2] = ClassMain.BuildShopPage(pass, new FamUser(pass,  userid), new FamActivity(pass, 0));
            return output;
        }

        [WebMethod]
        public static string[] LoadSettingArea(int userid)
        {
            //تحميل صفحة الأصدقاء
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", ClassMain.PageName.Setting.ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[2] = ClassMain.BuildSettingBox(pass);
            return output;
        }

        [WebMethod]
        public static string LoadFriendBlocks(int userid, int friendarea)
        {
            //عرض صفحة طلبات الصداقة او الاصدقاء او غيرها حسب الباراميتر المرسل
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";

            return ClassMain.BuildFriendsBlocks(pass, new FamUser(pass, userid), (ClassMain.FriendArea)friendarea);
        }

        [WebMethod]
        public static string LoadShopsArea(int userid, int actvid)
        {
            //تحميل محتوى صفحة الشركات
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";

            return ClassMain.BuildShopContentPage(pass, new FamUser(pass, userid), new FamActivity(pass, actvid));
        }

        [WebMethod]
        public static string RandomizeHeaderImages()
        {
            //تحميل محتوى الصور العشوائية الموجوده في رأس الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";
            return ClassMain.BuildUserImagesHeader(pass, pass.UserID, true);
        }

        #endregion

        #region Albums and Photos

        [WebMethod]
        public static string CreateNewAlbum()
        {
            //انشاء البوم جديد
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";
            FamAlbum alb = new FamAlbum(pass);
            alb.Name = "البوم جديد";
            alb.User.ID = pass.UserID;
            int id = alb.Save();
            string output = "";
            if (id > 0)
            {
                output = ClassMain.BuildAlbumContent(pass,new FamUser(pass,pass.UserID),alb);
            }
            //
            return output;
        }

        [WebMethod]
        public static bool DeleteAlbum(int albmid)
        {
            //عرض مقالة 
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            return new FamAlbum(pass, albmid).Delete();
        }

        [WebMethod]
        public static bool RenameAlbum(int albmid, string name)
        {
            //تعديل اسم البوم
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            if (name.Trim() == null) return false;
            FamAlbum alb = new FamAlbum(pass, albmid);
            alb.Name = name;
            return alb.Update();
        }

        // اظهار الصور في الالبوم 
        [WebMethod]
        public static string ShowPhotos(int alboum,int userid)
        {
            FamoPassport fpass = (FamoPassport)HttpContext.Current.Session["passport"]; ;
            if (fpass == null || !fpass.Logged) return "";
            return ClassMain.BuildPhotosBlock(fpass, new FamAlbum(fpass, alboum),new FamUser(fpass,userid));
        }

        [WebMethod]
        public static bool DeleteFile(int fileid)
        {
            //عرض مقالة 
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            return new FamFile(pass, fileid).Delete();
        }

        [WebMethod]
        public static string[] ChangeAlbumCover(int fileid)
        {
            //تحديد غلاف الالبوم من خلال اختيار الصورة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            FamFile f = new FamFile(pass, fileid);

            FamAlbum alb = new FamAlbum(pass, f.AlbumID);
            alb.File.ID = fileid;
            alb.Update();


            string[] output = new string[] { "0", "" };
            output[0] =alb.ID.ToString();
            output[1] = f.Path.Replace("~", ""); ;
            return output;
        }
        [WebMethod]
        public static string FillAlbum()
        {
            //تعبئة الالبوم
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";

            string output = "";
            FamAlbum alb = new FamAlbum(pass);
            DataTable dt = alb.GetUserAlbum(pass.UserID);
            foreach (DataRow dr in dt.Rows)
            {
                output += "<option value='" + dr["album_id"].ToString() + "'>" + dr["name"].ToString() + "</option>";
            }
            return output;
        }

        [WebMethod]
        public static bool MoveToAlum(int file, int album)
        {
            //تعبئة الالبوم
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            if (file == 0 && album == 0) return false;
            FamFile f = new FamFile(pass, file);
            f.AlbumID = album;
            bool b = f.Update();

            //remove albume cover image 
            FamAlbum alb = new FamAlbum(pass, album);
            if (alb.File.ID == file)//the moved photo was cover ?
            {
                alb.File.ID = 0;
                alb.Update();
            }
            return b;
        }
        #endregion

        #region LoginPage


        [WebMethod]
        public static bool IsEmailValid(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return isEmail;
        }

        [WebMethod]
        public static bool CheckEmailExist(string email)
        {
            var IsValid = false;
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];

            FamUser FU = new FamUser(pass);
            DataTable dt = FU.GetUserByEmail(email);
            if (dt.Rows.Count > 0)
                IsValid = false;//found
            else
                IsValid = true;//not found

            return IsValid;
        }
        [WebMethod]
        public static bool SendEmailForUser(string email)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (CheckEmailExist(email))
                return false;

            FamUser FU = new FamUser(pass);
            FU.FillUserByEmail(email);
            if (FU.ID > 0)
                return SendEmail(FU);
            else
                return false;
        }

        private static bool SendEmail(FamUser user)
        {
            try
            {
                string html = GmailSender.ScreenScrapeHtml(ClassMain.WebHosting + ClassMain.GetOptionValue(FamoBase.Const_Option_Pub_ResetPassword));
                html = html.Replace("|fullname|", user.FirstName + " " + user.LastName);
                html = html.Replace("|email|", user.Email);
                //html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?p=" + getParameter(user));
                //html = html.Replace("|password|", ClassMain.WebHosting + "NewPassword/" + getParameter(user));
                html = html.Replace("|password|", ClassMain.WebHosting + "newpass.aspx?d=" + getParameter(user));
                return ClassMain.SendEmail("نموذج استعادة كلمة المرور", user.Email, html);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static string getParameter(FamUser user)
        {
            EncryptionThread enc = new EncryptionThread();
            string t = DateTime.Now.Ticks.ToString();
            string Email = user.ID + "|" + user.Email + "|" + user.LanguageID + "|" + user.FullName + "|" + user.Gender.ToString() + "|" + t;
            return enc.EncryptString(Email, "abcsx").Replace("/", "$");
        }


        #endregion

        #region Setting
        //حفظ بيانات الوظيفة
        [WebMethod]
        public static bool SaveJopInfo(string jop, string companyName, string college)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamWorkProfile fpfile = new FamWorkProfile(pass);
            DataTable dt = fpfile.GetUserWorkProfile(pass.UserID);
            if (dt.Rows.Count > 0)
                fpfile.ID = Convert.ToInt32(dt.Rows[0]["work_id"]);
            fpfile.College = college;
            fpfile.CompanyName = companyName;
            fpfile.Employer = jop;
            return fpfile.Update();
        }

        // حفظ بيانات About Me
        [WebMethod]
        public static bool SaveJopAbout(string about)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamWorkProfile fpfile = new FamWorkProfile(pass);
            DataTable dt = fpfile.GetUserWorkProfile(pass.UserID);
            if (dt.Rows.Count > 0)
                fpfile.ID = Convert.ToInt32(dt.Rows[0]["work_id"]);
            fpfile.About = about;

            return fpfile.Update();
        }

        // حفظ المعلومات الشخصية
        // الاسم الاول والاسم الاخير 
        [WebMethod]
        public static bool SaveUserName(string firstname, string lastname)
        {
            if (firstname == "" || lastname == "")
                return false;
            else
            {
                FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
                if (pass == null) return false;
                if (pass.UserType != FamoBlock.enmUserType.User) return false;
                FamUser u = new FamUser(pass, pass.UserID);
                u.FirstName = firstname;
                u.LastName = lastname;
                return u.Update();
            }
        }

        // حفظ العنوان 
        [WebMethod]
        public static bool SaveAddressUser(string address, int country, string city, int nationality)
        {
            if (country == 0)
                return false;
            else
            {
                FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
                if (pass == null) return false;
                if (pass.UserType != FamoBlock.enmUserType.User) return false;
                FamContactProfile fcp = new FamContactProfile(pass, pass.UserID);
                fcp.Address = address;
                fcp.Country.ID = country;
                fcp.City = city;
                fcp.Nationality.ID = nationality;
                return fcp.Update();
            }
        }

        // حفظ الايميل 
        [WebMethod]
        public static bool SaveEmail(string email)
        {
            if (email.Trim() == "") return false;
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (!CheckEmailExist(email)) return false;
            FamUser fu = new FamUser(pass, pass.UserID);
            fu.Email = email;
            return fu.Update();
        }

        [WebMethod]
        public static bool SavePass(string currpass, string newpass, string reapetpass)
        {
            if (currpass == "" || newpass == "" || reapetpass == "")
                return false;
            else
            {
                FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
                if (pass == null) return false;

                FamUser FU = new FamUser(pass, pass.UserID);

                if (FU.Password == currpass)
                {
                    if (reapetpass == newpass)
                    {
                        FU.Password = newpass;
                        return FU.Update();
                    }
                    else
                        return false;
                }
                return false;
            }

        }

        // حفظ تاريخ الميلاد 
        [WebMethod]
        public static bool SaveBithDate(string day, string month, string year)
        {
            if (day == "0" || month == "0" || year == "0")
                return false;
            else
            {
                FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
                if (pass == null) return false;

                FamUser fu = new FamUser(pass, pass.UserID);
                fu.BirthDate = day + "/" + month + "/" + year;
                return fu.Update();
            }
        }


        // حفظ رقم التلفون  
        [WebMethod]
        public static bool SavePhone(string Phone)
        {
            if (Phone.Trim() == "") return false;
            if (!IsNumberValid(Phone)) return false;
            if (Phone.Length < 8) return false;
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (!CheckPhoneExist(Phone)) return false;
            else
            {
                FamPhoneProfile fphone = new FamPhoneProfile(pass);
                DataTable dt = fphone.GetPhoneProfiles((int)FamoBlock.enmObjectCode.Users, pass.UserID, FamoBlock.enmPhoneType.Mobile);
                bool b = false;
                if (dt.Rows.Count > 0)
                {
                    fphone = new FamPhoneProfile(pass, Convert.ToInt32(dt.Rows[0]["phon_id"]));
                    fphone.Number = Phone;
                    b = fphone.Update();
                }
                else
                {
                    fphone.ObjectCode = FamoBlock.enmObjectCode.Users;
                    fphone.ObjectId = pass.UserID;
                    fphone.Number = Phone;
                    fphone.Type = FamoBlock.enmPhoneType.Mobile;
                    if (fphone.Save() > 0)
                        b = true;
                }
                return b;
            }

        }

        //حفظ المستخدم

        [WebMethod]
        public static bool SaveUser(string User)
        {
            bool VUserName = true;
            if (User.Trim() == "") return false;
            VUserName = IsUserNameValid(User);
            if (!VUserName) return false;

            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (!CheckUserNameExist(User)) return false;
            else
            {
                FamUser fu = new FamUser(pass, pass.UserID);
                string UserName = User.ToString();
                fu.UserName = UserName.ToLower();
                //fu.UserName.Trim().ToString() = User;
                return fu.Update();
            }
        }


        public static bool CheckUserNameExist(string UserName)
        {
            var IsValid = false;
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];

            FamUser FU = new FamUser(pass);
            DataTable dt = FU.GetUserByName(UserName);
            if (dt.Rows.Count > 0)
                IsValid = false;//found
            else
                IsValid = true;//not found

            return IsValid;
        }


        // دالة كتابة اسم المستخدم بطريقة صحيحة
        public static bool IsUserNameValid(string User)
        {
            bool IsUser = Regex.IsMatch(User, @"^[\w]+$");
            return IsUser;
        }


        // فحص رقم التلفون
        [WebMethod]
        public static bool CheckPhoneExist(string phone)
        {
            var IsValid = false;
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamPhoneProfile fphone = new FamPhoneProfile(pass);
            DataTable dt = fphone.GetPhoneProfiles(phone);
            if (dt.Rows.Count > 0)
                IsValid = false;//found
            else
                IsValid = true;//not found

            return IsValid;
        }

        // التاكد من كتابة رقم التلفون باللغة العربية
        public static bool IsNumberValid(string Phpne)
        {
            bool isPhone = Regex.IsMatch(Phpne, @"^\d+$");
            return isPhone;
        }

        #endregion

        #region Notifications


        [WebMethod]
        public static string NewestNotification(int eventid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if(pass!=null)
            return ClassMain.BuildNewestNotification(pass, eventid);
            return "";
        }


        [WebMethod]
        public static string OldestNotification(int eventid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass != null)
            return ClassMain.BuildOldestNotification(pass, eventid);
            return "";
        }

        [WebMethod]
        public static int NotReadingEvents(int eventid)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            int x = 0;
            if (pass != null)
                x= ClassMain.NotReadingEvents(pass, eventid);

       //string val="<span class='notired'>"+x+"</span> ";
       //if (x > 0)
           return x;
            //return "";
        }

          [WebMethod]
        public static int SaveNotification(int objcode,int objid,int targetcode,int targetid)
        {


             FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
             int userid = 0;
             if (pass != null)
             {
                 if (targetcode == (int)FamoBlock.enmObjectCode.Topics)
                 {
                     FamTopic topc = new FamTopic(pass, targetid);
                     userid = topc.User.ID;
                 }

                 if (targetcode == (int)FamoBlock.enmObjectCode.Votes)
                 {
                     FamVote topc = new FamVote(pass, targetid);
                     userid = topc.User.ID;
                 }
                 ClassMain.SaveNotification(pass, (FamoBlock.enmObjectCode)objcode, objid, (FamoBlock.enmObjectCode)targetcode, targetid, userid);
             }
                return 0;
        }
        #endregion

          //hisham this method get see more for friends status Dontknow,MyFriends,OtherRequests,MayKnow and MyRequests 
        [WebMethod]
          public static string BuildFriendAreaSeemore(int frndArea,int lastid)
          {
              //build hole friendship page with friend bar and friend blocks
              FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
              StringBuilder output = new StringBuilder();

              //friends_bar
             
              //friends_request_blocks
              if(pass!=null)
              output.Append(ClassMain.BuildFriendsBlocksSeemore(pass, new FamUser(pass,pass.UserID),(ClassMain.FriendArea)frndArea , lastid));
              return output.ToString();
              /*
               <!-- BuildFriendBar -->
               <div id="friends_request_blocks">
                  <ul>
              //loop
                      <!-- BuildFriendBox -->
                  </ul>
                
              </div>
               */
          }

        //Hisham 
        #region AutoComplete
        [WebMethod]
        public static List<User>/*string[] */AutoComplete(string mail)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamUser user = new FamUser(pass);
            DataTable dt = new DataTable();
            dt= user.GetUserSearch(pass.UserID, mail);
            User userst = new User();
            userst.ID = 0;
            userst.Name = "";
            userst.PhotoPath = "";
            userst.Country = "";
            userst.Email = "";
            List<User> userlst = new List<User>();
            NopService.NopService no = new NopService.NopService();
            DataTable dtnop = new DataTable();
            userlst.Add(userst);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    userlst.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            dtnop = no.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, Convert.ToInt32(dr["costomer_id"]), true).Tables[0];
                        }
                        catch (Exception ex)
                        {
                            userst.ID = Convert.ToInt32(dr["user_id"]);
                            userst.Name = dr["First_Name"].ToString() + " " + dr["last_Name"].ToString();

                            userst.PhotoPath =ClassMain.WebHosting+ ClassMain.DefaultMalePhoto;
                            userst.Country = "";
                            userst.Email = dr["email"].ToString();
                        
                        }
                        userst.ID = Convert.ToInt32(dr["user_id"]);
                        try
                        {
                            userst.Name = dtnop.Rows[0]["FirstName"].ToString() + " " + dtnop.Rows[0]["lastName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            userst.Name = dr["First_Name"].ToString() + " " + dr["last_Name"].ToString();
                        }
                        
                         try
                        {
                        userst.PhotoPath = dtnop.Rows[0]["CustomerImage"].ToString();
                        }
                         catch (Exception ex)
                         {
                             userst.PhotoPath = ClassMain.WebHosting.Replace("com/","com") + ClassMain.DefaultMalePhoto;
                         }
                         try
                         {
                             userst.Country = dtnop.Rows[0]["Country"].ToString() + " " + dtnop.Rows[0]["City"].ToString();
                         }
                        catch (Exception ex)
                         {
                             userst.Country = "";
                         }
                         if (userst.Name == " " || userst.Name == null || userst.Name == "")
                             userst.Name = dr["First_Name"].ToString() + " " + dr["last_Name"].ToString();
                        userst.Link = ClassMain.WebHosting + "person/" + userst.ID + "Arena";
                        userlst.Add(userst);
                    }
                }
            }
            
            
            //string[] arr = new string[luser.Count];
            //int k=0;
            //foreach (FamUser u in luser.ToList())
            //{
            //    string x0 = "";
            //    x0 = u.FullName;
            //    arr[k++] = x0;
            //}

            return userlst; //  arr; 
            //string[] output = new string[luser.Count];
            //int x = 0;
            //foreach (FamUser fus in luser)
            //{
            //    output[x++] = fus.FullName+"_"+fus.ID;
            //}
            //return output;

            
        }

        [WebMethod]
        public static string/*string[] */SearchFriendsRelation(string mail,int area,int lastid)
        {
             FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
             return ClassMain.BuildFriendsBlocks_ForSearchByUserName(pass,(ClassMain.FriendArea) area, mail,lastid);
        }
        #endregion



        #region Messages
        [WebMethod]
        public static User UserLoginData(int id)
        {
            User user = new User();
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return user;
            user.Name = pass.FullName;
            user.Link = ClassMain.WebHosting + "person/" + pass.UserID + "/Arena";
            user.PhotoPath = ClassMain.getUserLogoPath(new FamUser(pass, pass.UserID));
            user.ID = pass.UserID;
            DataTable nopdt = new DataTable();
            try
            {
                NopService.NopService serv = new NopService.NopService();
                nopdt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, pass.Customer, true).Tables[0];
                user.Name = nopdt.Rows[0]["firstname"].ToString() + " " + nopdt.Rows[0]["firstname"].ToString();
                user.PhotoPath = nopdt.Rows[0]["CustomerImage"].ToString();

            }
            catch (Exception exp) { }

            return user;
        }

        [WebMethod]
        public static List<MessageData> GetMyFriendConversation(int myFriend, int lastid, string type)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            MessageData msd = new MessageData();
            List<MessageData> lstmsd = new List<MessageData>();
            FamMessage msg = new FamMessage(pass);
            DataTable dt = new DataTable();
            if (lastid == 0)
                dt = msg.GetLastMessages(pass.UserID, myFriend);
            else
                if (type == "old")
                    dt = msg.getOldMessages(lastid, pass.UserID, myFriend);
                else
                    dt = msg.GetNewMessage(lastid, pass.UserID, myFriend);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    User Sender = GetNopCustomerData(new FamUser(pass, pass.UserID));
                    User Reciver = GetNopCustomerData(new FamUser(pass, myFriend));
                    foreach (DataRow dr in dt.Rows)
                    {
                        MessageData msgdata = new MessageData();
                        msgdata.ID = Convert.ToInt32(dr["msg_id"]);
                        msgdata.Message = dr["message"].ToString();
                        msgdata.MsdID = Convert.ToInt32(dr["msd_id"]);
                        msgdata.Sender = Convert.ToInt32(dr["sender_id"]);
                        User usrsender = new User();
                        usrsender = msgdata.Sender == pass.UserID ? Sender : Reciver;
                        msgdata.SenderName = usrsender.Name;
                        msgdata.SenderPhotoPath = usrsender.PhotoPath;
                        msgdata.SenderUrl = usrsender.Link;
                        msgdata.Reciver = Convert.ToInt32(dr["user_id"]);
                        User userreciver = new User();
                        userreciver = msgdata.Reciver == pass.UserID ? Sender : Reciver;
                        msgdata.ReciverName = userreciver.Name;
                        msgdata.ReciverPhotoPath = userreciver.PhotoPath;
                        msgdata.ReciverUrl = userreciver.Link;
                        msgdata.Time = ClassMain.GetPrettyDate(Convert.ToDateTime(dr["cud"].ToString()));
                        lstmsd.Add(msgdata);
                    }


                }

            }
            return lstmsd;

        }

        [WebMethod]
        public static List<MessageData> GetMyLastPersonalMessages(int lastid, string type, int reciver = 0)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            MessageData msd = new MessageData();
            List<MessageData> lstmsd = new List<MessageData>();
            FamMessage msg = new FamMessage(pass);
            DataTable dt = new DataTable();
            if (lastid == 0)
                dt = msg.GetLastPersonsMessages(pass.UserID,reciver);
            else
                if (type == "old")
                    dt = msg.getOldPersonsMessages(lastid, pass.UserID, reciver);
                else
                    dt = msg.GetNewPersonsMessage(lastid, pass.UserID, reciver);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MessageData msgdata = new MessageData();
                        msgdata.ID = Convert.ToInt32(dr["msg_id"]);
                        msgdata.Message = dr["message"].ToString();
                        msgdata.MsdID = Convert.ToInt32(dr["msd_id"]);
                        msgdata.Sender = Convert.ToInt32(dr["sender_id"]);
                        User usrsender = new User();
                        FamUser senderuser = new FamUser(pass, msgdata.Sender);
                        usrsender = GetNopCustomerData(senderuser);
                        msgdata.SenderName = usrsender.Name;
                        if (usrsender.Name == null || usrsender.Name.Trim() == "")
                            msgdata.SenderName = senderuser.FullName;
                        msgdata.SenderPhotoPath = usrsender.PhotoPath;
                        msgdata.SenderUrl = usrsender.Link;
                        msgdata.Reciver = Convert.ToInt32(dr["user_id"]);
                        User userreciver = new User();
                        FamUser receiveruser = new FamUser(pass, msgdata.Reciver);
                        userreciver = GetNopCustomerData(receiveruser);
                        msgdata.ReciverName = userreciver.Name;
                        if (userreciver.Name == null || userreciver.Name.Trim() == "")
                            msgdata.ReciverName = receiveruser.FullName;
                        msgdata.ReciverPhotoPath = userreciver.PhotoPath;
                        msgdata.ReciverUrl = userreciver.Link;
                        msgdata.Time = ClassMain.GetPrettyDate(Convert.ToDateTime(dr["cud"].ToString()));
                        lstmsd.Add(msgdata);
                    }


                }

            }
            return lstmsd;

        }




        [WebMethod]
        public static int SendMessage(string Message, int Sender, int Reciver)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamMessage ms = new FamMessage(pass);
            int msgid = 0;

            ms.Message = Message;
            ms.Sender.ID = Sender;
            ms.Recivers.ID = Reciver;
            msgid = ms.Save();
            //ClassMain.IncreaseNotificationCounter(pass, Reciver, ClassMain.NotificationEnum.Notification_Message);
            FamoPersonalService pers = new FamoPersonalService();
            pers.IncreaseNotificationCounter(pass.Email, pass.Password, Reciver, 1);
            return msgid;

        }



        private static User GetNopCustomerData(FamUser users)
        {
            User user = new User();
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];


            if (pass == null) return user;
            DataTable nopdt = new DataTable();
            user.Name = pass.FullName;
            user.Link = ClassMain.WebHosting + "person/" + users.ID + "/Arena";
            user.PhotoPath = ClassMain.getUserLogoPath(new FamUser(pass, users.ID));
            user.ID = users.ID;


            try
            {
                NopService.NopService serv = new NopService.NopService();
                nopdt = serv.GetCustomerData(ClassMain.serviceUser, ClassMain.servicePass, users.CustomerId, true).Tables[0];
                user.Name = nopdt.Rows[0]["firstname"].ToString() + " " + nopdt.Rows[0]["lastname"].ToString();
                user.PhotoPath = nopdt.Rows[0]["CustomerImage"].ToString();

            }
            catch (Exception exp) { }

            return user;
        }
        #endregion


        #region NotificationCountsZero
        [WebMethod]
        public static void MessageNotiZero()
        {

            ChangeZero(1);


        }
        [WebMethod]
        public static string EventNotiZero()
        {

            return ChangeZero(2);


        }
        [WebMethod]
        public static string FriendNotiZero()
        {

            return ChangeZero(3);


        }

        [WebMethod]
        public static string[] GetNotiCount()
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamoPersonalService pers = new FamoPersonalService();
            // return 
            string[] output = new string[] { "0", "0", "0" };
            string msg = "";
            string evnt = "";
            string frnd = "";
            msg = pers.GetNotificationCounts(pass.Email, pass.Password, 1);
            if (msg == "")
                msg = "0";
            output[0] = msg;

            evnt = pers.GetNotificationCounts(pass.Email, pass.Password, 2);
            if (evnt == "")
                evnt = "0";
            output[1] = evnt;
            frnd = pers.GetNotificationCounts(pass.Email, pass.Password, 3);
            if (frnd == "")
                frnd = "0";
            output[2] = frnd;
            return output;
        }


        private static string ChangeZero(int type)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            FamoPersonalService pers = new FamoPersonalService();
            return pers.NotificationsCountZero(pass.Email, pass.Password, type);


        }
        #endregion
    }
}