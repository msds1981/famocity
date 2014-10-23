using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using FamoLibrary;
using System.Data.SqlClient;

namespace FamoCity.App_Code
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //main pages
            RouteTable.Routes.MapPageRoute("rtMain", "Main", "~/Default.aspx");
            RouteTable.Routes.MapPageRoute("rtLogin1", "Login", "~/Default.aspx");            
            RouteTable.Routes.MapPageRoute("rtLogin2", "Login/{email}/{password}", "~/Default2.aspx");
            RouteTable.Routes.MapPageRoute("rtReLogin", "reLogin", "~/Default2.aspx");
            RouteTable.Routes.MapPageRoute("rtLogout", "Logout", "~/logout.aspx");
            RouteTable.Routes.MapPageRoute("rtResetPass", "ForgetPassword", "~/ResetPass.aspx");
            RouteTable.Routes.MapPageRoute("rtShopReg", "Register", "~/Register.aspx");
            RouteTable.Routes.MapPageRoute("rtFeedback", "Feedback", "~/Feedback.aspx");
            RouteTable.Routes.MapPageRoute("rtNewpass", "NewPassword/{data}", "~/newpass.aspx");
            RouteTable.Routes.MapPageRoute("rtMessage", "message", "~/msg.aspx");
            RouteTable.Routes.MapPageRoute("rtChat", "chat", "~/message.aspx");
            RouteTable.Routes.MapPageRoute("rtChat2", "chat/{userid}", "~/message.aspx");

            //user pages
            RouteTable.Routes.MapPageRoute("rtUserProfile", "User/profile", "~/usrmain.aspx?pg=6");
            //RouteTable.Routes.MapPageRoute("rtUserMain", "User/{username}/main", "~/usrMain.aspx");
            //RouteTable.Routes.MapPageRoute("rtUserPhotos", "User/{username}/Photo", "~/usrMain.aspx");
            RouteTable.Routes.MapPageRoute("rtUserPage", "person/{userid}/{page}", "~/usrMain.aspx");
            RouteTable.Routes.MapPageRoute("rtUserPageTab", "person/{userid}/{page}/{tab}", "~/usrMain.aspx");
            RouteTable.Routes.MapPageRoute("rtUserPage2", "person/{userid}", "~/usrMain.aspx");//في حالة عدم ارسال الصفحه سيتم تشغيل الصفحه الرئيسية
            RouteTable.Routes.MapPageRoute("rtUserPage3", "person", "~/usrMain.aspx");
            //RouteTable.Routes.MapPageRoute("rtUsermain", "page/{userid}", "~/usrMain.aspx");//show topics
            //RouteTable.Routes.MapPageRoute("rtUserTopic", "{userid}/topic/{topid}", "~/usrtopic.aspx");//show one topic

            //Register Pages
            RouteTable.Routes.MapPageRoute("rtRegStep2", "step2", "~/usrRegStep2.aspx");
            RouteTable.Routes.MapPageRoute("rtRegStep3", "step3", "~/usrRegStep3.aspx");
            RouteTable.Routes.MapPageRoute("rtRegStep4", "step4", "~/usrRegStep4.aspx");
            RouteTable.Routes.MapPageRoute("rtRegStep5", "step5", "~/usrRegStep5.aspx");

            //shop pages
            RouteTable.Routes.MapPageRoute("rtShopMain", "Shop/{shopname}", "~/shopMain.aspx");
            RouteTable.Routes.MapPageRoute("rtShopProducts", "Shop/{shopname}/products", "~/shopMainProducts.aspx");
            RouteTable.Routes.MapPageRoute("rtShopProductsBrand", "Shop/{shopname}/products/{brand}", "~/shopMainProducts.aspx");
            RouteTable.Routes.MapPageRoute("rtShopMainProduct", "Shop/{shopname}/product/{id}", "~/shopMainProd.aspx");

           

            //shop edit pages
            RouteTable.Routes.MapPageRoute("rtShopCms", "Shop/edit/main", "~/shopCms.aspx");
            RouteTable.Routes.MapPageRoute("rtShopActivities", "Shop/edit/Activities", "~/shopActivity.aspx");
            RouteTable.Routes.MapPageRoute("rtShopBanars", "Shop/edit/Banars", "~/shopBanars2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopOwnerInfo", "Shop/edit/OwnerInfo", "~/shopBasic2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopBrandAddID", "Shop/edit/Brand/{id}", "~/shopBrandAdd2.aspx");//*edited by salwa //line 65
            RouteTable.Routes.MapPageRoute("rtShopBrandAdd", "Shop/edit/Brand", "~/shopBrandAdd2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopBrands", "Shop/edit/Brands", "~/shopBrands2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopInfo", "Shop/edit/Info", "~/shopInfo2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopLogo", "Shop/edit/Logo", "~/shopLogo2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopMap", "Shop/edit/Map", "~/shopMap2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopPass", "Shop/edit/ChangePassword", "~/shopPass2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopStatus", "Shop/edit/Status", "~/shopStatus2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopVideo", "Shop/edit/Video", "~/shopVideo2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopProductsEdit", "Shop/edit/Prods", "~/shopProducts2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopProductAdd", "Shop/edit/Prod", "~/shopProductAdd2.aspx");
            RouteTable.Routes.MapPageRoute("rtShopProductAddID", "Shop/edit/Prod/{id}", "~/shopProductAdd2.aspx");//*edited by salwa //75
            RouteTable.Routes.MapPageRoute("rtShopPromoAdd", "Shop/edit/Promosion", "~/shopPromoAdd.aspx");
            RouteTable.Routes.MapPageRoute("rtShopPromoEdit", "Shop/edit/Promosion/{id}", "~/shopPromoAdd.aspx");
            RouteTable.Routes.MapPageRoute("rtShopPromos", "Shop/edit/Promosions", "~/shopPromo.aspx");

            //Administrator Pages >>>>>>>>>>> this my work fawaz from here
            RouteTable.Routes.MapPageRoute("rtAdmMain", "admin/main", "~/admMain.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminGroups", "admin/groups", "~/admGroups.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminGroups2", "admin/groups/{id}", "~/admGroups.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminActivity", "admin/Activity", "~/admActivity.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminActivity2", "admin/Activity/{id}", "~/admActivity.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminAgree", "admin/Agree", "~/admAgree.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminAgree2", "admin/Agree/{id}", "~/admAgree.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminChar", "admin/Char", "~/admChar.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminChar2", "admin/Char/{id}", "~/admChar.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminCharGroup", "admin/Chargroup", "~/admCharGrop.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminCharGroup2", "admin/Chargrop/{id}", "~/admCharGrop.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminChargorpShow", "admin/Chargrps", "~/admCharGroupShow.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminChargorp", "admin/Chargrp/{id}", "~/admCharGrop.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminChargorp2", "admin/Chargrp", "~/admCharGrop.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminCharshow", "admin/Chars/{id}", "~/admCharShow.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminCharshow2", "admin/Chars", "~/admCharShow.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminCharclothShow", "admin/Charcloths", "~/admChrClothShow.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminCharcloth", "admin/Charcloth/{id}", "~/admChrCloth.aspx");
            RouteTable.Routes.MapPageRoute("rtAdminCharcloth2", "admin/Charcloth", "~/admChrCloth.aspx");


            RouteTable.Routes.MapPageRoute("rtAdminfeedback", "admin/feedback", "~/admFeedback.aspx");


            RouteTable.Routes.MapPageRoute("rtAdminIndexShow", "admin/CharIndexs", "~/admIndexShow.aspx");
            RouteTable.Routes.MapPageRoute("rtadmIndex", "admin/Index/{id}", "~/admIndex.aspx");
            RouteTable.Routes.MapPageRoute("rtadmIndex2", "admin/Index", "~/admIndex.aspx");

            RouteTable.Routes.MapPageRoute("rtadmLang", "admin/Lang/{id}", "~/admLang.aspx");
            RouteTable.Routes.MapPageRoute("rtadmLang2", "admin/Lang", "~/admLang.aspx");

            RouteTable.Routes.MapPageRoute("rtadmList", "admin/List/{type}", "~/admList.aspx");
            RouteTable.Routes.MapPageRoute("rtadmList2", "admin/List/{type}/{id}", "~/admList.aspx");

            RouteTable.Routes.MapPageRoute("rtadmLogs", "admin/admLogs", "~/admLogs.aspx");

            RouteTable.Routes.MapPageRoute("rtadmNew", "admin/News/{id}", "~/admNews.aspx");
            RouteTable.Routes.MapPageRoute("rtadmNew2", "admin/News", "~/admNews.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminObjectShow", "admin/Objects", "~/admObjectShow.aspx");
            RouteTable.Routes.MapPageRoute("rtadmObject", "admin/Object/{id}", "~/admObjects.aspx");
            RouteTable.Routes.MapPageRoute("rtadmObject2", "admin/Object", "~/admObjects.aspx");

         
            RouteTable.Routes.MapPageRoute("rtadmObtion", "admin/admObtion/{id}", "~/admOption.aspx");
            RouteTable.Routes.MapPageRoute("rtadmObtion2", "admin/admObtion", "~/admOption.aspx");

            RouteTable.Routes.MapPageRoute("rtAdminPermationShow", "admin/admPermissionshow", "~/admPermissionShow.aspx");
            RouteTable.Routes.MapPageRoute("rtadmPermation", "admin/admPermation/{id}", "~/admPermissionadd.aspx");
            RouteTable.Routes.MapPageRoute("rtadmPermation2", "admin/admPermation", "~/admPermissionadd.aspx");

            RouteTable.Routes.MapPageRoute("rtadmRoles", "admin/admRoles", "~/admRoles.aspx");

            RouteTable.Routes.MapPageRoute("rtadmSceneShow", "admin/Scenes", "~/admSceneShow.aspx");
            RouteTable.Routes.MapPageRoute("rtadmScene", "admin/Scene/{id}", "~/admScene.aspx");
            RouteTable.Routes.MapPageRoute("rtadmScene2", "admin/Scene", "~/admScene.aspx");

            RouteTable.Routes.MapPageRoute("rtadmTrigerShow", "admin/Triggers", "~/admTriggerShow.aspx");
            RouteTable.Routes.MapPageRoute("rtadmTriger", "admin/Trigger/{id}", "~/admTrigger.aspx");
            RouteTable.Routes.MapPageRoute("rtadmTriger2", "admin/Trigger", "~/admTrigger.aspx");

            RouteTable.Routes.MapPageRoute("rtadmUsers", "admin/admUsers", "~/admUsers.aspx");
            RouteTable.Routes.MapPageRoute("rtadmUserAdd", "admin/admUser/{id}", "~/admUserAdd.aspx");
            RouteTable.Routes.MapPageRoute("rtadmUserAdd2", "admin/admUser", "~/admUserAdd.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //increase vistors counters
            try
            {
                FamoPassport p = new FamoPassport(ClassMain.ConnectionString);
                FamOption o = new FamOption(p);
                o.IncreaseVisitorCount();

            }
            catch (Exception)
            {
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}