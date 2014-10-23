using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;

namespace FamoCity
{
    public partial class uploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FamoPassport Passport;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           ClassMain.LoginGuest();
           int[] fileID = ClassMain.UploadImage(AsyncUpload1, FamoBlock.enmObjectCode.Brands, 23);
        }
    }
}