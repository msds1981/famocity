using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using FamoLibrary;
using System.Text;
using System.Data;
using jQueryUploadTest;
using System.IO;
using System.Text.RegularExpressions;
using mUtilities;

namespace FamoCity
{
    /// <summary>
    /// Summary description for Methods
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Methods : System.Web.Services.WebService
    {

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

        [WebMethod]
        public static bool SaveUser(string User)
        {
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null) return false;
            if (pass.UserType != FamoBlock.enmUserType.User) return false;
            FamUser fu = new FamUser(pass, pass.UserID);
            fu.UserName = User;
            return fu.Update();
        }

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
            return v.VoteUp(FamoBlock.enmObjectCode.Topics, objid);
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
            return fc.Save();
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
            return fl.GetCountOfRequest(userid, (FamoBlock.enmLinkeStatus.Request));
        }

        [WebMethod]
        public static int SaveTopic(string description, string url)
        {
            //اضافة مقالة جديد
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return 0;
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

            if (countfiles <= 0 && description == "") return 0;

            HttpContext.Current.Session["filesarry"] = null;    //clear session

            //save topic
            ft.ObjectCode = FamoBlock.enmObjectCode.Users;
            ft.ObjectId = pass.UserID;
            ft.User.ID = pass.UserID;
            ft.Description = description;
            ft.Link = url == "" ? "" : url;
            int topid = ft.Save();

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
                    fl.FileType = FamoBlock.enmFileType.Photo;
                    fl.Save();
                }
            }
            return topid;
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

            StringBuilder output = new StringBuilder();
            foreach (FamShop sho in lstshop)
            {
                output.Append(ClassMain.BuildShopBox(pass, sho, activ));
            }
            return output.ToString();

        }

        #endregion

        #region User Pages
        [WebMethod]
        public static string LoadPage(int objcode, int objid, int page, int tab)
        {
            //تحميل الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return "error";

            return ClassMain.BuildUserPage(pass, (FamoBlock.enmObjectCode)objcode, objid, (ClassMain.PageName)page, tab);
        }

        [WebMethod]
        public static string[] LoadTopicsArea()
        {
            //تحميل محتوى صفحة المقالات فقط بدون باقي الصفحة القائمة الرئيسية ورأس الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", Convert.ToInt32(ClassMain.PageName.Arena).ToString(), "" };
            output[0] = pass.UserID.ToString();

            output[2] = ClassMain.BuildTopicsArea(pass, new FamUser(pass, pass.UserID));
            return output;
        }

        [WebMethod]
        public static string[] LoadPhotosArea()
        {
            //تحميل محتوى صفحة ألبومات الصور فقط بدون باقي الصفحة القائمة الرئيسية ورأس الصفحة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", Convert.ToInt32(ClassMain.PageName.Photos).ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[2] = ClassMain.BuildAlbumsArea(pass, new FamUser(pass, pass.UserID));
            return output;
        }

        [WebMethod]
        public static string[] LoadFriendsArea()
        {
            //تحميل صفحة الأصدقاء
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", Convert.ToInt32(ClassMain.PageName.Friends).ToString(), Convert.ToInt32(ClassMain.FriendArea.MyFriends).ToString(), "" };
            output[0] = pass.UserID.ToString();
            output[3] = ClassMain.BuildFriendArea(pass, new FamUser(pass, pass.UserID), ClassMain.FriendArea.MyFriends);
            return output;
        }

        [WebMethod]
        public static string[] LoadSettingArea()
        {
            //تحميل صفحة الأصدقاء
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return null;
            string[] output = new string[] { "0", Convert.ToInt32(ClassMain.PageName.UserSettings).ToString(), "" };
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
                output = ClassMain.BuildAlbumContent(pass, alb);
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
        public static string ShowPhotos(int alboum)
        {
            FamoPassport fpass = (FamoPassport)HttpContext.Current.Session["passport"]; ;
            if (fpass == null || !fpass.Logged) return "";
            return ClassMain.BuildPhotosBlock(fpass, new FamAlbum(fpass, alboum));
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
        public static bool SetAlbumCover(int fileid)
        {
            //تحديد غلاف الالبوم من خلال اختيار الصورة
            FamoPassport pass = (FamoPassport)HttpContext.Current.Session["passport"];
            if (pass == null || !pass.Logged) return false;
            FamFile f = new FamFile(pass, fileid);

            FamAlbum alb = new FamAlbum(pass, f.AlbumID);

            return false;
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
        #endregion
    }
}
